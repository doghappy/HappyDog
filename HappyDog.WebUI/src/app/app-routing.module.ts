import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent } from './components/index/index.component';
import { DetailComponent } from './components/detail/detail.component';
import { NetComponent } from './components/nav/net/net.component';
import { DbComponent } from './components/nav/db/db.component';
import { WindowsComponent } from './components/nav/windows/windows.component';
import { ReadComponent } from './components/nav/read/read.component';
import { EssaysComponent } from './components/nav/essays/essays.component';
import { UserLoginComponent } from './components/user/user-login/user-login.component';
import { ArticlePostComponent } from './components/article/article-post/article-post.component';
import { ArticleEditComponent } from './components/article/article-edit/article-edit.component';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'detail/:id', component: DetailComponent },
  { path: '.net', component: NetComponent },
  { path: 'db', component: DbComponent },
  { path: 'windows', component: WindowsComponent },
  { path: 'read', component: ReadComponent },
  { path: 'essays', component: EssaysComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'post', component: ArticlePostComponent },
  { path: 'edit/:id', component: ArticleEditComponent },
]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppTroutingModule { }
