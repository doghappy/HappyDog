package wang.doghappy.java.module.article;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import wang.doghappy.java.module.article.model.ArticleDetailDto;
import wang.doghappy.java.module.article.model.ArticleDto;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.article.repository.ArticleRepository;
import wang.doghappy.java.module.category.model.CategoryDto;
import wang.doghappy.java.module.category.repository.CategoryRepository;
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

    private ArticleRepository articleRepository;
    private TagRepository tagRepository;
    private CategoryRepository categoryRepository;
//    private ModelMapper modelMapper;

    @Autowired
    public void setArticleRepository(ArticleRepository articleRepository) {
        this.articleRepository = articleRepository;
    }

    @Autowired
    public void setTagRepository(TagRepository tagRepository) {
        this.tagRepository = tagRepository;
    }

    @Autowired
    public void setCategoryRepository(CategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

//    @Autowired
//    public void setModelMapper(ModelMapper modelMapper) {
//        this.modelMapper = modelMapper;
//    }

    public Pagination<ArticleDto> findEnabledDtos(int page, Optional<ArticleCategory> category) {
        var pagination = articleRepository.findEnabledDtos(page, category);
        setTags(pagination.getData());
        setCategory(pagination.getData());
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

    private void setTags(ArticleDto article) {
        if (article != null) {
            var tags = tagRepository.findTagDtoByArticleId(article.getId());
            if (!tags.isEmpty()) {
                article.setTags(tags);
            }
        }
    }

    private void setCategory(List<ArticleDto> articles) {
        if (!articles.isEmpty()) {
            var categories = categoryRepository.findAll();
//            for (var category : categories) {
//                for (var article :  articles) {
//                    if(article.)
//                }
//            }
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

    private void setCategory(ArticleDto article) {
        if (article != null) {
            var category = categoryRepository.findById(article.getCategoryId().getValue());
            if (category != null) {
                article.setCategory(category);
            }
        }
    }

    public Pagination<ArticleDto> findByIds(List<Integer> ids, int page) {
        var pagination = articleRepository.findByIds(ids, page);
        setTags(pagination.getData());
        return pagination;
    }

    public List<ArticleDto> findAllDisabled() {
        var articles = articleRepository.findAllDisabled();
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

    public ArticleDto post(PostArticleDto dto) {
        return articleRepository.post(dto);
    }

    public ArticleDetailDto findOneForConsole(int id) {
        var article = articleRepository.findByIdForConsole(id);
        setCategory(article);
        setTags(article);
        return article;
    }
}
