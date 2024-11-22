import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { User } from '../../auth/models/user.model';
import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';

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

  // exportAsExcelFile(data: any[], fileName: string): void {
  //   // Tạo WorkSheet từ dữ liệu
  //   const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);

  //   // Tạo Workbook và thêm WorkSheet vào
  //   const wb: XLSX.WorkBook = XLSX.utils.book_new();
  //   XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

  //   // Ghi file Excel dưới dạng buffer
  //   const excelBuffer: any = XLSX.write(wb, {
  //     bookType: 'xlsx',
  //     type: 'array',
  //   });

  //   // Lưu file
  //   this.saveAsExcelFile(excelBuffer, fileName);
  // }

  // private saveAsExcelFile(buffer: any, fileName: string): void {
  //   const data: Blob = new Blob([buffer], {
  //     type: 'application/octet-stream',
  //   });
  //   FileSaver.saveAs(data, `${fileName}_export_${new Date().getTime()}.xlsx`);
  // }

  exportTable(): void {
    // Lấy bảng HTML bằng ID
    const table = document.getElementById('tableExcel');

    // Chuyển bảng HTML thành WorkSheet
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(table);

    // Lấy tất cả các hàng trong bảng HTML
    const rows = table?.querySelectorAll('tbody tr');

    if (rows) {
      rows.forEach((row, rowIndex) => {
        // Lấy giá trị từ thẻ <input type="hidden"> trong cột "status"
        const hiddenInput = row.querySelector('input[name="status"]') as HTMLInputElement;

        // Nếu có thẻ <input>, thay giá trị ẩn vào WorkSheet
        if (hiddenInput && ws) {
          const cellAddress = XLSX.utils.encode_cell({ r: rowIndex + 1, c: 4 }); // Cột "status" ở vị trí c=4
          ws[cellAddress] = { v: hiddenInput.value, t: 's' }; // Cập nhật giá trị và kiểu dữ liệu
        }
      });
    }

    // Loại bỏ cột cuối cùng (nếu cần)
    const range = XLSX.utils.decode_range(ws['!ref']!); // Lấy phạm vi dữ liệu của bảng
    range.e.c -= 1; // Giảm chỉ số cột kết thúc (e.c) để loại bỏ cột cuối
    ws['!ref'] = XLSX.utils.encode_range(range); // Cập nhật phạm vi dữ liệu mới

    // Tạo Workbook
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    // Xuất file Excel
    XLSX.writeFile(wb, `html_table_export_${new Date().getTime()}.xlsx`);
  }

}