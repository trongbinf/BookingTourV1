import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterLink } from '@angular/router';
import { jwtDecode } from "jwt-decode";
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  email: string = '';

  model: LoginRequest;

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      email: '',
      password: ''
    }
  }
  onFormSubmit() {
    this.authService.login(this.model)
      .subscribe({
        next: response => {
          // Lưu token vào cookie
          this.cookieService.set('Authentication', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');
          this.cookieService.set('RefreshToken', response.refreshToken, undefined, '/', undefined, true, 'Strict');

          // Giải mã token để lấy thông tin vai trò
          const decodedToken: any = jwtDecode(response.token);
          const role = decodedToken['role'] || decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          const bookings = decodedToken['bookings'] ? JSON.parse(decodedToken['bookings']) : [];

          const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || decodedToken['Id'];
          const userName = decodedToken['sub'] || decodedToken['userName'];
          const fullName = decodedToken['name'] || decodedToken['fullName'];
          const email = decodedToken['email'] || decodedToken['Email'];
          sessionStorage.setItem('userId', userId);
          // Gọi setUser với role và bookings lấy từ response
          this.authService.setUser({
            id: userId,
            userName: userName,
            fullName: fullName,
            email: email,
            roles: role,
            status: true,
            bookings: bookings
          })

          window.location.href = '/'
        },
        error: err => {
          Swal.fire('Login fail!', 'Vui lòng kiểm tra lại tài khoản và mật khẩu!', 'warning');
        }
      });
  }

  onForgotPassword() {
    this.authService.forgotpass(this.email).subscribe({
      next: respose => {
        Swal.fire('Forgot Password!', 'Đã gửi xác thực về mail!', 'success');
      }
    })
  }
}