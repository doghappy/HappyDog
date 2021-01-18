package wang.doghappy.java.module.category.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.category.model.CategoryDto;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

@Repository
public class JdbcCategoryRepository implements CategoryRepository {

    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    public List<CategoryDto> findAll() {
        String sql = "SELECT * FROM Categories";
        return jdbcTemplate.query(sql, (row, number) -> {
            var dto = new CategoryDto();
            setProperty(row, dto);
            return dto;
        });
    }

    private void setProperty(ResultSet row, CategoryDto dto) throws SQLException {
        dto.setId(row.getInt("Id"));
        dto.setLabel(row.getString("Label"));
        dto.setValue(row.getString("Value"));
        dto.setColor(row.getString("Color"));
    }

    public CategoryDto findById(int id) {
        String sql = "SELECT * FROM Categories where Id = :Id";
        var params = new MapSqlParameterSource();
        params.addValue("Id", id);
        return jdbcTemplate.query(sql, params, row -> {
            if (row.next()) {
                var dto = new CategoryDto();
                setProperty(row, dto);
                return dto;
            }
            return null;
        });
    }
}
