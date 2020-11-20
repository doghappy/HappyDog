package wang.doghappy.java.module.article;

import org.springframework.stereotype.Service;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.FindEnabledDtosParameter;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.util.ArrayList;
import java.util.List;

import static java.util.stream.Collectors.toList;

@Service
public class ArticleService {

    public ArticleService(
            ArticleRepository articleRepository,
            TagRepository tagRepository
    ) {
        this.articleRepository = articleRepository;
        this.tagRepository = tagRepository;
    }

    private final ArticleRepository articleRepository;
    private final TagRepository tagRepository;

    public Pagination<ArticleDto> findEnabledDtos(int page) {
        var pagination=articleRepository.findEnabledDtos(page);
        var articles = pagination.getData();
        if (!articles.isEmpty()) {
            var articleIds = articles
                    .stream()
                    .map(d -> d.getId())
                    .collect(toList());
            var tags = tagRepository.findTagDtoByArticleIds(articleIds);
            articles
                    .stream()
                    .forEach(article -> {
                        var articleTags = new ArrayList<TagDto>();
                        article.setTags(articleTags);
                        tags
                                .stream()
                                .filter(tag -> tag.getArticleId() == article.getId())
                                .forEach(tag -> articleTags.add(tag));
                    });
        }
        return pagination;
    }
}
