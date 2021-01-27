package wang.doghappy.java.module.console.user;

import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;
import wang.doghappy.java.module.console.user.model.UserPrincipal;
import wang.doghappy.java.module.console.user.repository.UserRepository;

@Service
public class UserService implements UserDetailsService {
    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    private final UserRepository userRepository;

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        var ex = new UsernameNotFoundException("用户名或密码错误");
        if (!username.isEmpty()) {
            var user = userRepository.findByUsername(username);
            if (user != null) {
                return new UserPrincipal(user);
            }
        }
        throw ex;
    }
}
