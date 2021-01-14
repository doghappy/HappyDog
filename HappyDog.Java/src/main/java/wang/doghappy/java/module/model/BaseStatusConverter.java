package wang.doghappy.java.module.model;

import javax.persistence.AttributeConverter;
import javax.persistence.Converter;

@Converter(autoApply = true)
public class BaseStatusConverter implements AttributeConverter<BaseStatus, Integer> {
    @Override
    public Integer convertToDatabaseColumn(BaseStatus attribute) {
        return attribute.getValue();
    }

    @Override
    public BaseStatus convertToEntityAttribute(Integer dbData) {
        return BaseStatus.fromInteger(dbData);
    }
}
