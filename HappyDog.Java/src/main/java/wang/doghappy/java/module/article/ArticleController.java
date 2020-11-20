package wang.doghappy.java.module.article;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;

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
    public String index(Model model) {
        var parameter = new FindEnabledDtosParameter();
        var list = articleService.findEnabledDtos(parameter);
        model.addAttribute("list", list);
        return "article/index";
    }
}
