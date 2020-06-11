import { Component, EventEmitter, Output, ViewChild, Inject } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-details-dialog',
  templateUrl: './details-dialog.component.html'
})

//Rename to QuikDialog
export class DetailsDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    console.log(data);//Columns-Entity-Title
  }


  hide(): void {
    this.dialogRef.close();
  }

}
