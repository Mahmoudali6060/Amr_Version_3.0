import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DeviceInfoService } from 'src/app/shared/services/device-info.service';
declare var jQuery: any;

@Component({
	selector: 'app-network',
	templateUrl: './network.component.html'
})
export class NetworkComponent {

	public ipAddress:string;
	constructor(private deviceInfoService: DeviceInfoService) {

	}

	network(model: any) {

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
