export class PutArticleDto {
    title: string;
    content: string;
    status: number;
    categoryId: number;
    tagNames: string[];
}
