
import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { ChartModel } from 'src/app/shared/models/chart.model';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    public data: Array<ChartModel> = [];


    private hubConnection: signalR.HubConnection
    public bradcastedData: ChartModel[];

    constructor(private httpHelperService: HttpHelperService) {

    }
    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.httpHelperService.baseUrl}chart`)
            .build();
        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addTransferChartDataListener = () => {
        this.hubConnection.on('transferchartdata', (chart) => {
            this.data.shift();
            this.data.push(chart);

        });
    }

    public broadcastChartData = () => {
        // const data = this.data.map(m => {
        //     const temp = {
        //         data: m.data,
        //         label: m.label
        //     }
        //     return temp;
        // });
        // this.hubConnection.invoke('broadcastchartdata', data)
        //     .catch(err => console.error(err));
    }

    public addBroadcastChartDataListener = () => {
        this.hubConnection.on('broadcastchartdata', (data) => {
            this.bradcastedData = data;
        })
    }
}