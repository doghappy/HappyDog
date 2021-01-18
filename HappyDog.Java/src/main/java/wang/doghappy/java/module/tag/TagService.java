package wang.doghappy.java.module.tag;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import wang.doghappy.java.module.tag.model.PostTagDto;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;
import java.util.List;

@Service
public class TagService {

    @Autowired
    public void setTagRepository(TagRepository tagRepository) {
        this.tagRepository = tagRepository;
    }

    private TagRepository tagRepository;

    public List<TagDto> findTagDtos() {
        return tagRepository.findTagDtos();
    }

    public TagDto findTagByName(String name) {
        return tagRepository.findTagByName(name);
    }

    public List<Integer> findArticleIds(int tagId) {
        return tagRepository.findArticleIds(tagId);
    }

    public  TagDto post(PostTagDto dto){
        return tagRepository.post(dto);
    }
}
