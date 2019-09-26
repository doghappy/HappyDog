import { Category } from './category';

export class Article {
    id: number;
    title: string;
    content: string;
    viewCount: number;
    status: number;
    createTime: string;
    categoryId: number;
    category: Category;
}
