package wang.doghappy.java.module.article.repository;

import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;

import java.util.List;

public interface ArticleRepository {
    List<ArticleDto> findEnabledDtos(FindEnabledDtosParameter parameter);
}
