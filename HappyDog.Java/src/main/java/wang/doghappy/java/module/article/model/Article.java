package wang.doghappy.java.module.article.model;

import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.model.Tag;

import javax.persistence.*;
import java.sql.Timestamp;
import java.util.Set;
import java.util.StringJoiner;

@Entity
@Table(name = "Articles")
public class Article {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String title;
    private String content;
    private ArticleCategory categoryId;
    private Timestamp createTime;
    private int viewCount;

    private BaseStatus status;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "ArticleTagMappings",
            joinColumns = @JoinColumn(name = "ArticleId"),
            inverseJoinColumns = @JoinColumn(name = "TagId")
    )
    private Set<Tag> tags;

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

    public Set<Tag> getTags() {
        return tags;
    }

    public void setTags(Set<Tag> tags) {
        this.tags = tags;
    }
}
