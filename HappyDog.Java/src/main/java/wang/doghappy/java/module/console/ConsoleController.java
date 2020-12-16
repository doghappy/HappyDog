package wang.doghappy.java.module.console;

import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import wang.doghappy.java.handler.LoginFailureHandler;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;

import javax.servlet.http.HttpServletRequest;

@Controller
@RequestMapping("/console")
public class ConsoleController {

    public ConsoleController(JpaArticleRepository jpaArticleRepository) {
        this.jpaArticleRepository = jpaArticleRepository;
    }

    private final JpaArticleRepository jpaArticleRepository;

    @GetMapping("/login")
    public String login(
            Model model,
            HttpServletRequest request
    ) {
        var session = request.getSession(true);
        var username = session.getAttribute(LoginFailureHandler.LAST_USERNAME_KEY);
        if (username != null) {
            model.addAttribute("username", username.toString());
            session.removeAttribute(LoginFailureHandler.LAST_USERNAME_KEY);
        }
        var errorMessage = session.getAttribute(LoginFailureHandler.LOGIN_ERROR_MESSAGE_KEY);
        if (errorMessage != null) {
            model.addAttribute("errorMessage", errorMessage.toString());
            session.removeAttribute(LoginFailureHandler.LOGIN_ERROR_MESSAGE_KEY);
        }
        return "console/login";
    }

    @GetMapping("/hidden")
    public String hidden(
            Model model,
            @RequestParam(defaultValue = "1") int page
    ) {
        var sort = Sort.by(Sort.Direction.DESC, "Id");
        var pageable = PageRequest.of(page, 20, sort);
//        var articles = jpaArticleRepository.findByStatusIs0(pageable);
        var articles = jpaArticleRepository.findAllHidden();
        model.addAttribute("articles", articles);
        return "console/hidden";
    }
}
