package wang.doghappy.java.module.article.template;

import org.springframework.ui.Model;
import wang.doghappy.java.module.article.ArticleService;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.util.Pagination;

import javax.servlet.http.HttpServletRequest;

import java.util.Optional;

import static wang.doghappy.java.util.PaginationUrlGenerator.generate;

public abstract class CategoryTemplate {
    public void setData(
            HttpServletRequest request,
            Model model,
            int page,
            ArticleService articleService
    ) {
        var pagination = getArticles(articleService, page);
        model.addAttribute("list", pagination.getData());
        String bar = pagination.getFlexibleLinks(p -> generate(request, p), 7);
        model.addAttribute("bar", bar);
    }

    protected abstract Optional<ArticleCategory> getCategory();

    private Pagination<ArticleDto> getArticles(ArticleService articleService, int page) {
        return articleService.findEnabledDtos(page, getCategory());
    }
}
