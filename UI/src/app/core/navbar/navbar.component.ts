import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../features/auth/models/user.model';
import { AuthService } from '../../features/auth/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  user?: User;

  constructor(private authService: AuthService,
    private router: Router
  ) {
  }
  ngOnInit(): void {
    this.authService.getUserInfo()
      .subscribe({
        next: response => {
          this.user = response;
        }
      });
  }

  onLogout() {
    this.authService.logout();
    window.location.href = '/login'; // Chuyển hướng đến "/login" và reload lại trang
  }
}
