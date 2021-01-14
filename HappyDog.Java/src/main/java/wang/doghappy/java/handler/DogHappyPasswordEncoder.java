package wang.doghappy.java.handler;

import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;
import wang.doghappy.java.util.HashEncryptor;

@Component
public class DogHappyPasswordEncoder implements PasswordEncoder {
    @Override
    public String encode(CharSequence rawPassword) {
//        return HashEncryptor.hmacSha1()
        System.out.println("---------------" + "length:" + rawPassword.length() + "," + rawPassword);
        return "test";
    }

    @Override
    public boolean matches(CharSequence rawPassword, String encodedPassword) {
        System.out.println("================rawPassword:" + rawPassword + ", encodedPassword:" + encodedPassword);
        var arr = encodedPassword.split("\\s");
        if (arr.length == 2) {
            String salt = arr[0];
            String hash = arr[1];
            String pwd = HashEncryptor.hmacSha1(salt, rawPassword.toString());
            return pwd.equals(hash);
        }
        return false;
    }
}
