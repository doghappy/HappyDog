package wang.doghappy.java.module.category.repository;

import wang.doghappy.java.module.category.model.CategoryDto;

import java.util.List;

public interface CategoryRepository {
    CategoryDto findById(int id);
    List<CategoryDto> findAll();
}
