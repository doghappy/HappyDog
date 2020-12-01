package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.hamcrest.Matchers.*;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.view;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.model;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.standaloneSetup;

@SpringBootTest
@AutoConfigureMockMvc
public class IndexTest {

    //    @MockBean
//    private ArticleService articleService;
//
//    @MockBean
//    private ArticleRepository articleRepository;

    @Test
    public void testIndex() throws Exception {
        List<ArticleDto> list = new ArrayList<>();
        var dto1 = new ArticleDto();
        dto1.setId(1);
        dto1.setTitle("article1");
        list.add(dto1);
        var dto2 = new ArticleDto();
        dto2.setId(2);
        dto2.setTitle("article2");
        list.add(dto2);
        var dto3 = new ArticleDto();
        dto3.setId(3);
        dto3.setTitle("article3");
        list.add(dto3);

        var pagination = new Pagination<ArticleDto>();
        pagination.setData(list);

        var mockArticleRepository = mock(ArticleRepository.class);
        when(mockArticleRepository.findEnabledDtos(Mockito.anyInt(), Mockito.any(Optional.class))).thenReturn(pagination);

        var tags = new ArrayList<ArticleIdTagDto>();
        var tag1 = new ArticleIdTagDto();
        tag1.setArticleId(2);
        tag1.setName("tag1");
        tags.add(tag1);
        var tag2 = new ArticleIdTagDto();
        tag2.setArticleId(2);
        tag2.setName("tag2");
        tags.add(tag2);
        var tag3 = new ArticleIdTagDto();
        tag3.setArticleId(3);
        tag3.setName("tag3");
        tags.add(tag3);

        var mockTagRepository = mock(TagRepository.class);
        when(mockTagRepository.findTagDtoByArticleIds(Mockito.anyCollection())).thenReturn(tags);

        var articleService = new ArticleService(mockArticleRepository, mockTagRepository);
        var controller = new ArticleController(articleService);
        MockMvc mockMvc = standaloneSetup(controller).build();

        mockMvc.perform(get("/"))
                .andExpect(status().isOk())
                .andExpect(view().name("article/index"))
                .andExpect(model().attributeExists("list"))
                .andExpect(model().attribute("list", hasSize(3)))
                .andExpect(model().attribute("list", hasItem(
                        allOf(
                                hasProperty("id", is(1)),
                                hasProperty("title", is("article1")),
                                hasProperty("tags", hasSize(0))
                        )
                )))
                .andExpect(model().attribute("list", hasItem(
                        allOf(
                                hasProperty("id", is(2)),
                                hasProperty("title", is("article2")),
                                hasProperty("tags", hasSize(2)),
                                hasProperty("tags", allOf(
                                        hasItem(hasProperty("name", is("tag1"))),
                                        hasItem(hasProperty("name", is("tag2")))
                                ))
                        )
                )))
                .andExpect(model().attribute("list", hasItem(
                        allOf(
                                hasProperty("id", is(3)),
                                hasProperty("title", is("article3")),
                                hasProperty("tags", hasSize(1)),
                                hasProperty("tags", hasItem(hasProperty("name", is("tag3"))))
                        )
                )))
                .andExpect(model().attributeDoesNotExist("categoryActive"));
    }
}
