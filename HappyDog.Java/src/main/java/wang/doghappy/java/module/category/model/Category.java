//package wang.doghappy.java.module.category.model;
//
//import wang.doghappy.java.module.article.model.Article;
//import wang.doghappy.java.module.model.BaseStatus;
//
//import javax.persistence.*;
//import java.util.ArrayList;
//import java.util.List;
//
//@Entity
//@Table(name = "Categories")
//public class Category {
//    @Id
//    @GeneratedValue(strategy = GenerationType.IDENTITY)
//    @Column(name = "Id")
//    private int id;
//
//    @Column(name = "Label")
//    private String label;
//
//    @Column(name = "Value")
//    private String value;
//
//    @Column(name = "Color")
//    private String color;
//
//    @Column(name = "Status")
//    private BaseStatus status;
//
//    @OneToMany(mappedBy = "category", cascade = CascadeType.ALL)
////    @OneToMany
////    @JoinColumn(name = "Id", referencedColumnName = "CategoryId")
//    private List<Article> articles;
//
//    public int getId() {
//        return id;
//    }
//
//    public void setId(int id) {
//        this.id = id;
//    }
//
//    public String getLabel() {
//        return label;
//    }
//
//    public void setLabel(String label) {
//        this.label = label;
//    }
//
//    public String getValue() {
//        return value;
//    }
//
//    public void setValue(String value) {
//        this.value = value;
//    }
//
//    public String getColor() {
//        return color;
//    }
//
//    public void setColor(String color) {
//        this.color = color;
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
//    public List<Article> getArticles() {
//        return articles;
//    }
//
//    public void setArticles(List<Article> articles) {
//        this.articles = articles;
//    }
//}
