package wang.doghappy.java.module.console.user.model;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Collection;

public class UserPrincipal implements UserDetails {
    public UserPrincipal(User user) {
        this.user = user;
    }

    private final User user;

    public User getUser() {
        return user;
    }

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return new ArrayList<>();
    }

    @Override
    public String getPassword() {
        return getUser().getSecurityStamp() + " " + getUser().getPasswordHash();
    }

    @Override
    public String getUsername() {
        return getUser().getUsername();
    }

    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        var ts = getUser().getLockoutEnd();
        if (ts != null) {
            return ts.after(new Timestamp(System.currentTimeMillis()));
        }
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    @Override
    public boolean isEnabled() {
        return true;
    }
}
