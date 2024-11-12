import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterLink } from '@angular/router';
import { jwtDecode } from "jwt-decode";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

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
          console.log(response);

          // Lưu token vào cookie
          this.cookieService.set('Authentication', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');

          // Giải mã token để lấy thông tin vai trò
          const decodedToken: any = jwtDecode(response.token);

          const role = decodedToken['role'] || decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

          // Gọi setUser với role lấy từ token
          this.authService.setUser({
            email: this.model.email,
            role: role
          });
          console.log(role); // Kiểm tra vai trò lấy được

          this.router.navigateByUrl('/');
        },
        error: err => {
          console.log(err);
        }
      });
  }
}