package wang.doghappy.java.util;

import com.fasterxml.jackson.annotation.JsonValue;

public enum HttpStatus {
    OK(200);

    HttpStatus(int value) {
        this.value = value;
    }

    @JsonValue
    private int value;
}
