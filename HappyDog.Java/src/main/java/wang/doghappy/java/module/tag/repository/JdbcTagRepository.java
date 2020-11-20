package wang.doghappy.java.module.tag.repository;

import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;

import java.util.ArrayList;
import java.util.Collection;

@Repository
public class JdbcTagRepository implements TagRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

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
                tag.setId(row.getInt("Id"));
                tag.setName(row.getString("Name"));
                tag.setColor(row.getString("Color"));
                tag.setGlyphFont(row.getString("GlyphFont"));
                tag.setGlyph(row.getString("Glyph"));
                tag.setArticleId(row.getInt("ArticleId"));
                tags.add(tag);
            });
        }
        return tags;
    }
}
