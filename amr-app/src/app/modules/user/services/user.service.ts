import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../models/user.model';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';

@Injectable()
export class UserService {
  constructor(private http: HttpClient, private httpHelperService: HttpHelperService) { }

  getUserDetails(dataSource: DataSourceModel): Observable<UserModel[]> {
    return this.http.post<UserModel[]>(`${this.httpHelperService.baseUrl}api/User/GetAllUserDetails`, dataSource);
  }

}