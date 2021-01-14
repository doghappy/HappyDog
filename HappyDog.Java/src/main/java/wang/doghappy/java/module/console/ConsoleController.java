package wang.doghappy.java.module.console;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
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
    public String hidden() {
        return "console/hidden";
    }
}
