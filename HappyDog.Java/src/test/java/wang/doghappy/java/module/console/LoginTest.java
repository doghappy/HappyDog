package wang.doghappy.java.module.console;

import org.junit.jupiter.api.Test;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.standaloneSetup;

public class LoginTest {
    @Test
    public void test1() throws Exception {
        var controller = new ConsoleController(null);
        MockMvc mockMvc = standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/console/login"))
                .andExpect(status().isOk())
                .andExpect(view().name("console/login"));
    }
}
