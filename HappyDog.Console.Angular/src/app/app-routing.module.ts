import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './post/post.component';
import { HiddenComponent } from './hidden/hidden.component';
import { SearchComponent } from './search/search.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
    { path: "", component: SearchComponent },
    { path: "search", component: SearchComponent },
    { path: "post", component: PostComponent },
    { path: "hidden", component: HiddenComponent },
    { path: "edit/:id", component: EditComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
