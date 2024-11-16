import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppUser } from '../../auth/models/appUser.model';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-manage-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-user.component.html',
  styleUrl: './manage-user.component.css'
})
export class ManageUserComponent implements OnInit, OnDestroy {


  appUsers$?: Observable<AppUser[]>
  appUserSubcription?: Subscription;
  roles: string[] = ['User', 'Admin'];
  selectedUserId: string = '';
  selectedRole: string = 'User';

  constructor(private authService: AuthService) {

  }

  ngOnDestroy(): void {
    this.appUserSubcription?.unsubscribe();
  }

  ngOnInit(): void {
    this.appUsers$ = this.authService.getAllUser();

    const modalElement = document.getElementById('forgotPasswordModal');

    if (modalElement) {
      modalElement.addEventListener('shown.bs.modal', (event: any) => {
        const triggerElement = event.relatedTarget;
        this.selectedUserId = triggerElement.getAttribute('data-user-id');
        console.log('Selected User ID:', this.selectedUserId);
      });
    }
  }

  blockUser(id: string, status: boolean) {
    const action = status ? 'chặn' : 'bỏ chặn';
    if (confirm(`Bạn có muốn ${action} người dùng này không?`)) {
      this.appUserSubcription = this.authService.blockUser(id)
        .subscribe({
          next: response => {
            this.appUsers$ = this.authService.getAllUser();
          }
        });
    }
  }

  setRole() {

    this.appUserSubcription = this.authService.setRole(this.selectedUserId, this.selectedRole)
      .subscribe({
        next: Response => {
          this.appUsers$ = this.authService.getAllUser();
        }
      });
  }
}