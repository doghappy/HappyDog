package wang.doghappy.java.module.article;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import wang.doghappy.java.module.article.model.Article;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.article.repository.JpaArticleRepository;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.category.repository.JpaCategoryRepository;
import wang.doghappy.java.module.model.ArticleCategory;
import wang.doghappy.java.module.tag.model.Tag;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.JpaTagRepository;
import wang.doghappy.java.module.tag.repository.TagRepository;
import wang.doghappy.java.util.Pagination;

import java.sql.Timestamp;
import java.time.Instant;
import java.util.ArrayList;
import java.util.HashSet;
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
    private JpaCategoryRepository jpaCategoryRepository;
    private JpaTagRepository jpaTagRepository;
    private ModelMapper modelMapper;

    @Autowired
    public void setJpaCategoryRepository(JpaCategoryRepository jpaCategoryRepository) {
        this.jpaCategoryRepository = jpaCategoryRepository;
    }

    @Autowired
    public void setJpaTagRepository(JpaTagRepository jpaTagRepository) {
        this.jpaTagRepository = jpaTagRepository;
    }

    @Autowired
    public void setModelMapper(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

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
                                .filter(c -> c.getId() == a.getCategoryId().getValue())
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

    public List<ArticleDto> findAllDisabled() {
        var articles = jpaArticleRepository.findAllDisabled();
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

    public ArticleDetailDto post(PostArticleDto dto) {
        var article = modelMapper.map(dto, Article.class);
        article.setCreateTime(Timestamp.from(Instant.now()));
        var tagIds = dto.getTagIds();
        if (tagIds != null && !tagIds.isEmpty()) {
            var tags = jpaTagRepository.findAllById(tagIds);
            article.setTags(new HashSet<>(tags));
        }
        jpaArticleRepository.save(article);
        return modelMapper.map(article, ArticleDetailDto.class);
    }
}
