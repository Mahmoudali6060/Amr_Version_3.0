import { Component, OnInit, Inject, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { DataSourceModel } from '../../../shared/models/data-source.model';
import { MatDialog } from '@angular/material';
import { ActionModel } from '../../../shared/models/action.model';
import { ActionNameEnum } from '../../../shared/enums/Action.enum';

@Component({
  selector: 'app-data-list',
  templateUrl: './data-list.component.html',
  styleUrls: ['./data-list.component.css']
})

export class DataListComponent {
  //#region  Description:
  /*
 [M.Salman]
  this is a generic component for listing data
  */
  //#endregion

  //#region [M.Salman] -[30/1/2020] - [Variables]
  @Input() list;//List(table content)
  @Input() properties;//Table Header
  @Output() deleteRow = new EventEmitter<number>();//When you delete row this will send selected (Id) for parent component
  @Output() editRow = new EventEmitter<number>();//When you edit row this will send selected (Id) for parent component
  @Output() changePagination = new EventEmitter<DataSourceModel>();//When you edit row this will send selected (Id) for parent component
  @ViewChild(ConfirmationDialogComponent, { static: false }) confirmationDialogComponent;//This open dialog for confirmation delete
  @Input() total: number;
  dataSourceModel = new DataSourceModel();
  model: any = {};
  @Input() actions: Array<ActionModel>;//Custom Actions for tables component
  @Output() makeAction = new EventEmitter<any>();//Custom Action (Details)
  @Input() isReadonly: boolean = false;
  //#endregion

  constructor(public dialog: MatDialog) {

  }

  ngOnInit() {
  }

  //#region  Editing
  public edit(id) {
    this.editRow.emit(id);//Send selected id to parent after editing the entity
  }
  //#endregion

  //#region  Delation 
  showConfirmDialog(id): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Do you confirm the deletion of this data?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(id);
      }
    });

  }

  public delete(id) {
    this.deleteRow.emit(id);//Send selected id to parent after confirmation delete
  }
  //#endregion

  //#region Pagination
  onChangePagination(data) {
    this.dataSourceModel.PageSize = data.recordPerPage;
    this.dataSourceModel.PageNumber = data.currentPage;
    this.changePagination.emit(this.dataSourceModel);
  }
  //#endregion

  //#region Fileration
  public applyFilter() {
    this.dataSourceModel.Filter = [];
    for (let property of this.properties) {
      if (this.model[property]) {
        var filter = {
          Key: this.titleCaseWord(property),
          value: this.model[property]
        };
        this.dataSourceModel.Filter.push(filter);
      }

    }
    this.changePagination.emit(this.dataSourceModel);
  }

  public titleCaseWord(word: string) {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1);
  }
  //#endregion 

  public onActionClicked(action, id) {
    let event = {
      id: id,
      action: action
    };
    this.makeAction.emit(event);
  }
}


