import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'
import { CookieModule } from 'ngx-cookie';
import { MarkdownModule } from 'angular2-markdown'
import { AppComponent } from './app.component';
import { IndexComponent } from './components/index/index.component';
import { AppTroutingModule } from './app-routing.module';
import { DetailComponent } from './components/detail/detail.component';
import { CategoryService } from './services/category.service';
import { ArticleService } from './services/article.service';
import { NetComponent } from './components/nav/net/net.component';
import { DbComponent } from './components/nav/db/db.component';
import { WindowsComponent } from './components/nav/windows/windows.component';
import { ReadComponent } from './components/nav/read/read.component';
import { EssaysComponent } from './components/nav/essays/essays.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ArticleListComponent } from './components/article/article-list/article-list.component';
import { UserSignInComponent } from './components/user/user-sign-in/user-sign-in.component';
import { ArticleEditComponent } from './components/article/article-edit/article-edit.component';
import { ArticleEditButtonComponent } from './components/article/article-edit-button/article-edit-button.component';

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
    UserSignInComponent,
    ArticleEditComponent,
    ArticleEditButtonComponent
  ],
  imports: [
    BrowserModule,
    AppTroutingModule,
    HttpClientModule,
    MarkdownModule,
    FormsModule,
    CookieModule.forRoot(),
    PaginationModule.forRoot()
  ],
  providers: [CategoryService, ArticleService],
  bootstrap: [AppComponent]
})
export class AppModule { }
