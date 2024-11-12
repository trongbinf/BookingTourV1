import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
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
export class RegisterComponent {

  model: Register;
  registerSubcription?: Subscription;

  constructor(private authService: AuthService,
    private router: Router
  ) {
    this.model = {
      userName: '',
      email: '',
      password: '',
      confirmPassword: ''
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
            Swal.fire('Register success!', 'New user created!', 'success');
          }
        });
    }
  }
}
