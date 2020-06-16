import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullLayoutComponent } from './full-layout/full-layout.component';
import { AuthGuardService as AuthGuard } from '../modules/authentication/services/auth-guard.service';
export const routes: Routes = [
  {
    path: '',
    component: FullLayoutComponent,
    children: [
      { path: 'dashboard', canActivate: [AuthGuard] , loadChildren: () => import('../modules/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'meter',canActivate: [AuthGuard], loadChildren: () => import('../modules/meter/meter.module').then(m => m.MeterModule) },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
