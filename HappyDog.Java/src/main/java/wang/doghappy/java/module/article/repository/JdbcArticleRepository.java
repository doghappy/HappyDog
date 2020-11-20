package wang.doghappy.java.module.article.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.util.Pagination;

@Repository
public class JdbcArticleRepository implements ArticleRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    @Override
    public Pagination<ArticleDto> findEnabledDtos(int page) {
        var data = new Pagination<ArticleDto>();
        data.setPage(page);

        String countSql = "SELECT COUNT(*) FROM Articles";
        var sqlParameters = new MapSqlParameterSource();
        int count = jdbcTemplate.queryForObject(countSql, sqlParameters, Integer.class);
        data.setTotalItems(count);

        String sql = "select\n" +
                "    Articles.Id, Articles.Title, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "from Articles\n" +
                "    inner join Categories on Articles.CategoryId = Categories.Id\n" +
                "order by Articles.Id desc\n" +
                "limit 20 offset " + data.getOffset();
        var articles = jdbcTemplate.query(sql, (row, num) -> {
            var item = new ArticleDto();
            item.setId(row.getInt("Id"));
            item.setTitle(row.getString("Title"));
            item.setCreateTime(row.getTimestamp("CreateTime"));
            item.setViewCount(row.getInt("ViewCount"));

            var category = new CategoryDto();
            category.setId(row.getInt("CategoryId"));
            category.setColor(row.getString("CategoryColor"));
            category.setLabel(row.getString("CategoryLabel"));
            category.setValue(row.getString("CategoryValue"));
            item.setCategory(category);
            return item;
        });
        data.setData(articles);

        return data;
    }
}
