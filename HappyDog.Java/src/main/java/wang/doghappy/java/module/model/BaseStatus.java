package wang.doghappy.java.module.model;

public enum BaseStatus {
    DISABLED(0),
    ENABLED(1);

    BaseStatus(int value) {
        this.value = value;
    }

    private int value;

    private int getValue() {
        return value;
    }
}
