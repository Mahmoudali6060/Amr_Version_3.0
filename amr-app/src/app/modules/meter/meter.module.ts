import { NgModule } from '@angular/core';
import { MeterRoutingModule } from './meter-routing.module';
import { MeterListComponent } from './components/meter-list/meter-list.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MeterService } from './services/meter.service';
import { ReportModule } from 'src/app/modules/report/report.module';
import { MeterVendorService } from 'src/app/modules/meter/services/meter-vendor.service';

@NgModule({
  imports: [
    MeterRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule
  ],
  exports: [
    MeterListComponent
  ],
  declarations: [
    MeterListComponent
  ],
  providers: [
    AuthGuardService,
    MeterService,
    MeterVendorService
  ]
})

export class MeterModule {
}
