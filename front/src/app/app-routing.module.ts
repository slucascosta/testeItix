import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { CriarjogoComponent } from './pages/criarjogo/criarjogo.component';
import { SortearComponent } from './pages/sortear/sortear.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '#/criarjogo', component: CriarjogoComponent },
  { path: '#/sortear', component: SortearComponent }
];

@NgModule({
  exports: [ RouterModule ],
  imports: [ RouterModule.forRoot(routes) ]
})
export class AppRoutingModule { }