import { Component, OnInit } from '@angular/core';

import { TaskElementModel } from '../shared/entity/task-element.model';
import { TaskService } from '../shared/service/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  constructor(private taskService: TaskService) { }

  ngOnInit() {
    this.taskService.list().subscribe((tasks: Array<TaskElementModel>) => {
      console.log(tasks);
    });
  }

}
