import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Tour } from '../../Tour/models/tour.model';
import { Observable, Subject, Subscription, forkJoin, takeUntil, map } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { Booking, BookingWithReviewStatus, StatusType } from '../../Booking/models/booking.model';
import { PaginatedResponse } from '../../Tour/models/paginated.model';
import { BookingService } from '../../Booking/services/booking.service';
import { ReviewService } from '../../reviews/services/review.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  bookings?: PaginatedResponse<BookingWithReviewStatus>;
  user$?: Observable<User>;
  lastOrder?: string = '';
  listTour: Tour[] = [];
  totalOrderPrice: number = 0;
  isLoadingTotalMoney: boolean = false; // To track loading state
  private destroy$ = new Subject<void>();
  registerSubscription?: Subscription;

  constructor(
    private authService: AuthService,
    private bookingService: BookingService,
    private reviewService: ReviewService,
    private router: Router
  ) { }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.registerSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    const sessionUserId = this.authService.getUserId();

    if (sessionUserId) {
      this.loadBookingByUserId(sessionUserId);
      this.loadTotalMoneyByUser(sessionUserId);
    } else {
      console.log('User is not logged in or user ID is not available.');
    }

    this.user$ = this.authService.getUserInfo();
    this.user$.subscribe({
      next: (user) => {
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

  loadBookingByUserId(userId: string): void {
    this.bookingService.getBookingsByUserId(userId, 1, 10)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          const reviewChecks = response.items.map((booking) =>
            this.isReviewed(booking.bookingId).pipe(
              map((isReviewed) => ({ ...booking, isReviewed })) // Attach isReviewed dynamically
            )
          );

          forkJoin(reviewChecks).subscribe((updatedBookings) => {
            this.bookings = {
              ...response,
              items: updatedBookings
            };
          });
        },
        error: (err) => {
          console.error('Error loading bookings:', err);
        }
      });
  }


  isReviewed(bookingId: number): Observable<boolean> {
    return this.reviewService.checkIfReviewed(bookingId);
  }

  handleFeedbackClick(booking: BookingWithReviewStatus): void {
    // Check if booking has already been reviewed
    if (booking.isReviewed) {
      Swal.fire({
        title: 'Thông báo',
        text: 'Bạn đã đánh giá cho booking này!',
        icon: 'info',
        confirmButtonText: 'OK'
      });
    } else if (booking.status === StatusType.Completed) {
      // Navigate to the review page if status is Completed
      this.router.navigate(['/review-add', booking.tour?.tourId, booking.bookingId]);
    } else {
      // Show an error message if the status is not valid for review
      Swal.fire({
        title: 'Không hợp lệ',
        text: 'Chỉ có thể đánh giá booking với trạng thái Completed!',
        icon: 'error',
        confirmButtonText: 'OK'
      });
    }
  }


  loadTotalMoneyByUser(userId: string): void {
    this.isLoadingTotalMoney = true;

    this.bookingService.getTotalMoneyByUser(userId)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        response => {
          this.totalOrderPrice = Number(response);
          this.isLoadingTotalMoney = false;
        },
        error => {
          console.error('Error fetching total money:', error);
          this.isLoadingTotalMoney = false;
        }
      );
  }

  getStatusString(status: number | undefined): string {
    if (status === undefined) return 'Unknown';
    return StatusType[status] ?? 'Unknown';
  }

  getStatusColor(status: number | undefined): string {
    if (status === undefined) return 'badge-phoenix-dark';

    switch (status) {
      case StatusType.Submited:
        return 'badge-phoenix-primary';
      case StatusType.Pending:
        return 'badge-phoenix-warning';
      case StatusType.Confirmed:
        return 'badge-phoenix-success';
      case StatusType.Canceled:
        return 'badge-phoenix-danger';
      case StatusType.Available:
        return 'badge-phoenix-info';
      case StatusType.Unavailable:
        return 'badge-phoenix-secondary';
      default:
        return 'badge-phoenix-dark';
    }
  }
}
