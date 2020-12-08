package wang.doghappy.java.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.provisioning.InMemoryUserDetailsManager;
import wang.doghappy.java.handler.LoginFailureHandler;

@Configuration
@EnableWebSecurity
public class SecurityConfig extends WebSecurityConfigurerAdapter {
    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http
                .authorizeRequests()
                .antMatchers("/", "/NET", "/Java", "/Database", "/Windows", "/Essays", "/Read", "/tag", "/tag/*/article", "/detail/\\d+", "/css/**").permitAll()
                .anyRequest().authenticated()
                .and()
                .formLogin()
                .loginPage("/console/login")
                .defaultSuccessUrl("/")
                .failureHandler(new LoginFailureHandler())
                .permitAll()
                .and()
                .logout().permitAll();
    }

    @Override
    protected UserDetailsService userDetailsService() {
        var user = User.withUsername("test").password("{noop}123456").roles("User").build();
        return new InMemoryUserDetailsManager(user);
    }
}
