import { Observable } from 'rxjs';
import { TaskElementModel } from 'src/app/shared/entity/task-element.model';

import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

import { PropertiesService } from './properties.service';

@Injectable()
export class SignalRService {
  hubConnection: HubConnection;

  constructor(private propertiesService: PropertiesService) {

  }

  startConnection(): Observable<any> {
    return Observable.create(observer => {

      console.log(this.propertiesService.socketApiUrl + 'socket/task');
      this.hubConnection = new HubConnectionBuilder().withUrl(
        this.propertiesService.socketApiUrl + 'socket/task')
        .configureLogging(this.propertiesService.production ? LogLevel.Error : LogLevel.Trace)
        .build();

      this.hubConnection.start()
        .then(() => {
          observer.next();
          observer.complete();
        })
        .catch(error => {
          observer.error(error);
          observer.complete();
        });
    });
  }

  subscribeFunction(dataArrived: (processName: string, task: TaskElementModel) => {}) {
    this.hubConnection.on('TaskProcess', (processName: string, task: TaskElementModel) => {
      dataArrived(processName, task);
    });
  }
}
