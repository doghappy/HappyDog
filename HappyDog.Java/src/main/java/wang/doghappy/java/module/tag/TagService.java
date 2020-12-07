package wang.doghappy.java.module.tag;

import org.springframework.stereotype.Service;
import wang.doghappy.java.module.tag.model.TagDto;
import wang.doghappy.java.module.tag.repository.TagRepository;

import java.util.List;

@Service
public class TagService {

    public TagService(TagRepository tagRepository) {
        this.tagRepository = tagRepository;
    }

    private final TagRepository tagRepository;

    public List<TagDto> findTagDtos() {
        return tagRepository.findTagDtos();
    }
}
