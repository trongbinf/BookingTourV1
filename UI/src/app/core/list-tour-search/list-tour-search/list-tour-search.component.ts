import { Component, OnDestroy, OnInit } from '@angular/core';
import { TourService } from '../../../features/Tour/services/tour.service';
import { CategoryService } from '../../../features/category/services/category.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { filter, Subject, takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { CategoryTours } from '../../../features/category/model/category-tour.model';
import { PaginatedResponse } from '../../../features/Tour/models/paginated.model';
import { Tour } from '../../../features/Tour/models/tour.model';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-list-tour-search',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './list-tour-search.component.html',
  styleUrl: './list-tour-search.component.css'
})
export class ListTourSearchComponent implements OnInit, OnDestroy {
  searchParams: any = {};
  name: string = '';
  fliterParams: any = {};
  country: string[] = [];
  tours?: PaginatedResponse<Tour>;
  city?: string[] = [];
  categories: CategoryTours[] = [];
  category?: string = '';
  minPrice?: number;
  maxPrice?: number;
  selectedCountry: string = '';
  selectedCity: string = '';
  pageIndex?: number = 1;
  pageSize: number = 8;
  today: string = '';
  isFliter: boolean = false;
  private destroy$: Subject<void> = new Subject<void>();

  constructor(private tourSerive: TourService, private cateServive: CategoryService,
    private route: ActivatedRoute, private router: Router) {
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  ngOnInit(): void {
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
    if (this.searchParams.category == undefined) {
      this.updateUrl({ name: '' }, false);
    }
    this.searchNow();
  }

  searchNow() {
    const paramKeys = Object.keys(this.searchParams); // Lấy danh sách các key của searchParams
    // Nếu chỉ có duy nhất một key và đó là 'category'
    if (paramKeys.length === 1 && paramKeys[0] === 'category') {
      const category = this.searchParams.category;
      if (!category) {
        this.tourSerive.getTourSearch('', '', '', '', '', '', '', this.pageIndex, this.pageSize)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: response => {
              this.tours = response;
              // Cập nhật URL để xóa mọi tham số, đưa về trạng thái ban đầu
              this.updateUrl({ category: '' }, true);
            },
            error: err => console.log(`Err: ${err}`)
          });
      } else {
        this.tourSerive.getTourByCategory(category, this.pageSize, this.pageIndex)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: response => {
              this.category = category;
              this.tours = response;
              // Cập nhật URL sau khi tìm kiếm xong
              if (this.isFliter) {
                this.updateUrl({ category: `${category}` }, true);
              } else {
                this.updateUrl({ category: `${category}` }, false);
              }
              this.isFliter = false;
            },
            error: err => console.log(err)
          });
      }
    }
    else {
      // Nếu có nhiều key hơn hoặc không chỉ chứa 'category'
      const {
        name = '',
        city = '',
        country = '',
        dateRange = '',
        categories = '',
        minPrice = '',
        maxPrice = '',
        pageIndex = 1,
        pageSize = 8
      } = this.searchParams;
      this.tourSerive.getTourSearch(name, city, country, categories, minPrice, maxPrice, dateRange, pageIndex, pageSize)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: response => {
            this.tours = response;
            // Cập nhật URL sau khi tìm kiếm xong
            if (this.isFliter) {
              this.updateUrl({ name: `${name}`, dateRange: `${dateRange}` }, false);
            } else {
              this.updateUrl({ name: `${name}`, dateRange: `${dateRange}` }, true);
            }
            this.isFliter = true;
          },
          error: err => console.log(`Err: ${err}`)
        });
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
          console.log(response);
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



}
