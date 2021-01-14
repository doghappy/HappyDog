package wang.doghappy.java.module.article.repository;

import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.util.Pagination;
import java.util.List;
import java.util.Optional;

public interface ArticleRepository {
    Pagination<ArticleDto> findEnabledDtos(int page, Optional<ArticleCategory> category);
    ArticleDetailDto findOne(int id);
    Pagination<ArticleDto> findByIds(List<Integer> ids, int page);
}
