package wang.doghappy.java.module.article.model;

import org.apache.commons.lang3.builder.ToStringBuilder;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.model.TagDto;

import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.List;

public class ArticleDto {
    public ArticleDto() {
    }

    public ArticleDto(int id, String title, Timestamp createTime, int viewCount, BaseStatus status) {
        this.id = id;
        this.title = title;
        this.createTime = createTime;
        this.viewCount = viewCount;
        this.status = status;
    }

    private int id;
    private String title;
    private Timestamp createTime;
    private int viewCount;
    private BaseStatus status;
    private CategoryDto category;
    private List<TagDto> tags;

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

    public Timestamp getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Timestamp createTime) {
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

    public List<TagDto> getTags() {
        return tags;
    }

    public void setTags(List<TagDto> tags) {
        this.tags = tags;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
                .append("id", id)
                .append("title", title)
                .append("createTime", createTime)
                .append("viewCount", viewCount)
                .append("status", status)
                .append("category", category)
                .append("tags", tags)
                .toString();
    }
}
