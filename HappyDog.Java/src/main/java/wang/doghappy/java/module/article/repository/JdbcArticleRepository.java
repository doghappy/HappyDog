package wang.doghappy.java.module.article.repository;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.support.GeneratedKeyHolder;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.model.BaseStatus;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Repository
public class JdbcArticleRepository implements ArticleRepository {
    private ModelMapper modelMapper;
    private TagRepository tagRepository;

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    @Autowired
    public void setModelMapper(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

    @Autowired
    public void setTagRepository(TagRepository tagRepository) {
        this.tagRepository = tagRepository;
    }

    private void setProperties(ArticleDto dto, ResultSet row) throws SQLException {
        dto.setId(row.getInt("Id"));
        dto.setTitle(row.getString("Title"));
        dto.setCreateTime(row.getTimestamp("CreateTime"));
        dto.setViewCount(row.getInt("ViewCount"));
        int status = row.getInt("Status");
        dto.setStatus(BaseStatus.fromInteger(status));
        int categoryId = row.getInt("CategoryId");
        dto.setCategoryId(ArticleCategory.fromInteger(categoryId));
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
        String sql = "SELECT * FROM Articles WHERE Status = 1";

        if (category.isPresent())
            sql += " AND CategoryId = :categoryId";
        sql += " ORDER BY Id desc LIMIT 20 OFFSET :offset";
        var articles = jdbcTemplate.query(sql, sqlParameters, (row, num) -> {
            var item = new ArticleDto();
            setProperties(item, row);
            return item;
        });
        data.setData(articles);

        return data;
    }

    public ArticleDetailDto findOne(int id) {
        String sql = "SELECT * FROM Articles WHERE Id=:id AND Status = 1";
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

        String sql = "SELECT * FROM Articles WHERE Status = 1 AND Id IN (:articleIds)\n" +
                "ORDER BY Id desc\n" +
                "LIMIT 20 OFFSET :offset";
        var articles = jdbcTemplate.query(sql, sqlParameters, (row, num) -> {
            var item = new ArticleDto();
            setProperties(item, row);
            return item;
        });
        data.setData(articles);
        return data;
    }

    public List<ArticleDto> findAllDisabled() {
        String sql = "SELECT * FROM Articles WHERE Status = 0 ORDER BY Id DESC";
        return jdbcTemplate.query(sql, (row, num) -> {
            var item = new ArticleDto();
            setProperties(item, row);
            return item;
        });
    }

    public ArticleDetailDto post(PostArticleDto dto) {
        var article = modelMapper.map(dto, ArticleDetailDto.class);
        article.setCreateTime(Timestamp.from(Instant.now()));
        String sql = "INSERT Articles(Title, Content, CategoryId, CreateTime, ViewCount, Status) " +
                "VALUES(:Title, :Content, :CategoryId, :CreateTime, :ViewCount, :Status);";
        var params = new MapSqlParameterSource();
        params.addValue("Title", article.getTitle());
        params.addValue("Content", article.getContent());
        params.addValue("CategoryId", article.getCategoryId().getValue());
        params.addValue("CreateTime", article.getCreateTime());
        params.addValue("ViewCount", 0);
        params.addValue("Status", article.getStatus().getValue());
        var keyHolder = new GeneratedKeyHolder();
        jdbcTemplate.update(sql, params, keyHolder);
        int id = keyHolder.getKey().intValue();
        article.setId(id);
        insertTagIds(id, dto.getTagIds());
        return article;
    }

    private int insertTagIds(int articleId, List<Integer> tagIds) {
        if (tagIds != null && !tagIds.isEmpty()) {
            var ids = tagIds.stream().distinct().collect(Collectors.toList());
            if (!ids.isEmpty()) {
                var builder = new StringBuilder();
                for (var id : ids) {
                    builder
                            .append("INSERT ArticleTagMappings VALUES(")
                            .append(articleId)
                            .append(", ")
                            .append(id)
                            .append(");")
                            .append(System.lineSeparator());
                }
                return jdbcTemplate.update(builder.toString(), new MapSqlParameterSource());
            }
        }
        return 0;
    }

    public ArticleDetailDto findByIdForConsole(int id) {
        String sql = "SELECT * FROM Articles WHERE Id = :id";
        var params = new MapSqlParameterSource();
        params.addValue("id", id);
        return jdbcTemplate.query(sql, params, row -> {
            var dto = new ArticleDetailDto();
            if (row.next()) {
                setProperties(dto, row);
                dto.setContent(row.getString("Content"));
            }
            return dto;
        });
    }
}
