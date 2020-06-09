import { NgModule } from '@angular/core';
import { UserRoutingModule } from './user-routing.module';
import { UserListComponent } from './components/user-list/user-list.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { UserService } from './services/user.service';
import { ReportModule } from 'src/app/modules/report/report.module';

@NgModule({
  imports: [
    UserRoutingModule,
    SharedModule.forRoot(),
    InfiniteScrollModule,
    ReportModule
  ],
  exports:[
    UserListComponent
  ],
  declarations: [
    UserListComponent
  ],
  providers: [
    AuthGuardService,
    UserService
  ]
})

export class UserModule {
}
