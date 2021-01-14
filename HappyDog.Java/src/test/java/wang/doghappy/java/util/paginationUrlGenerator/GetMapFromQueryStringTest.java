package wang.doghappy.java.util.paginationUrlGenerator;

import org.junit.jupiter.api.Test;
import wang.doghappy.java.util.PaginationUrlGenerator;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class GetMapFromQueryStringTest {

    @Test
    public void nullQueryStringTest() {
        String url = null;
        var map = PaginationUrlGenerator.getQueryStringMap(url);
        assertEquals(0, map.size());
    }

    @Test
    public void emptyQueryStringTest() {
        String url = "";
        var map = PaginationUrlGenerator.getQueryStringMap(url);
        assertEquals(0, map.size());
    }

    @Test
    public void singleQuestionMarkStringTest() {
        String url = "?";
        var map = PaginationUrlGenerator.getQueryStringMap(url);
        assertEquals(0, map.size());
    }

    @Test
    public void questionMarkUrl1StringTest() {
        String url = "?a=1&b=2";
        var map = PaginationUrlGenerator.getQueryStringMap(url);
        assertEquals(2, map.size());
        assertTrue(map.containsKey("a"));
        assertEquals("1", map.get("a"));
        assertTrue(map.containsKey("b"));
        assertEquals("2", map.get("b"));
    }

    @Test
    public void url1StringTest() {
        String url = "a=1&b=2";
        var map = PaginationUrlGenerator.getQueryStringMap(url);
        assertEquals(2, map.size());
        assertTrue(map.containsKey("a"));
        assertEquals("1", map.get("a"));
        assertTrue(map.containsKey("b"));
        assertEquals("2", map.get("b"));
    }
}
