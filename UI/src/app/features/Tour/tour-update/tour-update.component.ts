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
export class TourUpdateComponent  {
  tour!: TourVm
  catelist: Category[] = [];
  tourId!: number; // Lấy từ URL
  category!:Category
  tourT!: TourGet

  mainImagePreview: string | null = null;
  detailImagesPreview: string[] = [];
  newMainImage: File | null = null;
  newDetailImages: File[] = [];
  otherImageArray: string[] = [];

  constructor(
    private tourService: TourService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.tour={
    activities:[],
    bookings:[],
    category:this.category,
    dateStarts:[],
    detailImages:[],
    mainImage:'',
    reviews:[],
    tour:this.tourT
  };
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
    if(this.tour.tour.mainImage) alert('đã nhận được Main Image'); 
    if(this.tour.detailImages && this.tour.detailImages.length > 0) alert('đã nhận được detali Image'); 
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
  
}
