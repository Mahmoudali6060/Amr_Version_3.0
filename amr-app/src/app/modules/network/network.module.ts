import { NgModule } from '@angular/core';
import { NetworkRoutingModule } from './network-routing.module';
import { NetworkComponent } from './components/network.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import { NetworkService } from 'src/app/modules/network/services/network.service';

@NgModule({
  imports: [
    NetworkRoutingModule,
    SharedModule.forRoot()
  ],
  declarations: [
    NetworkComponent
  ],
  providers: [
    AuthGuardService,
    NetworkService
  ]
})

export class NetworkModule {
}
