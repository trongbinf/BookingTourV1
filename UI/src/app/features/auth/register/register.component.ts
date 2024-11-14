import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Register } from '../models/register.model';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit, OnDestroy {

  model: Register;
  registerSubcription?: Subscription;

  constructor(private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute // Inject ActivatedRoute
  ) {
    this.model = {
      userName: '',
      email: '',
      password: '',
      confirmPassword: ''
    }
  }

  ngOnInit(): void {
    const token = this.route.snapshot.queryParamMap.get('token');
    if (token) {
      Swal.fire('Register success!', 'Đăng ký tài khoản thành công!', 'success');
    }
  }


  ngOnDestroy(): void {
    this.registerSubcription?.unsubscribe();
  }


  onFormSubmit(form: NgForm) {
    if (form.valid) {
      this.registerSubcription = this.authService.register(this.model)
        .subscribe({
          next: response => {
            console.log(response)
            Swal.fire('Confirm success!', 'Xác thực email đã được gửi đến email của bạn!', 'success');
          }
        });
    }
  }
}
