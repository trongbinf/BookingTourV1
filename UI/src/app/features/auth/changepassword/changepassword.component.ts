import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../models/user.model';
import { ChangePassword } from '../models/changepass..models';

@Component({
  selector: 'app-changepassword',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent implements OnInit {

  model: ChangePassword;
  user?: User;

  constructor(private route: ActivatedRoute, private authService: AuthService) {
    this.model = {
      email: "",
      oldPassword: '',
      newPassword: '',
      confirmNewPassword: ''
    }
  }

  ngOnInit(): void {
    this.user = this.authService.getUser();

    if (this.user) {
      this.model.email = this.user.email;
    } else {
      console.error("User not found in local storage.");
    }
  }

  onFormSubmit(_t18: NgForm) {
    if (this.model.newPassword !== this.model.confirmNewPassword) {
      alert("Passwords do not match!");
      return;
    } else {
      this.authService.changepass(this.model).subscribe({
        next: respose => {
          alert("Change pass thanh cong")
        }, error: err => {
          console.log(err)
        }
      })
    }
  }
}
