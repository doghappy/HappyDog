package wang.doghappy.java.module.console.user.repository;

import wang.doghappy.java.module.console.user.model.User;

public interface UserRepository {
    User findByUsername(String username);
}
