import { Component, OnDestroy, OnInit } from '@angular/core';
import { Category } from '../category/model/category.model';
import { Tour } from '../Tour/models/tour.model';
import { TourService } from '../Tour/services/tour.service';
import { CategoryService } from '../category/services/category.service';
import { CategoryTours } from '../category/model/category-tour.model';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  categories: CategoryTours[] = [];
  tours: Tour[] = [];
  private destroy$ = new Subject<void>();


  constructor(private tourService: TourService, private cateService: CategoryService) {

  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.cateService.getAllCategories().pipe(takeUntil(this.destroy$)).subscribe(cate => {
      this.categories = cate;
      if (this.categories.length > 0) {
        var first = this.categories[0].name;
        this.tourService.getTourByCategory(first).pipe(takeUntil(this.destroy$)).subscribe(tour => {
          this.tours = tour;
          this.tours.forEach(s => console.log(s.tour.tourName));
          console.log(first);
        })

      }
    });

  }

  loadToursByCategory(name: string) {
    this.tourService.getTourByCategory(name).pipe(takeUntil(this.destroy$)).subscribe(tour => {
      this.tours = tour;
    })
  }

}
