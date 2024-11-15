import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { BASE_URL } from '../../../app.config';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';
import { Register } from '../models/register.model';
import { ResetPassword } from '../models/reset-password.model';
import { ChangePassword } from '../models/changepass..models';
import { Booking } from '../../Booking/models/booking.model';
import { AppUser } from '../models/appUser.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {


  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  getAllUser(): Observable<AppUser[]> {
    return this.http.get<AppUser[]>(`${BASE_URL}/User/get-all-user`);
  }
  blockUser(id: string): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/User/block-user/${id}`, {});
  }
  setRole(userId: string, role: string): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/User/set-role-user/${userId}?roleName=${role}`, {});
  }


  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${BASE_URL}/Auth/login-user`, request);
  }

  isLoginedIn() {
    const auth = this.cookieService.get('Authentication');
    return auth != null && auth !== '';
  }

  forgotpass(email: string): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Auth/forgot-password?email=${email}`, {});
  }

  changepass(model: ChangePassword): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Auth/change-password`, model)
  }

  resetpass(model: ResetPassword): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Auth/reset-password`, model)
  }

  register(register: Register): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Auth/register-user`, register);
  }

  confirmEmail(token: string, email: string): Observable<any> {
    return this.http.get(`${BASE_URL}/confirm-email?token=${token}&email=${email}`);
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-role', user.role);
    localStorage.setItem('user-bookings', JSON.stringify(user.bookings)); // Lưu bookings dưới dạng JSON
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const role = localStorage.getItem('user-role');
    const bookings = localStorage.getItem('user-bookings');

    if (email && role) {
      let parsedBookings: Booking[] = [];
      try {
        parsedBookings = bookings ? JSON.parse(bookings) : [];
      } catch (e) {
        console.error("Lỗi khi phân tích JSON bookings từ localStorage:", e);
      }

      const user: User = {
        email: email,
        role: role,
        bookings: parsedBookings
      };

      return user;
    }
    return undefined;
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authentication', '/')
    this.$user.next(undefined);
  }
}
