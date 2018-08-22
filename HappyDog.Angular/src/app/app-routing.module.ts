import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent } from './components/article/index/index.component';
import { DetailComponent } from './components/article/detail/detail.component';
import { NetComponent } from './components/article/net/net.component';
import { DbComponent } from './components/article/db/db.component';
import { WindowsComponent } from './components/article/windows/windows.component';
import { ReadComponent } from './components/article/read/read.component';
import { EssaysComponent } from './components/article/essays/essays.component';
import { SignInComponent } from './components/user/sign-in/sign-in.component';
import { ArticlePostComponent } from './components/article/post/post.component';
import { ArticleEditComponent } from './components/article/edit/edit.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component'

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'detail/:id', component: DetailComponent },
  { path: '.net', component: NetComponent },
  { path: 'db', component: DbComponent },
  { path: 'windows', component: WindowsComponent },
  { path: 'read', component: ReadComponent },
  { path: 'essays', component: EssaysComponent },
  { path: 'signIn', component: SignInComponent },
  { path: 'post', component: ArticlePostComponent },
  { path: 'edit/:id', component: ArticleEditComponent },
  { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppTroutingModule { }
