package wang.doghappy.java.module.article.repository;

import org.springframework.beans.factory.annotation.Autowired;
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
import java.util.List;
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
        int categoryId = row.getInt("CategoryId");
        dto.setCategoryId(ArticleCategory.fromInteger(categoryId));

        var categoryDto = new CategoryDto();
        categoryDto.setId(categoryId);
        categoryDto.setColor(row.getString("CategoryColor"));
        categoryDto.setLabel(row.getString("CategoryLabel"));
        categoryDto.setValue(row.getString("CategoryValue"));
        dto.setCategory(categoryDto);
    }

    @Override
    public Pagination<ArticleDto> findEnabledDtos(int page, Optional<ArticleCategory> category) {
        var data = new Pagination<ArticleDto>();
        data.setPage(page);

        String countSql = "SELECT COUNT(*) FROM Articles WHERE Articles.Status = 1";
        var sqlParameters = new MapSqlParameterSource();
        sqlParameters.addValue("offset", data.getOffset());
        if (category.isPresent()) {
            countSql += " AND CategoryId = :categoryId";
            sqlParameters.addValue("categoryId", category.get().getValue());
        }

        int count = jdbcTemplate.queryForObject(countSql, sqlParameters, Integer.class);
        data.setTotalItems(count);
        String sql = "SELECT\n" +
                "    Articles.Id, Articles.Title, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "FROM Articles\n" +
                "    INNER JOIN Categories on Articles.CategoryId = Categories.Id\n" +
                "WHERE Articles.Status = 1";

        if (category.isPresent())
            sql += " AND Articles.CategoryId = :categoryId";
        sql += " ORDER BY Articles.Id desc\n" +
                "LIMIT 20 OFFSET :offset";
        var articles = jdbcTemplate.query(sql, sqlParameters, (row, num) -> {
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
                "WHERE Articles.Id=:id AND Articles.Status = 1";
        var sqlParameters = new MapSqlParameterSource();
        sqlParameters.addValue("id", id);
        return jdbcTemplate.query(sql, sqlParameters, row -> {
            var item = new ArticleDetailDto();
            if (row.next()) {
                setProperties(item, row);
                item.setContent(row.getString("Content"));
            }
            return item;
        });
    }

    public Pagination<ArticleDto> findByIds(List<Integer> ids, int page) {
        var data = new Pagination<ArticleDto>();
        data.setPage(page);

        String countSql = "SELECT COUNT(*) FROM Articles WHERE Status = 1 AND Id IN (:articleIds)";
        var sqlParameters = new MapSqlParameterSource();
        sqlParameters.addValue("articleIds", ids);
        sqlParameters.addValue("offset", data.getOffset());
        int count = jdbcTemplate.queryForObject(countSql, sqlParameters, Integer.class);
        data.setTotalItems(count);

        String sql = "SELECT\n" +
                "    Articles.Id, Articles.Title, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "FROM Articles\n" +
                "    INNER JOIN Categories on Articles.CategoryId = Categories.Id\n" +
                "WHERE Articles.Status = 1 AND Articles.Id IN (:articleIds)\n" +
                "ORDER BY Articles.Id desc\n" +
                "LIMIT 20 OFFSET :offset";
        var articles = jdbcTemplate.query(sql, sqlParameters, (row, num) -> {
            var item = new ArticleDto();
            setProperties(item, row);
            return item;
        });
        data.setData(articles);
        return data;
    }
}
