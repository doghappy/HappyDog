package wang.doghappy.java;

import com.ulisesbocchio.jasyptspringboot.annotation.EnableEncryptableProperties;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
@EnableEncryptableProperties
public class DogHappyApplication {

	public static void main(String[] args) {
		SpringApplication.run(DogHappyApplication.class, args);
	}

}
