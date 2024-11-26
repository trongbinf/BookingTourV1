import { Component, OnDestroy, OnInit } from '@angular/core';
import { TourService } from '../services/tour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TourVm } from '../models/tourVm.model';
import { TourGet } from '../models/tour-get.model';
import { Category } from '../../category/model/category.model';
import { ActivityType } from '../../activities/models/activity.model';
import { ActivityAdd } from '../../activities/models/create-activity.model';
import { ActivityService } from '../../activities/services/activity.service';
import { Subject, take, takeUntil } from 'rxjs';
import Swal from 'sweetalert2';
import { DateStart, TypeStatus } from '../../dateStarts/models/date-start.model';
import { DateStartService } from '../../dateStarts/services/date-start.service';


@Component({
  selector: 'app-tour-update',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tour-update.component.html',
  styleUrl: './tour-update.component.css'
})
export class TourUpdateComponent implements OnDestroy {
  tour!: TourVm
  catelist: Category[] = [];
  tourId!: number; // Lấy từ URL
  category!: Category
  tourT!: TourGet;
  idActivity?: number;
  idDateStart?: number;
  minDate: string = '';
  openActivity: boolean = false;
  openDateStart: boolean = false;
  isEditActivity: boolean = false;
  isEditDate: boolean = false;
  isDateDuplicate: boolean = false;
  selectedDates: DateStart[] = [];
  private destroy$: Subject<void> = new Subject<void>();
  dateStartSelect: DateStart = {
    dateStartId: 0,
    startDate: new Date(),
    typeStatus: TypeStatus.Available,
    tour: this.tour
  }
  activityTour: ActivityAdd = {
    activityName: '',
    activityType: ActivityType.Services,
    description: '',
    duration: '',
    location: '',
    tourId: this.tourId
  };

  activityTypes = Object.keys(ActivityType)
    .filter(key => isNaN(Number(key))); // Chỉ lấy các tên của enum

  dateStatus = Object.keys(TypeStatus).filter(key => isNaN(Number(key)));

  mainImagePreview: string | null = null;
  detailImagesPreview: string[] = [];
  newMainImage: File | null = null;
  newDetailImages: File[] = [];
  otherImageArray: string[] = [];

  constructor(
    private tourService: TourService,
    private activityService: ActivityService,
    private dateStartService: DateStartService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.tour = {
      activities: [],
      bookings: [],
      category: this.category,
      dateStarts: [],
      detailImages: [],
      mainImage: '',
      reviews: [],
      tour: this.tourT
    };
    const today = new Date().toISOString().split('T')[0];
    this.minDate = today;
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.tourId = +id;
      this.tourService.getTourVmById(this.tourId).subscribe({
        next: (data) => {
          this.tour = data;
          if (this.tour.tour.otherImage) {
            this.otherImageArray = this.tour.tour.otherImage
              .split(';')
              .map((img) => img.trim());
          }
          console.log(this.tour);
        },

        error: (err) => {
          console.error('Error fetching tour details:', err);
          alert('Failed to load tour details.');
        },
      });
    }
    this.tourService.getCategories().subscribe({
      next: (data) => (this.catelist = data),
      error: (err) => {
        console.error('Error fetching categories:', err);
        alert('Failed to load categories.');
      },
    });

  }
  onMainImageChange(event: Event): void {
    const file = (event.target as HTMLInputElement)?.files?.[0];
    if (file) {
      this.newMainImage = file;
      const reader = new FileReader();
      reader.onload = (e) => (this.mainImagePreview = e.target?.result as string);
      reader.readAsDataURL(file);
    }
  }

  onDetailImagesChange(event: Event): void {
    const files = (event.target as HTMLInputElement)?.files;
    if (files) {
      this.newDetailImages = Array.from(files);
      this.detailImagesPreview = [];
      for (let file of this.newDetailImages) {
        const reader = new FileReader();
        reader.onload = (e) =>
          this.detailImagesPreview.push(e.target?.result as string);
        reader.readAsDataURL(file);
      }
    }
  }
  onUpdate() {
    if (!this.tour.tour.tourName || !this.tour.tour.city || !this.tour.tour.country || !this.tour.tour.duration || !this.tour.tour.description) {
      alert('Please fill in all required fields.');
      if (this.tour.tour.mainImage) alert('đã nhận được Main Image');
      if (this.tour.detailImages && this.tour.detailImages.length > 0) alert('đã nhận được detali Image');
      return;
    }
    const formData = new FormData();

    formData.append('TourId', this.tour.tour.tourId.toString());
    formData.append('TourName', this.tour.tour.tourName);
    formData.append('Description', this.tour.tour.description);
    formData.append('City', this.tour.tour.city);
    formData.append('Country', this.tour.tour.country);
    formData.append('Duration', this.tour.tour.duration);
    formData.append('IsFullDay', this.tour.tour.isFullDay.toString());
    formData.append('Price', this.tour.tour.price.toString());
    formData.append('PersonNumber', this.tour.tour.personNumber.toString());
    formData.append('Status', this.tour.tour.status.toString());
    formData.append('CategoryId', this.tour.category.categoryId.toString());

    if (this.newMainImage) {
      formData.append('MainImage', this.newMainImage);
    }

    if (this.newDetailImages.length > 0) {
      for (let file of this.newDetailImages) {
        formData.append('DetailImages', file);
      }
    }

    this.tourService.updateTour(this.tourId, formData).subscribe({
      next: () => {
        alert('Tour updated successfully!');
        this.router.navigate(['/admin-tour']);
      },
      error: (err) => {
        console.error('Error updating tour:', err);
        alert('Failed to update tour.');
      },
    });
  }

  discardChanges() {
    this.ngOnInit();
  }

  realoadTour() {
    this.tourService.getTourVmById(this.tourId).pipe(takeUntil(this.destroy$))
      .subscribe({
        next: response => {
          this.tour = response;
        },
        error: err => {
          console.log(err);
        }
      });
  }

  updateActivity() {
    console.log(this.activityTour);
    this.activityTour.activityType = this.activityTour.activityType.valueOf();
    if (this.isEditActivity) {
      //update Activity
      this.activityService.updateActivity(this.idActivity!, this.activityTour).
        pipe(takeUntil(this.destroy$)).subscribe({
          next: response => {
            this.realoadTour();
            Swal.fire({
              icon: 'success',
              title: 'Cập nhật thành công',
              text: 'Thông tin đã được lưu.',
              confirmButtonText: 'OK',
            });
          },
          error: err => {
            Swal.fire({
              icon: 'error',
              title: 'Cập nhật thất bại',
              text: 'Hãy chỉnh sửa lại.',
              confirmButtonText: 'OK',
            });
            console.log(err);
          }
        });

    } else {
      //add Activity
      console.log(this.activityTour);
      this.activityService.addActivity(this.activityTour).pipe(takeUntil(this.destroy$)).subscribe({
        next: response => {
          this.realoadTour();
          Swal.fire({
            icon: 'success',
            title: 'Cập nhật thành công',
            text: 'Thông tin đã được lưu.',
            confirmButtonText: 'OK',
          });

        },
        error: err => {
          Swal.fire({
            icon: 'error',
            title: 'Cập nhật thất bại',
            text: 'Hãy chỉnh sửa lại.',
            confirmButtonText: 'OK',
          });
          console.log(err);
        }
      })
    }


  }

  onSubmitDateStart() {
    if (this.isEditDate) {
      // update datestart
      console.log(this.dateStartSelect);
      this.dateStartService.updateDateStart(this.idDateStart!, this.dateStartSelect).pipe(takeUntil(this.destroy$))
        .subscribe({
          next: response => {
            this.realoadTour();
            Swal.fire({
              icon: 'success',
              title: 'Cập nhật thành công',
              text: 'Thông tin đã được lưu.',
              confirmButtonText: 'OK',
            });
          },
          error: err => {
            Swal.fire({
              icon: 'error',
              title: 'Cập nhật thất bại',
              text: 'Hãy chỉnh sửa lại.',
              confirmButtonText: 'OK',
            });
            console.log(err);
          }
        })
    } else {
      // add
      this.selectedDates.push(this.dateStartSelect);
      console.log(this.dateStartSelect);
      this.dateStartService.addDateStart(this.tourId, this.selectedDates).pipe(takeUntil(this.destroy$))
        .subscribe({
          next: Response => {
            this.realoadTour();
            Swal.fire({
              icon: 'success',
              title: 'Cập nhật thành công',
              text: 'Thông tin đã được lưu.',
              confirmButtonText: 'OK',
            });
            console.log(Response);
          },
          error: err => {
            Swal.fire({
              icon: 'error',
              title: 'Cập nhật thất bại',
              text: 'Hãy chỉnh sửa lại.',
              confirmButtonText: 'OK',
            });
            console.log(err);
          }
        })
    }
  }

  showActivity() {
    if (!this.openActivity) {
      this.openDateStart = false;
      this.openActivity = true;
    } else {
      this.openActivity = false;
    }
  }

  showDateStart() {
    if (!this.openDateStart) {
      this.openActivity = false;
      this.openDateStart = true;
    } else {
      this.openDateStart = false;
    }
  }

  editActivity(id: number) {
    var findding = this.tour.activities.find(a => a.activityId == id);
    if (findding) {
      this.isEditActivity = true;
      this.idActivity = findding.activityId;
      this.activityTour = {
        activityName: findding.activityName,
        description: findding.description,
        activityType: findding.activityType,
        location: findding.location,
        duration: findding.duration,
        tourId: findding.tourId
      };
    }
    console.log(this.activityTour);
  }

  openAddActivity() {
    this.isEditActivity = false;
    this.activityTour = {
      activityName: '',
      activityType: ActivityType.Services,
      description: '',
      duration: '',
      location: '',
      tourId: this.tourId
    }
  }

  openAddDateStart() {
    this.isEditDate = false;
    this.dateStartSelect = {
      dateStartId: 0,
      startDate: new Date(),
      typeStatus: TypeStatus.Available,
      tour: this.tour
    }
  }

  editDate(id: number) {
    var findding = this.tour.dateStarts.find(d => d.dateStartId == id);
    if (findding) {
      this.isEditDate = true;
      this.idDateStart = findding.dateStartId;
      this.dateStartSelect = {
        dateStartId: findding.dateStartId,
        startDate: findding.startDate,
        typeStatus: findding.typeStatus,
      }
    }
  }

  deleteActivity(id: number) {
    var findding = this.tour.activities.find(d => d.activityId == id);
    if (findding) {
      this.activityService.deleteActivity(id).pipe(takeUntil(this.destroy$)).subscribe({
        next: response => {
          this.realoadTour();
          Swal.fire({
            icon: 'success',
            title: 'Đã xoá thành công',
            text: 'Thông tin đã được lưu.',
            confirmButtonText: 'OK',
          });
        },
        error: err => {
          Swal.fire({
            icon: 'error',
            title: 'Cập nhật thất bại',
            text: 'Hãy chỉnh sửa lại.',
            confirmButtonText: 'OK',
          });
          console.log(err);
        }
      })
    } else {
      console.log("not found");
    }
  }

  deleteDateStart(id: number) {
    var findding = this.tour.dateStarts.find(d => d.dateStartId == id);
    if (findding) {
      this.dateStartService.deleteDateStart(id).pipe(takeUntil(this.destroy$)).subscribe({
        next: response => {
          this.realoadTour();
          Swal.fire({
            icon: 'success',
            title: 'Đã xoá thành công',
            text: 'Thông tin đã được lưu.',
            confirmButtonText: 'OK',
          });
        },
        error: err => {
          Swal.fire({
            icon: 'error',
            title: 'Cập nhật thất bại',
            text: 'Hãy chỉnh sửa lại.',
            confirmButtonText: 'OK',
          });
          console.log(err);
        }
      })
    } else {
      console.log("not found");
    }
  }

  validateDate() {
    const selectedDate = new Date(this.dateStartSelect.startDate);
    const min = selectedDate > new Date(this.minDate);
    const isDup = this.tour.dateStarts.some(d => {
      const tourDate = new Date(d.startDate);
      return tourDate.getTime() === selectedDate.getTime();
    });

    if (isDup || !min) {
      this.isDateDuplicate = true;
    } else {
      this.isDateDuplicate = false;
    }
  }


}
