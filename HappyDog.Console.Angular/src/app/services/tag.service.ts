import { Injectable } from '@angular/core';
import { Tag } from '../models/tag/tag';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutTagDto } from '../models/tag/put-tag-dto';

@Injectable({
    providedIn: 'root'
})
export class TagService {

    constructor(
        private http: HttpClient
    ) { }

    getTags(): Observable<Tag[]> {
        return this.http.get<Tag[]>(`/api/tag`);
    }

    getTag(id: number): Observable<Tag> {
        return this.http.get<Tag>(`/api/tag/${id}`);
    }

    put(id: number, tag: PutTagDto): Observable<Tag> {
        return this.http.put<Tag>(`/api/tag/${id}`, tag);
    }
}
