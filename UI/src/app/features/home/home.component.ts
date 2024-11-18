import { Component, model, OnDestroy, OnInit } from '@angular/core';
import { Category } from '../category/model/category.model';
import { Tour } from '../Tour/models/tour.model';
import { TourService } from '../Tour/services/tour.service';
import { CategoryService } from '../category/services/category.service';
import { CategoryTours } from '../category/model/category-tour.model';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { PaginatedResponse } from '../Tour/models/paginated.model';
import flatpickr from 'flatpickr'
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  categories: CategoryTours[] = [];
  tours?: PaginatedResponse<Tour>;
  name: string = '';
  pageSize: number = 6;
  pageIndex: number = 1;
  total: number = 0;
  today: string = '';
  private destroy$ = new Subject<void>();
  searchData = {
    place: '',
    date: ''
  };

  constructor(private tourService: TourService, private cateService: CategoryService,
    private router: Router
  ) {

  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit(): void {
    this.loadCategories();
    const todayDate = new Date();
    const year = todayDate.getFullYear();
    const month = ('0' + (todayDate.getMonth() + 1)).slice(-2);
    const day = ('0' + todayDate.getDate()).slice(-2);
    this.today = `${year}-${month}-${day}`;

    flatpickr('.date-picker', {
      mode: 'range',
      minDate: this.today,
      dateFormat: 'd-m-Y'
    });

  }

  loadCategories() {
    this.cateService.getAllCategories().pipe(takeUntil(this.destroy$)).subscribe(cate => {
      this.categories = cate;
      if (this.categories.length > 0) {
        this.name = this.categories[0].name;
        this.tourService.getTourByCategory(this.name, this.pageSize, this.pageIndex).pipe(takeUntil(this.destroy$)).subscribe(tour => {
          this.tours = tour;
          console.log(this.name);
        })

      }
    });

  }

  loadToursByCategory(nameTour: string) {
    this.name = nameTour;
    this.tourService.getTourByCategory(this.name, this.pageSize, this.pageIndex).pipe(takeUntil(this.destroy$)).subscribe(tour => {
      this.tours = tour;
    });
    console.log(this.name);

  }

  onSearch() {
    this.router.navigate(['/search'], {
      queryParams: { name: this.searchData.place, dateRange: this.searchData.date }
    });
  }

  showMore(name: string) {
    this.router.navigate(['/search'], {
      queryParams: { category: name }
    }).then(() => {
      // This will reload the page after navigating
      window.location.reload();
    });
  }

}
