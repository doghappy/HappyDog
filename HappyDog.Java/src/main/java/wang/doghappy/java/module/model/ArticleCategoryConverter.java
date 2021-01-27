package wang.doghappy.java.module.model;

import javax.persistence.AttributeConverter;
import javax.persistence.Converter;

@Converter(autoApply = true)
public class ArticleCategoryConverter implements AttributeConverter<ArticleCategory, Integer> {
    @Override
    public Integer convertToDatabaseColumn(ArticleCategory attribute) {
        return attribute.getValue();
    }

    @Override
    public ArticleCategory convertToEntityAttribute(Integer dbData) {
        return ArticleCategory.fromInteger(dbData);
    }
}
