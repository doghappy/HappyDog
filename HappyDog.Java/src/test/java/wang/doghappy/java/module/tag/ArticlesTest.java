package wang.doghappy.java.module.tag;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import wang.doghappy.java.module.article.ArticleService;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.home.HomeController;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;
import java.util.ArrayList;
import static org.hamcrest.Matchers.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.standaloneSetup;

public class ArticlesTest {

    @Test
    public void tagIsNullTest() throws Exception {
        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagByName(Mockito.anyString())).thenReturn(null);

        var tagService = new TagService();
        tagService.setTagRepository(mockTagRepository);
        var controller = new TagController(tagService, null);
        MockMvc mockMvc = standaloneSetup(controller)
                .setControllerAdvice(new HomeController())
                .build();
        mockMvc.perform(MockMvcRequestBuilders.get("/tag/dog/article"))
                .andExpect(status().is4xxClientError())
                .andExpect(view().name("home/404"));
    }

    @Test
    public void articleIdsIsNullTest() throws Exception {
        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagByName(Mockito.anyString())).thenReturn(new TagDto());
        Mockito.when(mockTagRepository.findArticleIds(Mockito.anyInt())).thenReturn(new ArrayList<>());

        var tagService = new TagService();
        tagService.setTagRepository(mockTagRepository);
        var controller = new TagController(tagService, null);
        MockMvc mockMvc = standaloneSetup(controller)
                .setControllerAdvice(new HomeController())
                .build();
        mockMvc.perform(MockMvcRequestBuilders.get("/tag/dog/article"))
                .andExpect(status().is4xxClientError())
                .andExpect(view().name("home/404"));
    }

    @Test
    public void tagNameDtoTest() throws Exception {
        var tags = new ArrayList<ArticleIdTagDto>();
        var tag1 = new ArticleIdTagDto();
        tag1.setArticleId(2);
        tag1.setName("tag1");
        tags.add(tag1);

        var articleIds = new ArrayList<Integer>();
        articleIds.add(1);
        articleIds.add(2);

        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagByName(Mockito.anyString())).thenReturn(new TagDto());
        Mockito.when(mockTagRepository.findArticleIds(Mockito.anyInt())).thenReturn(articleIds);
        Mockito.when(mockTagRepository.findTagDtoByArticleIds(Mockito.anyList())).thenReturn(tags);

        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
        var articles = new ArrayList<ArticleDto>();

        var article0 = new ArticleDto();
        article0.setId(1);
        article0.setTitle("article 1");
        articles.add(article0);

        var article1 = new ArticleDto();
        article1.setId(2);
        article1.setTitle("article 2");
        articles.add(article1);

        var pagination = new Pagination<ArticleDto>();
        pagination.setData(articles);

        Mockito.when(mockArticleRepository.findByIds(Mockito.anyList(), Mockito.anyInt())).thenReturn(pagination);

        var tagService = new TagService();
        tagService.setTagRepository(mockTagRepository);
        var articleService = new ArticleService();
        articleService.setArticleRepository(mockArticleRepository);
        articleService.setTagRepository(mockTagRepository);
        var controller = new TagController(tagService, articleService);
        MockMvc mockMvc = standaloneSetup(controller)
                .setControllerAdvice(new HomeController())
                .build();
        mockMvc.perform(MockMvcRequestBuilders.get("/tag/dog/article"))
                .andExpect(status().isOk())
                .andExpect(view().name("tag/articles"))
                .andExpect(model().attribute("name", "dog"))
                .andExpect(model().attribute("articles", hasSize(2)))
                .andExpect(model().attribute("articles", hasItem(allOf(
                        hasProperty("id", is(1)),
                        hasProperty("title", is("article 1")),
                        hasProperty("tags", hasSize(0))
                ))))
                .andExpect(model().attribute("articles", hasItem(allOf(
                        hasProperty("id", is(2)),
                        hasProperty("title", is("article 2")),
                        hasProperty("tags", hasSize(1)),
                        hasProperty("tags", hasItem(hasProperty("name", is("tag1"))))
                ))));
    }
}
