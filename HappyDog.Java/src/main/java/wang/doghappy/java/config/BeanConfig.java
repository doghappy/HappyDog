package wang.doghappy.java.config;

import org.modelmapper.Converter;
import org.modelmapper.ModelMapper;
import org.modelmapper.convention.MatchingStrategies;
import org.modelmapper.spi.MappingContext;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import wang.doghappy.java.module.article.model.PostArticleDto;
import wang.doghappy.java.module.model.BaseStatus;

@Configuration
public class BeanConfig {
    @Bean
    public ModelMapper getModelMapper() {
        var mapper = new ModelMapper();
        mapper.getConfiguration().setMatchingStrategy(MatchingStrategies.STRICT);
//        mapper
//                .typeMap(PostArticleDto.class, Article.class)
//                .setConverter(ctx -> {
//                    var s = ctx.getSource();
//                    var d = ctx.getDestination();
//                    d.setStatus(BaseStatus.fromInteger(s.getStatus()));
//                    return d;
//                });
        return mapper;
    }
}
