package wang.doghappy.java.module.article.repository;

import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.util.Pagination;

import java.util.List;

public interface ArticleRepository {
    Pagination<ArticleDto> findEnabledDtos(int page);
}
