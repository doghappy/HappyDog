package wang.doghappy.java.module.tag.repository;

import org.apache.commons.lang3.StringUtils;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.support.GeneratedKeyHolder;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.model.PostTagDto;
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

    private ModelMapper modelMapper;

    @Autowired
    public void setModelMapper(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

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

    public List<TagDto> findTagDtos() {
        String sql = "SELECT Id, Name, Color, GlyphFont, Glyph FROM Tags";
        return jdbcTemplate.query(sql, (row, num) -> {
            var tag = new TagDto();
            setProperties(tag, row);
            return tag;
        });
    }

    public TagDto findTagByName(String name) {
        String sql = "SELECT Id, Name, Color, GlyphFont, Glyph FROM Tags WHERE Name = :name";
        var map = new MapSqlParameterSource();
        map.addValue("name", name);
        return jdbcTemplate.query(sql, map, row -> {
            var tag = new TagDto();
            if (row.next()) {
                setProperties(tag, row);
            }
            return tag;
        });
    }

    public List<Integer> findArticleIds(int tagId) {
        String sql = "SELECT ArticleId FROM ArticleTagMappings WHERE TagId=:tagId";
        var map = new MapSqlParameterSource();
        map.addValue("tagId", tagId);
        return jdbcTemplate.query(sql, map, (row, num) -> row.getInt("ArticleId"));
    }

    public TagDto post(PostTagDto dto) {
        String sql = "INSERT Tags(Name, Color, GlyphFont, Glyph) VALUES(:Name, :Color, :GlyphFont, :Glyph)";
        var map = new MapSqlParameterSource();
        map.addValue("Name", dto.getName());
        map.addValue("Color", dto.getColor());
        map.addValue("GlyphFont", dto.getGlyphFont());
        map.addValue("Glyph", dto.getGlyph());
        var keyHolder = new GeneratedKeyHolder();
        jdbcTemplate.update(sql, map, keyHolder);
        int id = keyHolder.getKey().intValue();
        var tag = modelMapper.map(dto, TagDto.class);
        tag.setId(id);
        return tag;
    }
}
