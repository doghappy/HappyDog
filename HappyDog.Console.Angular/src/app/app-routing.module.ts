import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './post/post.component';
import { HiddenComponent } from './hidden/hidden.component';
import { SearchComponent } from './search/search.component';
import { EditComponent } from './edit/edit.component';
import { AuthGuard } from './auth.guard';
import { SigninComponent } from './signin/signin.component';

const routes: Routes = [
    {
        path: "",
        canActivateChild: [AuthGuard],
        children: [
            { path: "", component: SearchComponent },
            { path: "search", component: SearchComponent },
            { path: "post", component: PostComponent },
            { path: "hidden", component: HiddenComponent },
            { path: "edit/:id", component: EditComponent }
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
