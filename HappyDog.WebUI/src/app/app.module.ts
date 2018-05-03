import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { IndexComponent } from './components/index/index.component';
import { AppTroutingModule } from './app-routing.module';
import { DetailComponent } from './components/detail/detail.component';
import { CategoryService } from './services/category.service';
import { ArticleService } from './services/article.service';
import { NetComponent } from './components/net/net.component';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    DetailComponent,
    NetComponent
  ],
  imports: [
    BrowserModule,
    AppTroutingModule,
    HttpClientModule
  ],
  providers: [CategoryService, ArticleService],
  bootstrap: [AppComponent]
})
export class AppModule { }
