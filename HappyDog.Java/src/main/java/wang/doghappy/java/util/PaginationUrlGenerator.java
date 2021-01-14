package wang.doghappy.java.util;

import javax.servlet.http.HttpServletRequest;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class PaginationUrlGenerator {

    public static String generate(HttpServletRequest request, int page) {
        request.setAttribute("page", page);
        var builder = new StringBuilder();
        builder.append(request.getRequestURI());
        String queryString = request.getQueryString();
        var queryMap = getQueryStringMap(queryString);
        queryMap.put("page", Integer.toString(page));
        return getQueryString(queryMap);
    }

    public static HashMap<String, String> getQueryStringMap(String queryString) {
        var map = new HashMap<String, String>();
        if (queryString != null) {
            if (queryString.length() > 0 && queryString.charAt(0) == '?')
                queryString = queryString.substring(1);
            if (!queryString.isEmpty()) {
                Arrays.stream(queryString.split("&"))
                        .forEach(item -> {
                            int index = item.indexOf('=');
                            if (index > -1) {
                                String key = item.substring(0, index);
                                String val = item.substring(index + 1);
                                map.put(key, val);
                            }
                        });
            }
        }
        return map;
    }

    /**
     * @param map
     * @return Note: that if the return value is not empty, the string will start with a question mark.
     */
    public static String getQueryString(Map<String, String> map) {
        if (map == null || map.size() == 0)
            return "";
        var builder = new StringBuilder();
        for (Map.Entry<String, String> item : map.entrySet()) {
            builder
                    .append("&")
                    .append(item.getKey())
                    .append("=")
                    .append(item.getValue());
        }
        if (builder.length() > 0)
            builder.replace(0, 1, "?");
        return builder.toString();
    }
}
