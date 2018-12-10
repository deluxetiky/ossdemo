import { Component, OnInit } from '@angular/core';

import { SignalRService } from '../core/services/signal-r.service';
import { SocketConstants } from '../shared/constants/socket.constants';
import { TaskElementModel } from '../shared/entity/task-element.model';
import { TaskService } from '../shared/service/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {
  tasks: Array<TaskElementModel> = new Array<TaskElementModel>();
  task: TaskElementModel = new TaskElementModel();

  constructor(private taskService: TaskService,
    private signalRService: SignalRService) {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.subscribeFunction(this.dataArrived.bind(this));
    });
  }

  dataArrived(processName: string, task: TaskElementModel) {
    switch (processName) {
      case SocketConstants.TaskAdd:
        this.socketAdd(task);
        break;
      case SocketConstants.TaskDelete:
        this.socketDelete(task);
        break;
      case SocketConstants.TaskFavorite:
        this.socketFavorite(task);
        break;
    }
  }

  socketAdd(task: TaskElementModel) {
    if (!task || !task.id) {
      return;
    }
    if (!this.tasks.some((tempTask: TaskElementModel) => tempTask.id === task.id)) {
      this.tasks.unshift(task);
      this.sort();
    }
  }

  socketDelete(task: TaskElementModel) {
    if (!task || !task.id) {
      return;
    }
    const position = this.tasks.findIndex((tempTask: TaskElementModel) => tempTask.id === task.id);
    if (position > -1) {
      this.tasks.splice(position, 1);
    }
  }

  socketFavorite(task: TaskElementModel) {
    if (!task || !task.id) {
      return;
    }
    const tempTasks = this.tasks.find((value: TaskElementModel) => value.id === task.id);
    if (tempTasks) {
      if (task.lastUpdatedTime > tempTasks.lastUpdatedTime) {
        tempTasks.lastUpdatedTime = task.lastUpdatedTime;
        tempTasks.favoriteCount = task.favoriteCount;
        this.sort();
      }
    }
  }

  ngOnInit() {
    this.taskService.list().subscribe((tasks: Array<TaskElementModel>) => {
      this.tasks = tasks;
      this.sort();
    });
  }

  create() {
    this.taskService.create(this.task).subscribe((newTask: TaskElementModel) => {
      this.socketAdd(newTask);
    });
    this.task = new TaskElementModel();
  }

  remove(task: TaskElementModel) {
    this.taskService.delete(task.id).subscribe((deletedTask: TaskElementModel) => {
      this.socketDelete(deletedTask);
    });
  }

  favorite(task: TaskElementModel) {
    if (task.isUpdated) {
      return;
    }
    task.isUpdated = true;
    this.taskService.favorite(task.id).subscribe((updatedTask: TaskElementModel) => {
      this.socketFavorite(updatedTask);
    });
  }

  sort() {
    this.tasks = this.tasks.sort((a: TaskElementModel, b: TaskElementModel) => b.favoriteCount - a.favoriteCount);
  }

}
