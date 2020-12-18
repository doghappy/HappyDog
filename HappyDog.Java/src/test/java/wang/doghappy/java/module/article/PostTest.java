package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.context.junit4.SpringRunner;
import wang.doghappy.java.DogHappyApplication;
import wang.doghappy.java.config.BeanConfig;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;

import java.text.SimpleDateFormat;

@DataJpaTest
public class PostTest {

    @BeforeAll
    private static void init() {
        var beanConfig = new BeanConfig();
        modelMapper = beanConfig.getModelMapper();
    }


    @Autowired
    private JpaArticleRepository jpaArticleRepository;

    private static ModelMapper modelMapper;

    @Test
    public void testPost() {
        var articleService = new ArticleService(null, null, jpaArticleRepository);
        articleService.setModelMapper(modelMapper);
        var controller = new ArticleController(articleService);
        var dto = new PostArticleDto();
        dto.setTitle("title");
        dto.setCategoryId(ArticleCategory.WINDOWS);
        dto.setStatus(BaseStatus.DISABLED);
        var result = controller.post(dto);

        var dbArticles = jpaArticleRepository.findAll();
        Assertions.assertEquals(1, dbArticles.size());

        var dbArticle = dbArticles.get(0);
        Assertions.assertEquals(1, dbArticle.getId());
        Assertions.assertEquals("title", dbArticle.getTitle());
        Assertions.assertTrue(dbArticle.getContent() == null || dbArticle.getContent().isEmpty());
        Assertions.assertNotNull(dbArticle.getCreateTime());
        Assertions.assertEquals(BaseStatus.DISABLED, dbArticle.getStatus());
        Assertions.assertEquals(0, dbArticle.getViewCount());
        Assertions.assertEquals(ArticleCategory.WINDOWS, dbArticle.getCategoryId());

        Assertions.assertEquals(dbArticle.getId(), result.getId());
        Assertions.assertEquals(dbArticle.getTitle(), result.getTitle());
        Assertions.assertEquals(dbArticle.getViewCount(), result.getViewCount());
        Assertions.assertEquals(dbArticle.getContent(), result.getContent());
        Assertions.assertEquals(dbArticle.getCategoryId(), result.getCategoryId());
        Assertions.assertEquals(dbArticle.getCreateTime(), result.getCreateTime());
    }
}
