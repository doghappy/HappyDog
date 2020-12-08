package wang.doghappy.java.handler;

import org.springframework.security.core.AuthenticationException;
import org.springframework.security.web.authentication.SimpleUrlAuthenticationFailureHandler;
import org.springframework.stereotype.Component;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@Component
public class LoginFailureHandler extends SimpleUrlAuthenticationFailureHandler {
    public static final String LAST_USERNAME_KEY = "SPRING_SECURITY_LAST_USERNAME";
    public static final String LOGIN_ERROR_MESSAGE_KEY = "LOGIN_ERROR_MESSAGE";

    @Override
    public void onAuthenticationFailure(HttpServletRequest request, HttpServletResponse response, AuthenticationException exception) throws IOException, ServletException {
        String username = request.getParameter("username");
        request.getSession(true).setAttribute(LAST_USERNAME_KEY, username);
        request.getSession(true).setAttribute(LOGIN_ERROR_MESSAGE_KEY, exception.getMessage());
        super.onAuthenticationFailure(request, response, exception);
    }
}
