import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { TourService } from '../../../features/Tour/services/tour.service';
import { CategoryService } from '../../../features/category/services/category.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { filter, min, Subject, take, takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { CategoryTours } from '../../../features/category/model/category-tour.model';
import { PaginatedResponse } from '../../../features/Tour/models/paginated.model';
import { Tour } from '../../../features/Tour/models/tour.model';
import flatpickr from 'flatpickr';
import * as noUiSlider from 'nouislider';

@Component({
  selector: 'app-list-tour-search',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './list-tour-search.component.html',
  styleUrl: './list-tour-search.component.css'
})
export class ListTourSearchComponent implements OnInit, OnDestroy {
  @ViewChild('slider', { static: true }) slider!: ElementRef;
  searchParams: any = {};
  name: string = '';
  country: string[] = [];
  tours?: PaginatedResponse<Tour>;
  city?: string[] = [];
  categories: CategoryTours[] = [];
  category?: string = '';
  minPrice?: number = 50000;
  maxPrice?: number = 1000000000;
  selectedCountry: string = '';
  selectedCity: string = '';
  pageIndex?: number = 1;
  pageSize: number = 8;
  today: string = '';
  isFliter: boolean = false;
  activities: string[] = [];
  duration: string[] = [];
  private destroy$: Subject<void> = new Subject<void>();

  constructor(private tourSerive: TourService, private cateServive: CategoryService,
    private route: ActivatedRoute, private router: Router) {
  }



  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  ngOnInit(): void {
    this.initializeSlider();
    this.tourSerive.getAllCountry().pipe(takeUntil(this.destroy$)).subscribe(data => {
      this.country = data;
    });
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
    this.route.queryParams.pipe(takeUntil(this.destroy$)).subscribe(params => {
      this.searchParams = { ...params };
    });
    this.searchNow();
  }

  loadCategories() {
    this.cateServive.getAllCategories().pipe(takeUntil(this.destroy$)).subscribe(cate => {
      this.categories = cate;
    })
  }

  onCategoryChange(event: Event) {
    const selectedCate = (event.target as HTMLSelectElement).value;
    this.duration = [];
    this.activities = [];
    this.selectedCity = '';
    this.selectedCountry = '';
    this.pageIndex = 1;
    if (selectedCate) {
      // Chỉ cập nhật tham số category
      this.searchParams = { category: selectedCate };
      this.category = selectedCate;
    } else {
      // Nếu không chọn category, reset tham số category trong searchParams
      this.searchParams = { category: undefined };
    }
    // Cập nhật URL chỉ với tham số category
    if (this.isFliter) {
      this.updateUrl({ category: selectedCate }, true);
    } else {
      this.updateUrl({ category: selectedCate }, false);
    }
    this.searchNow();
  }

  searchNow() {
    const paramKeys = Object.keys(this.searchParams);
    if (paramKeys.length === 1 && paramKeys[0] === 'category') {
      this.category = this.searchParams.category;
      this.tourSerive.filterTour(this.selectedCity, this.selectedCountry, this.category,
        this.minPrice?.toString(), this.maxPrice?.toString(), this.duration, this.activities, this.pageIndex, this.pageSize).pipe(takeUntil(this.destroy$))
        .subscribe({
          next: response => {
            this.tours = response;
          },
          error: err => {
            console.log(err);
          }
        })
    } else {
      this.pageIndex = 1;
      console.log(this.searchParams.name);
      console.log(this.searchParams.dateRange);
      this.tourSerive.searchTour(this.searchParams.name, this.searchParams.dateRange, this.pageIndex, this.pageSize)
        .pipe(takeUntil(this.destroy$)).subscribe({
          next: response => {
            this.tours = response;
            if (this.isFliter) {
              this.updateUrl({ name: this.searchParams.name, dateRange: this.searchParams.dateRange }, false);
            } else {
              this.updateUrl({ name: this.searchParams.name, dateRange: this.searchParams.dateRange }, true);
            }
            this.isFliter = true;

          },
          error: err => {
            console.log(err);
          }
        })
    }
  }


  goToPage(page?: number) {
    this.pageIndex = page;
    this.searchNow();
  }

  goToPreviousPage() {
    if (this.tours?.currentPage && this.tours?.currentPage > 1) {
      this.pageIndex = this.tours.currentPage - 1;
      this.searchNow();
    }
  }

  goToNextPage() {
    if (this.tours?.currentPage && this.tours?.currentPage < this.tours.totalPages) {
      this.pageIndex = this.tours.currentPage + 1;
      this.searchNow();
    }
  }

  onCountryChange(event: Event) {
    const selectedCountry = (event.target as HTMLSelectElement).value;
    if (selectedCountry) {
      this.tourSerive.getCityByCountry(selectedCountry).subscribe({
        next: response => {
          this.city = response;
        },
        error: err => {
          console.log(err);
        }
      })
    }
  }

  updateUrl(newParams: { [key: string]: string }, change: boolean) {
    if (!change) {
      // Giữ lại các tham số cũ trong URL và merge tham số mới vào
      this.router.navigate([], {
        queryParams: { ...this.route.snapshot.queryParams, ...newParams },
        queryParamsHandling: 'merge',  // Merge tham số mới với cũ
      });
    } else {
      // Chỉ truyền tham số mới vào, reset các tham số khác trong URL
      this.router.navigate([], {
        queryParams: newParams,  // Chỉ truyền các tham số mới vào, không merge với tham số cũ
        queryParamsHandling: '', // Xóa các tham số cũ
      });
    }
  }

  updateSearchParam(key: string, value: any) {
    // Đảm bảo searchParams không bị freeze/seal
    if (Object.isExtensible(this.searchParams)) {
      if (key === 'dateRange' && Array.isArray(value)) {
        // Chuyển mảng ngày thành chuỗi range để lưu vào searchParams
        const formattedDateRange = value.join(' to '); // "dd-mm-yyyy to dd-mm-yyyy"
        this.searchParams = { ...this.searchParams, [key]: formattedDateRange };
      } else {
        // Sao chép object trước khi cập nhật
        this.searchParams = { ...this.searchParams, [key]: value };
      }
    } else {
      console.error('searchParams is not extensible');
    }
  }

  // show number page 
  getVisiablePage(): number[] {
    if (!this.tours?.totalPages || !this.tours.currentPage) {
      return [];
    }
    const totalPage = this.tours.totalPages;
    const currentPage = this.tours.currentPage;

    const startPage = Math.max(1, currentPage - 1);
    const endPage = Math.min(totalPage, currentPage + 1);

    const pages: number[] = [];
    for (let page = startPage; page <= endPage; page++) {
      pages.push(page);
    }
    return pages;
  }



  onActivityChange(activity: string) {
    const index = this.activities.indexOf(activity);
    if (index === -1) {
      this.activities.push(activity);  // Nếu chưa có, thêm vào mảng
    } else {
      this.activities.splice(index, 1);  // Nếu đã có, xóa khỏi mảng
    }
  }

  onDurationChange(day: string) {
    const index = this.duration.indexOf(day);
    if (index === -1) {
      this.duration.push(day);  // Nếu chưa có, thêm vào mảng
    } else {
      this.duration.splice(index, 1);  // Nếu đã có, xóa khỏi mảng
    }
  }


  // slider
  initializeSlider() {
    const sliderElement = this.slider.nativeElement;
    noUiSlider.create(sliderElement, {
      //! make sure that is not null or underfind
      start: [this.minPrice!, 10000000],
      connect: true,
      range: {
        min: 50000,
        max: 30000000,
      },
      step: 50000
    });

    // Lắng nghe sự kiện khi giá trị slider thay đổi
    sliderElement.noUiSlider?.on('update', (values: string[]) => {
      this.minPrice = parseFloat(values[0]);
      this.maxPrice = parseFloat(values[1]);
    });
  }



}
