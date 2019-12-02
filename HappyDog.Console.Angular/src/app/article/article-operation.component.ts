export abstract class ArticleOperationComponent {

    public abstract ngOnInit(): void;

    public tagNames: string;

    protected getTagNames(): string[] {
        const result = [];
        if (this.tagNames) {
            var tags = this.tagNames.split(',');
            for (var i = 0; i < tags.length; i++) {
                var name = tags[i].trim();
                if (name && result.indexOf(name) === -1) {
                    result.push(name);
                }
            }
        }
        return result;
    }
}
