package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.util.ArrayList;
import java.util.Optional;

import static org.hamcrest.Matchers.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

public class DetailTest {
    @Test
    public void testDetail() throws Exception {
        var article = new ArticleDetailDto();
        article.setId(1);
        article.setTitle("article 1");
        article.setContent("# content");
        article.setViewCount(182);

        var tags = new ArrayList<TagDto>();
        var tag1 = new TagDto();
        tag1.setId(1);
        tag1.setName("tag 1");
        tags.add(tag1);
        article.setTags(tags);

        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
        Mockito.when(mockArticleRepository.findOne(Mockito.anyInt())).thenReturn(article);

        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagDtoByArticleId(Mockito.anyInt())).thenReturn(tags);

        var articleService = new ArticleService(mockArticleRepository, mockTagRepository, null);
        var controller = new ArticleController(articleService);
        var mockMvc = MockMvcBuilders.standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/detail/1"))
                .andExpect(status().isOk())
                .andExpect(view().name("article/detail"))
                .andExpect(model().attribute("article", allOf(
                        hasProperty("id", is(1)),
                        hasProperty("title", is("article 1")),
                        hasProperty("content", is("<h1>content</h1>\n")),
                        hasProperty("tags", hasSize(1)),
                        hasProperty("tags",hasItem(allOf(
                                hasProperty("id", is(1)),
                                hasProperty("name", is("tag 1"))
                        )))
                )));
    }
}
