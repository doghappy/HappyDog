package wang.doghappy.java.util;

public class HttpDataResponse<T> extends HttpResponse {
    public HttpDataResponse() {
        super();
    }

    public T data;

    public T getData() {
        return data;
    }

    public void setData(T data) {
        this.data = data;
    }
}
