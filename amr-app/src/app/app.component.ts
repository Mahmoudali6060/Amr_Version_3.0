import { Component, ViewEncapsulation } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { SignalRService } from 'src/app/shared/services/signal-r.service';
import { HttpClient } from '@angular/common/http';
import { NetworkService } from 'src/app/modules/network/services/network.service';
import { environment } from 'src/environments/environment';
import { DeviceInfoService } from 'src/app/shared/services/device-info.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {

  hostIp: string;
  ipAddress: string;
  constructor(private deviceInfoService: DeviceInfoService, private jwtHelper: JwtHelperService, private router: Router, private networkService: NetworkService) {
    this.hostIp = environment.hostIp;
  }

  ngOnInit() {
    this.getIP();
  }

  private getDevices(ipAddress) {
    this.networkService.getDevices(ipAddress).subscribe(response => {
      if (response) {
        this.router.navigate(["/layout/dashboard"]);
      }
    }, err => {

    });

  }

  getIP() {
    this.deviceInfoService.getIPAddress().subscribe((res: any) => {
      this.ipAddress = res.ip;
      this.getDevices(this.ipAddress);
    });
  }

  isUserAuthenticated() {
    let token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
  }
}

