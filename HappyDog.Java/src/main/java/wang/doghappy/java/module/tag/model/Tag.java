package wang.doghappy.java.module.tag.model;

import wang.doghappy.java.module.article.model.Article;

import javax.persistence.*;
import java.util.Set;

@Entity
@Table(name = "Tags")
public class Tag {
    public Tag() {
    }

    public Tag(int id, String name) {
        this.id = id;
        this.name = name;
    }

    public Tag(int id, String name, String color, String glyphFont, String glyph) {
        this.id = id;
        this.name = name;
        this.color = color;
        this.glyphFont = glyphFont;
        this.glyph = glyph;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
    private String color;
    private String glyphFont;
    private String glyph;

    @ManyToMany(mappedBy = "tags", fetch = FetchType.EAGER)
    private Set<Article> articles;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getGlyphFont() {
        return glyphFont;
    }

    public void setGlyphFont(String glyphFont) {
        this.glyphFont = glyphFont;
    }

    public String getGlyph() {
        return glyph;
    }

    public void setGlyph(String glyph) {
        this.glyph = glyph;
    }

    public Set<Article> getArticles() {
        return articles;
    }

    public void setArticles(Set<Article> articles) {
        this.articles = articles;
    }
}
