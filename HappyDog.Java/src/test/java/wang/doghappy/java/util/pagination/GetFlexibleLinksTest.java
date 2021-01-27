package wang.doghappy.java.util.pagination;

import org.junit.jupiter.api.Test;
import wang.doghappy.java.util.Pagination;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class GetFlexibleLinksTest {
    @Test
    public void test0() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(10);
        pagination.setAutoHide(false);
        pagination.setPreviousText("&laquo;");
        pagination.setNextText("&raquo;");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item active\"><span class=\"page-link\">1</span></li><li class=\"page-item\"><span class=\"page-link\">1 / 1</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test1() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(10);
        pagination.setAutoHide(true);
        pagination.setPreviousText("&laquo;");
        pagination.setNextText("&raquo;");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        assertEquals("", html);
    }

    @Test
    public void test2() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(2);
        pagination.setSize(10);
        pagination.setTotalItems(11);
        pagination.setPreviousText("Previous");
        pagination.setNextText("&raquo;");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item active\"><span class=\"page-link\">2</span></li><li class=\"page-item\"><span class=\"page-link\">2 / 2</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test3() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item active\"><span class=\"page-link\">1</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">1 / 5</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test4() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(2);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item active\"><span class=\"page-link\">2</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">2 / 5</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test5() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(3);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item active\"><span class=\"page-link\">3</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">3 / 5</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test6() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(4);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item active\"><span class=\"page-link\">4</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">4 / 5</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test7() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(9);
        pagination.setSize(10);
        pagination.setTotalItems(480);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 5);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=8\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=7\">7</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=8\">8</a></li><li class=\"page-item active\"><span class=\"page-link\">9</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=10\">10</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=11\">11</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=10\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">9 / 48</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test8() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(9);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 3);
        assertEquals("", html);
    }

    @Test
    public void test9() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(4);
        pagination.setSize(20);
        pagination.setTotalItems(63);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        String html = pagination.getFlexibleLinks(p -> "/test?p=" + p, 7);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item active\"><span class=\"page-link\">4</span></li><li class=\"page-item\"><span class=\"page-link\">4 / 4</span></li></ul></nav>";
        assertEquals(expected, html);
    }
}
