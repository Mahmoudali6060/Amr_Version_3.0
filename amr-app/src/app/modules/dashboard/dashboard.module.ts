import { NgModule } from '@angular/core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './components/dashboard.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import { UserModule } from 'src/app/modules/user/user.module';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule.forRoot(),
    UserModule
  ],
  declarations: [
    DashboardComponent
  ],
  providers: [
    AuthGuardService
  ]
})

export class DashboardModule {
}
