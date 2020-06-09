import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserModel } from '../../models/user.model';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';
import { ExcelService } from 'src/app/shared/services/excel.service';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
@Component({
	selector: 'app-user-list',
	templateUrl: './user-list.component.html',
	styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
	allUser: Array<UserModel> = new Array<UserModel>();
	displayedColumns: string[] = ['UserId', 'UserName', 'EmailId', 'Gender', 'Address', 'MobileNo', 'PinCode'];
	public dataSource: DataSourceModel = new DataSourceModel();

	constructor(private httpHelperService: HttpHelperService, private userService: UserService, private excelService: ExcelService, private reportService: ReportService) {
		this.dataSource.PageNumber = 1;
		pdfMake.vfs = pdfFonts.pdfMake.vfs;
	}

	ngOnInit() {
		this.getAllUserDetails();
	}

	getAllUserDetails() {
		this.userService.getUserDetails(this.dataSource).subscribe((res) => {
			this.setNewUsers(res);
		});
	}

	private setNewUsers(userList: any) {
		for (let user of userList) {
			this.allUser.push(user);
		}
	}
	
	onScroll() {
		this.dataSource.PageNumber = this.dataSource.PageNumber + 1;
		this.getAllUserDetails();
	}
	public search() {
		this.getAllUserDetails();
	}

	public exportAsXLSX(): void {
		this.excelService.exportAsExcelFile(this.allUser, 'Users');
	}

	generatePdf() {
		let title = "Users";
		let users = document.getElementById("users").innerHTML;
		let html = '<h1 class="center">Users</h1>' + users;
		this.reportService.createPDF(html).subscribe((res) => {
			res = `${this.httpHelperService.baseUrl}${res}`;
			window.open(res, '_blank');
		});
	}
}