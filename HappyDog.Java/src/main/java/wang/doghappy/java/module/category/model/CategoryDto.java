package wang.doghappy.java.module.category.model;

import wang.doghappy.java.module.model.ArticleCategory;

public class CategoryDto {
    private ArticleCategory id;
    private String label;
    private String value;
    private String color;

    public ArticleCategory getId() {
        return id;
    }

    public void setId(ArticleCategory id) {
        this.id = id;
    }

    public String getLabel() {
        return label;
    }

    public void setLabel(String label) {
        this.label = label;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    @Override
    public String toString() {
        return "CategoryDto{" +
                "id=" + id +
                ", label='" + label + '\'' +
                ", value='" + value + '\'' +
                ", color='" + color + '\'' +
                '}';
    }
}
