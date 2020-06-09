import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthService } from './services/auth.service';
import { RegisterComponent } from 'src/app/modules/authentication/components/register/register.component';

@NgModule({
  imports: [
    AuthRoutingModule,
    SharedModule.forRoot()
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    AuthService
  ]
})

export class AuthModule {
}
