package wang.doghappy.java.module.tag;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;

import java.util.ArrayList;

import static org.hamcrest.Matchers.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.standaloneSetup;

@SpringBootTest
@AutoConfigureMockMvc
public class TagsTest {
    @Test
    public void testEmpty() throws Exception {
        var mockTagRepository = Mockito.mock(TagRepository.class);
        Mockito.when(mockTagRepository.findTagDtos()).thenReturn(new ArrayList<>());
        var tagService = new TagService(mockTagRepository);
        var controller = new TagController(tagService);
        MockMvc mockMvc = standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/tags"))
                .andExpect(status().isOk())
                .andExpect(view().name("tag/tags"))
                .andExpect(model().attribute("tags", hasSize(0)));
    }

    @Test
    public void testTags() throws Exception {
        var mockTagRepository = Mockito.mock(TagRepository.class);

        var tags = new ArrayList<TagDto>();
        var tag0 = new TagDto();
        tag0.setId(1);
        tag0.setName("tag 0");
        tags.add(tag0);
        Mockito.when(mockTagRepository.findTagDtos()).thenReturn(tags);

        var tagService = new TagService(mockTagRepository);
        var controller = new TagController(tagService);
        MockMvc mockMvc = standaloneSetup(controller).build();
        mockMvc.perform(MockMvcRequestBuilders.get("/tags"))
                .andExpect(status().isOk())
                .andExpect(view().name("tag/tags"))
                .andExpect(model().attribute("tags", hasSize(1)))
                .andExpect(model().attribute("tags", hasItem(allOf(
                        hasProperty("id", is(1)),
                        hasProperty("name", is("tag 0"))
                ))));
    }
}
