package wang.doghappy.java.handler;

import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.web.authentication.SimpleUrlAuthenticationFailureHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class LoginFailureHandler extends SimpleUrlAuthenticationFailureHandler {
    public static final String LAST_USERNAME_KEY = "SPRING_SECURITY_LAST_USERNAME";
    public static final String LOGIN_ERROR_MESSAGE_KEY = "LOGIN_ERROR_MESSAGE";

    @Override
    public void onAuthenticationFailure(HttpServletRequest request, HttpServletResponse response, AuthenticationException exception) throws IOException, ServletException {
        String username = request.getParameter("username");
        request.getSession(true).setAttribute(LAST_USERNAME_KEY, username);
        String err = exception.getMessage();
        if (exception instanceof BadCredentialsException)
            err = "用户名或密码错误";
        request.getSession(true).setAttribute(LOGIN_ERROR_MESSAGE_KEY, err);
        super.onAuthenticationFailure(request, response, exception);
    }
}
