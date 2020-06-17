import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MeterModel } from '../models/meter.model';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';

@Injectable()
export class MeterService {
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getAllMetersAsync(dataSource: DataSourceModel): Observable<any> {
    return this.http.get<any>(`${this.httpHelperService.baseUrl}api/Meter/GetAllMetersAsync?pageSize=${dataSource.PageSize}&&currentPage=${dataSource.CurrentPage}&&keyword=${dataSource.Keyword}`);
  }

}