import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent } from './components/index/index.component';
import { DetailComponent } from './components/detail/detail.component';
import { NetComponent } from './components/net/net.component';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'detail/:id', component: DetailComponent },
  { path: 'net', component: NetComponent }
]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppTroutingModule { }
