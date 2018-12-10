import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';

const routes: Routes = [
  {
    path: '', component: AppComponent, children: [
      { path: '', component: TaskComponent }
    ],
  },
  {
    path: '**', redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
