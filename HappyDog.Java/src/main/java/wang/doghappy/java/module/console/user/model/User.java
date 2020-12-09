package wang.doghappy.java.module.console.user.model;

import org.apache.commons.lang3.builder.ToStringBuilder;
import java.sql.Timestamp;

public class User {
    private String username;
    private String passwordHash;
    private String securityStamp;
    private String email;
    private int accessFailedCount;
    private Timestamp lockoutEnd;

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPasswordHash() {
        return passwordHash;
    }

    public void setPasswordHash(String passwordHash) {
        this.passwordHash = passwordHash;
    }

    public String getSecurityStamp() {
        return securityStamp;
    }

    public void setSecurityStamp(String securityStamp) {
        this.securityStamp = securityStamp;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public int getAccessFailedCount() {
        return accessFailedCount;
    }

    public void setAccessFailedCount(int accessFailedCount) {
        this.accessFailedCount = accessFailedCount;
    }

    public Timestamp getLockoutEnd() {
        return lockoutEnd;
    }

    public void setLockoutEnd(Timestamp lockoutEnd) {
        this.lockoutEnd = lockoutEnd;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
                .append("username", username)
                .append("passwordHash", passwordHash)
                .append("securityStamp", securityStamp)
                .append("email", email)
                .append("accessFailedCount", accessFailedCount)
                .append("lockoutEnd", lockoutEnd)
                .toString();
    }
}
