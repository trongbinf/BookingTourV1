import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { ForgotPasswordComponent } from './features/auth/forgot-password/forgot-password.component';
import { ChangepasswordComponent } from './features/auth/changepassword/changepassword.component';

import { TourListComponent } from './features/Tour/tour-list/tour-list.component';
import { HomeComponent } from './features/home/home.component';
import { TourDetailComponent } from './features/tour-detail/tour-detail.component';
import { ManageUserComponent } from './features/manage/manage-user/manage-user.component';
import { ProfileComponent } from './features/auth/profile/profile.component';
import { ManagerBookingComponent } from './features/manage/manager-booking/manager-booking.component';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    }
    ,
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'reset-password',
        component: ForgotPasswordComponent
    }
    ,
    {
        path: 'changepass',
        component: ChangepasswordComponent
    },
    {
        path: 'tour',
        component: TourListComponent
    }
    ,
    {
        path: '',
        component: HomeComponent
    }
    ,
    {
        path: 'tour-detail/:id',
        component: TourDetailComponent
    }
    ,
    {
        path: 'adminUser',
        component: ManageUserComponent
    }
    ,
    {
        path: 'profile',
        component: ProfileComponent
    },
    {
        path: 'adminBooking',
        component: ManagerBookingComponent
    }
    ,
];
