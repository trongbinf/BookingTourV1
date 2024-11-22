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
  pageSize: number = 5;
  pageIndex?: number = 1;
  key: string = '';
  currentkey: string = '';
  selectedBookingId?: number;



  constructor(private bookingService: BookingService) {
  }
  ngOnInit(): void {
    this.fetchAllBookings();
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


  getStatusString(status: number): string {
    return StatusType[status] ?? "Unknown";
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

    // Validate status transitions
    const isValidTransition =
      (currentBooking.status === StatusType.Submited && (selectStatus === StatusType.Pending || selectStatus === StatusType.Canceled)) || // Submited -> Pending or Canceled
      (currentBooking.status === StatusType.Pending && selectStatus === StatusType.Confirmed); // Pending -> Confirmed only

    if (!isValidTransition) {
      Swal.fire({
        icon: 'error',
        title: 'Không hợp lệ',
        text: 'Trạng thái chỉ có thể chuyển từ "Submited" sang "Pending" hoặc "Canceled", và từ "Pending" sang "Confirmed".',
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


  getStatusColor(status: number): string {
    switch (status) {
      case 0: // Submited
        return 'badge-phoenix-primary'; // Màu xanh dương 
      case 1: // Pending
        return 'badge-phoenix-warning'; // Màu vàng
      case 2: // Confirmed
        return 'badge-phoenix-success'; // Màu xanh lá
      case 3: // Canceled
        return 'badge-phoenix-danger'; // Màu đỏ
      case 4: // Available
        return 'badge-phoenix-info'; // Màu xanh nhạt
      case 5: // Unavailable
        return 'badge-phoenix-secondary'; // Màu xám
      default:
        return 'badge-phoenix-dark'; // Màu tối cho trạng thái không xác định
    }
  }




  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.registerSubscription?.unsubscribe();
  }



}
