package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import wang.doghappy.java.config.BeanConfig;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.model.Tag;
import wang.doghappy.java.module.tag.repository.JpaTagRepository;
import java.util.Arrays;

@DataJpaTest
public class PostTest {

    @BeforeAll
    private static void init() {
        var beanConfig = new BeanConfig();
        modelMapper = beanConfig.getModelMapper();
    }


    @Autowired
    private JpaArticleRepository jpaArticleRepository;

    @Autowired
    private JpaTagRepository jpaTagRepository;

    private static ModelMapper modelMapper;

    @Test
    public void testPost() {
        // 提前把tag创建好
        jpaTagRepository.save(new Tag(1,"java"));

        var articleService = new ArticleService(null, null, jpaArticleRepository);
        articleService.setModelMapper(modelMapper);
        articleService.setJpaTagRepository(jpaTagRepository);
        var controller = new ArticleController(articleService);
        var dto = new PostArticleDto();
        dto.setTitle("title");
        dto.setCategoryId(ArticleCategory.WINDOWS);
        dto.setStatus(BaseStatus.DISABLED);
        dto.setTagIds(Arrays.asList(1));
        var result = controller.post(dto);
        Assertions.assertEquals(1, result.getId());
        Assertions.assertEquals("title", result.getTitle());
        Assertions.assertTrue(result.getContent() == null || result.getContent().isEmpty());
        Assertions.assertNotNull(result.getCreateTime());
        Assertions.assertEquals(BaseStatus.DISABLED, result.getStatus());
        Assertions.assertEquals(0, result.getViewCount());
        Assertions.assertEquals(ArticleCategory.WINDOWS, result.getCategoryId());
        Assertions.assertEquals(1, result.getTags().size());
        Assertions.assertEquals(1, result.getTags().get(0).getId());
        Assertions.assertEquals("java", result.getTags().get(0).getName());

        var dbArticles = jpaArticleRepository.findAll();
        Assertions.assertEquals(1, dbArticles.size());

        var dbTags = jpaTagRepository.findAll();
        Assertions.assertEquals(1, dbTags.size());

        var dbArticle = dbArticles.get(0);
        Assertions.assertEquals(1, dbArticle.getId());
        Assertions.assertEquals("title", dbArticle.getTitle());
        Assertions.assertTrue(dbArticle.getContent() == null || dbArticle.getContent().isEmpty());
        Assertions.assertNotNull(dbArticle.getCreateTime());
        Assertions.assertEquals(BaseStatus.DISABLED, dbArticle.getStatus());
        Assertions.assertEquals(0, dbArticle.getViewCount());
        Assertions.assertEquals(ArticleCategory.WINDOWS, dbArticle.getCategoryId());
        Assertions.assertEquals(1, dbArticle.getTags().size());
    }
}
