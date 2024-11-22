import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../app.config';
import { CreateBooking } from '../models/create-booking.model';
import { Booking } from '../models/booking.model';


@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = `${BASE_URL}/Booking`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.apiUrl);
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
