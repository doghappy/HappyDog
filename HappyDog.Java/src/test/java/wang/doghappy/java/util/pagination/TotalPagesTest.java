package wang.doghappy.java.util.pagination;

import org.junit.jupiter.api.Test;
import wang.doghappy.java.util.Pagination;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class TotalPagesTest {
    @Test
    public void test0() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(10);
        assertEquals(1, pagination.getTotalPages());
    }

    @Test
    public void test1() {
        var pagination = new Pagination<Integer>();
        pagination.setPage(1);
        pagination.setSize(10);
        pagination.setTotalItems(11);
        assertEquals(2, pagination.getTotalPages());
    }
}
