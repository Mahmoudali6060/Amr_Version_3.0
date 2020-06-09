import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { LocalStorage } from '@ngx-pwa/local-storage';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';

@Injectable()
export class NetworkService {
  constructor(private httpHelperService: HttpHelperService) {

  }

  public getDevices(hostIp:string): any {
    return this.httpHelperService.http.get(`${this.httpHelperService.baseUrl}Api/Network/GetDevices?hostIp=${hostIp}`);
  }
}