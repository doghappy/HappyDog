package wang.doghappy.java;

import com.ulisesbocchio.jasyptspringboot.annotation.EnableEncryptableProperties;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;

@SpringBootTest
@EnableEncryptableProperties
@ActiveProfiles("test")
class DogHappyApplicationTests {

	@Test
	void contextLoads() {
	}

}
