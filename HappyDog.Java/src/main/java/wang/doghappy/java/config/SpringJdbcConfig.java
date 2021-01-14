//package wang.doghappy.java.config;
//
//import org.springframework.beans.factory.annotation.Value;
//import org.springframework.context.annotation.Bean;
//import org.springframework.context.annotation.Configuration;
//import org.springframework.jdbc.datasource.DriverManagerDataSource;
//
//import javax.sql.DataSource;
//
//@Configuration
//public class SpringJdbcConfig {
//
//    @Value("${mypassword}")
//    private String password;
//
//    @Bean
//    public DataSource mysqlDataSource() {
//        DriverManagerDataSource dataSource = new DriverManagerDataSource();
//        dataSource.setDriverClassName("com.mysql.jdbc.Driver");
//        dataSource.setUrl("jdbc:mysql://zhouxuezhong.top/doghappy-dev");
//        dataSource.setUsername("root");
//        dataSource.setPassword("Dztlh4uIqoVUwgxL");
//        return dataSource;
//    }
//}
