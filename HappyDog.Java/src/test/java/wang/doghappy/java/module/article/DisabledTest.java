package wang.doghappy.java.module.article;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import wang.doghappy.java.module.article.model.Article;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;
import wang.doghappy.java.module.category.model.Category;
import wang.doghappy.java.module.category.repository.JpaCategoryRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;

import java.sql.Timestamp;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Arrays;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class DisabledTest {
    @Test
    public void testDisabled() {
        var mockJpaArticleRepository = mock(JpaArticleRepository.class);
        var article0 = new Article();
        article0.setId(1);
        article0.setCategoryId(ArticleCategory.NET);
        article0.setContent("article0 content");
        article0.setTitle("article0 title");
        article0.setStatus(BaseStatus.DISABLED);
        article0.setCreateTime(Timestamp.from(Instant.now()));
        article0.setViewCount(2);
        var article1 = new Article();
        article1.setId(2);
        article1.setCategoryId(ArticleCategory.DATABASE);
        article1.setContent("article1 content");
        article1.setTitle("article1 title");
        article1.setStatus(BaseStatus.DISABLED);
        article1.setCreateTime(Timestamp.from(Instant.now()));
        article1.setViewCount(2);
        var article2 = new Article();
        article2.setId(3);
        article2.setCategoryId(ArticleCategory.WINDOWS);
        article2.setContent("article2 content");
        article2.setTitle("article2 title");
        article2.setStatus(BaseStatus.DISABLED);
        article2.setCreateTime(Timestamp.from(Instant.now()));
        article2.setViewCount(2);
        var articles = Arrays.asList(article0, article1, article2);
        when(mockJpaArticleRepository.findAllDisabled()).thenReturn(articles);

        var mockTagRepository = mock(TagRepository.class);
        var tags = new ArrayList<ArticleIdTagDto>();
        var tag1 = new ArticleIdTagDto();
        tag1.setArticleId(1);
        tag1.setName("tag1");
        tags.add(tag1);
        var tag2 = new ArticleIdTagDto();
        tag2.setArticleId(2);
        tag2.setName("tag2");
        tags.add(tag2);
        var tag3 = new ArticleIdTagDto();
        tag3.setArticleId(2);
        tag3.setName("tag3");
        tags.add(tag3);
        when(mockTagRepository.findTagDtoByArticleIds(Mockito.eq(Arrays.asList(1, 2, 3)))).thenReturn(tags);

        var mockJpaCategoryRepository = mock(JpaCategoryRepository.class);
        var category0 = new Category();
        category0.setId(1);
        category0.setLabel(".NET");
        category0.setValue("NET");
        var category1 = new Category();
        category1.setId(2);
        category1.setLabel("Java");
        category1.setValue("Java");
        var category2 = new Category();
        category2.setId(3);
        category2.setLabel("Test");
        category2.setValue("Test");
        when(mockJpaCategoryRepository.findAll()).thenReturn(Arrays.asList(category0, category1, category2));

        var articleService = new ArticleService(null, mockTagRepository, mockJpaArticleRepository);
        articleService.setJpaCategoryRepository(mockJpaCategoryRepository);
        var controller = new ArticleController(articleService);
        var result = controller.disabled();

        Assertions.assertEquals(3, result.size());

        Assertions.assertEquals(1, result.get(0).getId());
        Assertions.assertEquals("article0 title", result.get(0).getTitle());
        Assertions.assertEquals(ArticleCategory.NET, result.get(0).getCategoryId());
        Assertions.assertEquals(".NET", result.get(0).getCategory().getLabel());
        Assertions.assertEquals(1, result.get(0).getTags().size());
        Assertions.assertEquals("tag1", result.get(0).getTags().get(0).getName());

        Assertions.assertEquals(2, result.get(1).getId());
        Assertions.assertEquals("article1 title", result.get(1).getTitle());
        Assertions.assertEquals(ArticleCategory.DATABASE, result.get(1).getCategoryId());
        Assertions.assertEquals("Java", result.get(1).getCategory().getLabel());
        Assertions.assertEquals(2, result.get(1).getTags().size());
        Assertions.assertEquals("tag2", result.get(1).getTags().get(0).getName());
        Assertions.assertEquals("tag3", result.get(1).getTags().get(1).getName());

        Assertions.assertEquals(3, result.get(2).getId());
        Assertions.assertEquals("article2 title", result.get(2).getTitle());
        Assertions.assertEquals(ArticleCategory.WINDOWS, result.get(2).getCategoryId());
        Assertions.assertEquals("Test", result.get(2).getCategory().getLabel());
        Assertions.assertEquals(0, result.get(2).getTags().size());
    }
}
