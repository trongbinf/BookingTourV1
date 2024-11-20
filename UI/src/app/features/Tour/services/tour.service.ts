import { CreateTour } from './../models/create-tour.model';
import { BASE_URL } from './../../../app.config';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tour } from '../models/tour.model';
import { TourVm } from '../models/tourVm.model';
import { PaginatedResponse } from '../models/paginated.model';

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
  createTour(tour: CreateTour): Observable<TourVm> {
    return this.http.post<TourVm>(`${this.apiUrl}`, tour);
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

  getTourSearch(nameSearch?: string, city?: string, country?: string, categories?: string,
    minPrice?: string, maxPrice?: string, dateRange?: string, pageIndex = 1, pageSize = 8
  ): Observable<PaginatedResponse<Tour>> {
    const params = new URLSearchParams();
    if (nameSearch) params.append('name', nameSearch);
    if (city) params.append('city', city);
    if (country) params.append('country', country);
    if (categories) params.append('categories', categories);
    if (minPrice) params.append('minPrice', minPrice);
    if (maxPrice) params.append('maxPrice', maxPrice);
    if (dateRange) params.append('dateRange', dateRange);
    params.append('pageIndex', pageIndex.toString());
    params.append('pageSize', pageSize.toString());
    const url = `${this.apiUrl}/search?${params.toString()}`;
    return this.http.get<PaginatedResponse<Tour>>(url);
  }

}
