import { Component, OnInit } from '@angular/core';
import { TourService } from '../services/tour.service';
import { Router } from '@angular/router';
import { CreateTour } from '../models/create-tour.model';
import { FormsModule } from '@angular/forms';
import { Category } from '../../category/model/category.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tour-add',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tour-add.component.html',
  styleUrl: './tour-add.component.css'
})
export class TourAddComponent {
  tour: CreateTour = {
    tourName: '',
    description: '',
    city: '',
    country: '',
    duration: '',
    price: 0,
    personNumber: 0,
    isFullDay: false,
    status: true,
    categoryId: 1,
    dateStarts: null,
    activities: null,
    bookings: null,
    reviews: null,
    mainImage: null,
    detailImages: []
  };
  catelist: Category[]= [];

  mainImagePreview: string | null = null;
  detailImagesPreview: string[] = [];

  constructor(private tourService: TourService, private router: Router) {}

  ngOnInit(): void{
    this.tourService.getCategories().subscribe({
      next: (data) => {
        this.catelist = data;  
        console.log(this.catelist);
      },
      error: (err) => {
        console.error('Error fetching categories:', err);
        alert('Failed to load categories.');
      }
    });
  }

  onSubmit() {
    if (!this.tour.tourName || !this.tour.city || !this.tour.country || !this.tour.duration || !this.tour.description) {
    alert('Please fill in all required fields.');
    if(this.tour.mainImage) alert('đã nhận được Main Image'); 
    if(this.tour.detailImages && this.tour.detailImages.length > 0) alert('đã nhận được detali Image'); 
    return;
    }
  const formData = new FormData();

  formData.append('TourName', this.tour.tourName);
  formData.append('Description', this.tour.description);
  formData.append('City', this.tour.city);
  formData.append('Country', this.tour.country);
  formData.append('Duration', this.tour.duration);
  formData.append('IsFullDay', this.tour.isFullDay.toString());
  formData.append('Price', this.tour.price.toString());
  formData.append('PersonNumber', this.tour.personNumber.toString());
  formData.append('Status', this.tour.status.toString());
  formData.append('CategoryId', this.tour.categoryId.toString());
  
  if (this.tour.mainImage) {
    formData.append('MainImage', this.tour.mainImage, this.tour.mainImage.name);
  }
  
  if (this.tour.detailImages && this.tour.detailImages.length > 0) {
    for (let file of this.tour.detailImages) {
      formData.append('DetailImages', file, file.name);
    }
  }
    this.tourService.createTour(formData).subscribe({
    next: (response) => {
      alert('Tour added successfully!');
      this.router.navigate(['/admin-tour']);  
    },
    error: (err) => {
      console.error('Error adding tour:', err);
      alert('Failed to add tour.');
    }
  });
  }

  discardChanges() {
    this.tour = { 
      tourName: '', 
      description: '', 
      city: '', 
      country: '', 
      duration: '', 
      price: 0, 
      personNumber: 0, 
      isFullDay: false, 
      status: true, 
      categoryId: 1,
      mainImage: null, 
    detailImages: []
    };
    this.mainImagePreview = null;
  this.detailImagesPreview = [];
  }

  onMainImageChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.tour.mainImage = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.mainImagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  onDetailImagesChange(event: any) {
    const files = event.target.files;
    this.tour.detailImages = Array.from(files);
    this.detailImagesPreview = [];

    for (let file of files) {
      const reader = new FileReader();
      reader.onload = () => {
        this.detailImagesPreview.push(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }

}
