package wang.doghappy.java.module.article.model;

import wang.doghappy.java.module.model.ArticleCategory;

public class FindEnabledDtosParameter {
    private int page;
    private ArticleCategory category;
    private String query;

    public int getPage() {
        return page;
    }

    public void setPage(int page) {
        this.page = page;
    }

    public ArticleCategory getCategory() {
        return category;
    }

    public void setCategory(ArticleCategory category) {
        this.category = category;
    }

    public String getQuery() {
        return query;
    }

    public void setQuery(String query) {
        this.query = query;
    }
}
