import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TourService } from '../../Tour/services/tour.service';
import { Tour } from '../../Tour/models/tour.model';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user?: User;
  lastOrder?: string = '';
  listTour: Tour[] = [];
  totalOrderPrice: number = 0;

  page: number = 1;

  constructor(private authService: AuthService,
    private tourService: TourService,
    private cookieService: CookieService,
  ) {
  }

  ngOnInit(): void {
    this.authService.refreshToken().subscribe({
      next: (response) => {

        this.cookieService.set('Authentication', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');
        this.cookieService.set('RefreshToken', response.refreshToken, undefined, '/', undefined, true, 'Strict');

        const decodedToken: any = jwtDecode(response.token);
        const role = decodedToken['role'] || decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        const bookings = decodedToken['bookings'] ? JSON.parse(decodedToken['bookings']) : [];

        const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || decodedToken['Id'];
        const userName = decodedToken['sub'] || decodedToken['userName'];
        const fullName = decodedToken['name'] || decodedToken['fullName'];
        const email = decodedToken['email'] || decodedToken['Email'];
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

        // Refresh token thành công, bắt đầu thực hiện các API khác
        this.tourService.getAllTours().subscribe({
          next: (response) => {
            this.listTour = response;
            this.calculateTotalOrderPrice();
          }
        });

        this.user = this.authService.getUser();

        // Tìm ngày gần nhất
        if (this.user?.bookings?.length) {
          this.lastOrder = this.user.bookings
            .map((booking) => new Date(booking.BookingDate))
            .sort((a, b) => b.getTime() - a.getTime())[0]
            .toString();
        } else {
          this.lastOrder = 'Chưa có order!';
        }
      }
    });
  }

  calculateTotalOrderPrice(): void {
    if (this.user?.bookings?.length && this.listTour.length) {
      this.totalOrderPrice = this.user.bookings
        .map((booking) => {
          const tour = this.listTour.find((t) => t.tour.tourId === booking.TourId);
          return tour ? tour.tour.price : 0;
        })
        .reduce((sum, price) => sum + price, 0);
    } else {
      this.totalOrderPrice = 0;
    }
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 3:
        return 'Confirmed';
      case 2:
        return 'Pending';
      case 1:
        return 'Canceled';
      default:
        return 'Unknown';
    }
  }
}
