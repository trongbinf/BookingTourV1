import { Injectable } from '@angular/core';
import { BASE_URL } from '../../../app.config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DateStart } from '../models/date-start.model';

@Injectable({
  providedIn: 'root'
})
export class DateStartService {

  private apiUrl = `${BASE_URL}/DateStart`;
  constructor(private http: HttpClient) { }

  addDateStart(id: number, model: DateStart[]): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add?idTour=${id}`, model)
  }
  updateDateStart(id: number, model: DateStart): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update?idDate=${id}`, model);
  }

  deleteDateStart(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete?id=${id}`);
  }
}
