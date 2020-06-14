import { Injectable } from '@angular/core';
import { DeviceVendorService } from 'src/app/modules/meter/services/device-vendor.service';
import { QuickDialogEntitiesEnum } from 'src/app/shared/enums/quick-dialog-entities.enum';

@Injectable()
export class QuickDialogService {
    public quickData: any;
    constructor(private deviceVendorService: DeviceVendorService) {
    }

    public prepareQuickDialog(entityName: QuickDialogEntitiesEnum, id: number) {
        switch (entityName) {
            case QuickDialogEntitiesEnum.DeviceVendor:
                this.getMeterVendorDetailsById(id);
                break;
            default:
                break;
        }
    }

    private getMeterVendorDetailsById(id) {
        let columns = ["vendorName", "comments"];//Displaying Columns
        let title = "Device Vendor";//Title of Modal
        this.deviceVendorService.getDeviceVendorDetailsByIdAsync(id).subscribe((res) => {
            this.quickData = {
                entity: res,
                columns: columns,
                title: title
            };
        });

    }
}
