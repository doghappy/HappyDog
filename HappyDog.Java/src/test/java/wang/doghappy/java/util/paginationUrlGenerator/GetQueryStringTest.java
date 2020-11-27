package wang.doghappy.java.util.paginationUrlGenerator;

import org.junit.jupiter.api.Test;
import wang.doghappy.java.util.PaginationUrlGenerator;
import org.junit.jupiter.api.Assertions;

import java.util.HashMap;
import java.util.Map;

public class GetQueryStringTest {
    @Test
    public void nullTest() {
        String queryString = PaginationUrlGenerator.getQueryString(null);
        Assertions.assertEquals("", queryString);
    }

    @Test
    public void emptyMapTest() {
        var map = new HashMap<String, String>();
        String queryString = PaginationUrlGenerator.getQueryString(map);
        Assertions.assertEquals("", queryString);
    }

    @Test
    public void oneElementsTest() {
        var map = new HashMap<String, String>();
        map.put("a", "qwe");
        String queryString = PaginationUrlGenerator.getQueryString(map);
        Assertions.assertEquals("?a=qwe", queryString);
    }

    @Test
    public void twoElementsTest() {
        var map = new HashMap<String, String>();
        map.put("a", "qwe");
        map.put("b", "123");
        String queryString = PaginationUrlGenerator.getQueryString(map);
        Assertions.assertEquals("?a=qwe&b=123", queryString);
    }
}
