import { Component } from '@angular/core';
declare var $: any;
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { Router } from '@angular/router';
import { BsLocaleService } from 'ngx-bootstrap';
import { MatDialog } from '@angular/material';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {

  constructor(private translate: TranslateService,
    private authService: AuthService,
    private router: Router,
    private localeService: BsLocaleService,
    private dialog: MatDialog) {
    translate.setDefaultLang('en');
    this.localeService.use('en');
  }



  public switchLanguage(language: string) {
    this.translate.use(language);
    this.localeService.use(language);

  }

  public logOut() {
    this.authService.logOut();
    this.router.navigate(["/"]);
  }
}
