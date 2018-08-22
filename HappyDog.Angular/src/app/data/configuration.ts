import { Category } from "../models/category";

export class Configuration {
  public static categories: Category[] = [
    { id: 1, value: 'net', label: '.Net', color: '#7014e8', iconClass: 'fa fa-cubes' },
    { id: 2, value: 'db', label: '数据库', color: '#f65314', iconClass: 'fa fa-database' },
    { id: 3, value: 'windows', label: 'Windows', color: '#00a1f1', iconClass: 'fa fa-windows' },
    { id: 4, value: 'read', label: '阅读', color: '#7cbb00', iconClass: 'fa fa-bookmark' },
    { id: 5, value: 'essays', label: '随笔', color: '#ffbb00', iconClass: 'fa fa-pencil' },
  ]
}
