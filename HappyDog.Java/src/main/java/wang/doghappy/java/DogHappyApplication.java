package wang.doghappy.java;

import com.ulisesbocchio.jasyptspringboot.annotation.EnableEncryptableProperties;
import org.springframework.beans.factory.annotation.Autowired;
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

//    @Autowired
//    private JpaArticleRepository jpaArticleRepository;
//
//    @Bean
//    public CommandLineRunner demo() {
//        return args -> {
//            System.out.println("*************");
//            var article = jpaArticleRepository.findById(10111);
//            System.out.println(article);
//            article.ifPresent(a-> System.out.println(a.getCategory()));
//        };
//    }
}
