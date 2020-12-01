package wang.doghappy.java.module.article.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.util.Pagination;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Optional;

@Repository
public class JdbcArticleRepository implements ArticleRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    private void setProperties(ArticleDto dto, ResultSet row) throws SQLException {
        dto.setId(row.getInt("Id"));
        dto.setTitle(row.getString("Title"));
        dto.setCreateTime(row.getTimestamp("CreateTime"));
        dto.setViewCount(row.getInt("ViewCount"));

        var categoryDto = new CategoryDto();
        categoryDto.setId(row.getInt("CategoryId"));
        categoryDto.setColor(row.getString("CategoryColor"));
        categoryDto.setLabel(row.getString("CategoryLabel"));
        categoryDto.setValue(row.getString("CategoryValue"));
        dto.setCategory(categoryDto);
    }

    @Override
    public Pagination<ArticleDto> findEnabledDtos(int page, Optional<ArticleCategory> category) {
        var data = new Pagination<ArticleDto>();
        data.setPage(page);

        String countSql = "SELECT COUNT(*) FROM Articles";
        if (category.isPresent())
            countSql += " WHERE CategoryId = " + category.get().getValue();
        var sqlParameters = new MapSqlParameterSource();
        int count = jdbcTemplate.queryForObject(countSql, sqlParameters, Integer.class);
        data.setTotalItems(count);

        String sql = "SELECT\n" +
                "    Articles.Id, Articles.Title, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "FROM Articles\n" +
                "    INNER JOIN Categories on Articles.CategoryId = Categories.Id\n";

        if (category.isPresent())
            sql += "WHERE Articles.CategoryId = " + category.get().getValue();
        sql += " ORDER BY Articles.Id desc\n" +
                "LIMIT 20 OFFSET " + data.getOffset();
        var articles = jdbcTemplate.query(sql, (row, num) -> {
            var item = new ArticleDto();
            setProperties(item, row);
            return item;
        });
        data.setData(articles);

        return data;
    }

    public ArticleDetailDto findOne(int id) {
        String sql = "SELECT\n" +
                "    Articles.Id, Articles.Title, Articles.Content, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "FROM Articles\n" +
                "    INNER JOIN Categories on Articles.CategoryId = Categories.Id\n" +
                "WHERE Articles.Id=" + id;
        return jdbcTemplate.query(sql, row -> {
            var item = new ArticleDetailDto();
            if (row.next()) {
                setProperties(item, row);
                item.setContent(row.getString("Content"));
            }
            return item;
        });
    }
}
