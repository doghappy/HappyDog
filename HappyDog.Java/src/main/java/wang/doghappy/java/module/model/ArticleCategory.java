package wang.doghappy.java.module.model;

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
    public int getValue(){
        return  value;
    }
}
