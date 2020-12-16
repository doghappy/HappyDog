package wang.doghappy.java.module.article;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import wang.doghappy.java.module.article.model.Article;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.category.repository.JpaCategoryRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static java.util.stream.Collectors.toList;

@Service
public class ArticleService {

    public ArticleService(
            ArticleRepository articleRepository,
            TagRepository tagRepository,
            JpaArticleRepository jpaArticleRepository
    ) {
        this.articleRepository = articleRepository;
        this.tagRepository = tagRepository;
        this.jpaArticleRepository = jpaArticleRepository;
    }

    private final ArticleRepository articleRepository;
    private final TagRepository tagRepository;
    private final JpaArticleRepository jpaArticleRepository;

    @Autowired
    private JpaCategoryRepository jpaCategoryRepository;

    public Pagination<ArticleDto> findEnabledDtos(int page, Optional<ArticleCategory> category) {
        var pagination = articleRepository.findEnabledDtos(page, category);
        setTags(pagination.getData());
        return pagination;
    }

    public ArticleDetailDto findOne(int id) {
        var dto = articleRepository.findOne(id);
        if (dto != null) {
            var tags = tagRepository.findTagDtoByArticleId(dto.getId());
            dto.setTags(tags);
        }
        return dto;
    }

    private void setTags(List<ArticleDto> articles) {
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
    }

    private void setCategory(List<ArticleDto> articles) {
        if (!articles.isEmpty()) {
            var categories = jpaCategoryRepository.findAll();
            articles
                    .stream()
                    .forEach(a -> {
                        categories
                                .stream()
                                .filter(c -> c.getId() == a.getCategoryId())
                                .findFirst()
                                .ifPresent(c -> {
                                    var dto = new CategoryDto();
                                    dto.setId(c.getId());
                                    dto.setColor(c.getColor());
                                    dto.setLabel(c.getLabel());
                                    dto.setValue(c.getValue());
                                    a.setCategory(dto);
                                });

                    });
        }
    }

    public Pagination<ArticleDto> findByIds(List<Integer> ids, int page) {
        var pagination = articleRepository.findByIds(ids, page);
        setTags(pagination.getData());
        return pagination;
    }

    public List<ArticleDto> findAllHidden() {
        var articles = jpaArticleRepository.findAllHidden();
        var dtos = articles
                .stream()
                .map(a -> {
                    var dto = new ArticleDto();
                    dto.setId(a.getId());
                    dto.setTitle(a.getTitle());
                    dto.setCategoryId(a.getCategoryId());
                    dto.setViewCount(a.getViewCount());
                    dto.setCreateTime(a.getCreateTime());
                    dto.setStatus(a.getStatus());
                    return dto;
                })
                .collect(toList());
        setTags(dtos);
        setCategory(dtos);
        return dtos;
    }
}
