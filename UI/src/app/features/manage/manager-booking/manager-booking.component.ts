import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { BookingService } from '../../Booking/services/booking.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Booking, StatusType } from '../../Booking/models/booking.model';
import Swal from 'sweetalert2';
import { PaginatedResponse } from '../../Tour/models/paginated.model';




@Component({
  selector: 'app-manager-booking',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './manager-booking.component.html',
  styleUrl: './manager-booking.component.css'
})
export class ManagerBookingComponent implements OnDestroy, OnInit {
  registerSubscription?: Subscription;
  private destroy$ = new Subject<void>();
  bookings?: PaginatedResponse<Booking>;
  pageSize: number = 10;
  pageIndex?: number = 1;
  key: string = '';
  currentkey: string = '';
  selectedBookingId?: number;



  constructor(private bookingService: BookingService) {
  }
  ngOnInit(): void {
    this.fetchAllBookings();
  }

  onKeyChange(): void {
    if (this.currentkey !== this.key) {
      this.pageIndex = 1; // Reset về trang đầu khi tìm kiếm mới
      this.currentkey = this.key;
      this.fetchAllBookings();
    }
  }


  fetchAllBookings(): void {
    if (this.currentkey != this.key) {
      this.pageIndex = 1;
      this.currentkey = this.key;
    }
    this.bookingService.getAll(this.key, this.pageIndex, this.pageSize).subscribe({
      next: (data) => {
        this.bookings = data;
      },
      error: (err) => {
        console.error('Error fetching bookings:', err);
      }
    });
  }

  goToPage(page?: number) {
    this.pageIndex = page;
    this.fetchAllBookings();
  }

  goToPreviousPage() {
    if (this.bookings?.currentPage && this.bookings?.currentPage > 1) {
      this.pageIndex = this.bookings.currentPage - 1;
      this.fetchAllBookings();
    }
  }

  goToNextPage() {
    if (this.bookings?.currentPage && this.bookings?.currentPage < this.bookings.totalPages) {
      this.pageIndex = this.bookings.currentPage + 1;
      this.fetchAllBookings();
    }
  }

  // show number page 
  getVisiablePage(): number[] {
    if (!this.bookings?.totalPages || !this.bookings.currentPage) {
      return [];
    }
    const totalPage = this.bookings.totalPages;
    const currentPage = this.bookings.currentPage;

    const startPage = Math.max(1, currentPage - 1);
    const endPage = Math.min(totalPage, currentPage + 1);

    const pages: number[] = [];
    for (let page = startPage; page <= endPage; page++) {
      pages.push(page);
    }
    return pages;
  }





  setSelectedBooking(bookingId: number): void {
    this.selectedBookingId = bookingId;
  }
  updateStatus(): void {
    const selectStatus = Number(
      (document.getElementById('selectStatus') as HTMLSelectElement)?.value || ''
    );

    if (!selectStatus && selectStatus !== 0) {
      console.error('No status selected');
      return;
    }

    if (!this.selectedBookingId) {
      console.error('No booking selected');
      return;
    }

    // Find the current booking
    const currentBooking = this.bookings?.items.find(b => b.bookingId === this.selectedBookingId);
    if (!currentBooking) {
      console.error('Booking not found');
      return;
    }

    const statusValue = typeof currentBooking.status === 'string'
      ? StatusType[currentBooking.status as keyof typeof StatusType]
      : currentBooking.status;

    const isValidTransition =
      (statusValue === StatusType.Submited &&
        (selectStatus === StatusType.Pending || selectStatus === StatusType.Canceled)) || // Submited -> Pending or Canceled
      (statusValue === StatusType.Pending &&
        selectStatus === StatusType.Confirmed) || // Pending -> Confirmed only
      (statusValue === StatusType.Confirmed &&
        selectStatus === StatusType.Completed); // Confirmed -> Completed only

    if (!isValidTransition) {
      Swal.fire({
        icon: 'error',
        title: 'Không hợp lệ',
        text: 'Trạng thái chỉ có thể chuyển từ "Submited" sang "Pending" hoặc "Canceled", từ "Pending" sang "Confirmed", và từ "Confirmed" sang "Completed".',
        confirmButtonText: 'OK',
      });
      return;
    }

    this.bookingService.updateStatus(this.selectedBookingId, selectStatus).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'Cập nhật trạng thái thành công',
          text: 'Trạng thái đã được lưu.',
          confirmButtonText: 'OK',
        }).then(() => {
          this.fetchAllBookings();
        });
      },
      error: (err) => {
        console.error('Error updating status:', err);
      }
    });
  }

  getStatusColor(status: StatusType | undefined): string {
    if (status === undefined) return 'badge-phoenix-dark'; // Default color for undefined status

    const statusValue = typeof status === 'string'
      ? StatusType[status as keyof typeof StatusType]
      : status;

    switch (statusValue) {
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



  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.registerSubscription?.unsubscribe();
  }



}
