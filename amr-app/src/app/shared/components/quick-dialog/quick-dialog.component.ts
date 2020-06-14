import { Component, EventEmitter, Output, ViewChild, Inject, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DeviceVendorService } from 'src/app/modules/meter/services/device-vendor.service';
import { QuickDialogEntitiesEnum } from 'src/app/shared/enums/quick-dialog-entities.enum';
import { QuickDialogService } from 'src/app/shared/services/quick-dialog.service';

@Component({
  selector: 'app-quick-dialog',
  templateUrl: './quick-dialog.component.html'
})

export class QuickDialogComponent implements OnInit {
  quickData: any;
  constructor(
    public dialogRef: MatDialogRef<QuickDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public quickDialogService: QuickDialogService) { }

  ngOnInit() {
    this.quickDialogService.prepareQuickDialog(this.data.entityName, this.data.id);
  }

  hide(): void {
    this.dialogRef.close();
  }


}
