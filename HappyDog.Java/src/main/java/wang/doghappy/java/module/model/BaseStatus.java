package wang.doghappy.java.module.model;

import com.fasterxml.jackson.annotation.JsonValue;

public enum BaseStatus {
    DISABLED(0),
    ENABLED(1);

    BaseStatus(int value) {
        this.value = value;
    }

    @JsonValue
    private int value;

    private int getValue() {
        return value;
    }
}
