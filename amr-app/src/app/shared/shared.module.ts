import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule, registerLocaleData, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { defineLocale, arLocale, BsLocaleService, BsDatepickerModule, TooltipModule, BsModalService, ModalModule } from 'ngx-bootstrap';
import arSaLocale from '@angular/common/locales/ar-SA';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { NgSelectModule } from '@ng-select/ng-select';
import { OrderModule } from 'ngx-order-pipe';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MaterialModule } from '../shared/modules/material.module';
import { PaginationComponent } from '../shared/components/pagination/pagination.component';
import { DataListComponent } from '../shared/components/data-list/data-list.component';
import { ConfirmationDialogComponent } from '../shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { AuthGuardService } from './guards/auth-guard.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HelperService } from './services/helper.service';
import { ExcelService } from 'src/app/shared/services/excel.service';
import { QuickDialogComponent } from 'src/app/shared/components/quick-dialog/quick-dialog.component';
import { QuickDialogService } from 'src/app/shared/services/quick-dialog.service';
import { DeviceVendorService } from 'src/app/modules/meter/services/device-vendor.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@NgModule({

  imports: [
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    SweetAlert2Module.forRoot(),
    BsDatepickerModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    ScrollToModule.forRoot(),
    TranslateModule,
    HttpClientModule,
    NgSelectModule,
    OrderModule,
    NgbModule,
    MaterialModule,
    MatDialogModule
  ],

  exports: [
    CommonModule,
    ToastrModule,
    FormsModule,
    TranslateModule,
    HttpClientModule,
    SweetAlert2Module,
    TooltipModule,
    ModalModule,
    ScrollToModule,
    NgSelectModule,
    BsDatepickerModule,
    OrderModule,
    NgbModule,
    PaginationComponent,
    DataListComponent,
    MaterialModule,
    QuickDialogComponent,
    MatDialogModule
    // ModalBasicComponent
  ],
  declarations: [
    ConfirmationDialogComponent,
    PaginationComponent,
    DataListComponent,
    QuickDialogComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent,
    DataListComponent,
    QuickDialogComponent
  ],
  providers: [
    BsModalService,
    DatePipe,
    AuthGuardService,
    HelperService,
    ExcelService,
    DeviceVendorService,
    QuickDialogService,
    NgbActiveModal, 
    { provide: MatDialogRef, useValue: {} },
    { provide: MAT_DIALOG_DATA, useValue: {} },
  ],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule
    };
  }
}
registerLocaleData(arSaLocale);
defineLocale('ar', arLocale);
