package wang.doghappy.java.module.tag;

import javassist.NotFoundException;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import wang.doghappy.java.module.article.ArticleService;
import wang.doghappy.java.module.tag.model.TagDto;

import javax.servlet.http.HttpServletRequest;

import java.util.ArrayList;
import java.util.List;

import static wang.doghappy.java.util.PaginationUrlGenerator.generate;

@Controller
public class TagController {
    public TagController(TagService tagService, ArticleService articleService) {
        this.tagService = tagService;
        this.articleService = articleService;
    }

    private final TagService tagService;
    private final ArticleService articleService;

    @GetMapping("/tag")
    public String tags(Model model) {
        var tags = tagService.findTagDtos();
        model.addAttribute("tags", tags);
        return "tag/tag";
    }

    @GetMapping("/tag/{name}/article")
    public String articles(
            Model model,
            HttpServletRequest request,
            @PathVariable String name,
            @RequestParam(defaultValue = "1") int page
    ) throws NotFoundException {
        model.addAttribute("name", name);
        var tag = tagService.findTagByName(name);
        if (tag == null)
            throw new NotFoundException("Can not found");
        else {
            var articleIds = tagService.findArticleIds(tag.getId());
            if (articleIds.isEmpty())
                throw new NotFoundException("Can not found");
            else {
                var pagination = articleService.findByIds(articleIds, page);
                model.addAttribute("articles", pagination.getData());
                String bar = pagination.getFlexibleLinks(p -> generate(request, p), 7);
                model.addAttribute("bar", bar);
            }
        }
        return "tag/articles";
    }

    @GetMapping("/api/tag")
    @ResponseBody
    public List<TagDto> tags(){
        return new ArrayList<>();
    }
}
