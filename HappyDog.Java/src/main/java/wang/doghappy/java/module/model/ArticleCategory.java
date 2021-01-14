package wang.doghappy.java.module.model;

import com.fasterxml.jackson.annotation.JsonValue;

public enum ArticleCategory {
    NET(1),
    DATABASE(2),
    WINDOWS(3),
    READ(4),
    ESSAYS(5),
    JAVA(6);

    ArticleCategory(int value) {
        this.value = value;
    }

    private int value;

    @JsonValue
    public int getValue(){
        return  value;
    }

    public static ArticleCategory fromInteger(int val) {
        ArticleCategory category = null;
        for (var item : ArticleCategory.values()) {
            if (item.value == val) {
                category = item;
                break;
            }
        }
        return category;
    }
}
