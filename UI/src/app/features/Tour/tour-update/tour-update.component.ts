import { Component, OnInit } from '@angular/core';
import { TourService } from '../services/tour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateTour } from '../models/create-tour.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TourVm } from '../models/tourVm.model';
import { TourGet } from '../models/tour-get.model';
import { Category } from '../../category/model/category.model';

@Component({
  selector: 'app-tour-update',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tour-update.component.html',
  styleUrl: './tour-update.component.css'
})
export class TourUpdateComponent {
  tour!: TourVm
  catelist: Category[] = [];
  imagesPreview: string[] = [];
  tourId!: number; // Lấy từ URL
  category!: Category
  tourT!: TourGet

  constructor(
    private tourService: TourService,
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
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.tourId = +id;
      this.tourService.getTourVmById(this.tourId).subscribe({
        next: (data) => {
          this.tour = data;
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

  onUpdate() {
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

    // if (this.tour.mainImage) {
    //   formData.append('MainImage', this.tour.mainImage, this.tour.mainImage.name);
    // }

    // if (this.tour.detailImages) {
    //   for (let file of this.tour.detailImages) {
    //     formData.append('DetailImages', file, file.name);
    //   }
    // }

    // Gửi yêu cầu cập nhật
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
  onFileChange(event: any) {
    const files = event.target.files;
    for (let i = 0; i < files.length; i++) {
      this.createImagePreview(files[i]);
    }
  }

  onFileDrop(event: any) {
    event.preventDefault();
    const files = event.dataTransfer.files;
    for (let i = 0; i < files.length; i++) {
      this.createImagePreview(files[i]);
    }
  }

  onDragOver(event: any) {
    event.preventDefault();
  }

  createImagePreview(file: File) {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imagesPreview.push(e.target.result);
    };
    reader.readAsDataURL(file);
  }

  removeImage(image: string) {
    this.imagesPreview = this.imagesPreview.filter((img) => img !== image);
  }
}
