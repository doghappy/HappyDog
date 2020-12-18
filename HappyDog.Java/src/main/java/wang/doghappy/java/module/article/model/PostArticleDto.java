package wang.doghappy.java.module.article.model;

import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;

import javax.validation.constraints.*;
import java.util.List;

public class PostArticleDto {
    @NotBlank
    @Size(max = 200)
    private String title;
    private String content;

    @NotNull
    private ArticleCategory categoryId;

    @NotNull
    private BaseStatus status;

    private List<Integer> tagIds;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public ArticleCategory getCategoryId() {
        return categoryId;
    }

    public void setCategoryId(ArticleCategory categoryId) {
        this.categoryId = categoryId;
    }

    public BaseStatus getStatus() {
        return status;
    }

    public void setStatus(BaseStatus status) {
        this.status = status;
    }

    public List<Integer> getTagIds() {
        return tagIds;
    }

    public void setTagIds(List<Integer> tagIds) {
        this.tagIds = tagIds;
    }
}
