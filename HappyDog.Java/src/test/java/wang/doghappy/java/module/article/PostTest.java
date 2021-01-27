//package wang.doghappy.java.module.article;
//
//import org.junit.jupiter.api.Assertions;
//import org.junit.jupiter.api.BeforeAll;
//import org.junit.jupiter.api.Test;
//import org.mockito.Mockito;
//import org.modelmapper.ModelMapper;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
//import wang.doghappy.java.config.BeanConfig;
//import wang.doghappy.java.module.article.model.PostArticleDto;
//import wang.doghappy.java.module.article.repository.ArticleRepository;
//import wang.doghappy.java.module.model.ArticleCategory;
//import wang.doghappy.java.module.model.BaseStatus;
//
//import java.util.Arrays;
//
//public class PostTest {
//
//    @Test
//    public void testPost() {
//        var postDto = new PostArticleDto();
//        postDto.setTitle("title");
//        postDto.setCategoryId(ArticleCategory.WINDOWS);
//        postDto.setStatus(BaseStatus.DISABLED);
//        postDto.setTagIds(Arrays.asList(1));
//
//
//        var mockArticleRepository = Mockito.mock(ArticleRepository.class);
//        Mockito.when(mockArticleRepository.post(postDto)).thenReturn(postDto);
//
//        var articleService = new ArticleService();
//        articleService.setArticleRepository(mockArticleRepository);
//        var controller = new ArticleController(articleService);
//        var result = controller.post(postDto);
//        Assertions.assertEquals(1, result.getId());
//        Assertions.assertEquals("title", result.getTitle());
//        Assertions.assertNotNull(result.getCreateTime());
//        Assertions.assertEquals(BaseStatus.DISABLED, result.getStatus());
//        Assertions.assertEquals(0, result.getViewCount());
//        Assertions.assertEquals(ArticleCategory.WINDOWS, result.getCategoryId());
//        Assertions.assertEquals(1, result.getTags().size());
//        Assertions.assertEquals(1, result.getTags().get(0).getId());
//        Assertions.assertEquals("java", result.getTags().get(0).getName());
//    }
//}
