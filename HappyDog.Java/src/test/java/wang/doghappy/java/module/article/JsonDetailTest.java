package wang.doghappy.java.module.article;

import org.hamcrest.MatcherAssert;
import org.hamcrest.Matchers;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.category.repository.CategoryRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import java.sql.Timestamp;
import java.time.Instant;
import java.util.ArrayList;

public class JsonDetailTest {

    @Test
    public void testJsonDetail() {
        var article = new ArticleDetailDto();
        article.setId(25);
        article.setTitle("title");
        article.setCategoryId(ArticleCategory.JAVA);
        article.setContent("test content");
        article.setCreateTime(Timestamp.from(Instant.now()));
        article.setViewCount(15);
        article.setStatus(BaseStatus.DISABLED);
        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
        Mockito.when(mockArticleRepository.findByIdForConsole(Mockito.anyInt())).thenReturn(article);

        var category = new CategoryDto();
        category.setId(ArticleCategory.JAVA.getValue());
        category.setValue("Java");
        category.setLabel("JAVA");
        category.setColor("Yellow");
        var mockCategoryRepository = Mockito.mock(CategoryRepository.class);
        Mockito.when(mockCategoryRepository.findById(ArticleCategory.JAVA.getValue())).thenReturn(category);

        var tags = new ArrayList<TagDto>();
        var tag1= new TagDto();
        tag1.setId(21);
        tag1.setName("tag1");
        tags.add(tag1);
        var tag2= new TagDto();
        tag2.setId(23);
        tag2.setName("tag2");
        tags.add(tag2);
        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagDtoByArticleId(25)).thenReturn(tags);

        var articleService = new ArticleService();
        articleService.setArticleRepository(mockArticleRepository);
        articleService.setCategoryRepository(mockCategoryRepository);
        articleService.setTagRepository(mockTagRepository);
        var controller = new ArticleController(articleService);
        var dto = controller.jsonDetail(1);

        Assertions.assertEquals(25, dto.getId());
        Assertions.assertEquals("title", dto.getTitle());
        Assertions.assertEquals("test content", dto.getContent());
        Assertions.assertEquals(15, dto.getViewCount());
        Assertions.assertEquals(ArticleCategory.JAVA, dto.getCategoryId());
        Assertions.assertEquals(ArticleCategory.JAVA.getValue(), dto.getCategory().getId());
        Assertions.assertEquals("JAVA", dto.getCategory().getLabel());
        Assertions.assertEquals("Java", dto.getCategory().getValue());
        Assertions.assertEquals("Yellow", dto.getCategory().getColor());
        Assertions.assertEquals(2, dto.getTags().size());
        MatcherAssert.assertThat(dto.getTags(), Matchers.hasItems(Matchers.allOf(
                Matchers.hasProperty("name", Matchers.is("tag1")),
                Matchers.hasProperty("id", Matchers.is(21))
        )));
        MatcherAssert.assertThat(dto.getTags(), Matchers.hasItems(Matchers.allOf(
                Matchers.hasProperty("name", Matchers.is("tag2")),
                Matchers.hasProperty("id", Matchers.is(23))
        )));
    }
}
