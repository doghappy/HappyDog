package wang.doghappy.java.module.article.template;

import wang.doghappy.java.module.model.ArticleCategory;

import java.util.Optional;

public class DatabaseTemplate extends CategoryTemplate {
    @Override
    protected Optional<ArticleCategory> getCategory() {
        return Optional.of(ArticleCategory.DATABASE);
    }
}
