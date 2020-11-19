package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.article.service.ArticleService;

import java.util.ArrayList;
import java.util.List;

import static org.hamcrest.Matchers.hasItems;
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
        var dto0=new ArticleDto();
        dto0.setTitle("dto 0");
        list.add(new ArticleDto());

        var mockArticleRepository = mock(ArticleRepository.class);
        when(mockArticleRepository.findEnabledDtos(Mockito.any(FindEnabledDtosParameter.class))).thenReturn(list);
        var articleService = new ArticleService(mockArticleRepository);
        var controller = new ArticleController(articleService);
        // @Autowired
        MockMvc mockMvc = standaloneSetup(controller).build();

        mockMvc.perform(get("/"))
                .andExpect(status().isOk())
                .andExpect(view().name("article/index"))
                .andExpect(model().attributeExists("list"))
                .andExpect(model().attribute("list", hasItems(list.toArray())));
    }
}
