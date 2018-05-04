import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
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


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    DetailComponent,
    NetComponent,
    DbComponent,
    WindowsComponent,
    ReadComponent,
    EssaysComponent
  ],
  imports: [
    BrowserModule,
    AppTroutingModule,
    HttpClientModule,
    MarkdownModule
  ],
  providers: [CategoryService, ArticleService],
  bootstrap: [AppComponent]
})
export class AppModule { }
