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


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

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

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-role', user.role);
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const role = localStorage.getItem('user-role');

    if (email && role) {
      const user: User = {
        email: email,
        role: role
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
