import { Category } from "./category";

export class ArticleSummary {
  id: number;
  title: string;
  createTime: string;
  viewCount: number;
  category: Category;
}
