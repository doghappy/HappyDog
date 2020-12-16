package wang.doghappy.java;

import com.ulisesbocchio.jasyptspringboot.annotation.EnableEncryptableProperties;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import wang.doghappy.java.module.model.BaseStatus;

@SpringBootApplication
@EnableEncryptableProperties
public class DogHappyApplication {

    public static void main(String[] args) {
        SpringApplication.run(DogHappyApplication.class, args);
    }

    @Bean
    public CommandLineRunner demo() {
        return args -> {
            System.out.println(BaseStatus.ENABLED);
        };
    }
}
