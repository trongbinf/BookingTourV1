import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ResetPassword } from '../models/reset-password.model';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../models/user.model';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent implements OnInit {

  model: ResetPassword;

  constructor(private route: ActivatedRoute, private authService: AuthService) {
    this.model = {
      email: "",
      token: '',
      password: '',
      confirmPassword: ''
    }
  }

  ngOnInit(): void {
    // Lấy token và email từ URL và gán vào model
    this.route.queryParams.subscribe(params => {
      this.model.email = params['email'] || '';
      this.model.token = params['token'] || '';
    });
  }

  onFormSubmit(form: NgForm) {
    if (this.model.password !== this.model.confirmPassword) {
      alert("Passwords do not match!");
      return;
    } else {
      this.authService.resetpass(this.model).subscribe({
        next: respose => {
          alert("Change pass thanh cong")
        }
      })
    }
  }

}
