package wang.doghappy.java.module.tag.repository;

import wang.doghappy.java.module.tag.model.ArticleIdTagDto;
import wang.doghappy.java.module.tag.model.TagDto;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public interface TagRepository {
    ArrayList<ArticleIdTagDto> findTagDtoByArticleIds(Collection<Integer> articleIds);
    List<TagDto> findTagDtoByArticleId(int articleId);
    List<TagDto> findTagDtos();
    TagDto findTagByName(String name);
    List<Integer> findArticleIds(int tagId);
}
