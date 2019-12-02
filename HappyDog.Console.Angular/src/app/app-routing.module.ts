import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './article/post/post.component';
import { HiddenComponent } from './article/hidden/hidden.component';
import { EditComponent } from './article/edit/edit.component';
import { SearchComponent } from './article/search/search.component';
import { AuthGuard } from './auth.guard';
import { SigninComponent } from './signin/signin.component';
import { TagListComponent } from './tag/tag-list/tag-list.component';
import { TagEditComponent } from './tag/tag-edit/tag-edit.component';

const routes: Routes = [
    {
        path: "",
        canActivate: [AuthGuard],
        children: [
            { path: "", component: SearchComponent },
            { path: "search", component: SearchComponent },
            { path: "post", component: PostComponent },
            { path: "hidden", component: HiddenComponent },
            { path: "edit/:id", component: EditComponent },
            { path: "tags", component: TagListComponent },
            { path: "tags/:name", component: TagEditComponent }
        ]
    },
    {
        path: "signin",
        component: SigninComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
