import { Injectable } from '@angular/core';
import { BASE_URL } from '../../../app.config';
import { ActivityAdd } from '../models/create-activity.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  private apiUrl = `${BASE_URL}/ActivityTour`;
  constructor(private http: HttpClient) { }

  updateActivity(id: number, model: ActivityAdd): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update/${id}`, model);
  }

  addActivity(model: ActivityAdd): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, model);
  }

  deleteActivity(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete?id=${id}`);
  }
}
