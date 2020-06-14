import { Component, OnInit, ViewChild } from '@angular/core';
import { MeterService } from '../../services/meter.service';
import { MeterModel } from '../../models/meter.model';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';
import { ExcelService } from 'src/app/shared/services/excel.service';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { QuickDialogComponent } from 'src/app/shared/components/quick-dialog/quick-dialog.component';
import { MatDialog } from '@angular/material';
import { QuickDialogEntitiesEnum } from 'src/app/shared/enums/quick-dialog-entities.enum';
@Component({
	selector: 'app-meter-list',
	templateUrl: './meter-list.component.html',
	styleUrls: ['./meter-list.component.css']
})
export class MeterListComponent implements OnInit {
	allMeter: Array<MeterModel> = new Array<MeterModel>();
	displayedColumns: string[] = ['MeterId', 'MeterName', 'EmailId', 'Gender', 'Address', 'MobileNo', 'PinCode'];
	public dataSource: DataSourceModel = new DataSourceModel();
	// @ViewChild(QuickDialogComponent, { static: false }) quickDialogComponent;//This open dialog for confirmation delete

	constructor(private httpHelperService: HttpHelperService,
		private meterService: MeterService,
		private excelService: ExcelService,
		private reportService: ReportService,
		private dialog: MatDialog) {
		this.dataSource.PageNumber = 1;
		pdfMake.vfs = pdfFonts.pdfMake.vfs;
	}

	ngOnInit() {
		this.getAllMeterDetails();
		// debugger;
		// this.getMeterVendorDetailsById();
	}

	getAllMeterDetails() {
		this.meterService.getAllMetersAsync(this.dataSource).subscribe((res) => {
			this.allMeter = res;
			// this.setNewMeters(res);
			// console.log(res);
		});
	}

	public getMeterVendorDetailsById(id: number) {
		this.dialog.open(QuickDialogComponent, { data: { entityName: QuickDialogEntitiesEnum.DeviceVendor, id: id } })///Make Vendor Enum
	}
	//>>>END Meter Vendor
	private setNewMeters(meterList: any) {
		for (let meter of meterList) {
			this.allMeter.push(meter);
		}
	}

	onScroll() {
		this.dataSource.PageNumber = this.dataSource.PageNumber + 1;
		this.getAllMeterDetails();
	}
	public search() {
		this.getAllMeterDetails();
	}

	public exportAsXLSX(): void {
		this.excelService.exportAsExcelFile(this.allMeter, 'Meters');
	}

	generatePdf() {
		let title = "Meters";
		let meters = document.getElementById("meters").innerHTML;
		let html = '<h1 class="center">Meters</h1>' + meters;
		this.reportService.createPDF(html).subscribe((res) => {
			res = `${this.httpHelperService.baseUrl}${res}`;
			window.open(res, '_blank');
		});
	}
}