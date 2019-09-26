import { TestBed } from '@angular/core/testing';

import { ArticleServiceService } from './article-service.service';

describe('ArticleServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArticleServiceService = TestBed.get(ArticleServiceService);
    expect(service).toBeTruthy();
  });
});
