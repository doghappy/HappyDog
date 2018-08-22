import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'
import { CookieModule } from 'ngx-cookie';
import { MarkdownModule } from 'angular2-markdown'
import { AppComponent } from './app.component';
import { IndexComponent } from './components/article/index/index.component';
import { AppTroutingModule } from './app-routing.module';
import { DetailComponent } from './components/article/detail/detail.component';
import { ArticleService } from './services/article.service';
import { NetComponent } from './components/article/net/net.component';
import { DbComponent } from './components/article/db/db.component';
import { WindowsComponent } from './components/article/windows/windows.component';
import { ReadComponent } from './components/article/read/read.component';
import { EssaysComponent } from './components/article/essays/essays.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ArticleListComponent } from './components/article/article-list/article-list.component';
import { UserLoginComponent } from './components/user/user-login/user-login.component';
import { ArticleEditComponent } from './components/article/article-edit/article-edit.component';
import { UserService } from './services/user.service';
import { AlertModule } from 'ngx-bootstrap/alert';
import { LMarkdownEditorModule } from 'ngx-markdown-editor';
import { AuthenticationService } from './services/authentication.service';
import { ArticlePostComponent } from './components/article/article-post/article-post.component';
import { LoadingModule } from 'ngx-loading';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component'

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    DetailComponent,
    NetComponent,
    DbComponent,
    WindowsComponent,
    ReadComponent,
    EssaysComponent,
    ArticleListComponent,
    UserLoginComponent,
    ArticleEditComponent,
    ArticlePostComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    AppTroutingModule,
    HttpClientModule,
    MarkdownModule,
    FormsModule,
    CookieModule.forRoot(),
    PaginationModule.forRoot(),
    AlertModule.forRoot(),
    LMarkdownEditorModule,
    LoadingModule
  ],
  providers: [ArticleService, UserService, AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
