import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DeviceInfoService } from 'src/app/shared/services/device-info.service';
declare var jQuery: any;

@Component({
	selector: 'app-dashboard',
	templateUrl: './dashboard.component.html'
})
export class DashboardComponent {

	public ipAddress:string;
	constructor(private deviceInfoService: DeviceInfoService) {

	}

	dashboard(model: any) {

	}

	ngOnInit()  
	{  
	  this.getIP();  
	}  
	getIP()  
	{  
	  this.deviceInfoService.getIPAddress().subscribe((res:any)=>{  
		this.ipAddress=res.ip;  
	  });  
	} 

}
