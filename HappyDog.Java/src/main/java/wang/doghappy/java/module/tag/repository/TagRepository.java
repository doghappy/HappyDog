package wang.doghappy.java.module.tag.repository;

import wang.doghappy.java.module.tag.model.ArticleIdTagDto;

import java.util.ArrayList;
import java.util.Collection;

public interface TagRepository {
    ArrayList<ArticleIdTagDto> findTagDtoByArticleIds(Collection<Integer> articleIds);
}
