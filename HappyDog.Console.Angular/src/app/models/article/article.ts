import { Category } from './category';
import { Tag } from '../tag/tag';

export class Article {
    id: number;
    title: string;
    content: string;
    viewCount: number;
    status: number;
    createTime: string;
    categoryId: number;
    category: Category;
    tags: Tag[];
}
