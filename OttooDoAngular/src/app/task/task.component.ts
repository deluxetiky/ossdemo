import { Component, OnInit } from '@angular/core';

import { SignalRService } from '../core/services/signal-r.service';
import { TaskElementModel } from '../shared/entity/task-element.model';
import { TaskService } from '../shared/service/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  constructor(private taskService: TaskService,
    private signalRService: SignalRService) {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.subscribeFunction(this.dataArrived.bind(this));
    });
  }

  dataArrived(processName: string, task: TaskElementModel) {
    console.log(processName, task);
  }

  ngOnInit() {
    this.taskService.list().subscribe((tasks: Array<TaskElementModel>) => {
      console.log(tasks);
    });
  }

}
