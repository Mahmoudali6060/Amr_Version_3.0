import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { environment } from 'src/environments/environment';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';

@Injectable()

export class ReportService {
  constructor(private httpHelperService: HttpHelperService) {

  }

  createPDF(html: string): any {
    let template = {
      Html: html
    };
    return this.httpHelperService.http.post(`${this.httpHelperService.baseUrl}Api/Report/CreatePDF`, template);
  }

}