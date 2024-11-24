import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { TourService } from '../../Tour/services/tour.service';
import { Tour } from '../../Tour/models/tour.model';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  model: User

  user$?: Observable<User>;
  lastOrder?: string = '';
  listTour: Tour[] = [];
  totalOrderPrice: number = 0;

  constructor(private authService: AuthService,
    private tourService: TourService,
  ) {
    this.model = {
      id: '',
      fullName: '',
      userName: '',
      email: '',
      roles: '',
      status: true,
      bookings: [],
    }
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
        this.model.fullName = user.fullName;
        this.model.userName = user.userName;
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

  isEditingFullName = false;
  isEditingUserName = false;

  toggleEdit(field: string): void {
    if (field === 'fullName' || field === 'userName') {
      this.isEditingFullName = !this.isEditingFullName;
      this.isEditingUserName = !this.isEditingUserName;
    } else {
      this.isEditingFullName = false;
      this.isEditingUserName = false;
    }
  }

  updateProfile(form: NgForm) {
    if (!form.invalid) {
      console.log(this.model.fullName + '' + this.model.userName)
      this.authService.updateProfile(this.model).subscribe({
        next: respose => {
          Swal.fire('Update success!', 'Cập nhật profile thành công!', 'success');
          this.isEditingFullName = false;
          this.isEditingUserName = false;
        }
      })
    } else {
      alert("Vui lòng nhập đủ thông tin!")
    }
  }

}
