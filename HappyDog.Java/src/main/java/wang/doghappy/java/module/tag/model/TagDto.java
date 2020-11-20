package wang.doghappy.java.module.tag.model;

public class TagDto {
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
        return "TagDto{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", color='" + color + '\'' +
                ", glyphFont='" + glyphFont + '\'' +
                ", Glyph='" + Glyph + '\'' +
                '}';
    }
}
