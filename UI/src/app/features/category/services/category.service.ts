import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryTours } from '../model/category-tour.model';
import { BASE_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getAllCategories(): Observable<CategoryTours[]> {
    return this.http.get<CategoryTours[]>(`${BASE_URL}/Category/all`)
  }

}
