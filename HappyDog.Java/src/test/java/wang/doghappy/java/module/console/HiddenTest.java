package wang.doghappy.java.module.console;

import org.junit.jupiter.api.Test;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.view;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.standaloneSetup;

public class HiddenTest {
    @Test
    public void testAnonymousAccess() throws Exception {
        var controller = new ConsoleController();
        MockMvc mockMvc = standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/console/hidden"))
                .andExpect(status().isOk())
                .andExpect(view().name("console/hidden"));
    }
}
