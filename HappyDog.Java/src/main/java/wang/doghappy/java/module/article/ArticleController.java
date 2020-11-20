package wang.doghappy.java.module.article;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class ArticleController {
    @Autowired
    public ArticleController(ArticleService articleService) {
        this.articleService = articleService;
    }

    //    @Value("${spring.profile}")
//    private String profile;
//    private ArticleRepository articleRepository;
    private final ArticleService articleService;

    @GetMapping("/")
    public String index(
            Model model,
            @RequestParam(defaultValue = "1") int page
    ) {
        var pagination = articleService.findEnabledDtos(page);
        model.addAttribute("list", pagination.getData());
        String bar = pagination.getFlexibleLinks(p -> p.toString(), 7);
        model.addAttribute("bar", bar);
        return "article/index";
    }
}
