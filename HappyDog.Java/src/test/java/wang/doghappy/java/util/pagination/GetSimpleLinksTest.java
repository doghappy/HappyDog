package wang.doghappy.java.util.pagination;

import org.junit.jupiter.api.Test;
import wang.doghappy.java.util.Pagination;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class GetSimpleLinksTest {
    @Test
    public void test0() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(10);
        pagination.setPreviousText("Prev");
        pagination.setNextText("Next");
        pagination.setAutoHide(false);
        String html = pagination.getSimpleLinks(p -> "/test?p=" + p);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item disabled\"><span class=\"page-link\">Prev</span></li><li class=\"page-item disabled\"><span class=\"page-link\">Next</span></li><li class=\"page-item\"><span class=\"page-link\">1 / 1</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void test1() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(2);
        pagination.setSize(10);
        pagination.setTotalItems(11);
        pagination.setPreviousText("Prev");
        pagination.setNextText("Next");
        String html = pagination.getSimpleLinks(p -> "/test?p=" + p);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Prev</a></li><li class=\"page-item disabled\"><span class=\"page-link\">Next</span></li><li class=\"page-item\"><span class=\"page-link\">2 / 2</span></li></ul></nav>";
        assertEquals(expected, html);
    }

    @Test
    public void Test2() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(3);
        pagination.setSize(10);
        pagination.setTotalItems(48);
        pagination.setPreviousText("Previous");
        pagination.setNextText("Next");
        pagination.setFirstPageText("&lt;&lt;");
        pagination.setLastPageText("&gt;&gt;");
        String html = pagination.getSimpleLinks(p -> "/test?p=" + p);
        String expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">3 / 5</span></li></ul></nav>";
        assertEquals(expected, html);
    }
}
