import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TourService } from '../services/tour.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tour-list',
  standalone: true,
  imports: [ CommonModule, RouterModule, FormsModule],
  templateUrl: './tour-list.component.html',
  styleUrl: './tour-list.component.css'
})
export class TourListComponent implements OnInit {
  tours: any[] = [];
  filteredTours: any[] = [];
  currentStatus: boolean = true; 

  sortedTours: any[] = [];

  currentSortField: string = '';
  currentSortDirection: string = 'asc';
  searchKeyword: string = '';

  constructor(private tourService: TourService) {}

  ngOnInit(): void {
    this.tourService.getAllTours().subscribe({
      next: (data) => {
        this.tours = data;
        console.log(this.tours); 
        this.filterTours(true); 
      },
      error: (err) => {
        console.error('Không lấy được dữ liệu', err);
      }
    });
  }
  searchTours(status: boolean): void {
    this.currentStatus = status;
    this.filteredTours = this.tours.filter((tour) => tour.tour.status === this.currentStatus);
    const keyword = this.searchKeyword.toLowerCase().trim(); 
    this.filteredTours = this.filteredTours.filter((tour) =>
      tour.tour.tourName.toLowerCase().includes(keyword) ||    
    tour.tour.city.toLowerCase().includes(keyword) ||       
    tour.tour.country.toLowerCase().includes(keyword) ||   
    tour.tour.duration.toLowerCase().includes(keyword) ||
    tour.tour.personNumber == keyword
    );
  }

  filterTours(status: boolean) {
    this.currentStatus = status;
    this.filteredTours = this.tours.filter((tour) => tour.tour.status === status);
  }

  sortData(field: string): void {
    if (this.currentSortField === field) {

      this.currentSortDirection = this.currentSortDirection === 'asc' ? 'desc' : 'asc';
    } else {

      this.currentSortDirection = 'asc';
      this.currentSortField = field;
    }

    this.sortedTours = [...this.tours].sort((a, b) => {
      const aValue = a[field];
      const bValue = b[field];

      if (aValue < bValue) {
        return this.currentSortDirection === 'asc' ? -1 : 1;
      }
      if (aValue > bValue) {
        return this.currentSortDirection === 'asc' ? 1 : -1;
      }
      return 0;
    });
  }
  onDelete(id: number): void {
    if (confirm('Are you sure you want to delete this tour?')) {
      this.tourService.deleteTour(id).subscribe({
        next: () => {
          alert('Tour has been deleted.');
        },
        error: (err) => {
          console.error('Error deleting tour:', err);
          alert('Failed to delete tour.');
        }
      });
    }
    this.ngOnInit();
  }
  onRestore(id: number): void {
    this.tourService.deleteTour(id).subscribe({
        next: () => {
          alert('Tour has been restore.');
        },
        error: (err) => {
          console.error('Error restore tour:', err);
          alert('Failed to restore tour.');
        }
      });
      this.ngOnInit();
  }
}
