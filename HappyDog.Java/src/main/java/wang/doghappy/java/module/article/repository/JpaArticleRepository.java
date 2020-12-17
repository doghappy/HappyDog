package wang.doghappy.java.module.article.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.article.model.Article;

import java.util.List;

@Repository
public interface JpaArticleRepository extends JpaRepository<Article, Integer> {
//    @Query(value = "select Id, CategoryId, Title, CreateTime, ViewCount, Status from Articles where Status = 0 order by Id desc", nativeQuery = true)
    @Query(value = "select * from Articles where Status = 0 order by Id desc", nativeQuery = true)
    List<Article> findAllDisabled();
}
