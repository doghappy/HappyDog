package wang.doghappy.java.module.console.user.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import wang.doghappy.java.module.console.user.model.User;

@Repository
public class JdbcUserRepository implements UserRepository {
    @Autowired
    private NamedParameterJdbcTemplate jdbcTemplate;

    @Override
    public User findByUsername(String username) {
        String sql = "SELECT * FROM Users WHERE UserName = :username";
        var map = new MapSqlParameterSource();
        map.addValue("username", username);
        return jdbcTemplate.query(sql, map, row -> {
            if (row.next()) {
                var user = new User();
                user.setUsername(row.getString("UserName"));
                user.setPasswordHash(row.getString("PasswordHash"));
                user.setSecurityStamp(row.getString("SecurityStamp"));
                user.setEmail(row.getString("Email"));
                user.setAccessFailedCount(row.getInt("AccessFailedCount"));
                user.setLockoutEnd(row.getTimestamp("LockoutEnd"));
                return user;
            }
            return null;
        });
    }
}
