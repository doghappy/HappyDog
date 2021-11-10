//package wang.doghappy.java.module.article.model;
//
//import org.hibernate.annotations.OnDelete;
//import wang.doghappy.java.module.category.model.Category;
//import wang.doghappy.java.module.model.ArticleCategory;
//import wang.doghappy.java.module.model.BaseStatus;
//import wang.doghappy.java.module.tag.model.Tag;
//
//import javax.persistence.*;
//import java.sql.Timestamp;
//import java.util.List;
//
//@Entity
//@Table(name = "Articles")
//public class Article {
//    @Id
//    @GeneratedValue(strategy = GenerationType.IDENTITY)
//    @Column(name = "Id")
//    private int id;
//
//    @Column(name = "Title")
//    private String title;
//
//    @Column(name = "Content")
//    private String content;
//
//    @Column(name = "CategoryId", nullable = false)
//    private ArticleCategory categoryId;
//
//    @Column(name = "CreateTime")
//    private Timestamp createTime;
//
//    @Column(name = "ViewCount")
//    private int viewCount;
//
//    @Column(name = "Status")
//    private BaseStatus status;
//
//    @ManyToMany//(cascade = CascadeType.MERGE)
//    @JoinTable(
//            name = "ArticleTagMappings",
//            joinColumns = @JoinColumn(name = "ArticleId", referencedColumnName = "Id"),
//            inverseJoinColumns = @JoinColumn(name = "TagId", referencedColumnName = "Id")
//    )
//    private List<Tag> tags;
//
////    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.ALL)
////    @JoinColumn(name = "CategoryId", referencedColumnName = "Id")
////    @JoinColumn(name = "Id", referencedColumnName = "CategoryId")
//    @ManyToOne
//    @JoinColumn(name = "CategoryId", referencedColumnName = "Id")
//    private Category category;
//
//    public int getId() {
//        return id;
//    }
//
//    public void setId(int id) {
//        this.id = id;
//    }
//
//    public String getTitle() {
//        return title;
//    }
//
//    public void setTitle(String title) {
//        this.title = title;
//    }
//
//    public String getContent() {
//        return content;
//    }
//
//    public void setContent(String content) {
//        this.content = content;
//    }
//
//    public ArticleCategory getCategoryId() {
//        return categoryId;
//    }
//
//    public void setCategoryId(ArticleCategory categoryId) {
//        this.categoryId = categoryId;
//    }
//
//    public Timestamp getCreateTime() {
//        return createTime;
//    }
//
//    public void setCreateTime(Timestamp createTime) {
//        this.createTime = createTime;
//    }
//
//    public int getViewCount() {
//        return viewCount;
//    }
//
//    public void setViewCount(int viewCount) {
//        this.viewCount = viewCount;
//    }
//
//    public BaseStatus getStatus() {
//        return status;
//    }
//
//    public void setStatus(BaseStatus status) {
//        this.status = status;
//    }
//
//    public List<Tag> getTags() {
//        return tags;
//    }
//
//    public void setTags(List<Tag> tags) {
//        this.tags = tags;
//    }
//
//    public Category getCategory() {
//        return category;
//    }
//
//    public void setCategory(Category category) {
//        this.category = category;
//    }
//}
