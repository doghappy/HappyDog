import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import { PostComponent } from './post/post.component';
import { HiddenComponent } from './hidden/hidden.component';
import { EditComponent } from './edit/edit.component';
import { SigninComponent } from './signin/signin.component';
import { SearchComponent } from './search/search.component';

@NgModule({
    declarations: [
        AppComponent,
        PostComponent,
        HiddenComponent,
        EditComponent,
        SigninComponent,
        SearchComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
