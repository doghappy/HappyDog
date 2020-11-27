package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.tag.repository.TagRepository;

import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.view;

public class NetTest {
    @Test
    public void testNet() throws Exception {
        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
        var mockTagRepository = Mockito.mock(TagRepository.class);
        var articleService = new ArticleService(mockArticleRepository, mockTagRepository);
        var controller = new ArticleController(articleService);
        var mockMvc = MockMvcBuilders.standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/net"))
                .andExpect(status().isOk())
                .andExpect(view().name("article/net"));
    }
}
