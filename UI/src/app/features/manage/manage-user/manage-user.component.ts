import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { User } from '../../auth/models/user.model';

@Component({
  selector: 'app-manage-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-user.component.html',
  styleUrl: './manage-user.component.css'
})
export class ManageUserComponent implements OnInit, OnDestroy {

  appUsers$?: Observable<User[]>
  key: string = '';
  appUserSubcription?: Subscription;
  roles: string[] = ['User', 'Admin'];
  selectedUserId: string = '';
  selectedRole: string = 'User';

  currentPage: number = 1;
  totalCount: number = 0;
  pageSize: number = 3;
  totalPages: number = 0;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;

  isViewAll: boolean = false;

  constructor(private authService: AuthService) {
  }

  ngOnDestroy(): void {
    this.appUserSubcription?.unsubscribe();
  }

  ngOnInit(): void {
    this.fetchUsers();
  }

  setSelectedUserId(userId: string): void {
    this.selectedUserId = userId;
  }

  fetchUsers(): void {
    this.authService.searchUser(this.key, this.currentPage, this.pageSize).subscribe({
      next: response => {
        // Cập nhật danh sách người dùng
        this.appUsers$ = new Observable(subscriber => subscriber.next(response.items));

        // Cập nhật thông tin phân trang
        this.currentPage = response.currentPage;
        this.totalCount = response.totalCount;
        this.pageSize = response.pageSize;
        this.totalPages = response.totalPages;
        this.hasPreviousPage = response.hasPreviousPage;
        this.hasNextPage = response.hasNextPage;
      },
      error: err => {
        console.error('Error fetching users:', err);
      }
    });
  }

  blockUser(id: string, status: boolean) {
    const action = status ? 'chặn' : 'bỏ chặn';
    if (confirm(`Bạn có muốn ${action} người dùng này không?`)) {
      this.appUserSubcription = this.authService.blockUser(id)
        .subscribe({
          next: response => {
            this.fetchUsers();
          }
        });
    }
  }

  setRole() {
    this.appUserSubcription = this.authService.setRole(this.selectedUserId, this.selectedRole)
      .subscribe({
        next: Response => {
          this.fetchUsers();
        }
      });
  }

  searchUser(event?: Event): void {
    if (event) {
      event.preventDefault(); // Ngăn form submit gây reload
    }

    this.currentPage = 1; // Reset về trang đầu tiên khi tìm kiếm
    this.fetchUsers();
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.fetchUsers();
  }
  updatePageSize(): void {
    if (this.pageSize < 1) {
      this.pageSize = 1; // Đặt giá trị tối thiểu nếu nhập số nhỏ hơn 1
    }

    this.currentPage = 1; // Reset về trang đầu tiên khi thay đổi số phần tử
    this.fetchUsers(); // Tải lại dữ liệu với kích thước trang mới
  }

  toggleView(): void {
    this.isViewAll = !this.isViewAll;

    if (this.isViewAll) {
      this.pageSize = this.totalCount; // Hiển thị tất cả
    } else {
      this.pageSize = 3;
    }
    this.currentPage = 1; // Reset về trang đầu tiên
    this.fetchUsers(); // Cập nhật danh sách
  }
}
