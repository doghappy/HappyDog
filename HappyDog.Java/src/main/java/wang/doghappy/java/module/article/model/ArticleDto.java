package wang.doghappy.java.module.article.model;

import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;

import java.time.OffsetDateTime;

public class ArticleDto {
    private int id;
    private String title;
    private OffsetDateTime createTime;
    private int viewCount;
    private BaseStatus status;
    private CategoryDto category;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public OffsetDateTime getCreateTime() {
        return createTime;
    }

    public void setCreateTime(OffsetDateTime createTime) {
        this.createTime = createTime;
    }

    public int getViewCount() {
        return viewCount;
    }

    public void setViewCount(int viewCount) {
        this.viewCount = viewCount;
    }

    public BaseStatus getStatus() {
        return status;
    }

    public void setStatus(BaseStatus status) {
        this.status = status;
    }

    public CategoryDto getCategory() {
        return category;
    }

    public void setCategory(CategoryDto category) {
        this.category = category;
    }

    @Override
    public String toString() {
        return "ArticleDto{" +
                "id=" + id +
                ", title='" + title + '\'' +
                ", category=" + category +
                ", createTime=" + createTime +
                ", viewCount=" + viewCount +
                ", status=" + status +
                '}';
    }
}
