package wang.doghappy.java.module.tag.model;

import org.apache.commons.lang3.builder.ToStringBuilder;

public class ArticleIdTagDto extends TagDto {
    private int articleId;

    public int getArticleId() {
        return articleId;
    }

    public void setArticleId(int articleId) {
        this.articleId = articleId;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
                .append("articleId", articleId)
                .toString();
    }
}
