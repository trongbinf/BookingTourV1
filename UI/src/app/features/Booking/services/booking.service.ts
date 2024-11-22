import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../app.config';
import { CreateBooking } from '../models/create-booking.model';
import { Booking } from '../models/booking.model';
import { PaginatedResponse } from '../../Tour/models/paginated.model';


@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = `${BASE_URL}/Booking`;

  constructor(private http: HttpClient) { }

  getAll(name?: string, pageIndex: number = 1, pageSize: number = 5): Observable<PaginatedResponse<Booking>> {
    const params = new URLSearchParams();
    if (name) params.append('key', name);
    params.append('pageIndex', pageIndex.toString());
    params.append('pageSize', pageSize.toString());

    return this.http.get<PaginatedResponse<Booking>>(`${this.apiUrl}/search?${params.toString()}`);
  }

  getById(id: number): Observable<Booking> {
    return this.http.get<Booking>(`${this.apiUrl}/${id}`);
  }

  insert(booking: CreateBooking): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, booking);
  }

  updateStatus(bookingId: number, status: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update-status/${bookingId}?status=${status}`, {});
  }
}
