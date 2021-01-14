package wang.doghappy.java.module.article.model;

import org.apache.commons.lang3.builder.ToStringBuilder;

public class ArticleDetailDto extends ArticleDto {
    private String content;

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
                .append("content", content)
                .toString();
    }
}
