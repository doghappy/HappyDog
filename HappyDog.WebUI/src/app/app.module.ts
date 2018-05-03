import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { IndexComponent } from './components/index/index.component';
import { AppTroutingModule } from './/app-trouting.module';
import { DetailComponent } from './components/detail/detail.component';
import { CategoryService } from './services/category.service';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    DetailComponent
  ],
  imports: [
    BrowserModule,
    AppTroutingModule,
    HttpClientModule
  ],
  providers: [CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
