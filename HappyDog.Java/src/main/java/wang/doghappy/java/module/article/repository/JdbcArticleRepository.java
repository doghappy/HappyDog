package wang.doghappy.java.module.article.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.model.ArticleCategory;

import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;

@Repository
public class JdbcArticleRepository implements ArticleRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    @Override
    public List<ArticleDto> findEnabledDtos(FindEnabledDtosParameter parameter) {
        String sql = "select\n" +
                "    Articles.Id, Articles.Title, Articles.CategoryId, Articles.CreateTime, Articles.ViewCount,\n" +
                "    Categories.Id CategoryId, Categories.Label CategoryLabel, Categories.Value CategoryValue, Categories.Color CategoryColor\n" +
                "from Articles\n" +
                "    inner join Categories on Articles.CategoryId = Categories.Id\n" +
                "limit 1 offset 2";
        return jdbcTemplate.query(sql, (row, num) -> {
            var item = new ArticleDto();
            item.setId(row.getInt("Id"));
            item.setTitle(row.getString("Title"));
//            item.setCreateTime(OffsetDateTime.parse(row.getda("CreateTime")));
            item.setId(row.getInt("ViewCount"));

            var category = new CategoryDto();
//            ArticleCategory.values()[0]
//            var ac=new ArticleCategory(row.getInt("CategoryId"));
//            category.setId();
            category.setColor(row.getString("CategoryColor"));
            category.setLabel(row.getString("CategoryLabel"));
            category.setValue(row.getString("CategoryValue"));
            item.setCategory(category);
            return item;
        });
    }
}
