package wang.doghappy.java.module.article;

import org.commonmark.node.Node;
import org.commonmark.parser.Parser;
import org.commonmark.renderer.html.HtmlRenderer;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.template.*;
import wang.doghappy.java.util.HttpDataResponse;

import javax.servlet.http.HttpServletRequest;
import java.util.List;

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

    @GetMapping("/NET")
    public String net(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new NetTemplate();
        template.setData(request, model, page, articleService);
        return "article/net";
    }

    @GetMapping("/Java")
    public String java(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new JavaTemplate();
        template.setData(request, model, page, articleService);
        return "article/java";
    }

    @GetMapping("/Database")
    public String database(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new DatabaseTemplate();
        template.setData(request, model, page, articleService);
        return "article/database";
    }

    @GetMapping("/Windows")
    public String windows(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new WindowsTemplate();
        template.setData(request, model, page, articleService);
        return "article/windows";
    }

    @GetMapping("/Read")
    public String read(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new ReadTemplate();
        template.setData(request, model, page, articleService);
        return "article/read";
    }

    @GetMapping("/Essays")
    public String essays(
            Model model,
            HttpServletRequest request,
            @RequestParam(defaultValue = "1") int page
    ) {
        var template = new EssaysTemplate();
        template.setData(request, model, page, articleService);
        return "article/essays";
    }

    @GetMapping("/detail/{id:\\d+}")
    public String detail(
            Model model,
            @PathVariable int id
    ) {
        var article = articleService.findOne(id);
        Parser parser = Parser.builder().build();
        Node doc = parser.parse(article.getContent());
        HtmlRenderer renderer = HtmlRenderer.builder().build();
        article.setContent(renderer.render(doc));
        model.addAttribute("article", article);
        return "article/detail";
    }

    @GetMapping("/api/article/disabled")
    @ResponseBody
    public HttpDataResponse<List<ArticleDto>> hidden() {
        var response = new HttpDataResponse<List<ArticleDto>>();
        var articles = articleService.findAllHidden();
        response.setData(articles);
        return response;
    }
}
