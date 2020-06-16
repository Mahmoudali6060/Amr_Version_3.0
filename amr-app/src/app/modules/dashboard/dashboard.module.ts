import { NgModule } from '@angular/core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './components/dashboard.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule.forRoot(),
  ],
  declarations: [
    DashboardComponent
  ],
  providers: [
  ]
})

export class DashboardModule {
}
