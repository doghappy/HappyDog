package wang.doghappy.java.util;

import org.apache.commons.lang3.builder.ToStringBuilder;

import java.util.List;
import java.util.function.Function;

public class Pagination<T> {
    public Pagination() {
        setPreviousText("&laquo;");
        setNextText("&raquo;");
        setSize(20);
    }

    private List<T> data;
    private int totalItems;
    private int size;
    private int page;
    private int totalPages;

    private String previousText;
    private String nextText;
    private String firstPageText;
    private String lastPageText;
    private boolean autoHide;

    public List<T> getData() {
        return data;
    }

    public void setData(List<T> data) {
        this.data = data;
    }

    public int getTotalItems() {
        return totalItems;
    }

    public void setTotalItems(int totalItems) {
        this.totalItems = totalItems;
    }

    public int getTotalPages() {
        totalPages = (int) Math.ceil((double) getTotalItems() / getSize());
        return totalPages;
    }

    public int getSize() {
        return size;
    }

    public void setSize(int size) {
        this.size = size;
    }

    public int getPage() {
        return page;
    }

    public void setPage(int page) {
        this.page = page;
    }

    public String getPreviousText() {
        return previousText;
    }

    public void setPreviousText(String previousText) {
        this.previousText = previousText;
    }

    public String getNextText() {
        return nextText;
    }

    public void setNextText(String nextText) {
        this.nextText = nextText;
    }

    public String getFirstPageText() {
        return firstPageText;
    }

    public void setFirstPageText(String firstPageText) {
        this.firstPageText = firstPageText;
    }

    public String getLastPageText() {
        return lastPageText;
    }

    public void setLastPageText(String lastPageText) {
        this.lastPageText = lastPageText;
    }

    public String getAppend() {
        return getPage() + " / " + getTotalPages();
    }

    public boolean isAutoHide() {
        return autoHide;
    }

    public void setAutoHide(boolean autoHide) {
        this.autoHide = autoHide;
    }

    public int getOffset() {
        return (getPage() - 1) * getSize();
    }

    public String getPageNumberLinks(Function<Integer, String> pageUrl) {
        if (isAutoHide() && getTotalPages() == 1) {
            return "";
        }
        StringBuilder builder = new StringBuilder();
        builder.append("<nav><ul class=\"pagination\">");
        if (getPage() > 1) {
            if (getFirstPageText() != null) {
                builder
                        .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(1))
                        .append("\">")
                        .append(getFirstPageText())
                        .append("</a></li>");
            }
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(getPage() - 1))
                    .append("\">")
                    .append(getPreviousText())
                    .append("</a></li>");
        }
        for (int i = 1; i <= getTotalPages(); i++) {
            if (getPage() == i)
                builder
                        .append("<li class=\"page-item active\"><span class=\"page-link\">")
                        .append(i)
                        .append("</span></li>");
            else
                builder.append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(i))
                        .append("\">")
                        .append(i)
                        .append("</a></li>");
        }
        if (getPage() < getTotalPages()) {
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(getPage() + 1))
                    .append("\">")
                    .append(getNextText())
                    .append("</a></li>");
            if (getLastPageText() != null) {
                builder
                        .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(getTotalPages()))
                        .append("\">")
                        .append(getLastPageText())
                        .append("</a></li>");
            }
        }
        builder
                .append("<li class=\"page-item\"><span class=\"page-link\">")
                .append(getAppend())
                .append("</span></li>");
        builder.append("</ul></nav>");
        return builder.toString();
    }

    public String getFlexibleLinks(Function<Integer, String> pageUrl, int total) {
        if (isAutoHide() && getTotalPages() == 1) {
            return "";
        }
        if (getPage() > getTotalPages() || getPage() < 1) {
            return "";
        }
        if (total < 1) {
            throw new IllegalArgumentException("Make sure that ElasticityCount is greater than 0");
        }
        StringBuilder builder = new StringBuilder();
        builder.append("<nav><ul class=\"pagination\">");

        int left = (total - 1) / 2;
        int right = total - left - 1;
        int start, end;
        if (getPage() <= left) {
            start = 1;
            end = total > getTotalPages() ? getTotalPages() : total;
        } else if ((getTotalPages() - getPage()) < right) {
            end = getTotalPages();
            start = end - total + 1;
            if (start < 1)
                start = 1;
        } else {
            start = getPage() - left;
            end = getPage() + right;
        }

        if (getPage() > 1) {
            if (getFirstPageText() != null) {
                builder
                        .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(1))
                        .append("\">")
                        .append(getFirstPageText())
                        .append("</a></li>");
            }
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(getPage() - 1))
                    .append("\">")
                    .append(getPreviousText())
                    .append("</a></li>");
        }
        for (int i = start; i <= end; i++) {
            if (getPage() == i)
                builder
                        .append("<li class=\"page-item active\"><span class=\"page-link\">")
                        .append(i)
                        .append("</span></li>");
            else
                builder
                        .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(i))
                        .append("\">")
                        .append(i)
                        .append("</a></li>");
        }

        if (getPage() < getTotalPages()) {
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(getPage() + 1))
                    .append("\">")
                    .append(getNextText())
                    .append("</a></li>");
            if (getLastPageText() != null) {
                builder
                        .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .append(pageUrl.apply(getTotalPages()))
                        .append("\">")
                        .append(getLastPageText())
                        .append("</a></li>");
            }
        }
        builder
                .append("<li class=\"page-item\"><span class=\"page-link\">")
                .append(getAppend())
                .append("</span></li>");
        builder.append("</ul></nav>");
        return builder.toString();
    }

    public String getSimpleLinks(Function<Integer, String> pageUrl) {
        if (isAutoHide() && getTotalPages() == 1) {
            return "";
        }
        StringBuilder builder = new StringBuilder();
        builder.append("<nav><ul class=\"pagination\">");
        if (getPage() <= 1) {
            builder
                    .append("<li class=\"page-item disabled\"><span class=\"page-link\">")
                    .append(getPreviousText())
                    .append("</span></li>");
        } else {
            int prev = getPage() - 1;
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(prev))
                    .append("\">")
                    .append(getPreviousText())
                    .append("</a></li>");
        }
        if (getPage() >= getTotalPages()) {
            builder
                    .append("<li class=\"page-item disabled\"><span class=\"page-link\">")
                    .append(getNextText())
                    .append("</span></li>");
        } else {
            int next = getPage() + 1;
            builder
                    .append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .append(pageUrl.apply(next))
                    .append("\">")
                    .append(getNextText())
                    .append("</a></li>");
        }
        builder
                .append("<li class=\"page-item\"><span class=\"page-link\">")
                .append(getAppend())
                .append("</span></li>");
        builder.append("</ul></nav>");
        return builder.toString();
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
                .append("data", data)
                .append("totalItems", totalItems)
                .append("size", size)
                .append("page", page)
                .append("previousText", previousText)
                .append("nextText", nextText)
                .append("firstPageText", firstPageText)
                .append("lastPageText", lastPageText)
                .append("autoHide", autoHide)
                .toString();
    }
}
