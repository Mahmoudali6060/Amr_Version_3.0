import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { environment } from '../../../environments/environment';
import { CRUDOperation } from '../../shared/enums/CRUD-Operation.enum';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ToastrService } from 'ngx-toastr';

@Injectable(
    { providedIn: 'root' }
)
export class BaseEntityService {
    baseUrl: string = "";
    private urlGetAll = "GetAll";
    private urlGetById = "GetById";
    private urlSave = "Save";
    private urlDelete = "Delete";
    private urlGetAllLite = "GetAllLite";
    entityName: string = "";

    constructor(public _http: HttpClient, private toastrService: ToastrService) {
        this.baseUrl = environment.baseServiceUrl;
    }

    getAll(dataSourceModel): any {
        return this._http.post(this.baseUrl + 'api/' + this.entityName + '/' + this.urlGetAll, dataSourceModel);
        // //     .subscribe((response: Response) => response.json())
        // //     , catchError(this.errorHandler);
    }

    getAllLite(): any {
        return this._http.get(this.baseUrl + 'api/' + this.entityName + '/' + this.urlGetAllLite);
        // //     .subscribe((response: Response) => response.json())
        // //     , catchError(this.errorHandler);
    }
    getById(id: number): any {
        return this._http.get(this.baseUrl + "api/" + this.entityName + '/' + this.urlGetById + '/' + id);
        // .subscribe((response: Response) => response.json())
        // , catchError(this.errorHandler);
    }

    save(entity) {
        return this._http.post(`${this.baseUrl}api/${this.entityName}/${this.urlSave}/`, entity);
    }

    delete(id) {
        return this._http.delete(this.baseUrl + "api/" + this.entityName + "/" + this.urlDelete + "/" + id);
        // .subscribe((response: Response) => response.json())
        // , catchError(this.errorHandler);
    }

    deleteFarm(id) {
        this._http.delete(this.baseUrl + "api/" + this.entityName + "/" + this.urlDelete + "/" + id)
            .subscribe((response: Response) => {
                this.toastrService.success("تم الحذف بنجاح", "عملية ناجحة", { positionClass: 'toast-bottom-right' });
                return response;
            })
            , catchError(this.errorHandler);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}
