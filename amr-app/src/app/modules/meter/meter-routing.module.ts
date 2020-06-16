import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MeterListComponent } from './components/meter-list/meter-list.component';

const routes: Routes = [
  { path: '', component: MeterListComponent },
  { path: 'meter-list', component: MeterListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MeterRoutingModule {
}
