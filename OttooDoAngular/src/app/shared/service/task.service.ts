import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { PropertiesService } from '../../core/services/properties.service';
import { TaskElementModel } from '../entity/task-element.model';

@Injectable()
export class TaskService {
  apiUrl: string = this.propertiesService.apiUrl + 'api/task';

  constructor(private propertiesService: PropertiesService,
    private http: HttpClient) {

  }

  list(): Observable<Array<TaskElementModel>> {
    return this.http.get<Array<TaskElementModel>>(this.apiUrl);
  }

  one(id: string): Observable<TaskElementModel> {
    return this.http.get<TaskElementModel>(this.apiUrl + '/' + id);
  }

  create(task: TaskElementModel): Observable<TaskElementModel> {
    return this.http.post<TaskElementModel>(this.apiUrl, task);
  }

  update(task: TaskElementModel): Observable<TaskElementModel> {
    return this.http.put<TaskElementModel>(this.apiUrl, task);
  }

  delete(id: string): Observable<TaskElementModel> {
    return this.http.delete<TaskElementModel>(this.apiUrl + '/' + id);
  }

  favorite(id: string): Observable<TaskElementModel> {
    return this.http.put<TaskElementModel>(this.apiUrl + '/favorite/' + id, {});
  }
}
