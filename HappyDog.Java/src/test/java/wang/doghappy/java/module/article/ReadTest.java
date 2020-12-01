package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.util.ArrayList;
import java.util.Optional;

import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.model;

public class ReadTest {
    @Test
    public void testRead() throws Exception {
        var pagination = new Pagination<ArticleDto>();
        pagination.setData(new ArrayList<>());
        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
        Mockito.when(mockArticleRepository.findEnabledDtos(Mockito.anyInt(), Mockito.any(Optional.class))).thenReturn(pagination);
        var mockTagRepository = Mockito.mock(TagRepository.class);
        var articleService = new ArticleService(mockArticleRepository, mockTagRepository);
        var controller = new ArticleController(articleService);
        var mockMvc = MockMvcBuilders.standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/read"))
                .andExpect(status().isOk())
                .andExpect(view().name("article/read"))
                .andExpect(model().attributeExists("categoryActive"))
                .andExpect(model().attribute("categoryActive", true));
    }
}
