import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NetworkComponent } from './components/network.component';
import { AuthGuardService } from '../../shared/guards/auth-guard.service';

const routes: Routes = [
  { path: '', component: NetworkComponent },
  { path: 'network', component: NetworkComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NetworkRoutingModule {
}
