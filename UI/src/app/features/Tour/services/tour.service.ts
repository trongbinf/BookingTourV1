import { CreateTour } from './../models/create-tour.model';
import { BASE_URL } from './../../../app.config';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tour } from '../models/tour.model';
import { TourVm } from '../models/tourVm.model';
import { PaginatedResponse } from '../models/paginated.model';
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

  getTourByCategory(name?: string, pageSize = 6, pageIndex = 1): Observable<PaginatedResponse<Tour>> {
    const params = new URLSearchParams({
      pageSize: pageSize.toString(),
      pageIndex: pageIndex.toString()
    });

    const url = `${this.apiUrl}/categories/${name}?${params.toString()}`
    return this.http.get<PaginatedResponse<Tour>>(url);
  }

  getTourVmById(id: number): Observable<TourVm> {
    return this.http.get<TourVm>(`${this.apiUrl}/${id}`);
  }
  createTour(tour: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, tour);
  }
  updateTour(tourId: number, tour: FormData) :Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${tourId}`, tour);
  }
  deleteTour(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  getCategories(): Observable<any[]> {
    return this.http.get<Category[]>(`${BASE_URL}/Category/all`);
  }

  getAllCountry(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/get-country`);
  }

  getCityByCountry(name: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/get-city/${name}`,);
  }

  filterTour(city?: string, country?: string, category?: string,
    minPrice?: string, maxPrice?: string, duration: string[] = [], activities: string[] = [],
    pageIndex = 1, pageSize = 8
  ): Observable<PaginatedResponse<Tour>> {
    const params = new URLSearchParams();
    if (city) params.append('city', city);
    if (country) params.append('country', country);
    if (category) params.append('category', category);
    if (minPrice) params.append('minPrice', minPrice);
    if (maxPrice) params.append('maxPrice', maxPrice);
    if (duration.length) {
      params.append('durations', duration.join(','));
      console.log(params.toString());
    }
    if (activities.length) {
      params.append('activity', activities.join(','));
      console.log(params.toString());
    }
    params.append('pageIndex', pageIndex.toString());
    params.append('pageSize', pageSize.toString());
    const url = `${this.apiUrl}/filter?${params.toString()}`;
    console.log(params.toString());
    return this.http.get<PaginatedResponse<Tour>>(url);
  }

  searchTour(
    name: string | null = null,
    dateRange: string | null = null,
    pageIndex: number = 1,
    pageSize: number = 8
  ): Observable<PaginatedResponse<Tour>> {
    let params = new HttpParams();

    if (name) {
      params = params.append('name', name);
    }
    if (dateRange) {
      params = params.append('dateRange', dateRange);
    }
    params = params.append('pageIndex', pageIndex.toString());
    params = params.append('pageSize', pageSize.toString());

    console.log(params.toString());
    return this.http.get<PaginatedResponse<Tour>>(`${this.apiUrl}/search`, {
      params,
    });
  }

  tourRandom(): Observable<Tour[]> {
    return this.http.get<Tour[]>(`${this.apiUrl}/random`);
  }

}
