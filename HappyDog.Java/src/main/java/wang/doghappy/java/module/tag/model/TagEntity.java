package wang.doghappy.java.module.tag.model;

import javax.persistence.*;
import java.util.StringJoiner;

@Entity
@Table(name = "Tags")
public class TagEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
    private String color;
    private String glyphFont;
    private String Glyph;

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
        return Glyph;
    }

    public void setGlyph(String glyph) {
        Glyph = glyph;
    }

    @Override
    public String toString() {
        return new StringJoiner(", ", TagEntity.class.getSimpleName() + "[", "]")
                .add("id=" + id)
                .add("name='" + name + "'")
                .add("color='" + color + "'")
                .add("glyphFont='" + glyphFont + "'")
                .add("Glyph='" + Glyph + "'")
                .toString();
    }
}
