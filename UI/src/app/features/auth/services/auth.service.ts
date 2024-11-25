import { HttpClient, HttpParams } from '@angular/common/http';
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
import { PagedResponse } from '../models/PaginatedList';
import { Booking } from '../../Booking/models/booking.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {



  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  getAllUser(): Observable<User[]> {
    return this.http.get<User[]>(`${BASE_URL}/User/get-all-user`);
  }

  blockUser(id: string): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/User/block-user/${id}`, {});
  }
  setRole(userId: string, role: string): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/User/set-role-user/${userId}?roleName=${role}`, {});
  }

  searchUser(key: string | null, pageIndex: number, pageSize: number): Observable<PagedResponse<User>> {
    const params = new HttpParams()
      .set('key', key || '')
      .set('pageIndex', pageIndex)
      .set('pageSize', pageSize);

    return this.http.get<PagedResponse<User>>(`${BASE_URL}/User/search-user`, { params });
  }

  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${BASE_URL}/Auth/get-user-info`, {
      headers: {
        Authorization: this.cookieService.get('Authentication') || ''
      }
    });
  }

  updateProfile(model: User): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Auth/update-user-info`, model);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${BASE_URL}/Auth/login-user`, request);
  }

  refreshToken(): Observable<LoginResponse> {
    const refreshToken = this.cookieService.get('RefreshToken');
    if (!refreshToken) {
      this.logout();
    }

    return this.http.post<LoginResponse>(
      `${BASE_URL}/Auth/refresh-token?refreshToken=${refreshToken}`, {}
    );
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
    localStorage.setItem('user-id', user.id); // Lưu id
    localStorage.setItem('user-username', user.userName); // Lưu username
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-role', user.roles);
    localStorage.setItem('user-bookings', JSON.stringify(user.bookings)); // Lưu bookings dưới dạng JSON
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const id = localStorage.getItem('user-id'); // Lấy id
    const userName = localStorage.getItem('user-username'); // Lấy username
    const email = localStorage.getItem('user-email');
    const role = localStorage.getItem('user-role');
    const bookings = localStorage.getItem('user-bookings');

    if (id && userName && email && role) {
      let parsedBookings: Booking[] = [];
      try {
        parsedBookings = bookings ? JSON.parse(bookings) : [];
      } catch (e) {
        console.error("Lỗi khi phân tích JSON bookings từ localStorage:", e);
      }
      return {
        id: id,
        userName: userName,
        fullName: '', // Nếu bạn cần thêm fullName, có thể sửa thành giá trị thích hợp
        email: email,
        roles: role,
        status: true, // Giá trị này có thể được quản lý động nếu cần
        bookings: parsedBookings
      };
    }
    return undefined;
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authentication', '/')
    this.cookieService.delete('RefreshToken', '/')
    this.$user.next(undefined);
  }

  getUserId(): string | null {
    return sessionStorage.getItem('userId');
  }
}
