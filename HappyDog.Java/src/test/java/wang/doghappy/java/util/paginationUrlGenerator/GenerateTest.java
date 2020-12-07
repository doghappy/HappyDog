package wang.doghappy.java.util.paginationUrlGenerator;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import wang.doghappy.java.util.PaginationUrlGenerator;
import javax.servlet.http.HttpServletRequest;

public class GenerateTest {
    @Test
    public void nullTest() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn(null);
        String queryString = PaginationUrlGenerator.generate(mockRequest, 1);
        Assertions.assertEquals("?page=1", queryString);
    }

    @Test
    public void emptyTest() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 2);
        Assertions.assertEquals("?page=2", queryString);
    }

    @Test
    public void page1Test() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("?page");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 1);
        Assertions.assertEquals("?page=1", queryString);
    }

    @Test
    public void page2Test() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("?page=");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 2);
        Assertions.assertEquals("?page=2", queryString);
    }

    @Test
    public void page3Test() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("page=");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 3);
        Assertions.assertEquals("?page=3", queryString);
    }

    @Test
    public void aAndBTest() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("a=&b=");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 3);
        Assertions.assertEquals("?a=&b=&page=3", queryString);
    }

    @Test
    public void aAndBAndPageTest() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("a=&b=&page=");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 3);
        Assertions.assertEquals("?a=&b=&page=3", queryString);
    }

    @Test
    public void aAndBAndPage3Test() {
        var mockRequest = Mockito.mock(HttpServletRequest.class);
        Mockito.when(mockRequest.getQueryString()).thenReturn("a=&b=&page=1");
        String queryString = PaginationUrlGenerator.generate(mockRequest, 3);
        Assertions.assertEquals("?a=&b=&page=3", queryString);
    }
}
