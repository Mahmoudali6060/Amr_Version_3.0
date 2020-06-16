import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { LocalStorage } from '@ngx-pwa/local-storage';
import { LoginModel } from '../models/login.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({ providedIn: 'root' })
export class AuthService {
  baseUrl: string;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    this.baseUrl = environment.baseServiceUrl;
  }
  

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    // Check whether the token is expired and return
    // true or false
    return !this.jwtHelper.isTokenExpired(token);
  }


  login(model: any) {
    return this.http.post(`${this.baseUrl}Api/Account/Login`, model, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}Api/Account/Register`, model, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  logOut() {
    localStorage.removeItem("jwt");
  }

}