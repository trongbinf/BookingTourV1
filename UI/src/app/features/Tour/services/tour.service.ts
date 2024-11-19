import { CreateTour } from './../models/create-tour.model';
import { BASE_URL } from './../../../app.config';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tour } from '../models/tour.model';
import { TourVm } from '../models/tourVm.model';
import { Category } from '../../category/model/category.model';

@Injectable({
  providedIn: 'root'
})
export class TourService {
  private apiUrl = `${BASE_URL}/Tour`;
  constructor(private http: HttpClient) { }

  getAllTours(): Observable<Tour[]> {
    return this.http.get<Tour[]>(this.apiUrl);
  }

  getTourByCategory(name?: string): Observable<Tour[]> {
    return this.http.get<Tour[]>(`${BASE_URL}/Tour/categories/${name}`);
  }

  getTourVmById(id: number): Observable<TourVm> {
    return this.http.get<TourVm>(`${this.apiUrl}/${id}`);
  }
  createTour(tour: CreateTour): Observable<TourVm> {
    return this.http.post<TourVm>(`${this.apiUrl}`, tour);
  }
  getCategories(): Observable<any[]> {
    return this.http.get<Category[]>(`${BASE_URL}/Category/all`);
  }

}
