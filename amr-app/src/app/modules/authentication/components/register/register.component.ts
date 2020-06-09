import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  constructor(private router: Router, private authService: AuthService) {

  }

  register(form: NgForm) {
    let userData = JSON.stringify(form.value);
    this.authService.register(userData).subscribe(response => {
      let token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.router.navigate(["/layout/dashboard"]);
    }, err => {
    });
  }
}
