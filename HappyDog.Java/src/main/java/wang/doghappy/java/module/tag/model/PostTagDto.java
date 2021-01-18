package wang.doghappy.java.module.tag.model;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.Size;

public class PostTagDto {
    @NotBlank
    @Size(max = 20)
    private String name;

    @Size(max = 10)
    private String color;

    @Size(max = 100)
    private String glyphFont;

    @Size(max = 10)
    private String Glyph;

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
}
