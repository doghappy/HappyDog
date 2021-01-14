import { Injectable } from '@angular/core';
import { Tag } from '../models/tag/tag';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutTagDto } from '../models/tag/put-tag-dto';
import { PostTagDto } from '../models/tag/post-tag-dto';

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

    post(tag: PostTagDto): Observable<Tag> {
        // const httpOptions = {
        //     headers: new HttpHeaders({ 'Content-Type': 'application/json' })
        // }
        console.log(tag);
        return this.http.post<Tag>(`/api/tag`, tag);
    }
}
