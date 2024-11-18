import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../app.config';
import { CreateBooking } from '../models/create-booking.model';
import { BookingVM } from '../models/bookingVm.model';


@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = `${BASE_URL}/Booking`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<BookingVM[]> {
    return this.http.get<BookingVM[]>(this.apiUrl);
  }

  getById(id: number): Observable<BookingVM> {
    return this.http.get<BookingVM>(`${this.apiUrl}/${id}`);
  }

  update(id: number, booking: BookingVM): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, booking);
  }

  insert(booking: CreateBooking): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, booking);
  }

}
