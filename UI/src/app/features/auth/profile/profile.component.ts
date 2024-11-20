import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TourService } from '../../Tour/services/tour.service';
import { Tour } from '../../Tour/models/tour.model';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  user$?: Observable<User>;
  lastOrder?: string = '';
  listTour: Tour[] = [];
  totalOrderPrice: number = 0;

  constructor(private authService: AuthService,
    private tourService: TourService,
  ) {
  }

  ngOnInit(): void {
    this.tourService.getAllTours().subscribe({
      next: (response) => {
        this.listTour = response;
        this.calculateTotalOrderPrice();
      }
    });

    this.user$ = this.authService.getUserInfo();

    this.user$.subscribe({
      next: (user) => {
        // Tìm ngày gần nhất
        if (user?.bookings?.length) {
          this.lastOrder = user.bookings
            .map((booking) => new Date(booking.bookingDate))
            .sort((a, b) => b.getTime() - a.getTime())[0]
            .toString();
        } else {
          this.lastOrder = 'Chưa có order!';
        }
      }
    });
  }


  calculateTotalOrderPrice(): void {
    this.user$?.subscribe({
      next: (user) => {
        if (user?.bookings?.length && this.listTour.length) {
          this.totalOrderPrice = user.bookings
            .map((booking) => {
              const tour = this.listTour.find((t) => t.tour.tourId === booking.tourId);
              return tour ? tour.tour.price : 0;
            })
            .reduce((sum, price) => sum + price, 0);
        } else {
          this.totalOrderPrice = 0;
        }
      }
    });
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 2:
        return 'Confirmed';
      case 3:
        return 'Pending';
      case 4:
        return 'Canceled';
      default:
        return 'Unknown';
    }
  }
}
