import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import { SigninComponent } from './signin/signin.component';
import { ArticleListComponent } from './components/article-list/article-list.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TagListComponent } from './tag/tag-list/tag-list.component';
import { PostComponent } from './article/post/post.component';
import { HiddenComponent } from './article/hidden/hidden.component';
import { EditComponent } from './article/edit/edit.component';
import { SearchComponent } from './article/search/search.component';
import { TagEditComponent } from './tag/tag-edit/tag-edit.component';
import { TagBadgeComponent } from './components/tag-badge/tag-badge.component';
import { CheckTagsComponent } from './components/check-tags/check-tags.component';
import { TagPostComponent } from './tag/tag-post/tag-post.component';

@NgModule({
    declarations: [
        AppComponent,
        PostComponent,
        HiddenComponent,
        EditComponent,
        SigninComponent,
        SearchComponent,
        ArticleListComponent,
        PaginationComponent,
        TagListComponent,
        TagEditComponent,
        TagBadgeComponent,
        CheckTagsComponent,
        TagPostComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
    ],
    providers: [
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
