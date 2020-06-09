import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from '../shared/shared.module';
import { HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { LayoutRoutingModule } from './layout-routing.module';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { FullLayoutComponent } from './full-layout/full-layout.component';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SideMenuComponent,
    FullLayoutComponent
  ],
  imports: [
    LayoutRoutingModule,
    SharedModule.forRoot(),
  ],
  providers: [
    AuthService
  ]
})
export class LayoutModule { }
