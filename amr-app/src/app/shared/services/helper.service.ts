import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActionNameEnum } from 'src/app/shared/enums/Action.enum';


@Injectable()
export class HelperService {

    constructor(private _datePipe: DatePipe) {

    }
    transformDate(date) {
        return this._datePipe.transform(date, "yyyy-MM-dd"); //whatever format you need. 
    }


    public addAction(actionName: ActionNameEnum, icon: string) {
        let action = {
            name: actionName,
            icon: icon
        };
        return action;
    }

}
