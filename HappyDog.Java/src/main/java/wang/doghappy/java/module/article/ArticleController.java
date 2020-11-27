package wang.doghappy.java.module.article;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import wang.doghappy.java.module.article.template.IndexTemplate;
import wang.doghappy.java.module.article.template.NetTemplate;

import javax.servlet.http.HttpServletRequest;

import java.util.stream.IntStream;

import static wang.doghappy.java.util.PaginationUrlGenerator.generate;

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
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new IndexTemplate();
        template.setData(request, model, page, articleService);
        return "article/index";
    }

    @GetMapping("/net")
    public String net(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new NetTemplate();
        template.setData(request, model, page, articleService);
        return "article/net";
    }
}
