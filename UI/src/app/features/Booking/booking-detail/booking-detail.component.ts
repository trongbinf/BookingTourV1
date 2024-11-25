import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { Booking, StatusType } from '../models/booking.model';
import { BookingService } from '../services/booking.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-booking-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './booking-detail.component.html',
  styleUrls: ['./booking-detail.component.css']
})
export class BookingDetailComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  bookingId!: number;
  booking?: Booking;
  registerSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private bookingService: BookingService
  ) { }

  ngOnInit(): void {
    this.loadBookingById();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.registerSubscription?.unsubscribe();
  }
  getStatusString(status: number | undefined): string {
    if (status === undefined) return 'Unknown';
    return StatusType[status] ?? 'Unknown';
  }

  getStatusColor(status: number | undefined): string {
    if (status === undefined) return 'badge-phoenix-dark'; // Default color for undefined status

    switch (status) {
      case StatusType.Submited: // 0
        return 'badge-phoenix-primary'; // Blue
      case StatusType.Pending: // 1
        return 'badge-phoenix-warning'; // Yellow
      case StatusType.Confirmed: // 2
        return 'badge-phoenix-success'; // Green
      case StatusType.Canceled: // 3
        return 'badge-phoenix-danger'; // Red
      case StatusType.Available: // 4
        return 'badge-phoenix-info'; // Light Blue
      case StatusType.Unavailable: // 5
        return 'badge-phoenix-secondary'; // Gray
      default:
        return 'badge-phoenix-dark'; // Dark for unknown states
    }
  }

  private loadBookingById(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.bookingId = +id;

        this.registerSubscription = this.bookingService.getBookingById(this.bookingId).subscribe({
          next: (response: Booking) => {
            this.booking = response;
          },
          error: (err) => {
            console.error('Failed to load booking:', err);
          }
        });
      }
    });
  }


}
