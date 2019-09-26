import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './post/post.component';
import { HiddenComponent } from './hidden/hidden.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
    { path: "", component: SearchComponent },
    { path: "search", component: SearchComponent },
    { path: "post", component: PostComponent },
    { path: "hidden", component: HiddenComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
