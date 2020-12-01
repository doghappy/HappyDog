package wang.doghappy.java.module.tag.repository;

import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.model.TagDto;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

@Repository
public class JdbcTagRepository implements TagRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    private void setProperties(TagDto dto, ResultSet row) throws SQLException {
        dto.setId(row.getInt("Id"));
        dto.setName(row.getString("Name"));
        dto.setColor(row.getString("Color"));
        dto.setGlyphFont(row.getString("GlyphFont"));
        dto.setGlyph(row.getString("Glyph"));
    }

    @Override
    public ArrayList<ArticleIdTagDto> findTagDtoByArticleIds(Collection<Integer> articleIds) {
        var tags = new ArrayList<ArticleIdTagDto>();
        if (!articleIds.isEmpty()) {
            String articleIdsStr = StringUtils.join(articleIds, ',');
            String sql = "SELECT\n" +
                    "    ArticleTagMappings.ArticleId,\n" +
                    "    Tags.Id, Tags.Name, Tags.Color, Tags.GlyphFont, Tags.Glyph\n" +
                    "FROM ArticleTagMappings\n" +
                    "    INNER JOIN Tags ON ArticleTagMappings.TagId = Tags.Id\n" +
                    "WHERE ArticleId in(" + articleIdsStr + ")";
            jdbcTemplate.query(sql, row -> {
                var tag = new ArticleIdTagDto();
                setProperties(tag, row);
                tag.setArticleId(row.getInt("ArticleId"));
                tags.add(tag);
            });
        }
        return tags;
    }

    public List<TagDto> findTagDtoByArticleId(int articleId) {
        String sql = "SELECT\n" +
                "    Tags.Id, Tags.Name, Tags.Color, Tags.GlyphFont, Tags.Glyph\n" +
                "FROM ArticleTagMappings\n" +
                "    INNER JOIN Tags ON ArticleTagMappings.TagId = Tags.Id\n" +
                "WHERE ArticleId =" + articleId;
        return jdbcTemplate.query(sql, (row, num) -> {
            var tag = new ArticleIdTagDto();
            setProperties(tag, row);
            return tag;
        });
    }
}
