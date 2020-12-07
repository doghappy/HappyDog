package wang.doghappy.java.module.tag;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class TagController {
    public TagController(TagService tagService) {
        this.tagService = tagService;
    }

    private final TagService tagService;

    @GetMapping("/tags")
    public String tags(Model model) {
        var tags = tagService.findTagDtos();
        model.addAttribute("tags", tags);
        return "tag/tags";
    }
}
