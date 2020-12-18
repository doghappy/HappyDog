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

    public int getValue() {
        return value;
    }

    public static BaseStatus fromInteger(int val) {
        BaseStatus status = null;
        for (var item : BaseStatus.values()) {
            if (item.value == val) {
                status = item;
                break;
            }
        }
        return status;
    }
}
