import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isLoginedIn()) {
    const user = authService.getUser();
    console.log(user?.email + "+" + user?.roles)
    if (user && user.roles === 'Admin') {
      return true;
    } else {
      // Nếu không phải admin, chuyển hướng về trang không có quyền truy cập
      router.navigateByUrl('/admin/posts');
      return false;
    }
  } else {
    // Chuyển hướng về trang đăng nhập nếu chưa đăng nhập
    router.navigateByUrl('/login');
    return false;
  }
};