import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullLayoutComponent } from './full-layout/full-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: FullLayoutComponent,
    children: [
      { path: 'dashboard', loadChildren: () => import('../modules/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'meter', loadChildren: () => import('../modules/meter/meter.module').then(m => m.MeterModule) },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
