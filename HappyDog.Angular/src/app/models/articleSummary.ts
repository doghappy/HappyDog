import { Category } from "./category";
import { BaseState } from "../enums/baseState";

export class ArticleSummary {
  id: number;
  title: string;
  createTime: string;
  viewCount: number;
  categoryId: number;
  category: Category;
  state: BaseState;
}
