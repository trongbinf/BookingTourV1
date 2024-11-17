import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TourService } from '../Tour/services/tour.service';
import { BookingService } from '../Booking/services/booking.service';
import { CommonModule } from '@angular/common';
import { TourVm } from '../Tour/models/tourVm.model';
import { CreateBooking, StatusType } from '../Booking/models/create-booking.model';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tour-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css']
})
export class TourDetailComponent implements OnInit, OnDestroy {
  tourVm?: TourVm;
  registerSubscription?: Subscription;
  tourId!: number;
  personCount: number = 1;
  isFullDay: boolean = true;
  selectedPickDate: string | undefined = '';

  constructor(
    private route: ActivatedRoute,
    private tourService: TourService,
    private bookingService: BookingService
  ) { }

  ngOnDestroy(): void {
    this.registerSubscription?.unsubscribe();
  }

  fetchTourDetails(id: number): void {
    this.registerSubscription = this.tourService.getTourVmById(id).subscribe({
      next: (tourVm) => {
        this.tourVm = tourVm;
        this.isFullDay = this.tourVm?.tour?.isFullDay || true;
        console.log('Fetched Tour Details:', this.tourVm);

        if (this.tourVm?.dateStarts && this.tourVm.dateStarts.length > 0) {
          this.selectedPickDate = String(this.tourVm?.dateStarts[0].dateStartId);
          console.log('Selected pick date:', this.selectedPickDate);
        }
      },
      error: (err) => console.error('Error fetching tour details:', err),
    });
  }

  updateTotal(change: number): void {
    const maxPersons = this.tourVm?.tour?.personNumber || 0;
    if (this.tourVm && this.personCount + change > 0 && this.personCount + change <= maxPersons) {
      this.personCount += change;
    } else if (this.personCount + change > maxPersons) {
      Swal.fire({
        icon: 'error',
        title: 'Lỗi',
        text: `Không thể đặt nhiều hơn ${maxPersons} người.`,
        confirmButtonText: 'OK',
      });
    }
  }
  proceedToBooking(): void {
    if (!this.tourVm) {
      console.error('Tour details are not available yet.');
      return;
    }

    let pickDate: Date;

    if (this.tourVm?.tour?.isFullDay) {
      if (!this.selectedPickDate) {
        console.error('Pick date is not selected.');
        return;
      }
      pickDate = new Date(this.selectedPickDate);
    } else {
      const selectedDateId = Number(this.selectedPickDate);
      const selectedStartDate = this.tourVm?.dateStarts?.find(dateStart => dateStart.dateStartId === selectedDateId)?.startDate;

      if (selectedStartDate) {
        pickDate = new Date(selectedStartDate);
      } else {
        console.error('No valid startDate found for the selected dateStartId.');
        return;
      }
    }

    const selectedTime = (document.getElementById('selectTime') as HTMLSelectElement)?.value || '';


    if (!selectedTime) {
      console.error('Start time is not selected.');
      return;
    }

    const booking: CreateBooking = {
      tourId: this.tourVm.tour.tourId,
      bookingDate: new Date().toISOString(),
      personNumber: this.personCount,
      pickDate: pickDate.toISOString(),
      startTime: selectedTime,
      notes: 'Ghi chú mặc định',
      status: StatusType.Submited,
      userId: 'test-user-id',
    };

    console.log('Booking request:', booking);

    this.bookingService.insert(booking).subscribe({
      next: (response) => {
        Swal.fire({
          icon: 'success',
          title: 'Đặt vé thành công',
          text: 'Thông tin đặt vé đã được lưu.',
          confirmButtonText: 'OK',
        });
        console.log('Booking successful:', response);
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Lỗi',
          text: 'Đặt vé không thành công. Vui lòng thử lại.',
          confirmButtonText: 'OK',
        });
        console.error('Error inserting booking:', err);
      },
    });
  }


  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id && !isNaN(+id)) {
        this.tourId = +id;
        this.fetchTourDetails(this.tourId);
      } else {
        console.error('Invalid or missing tour ID in the route.');
      }
    });
  }


}
