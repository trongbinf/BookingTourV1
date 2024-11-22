import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { ForgotPasswordComponent } from './features/auth/forgot-password/forgot-password.component';
import { ChangepasswordComponent } from './features/auth/changepassword/changepassword.component';

import { TourListComponent } from './features/Tour/tour-list/tour-list.component';
import { HomeComponent } from './features/home/home.component';
import { TourDetailComponent } from './features/tour-detail/tour-detail.component';
import { ListTourSearchComponent } from './core/list-tour-search/list-tour-search/list-tour-search.component';
import { ManageUserComponent } from './features/manage/manage-user/manage-user.component';
import { ProfileComponent } from './features/auth/profile/profile.component';
import { TourAddComponent } from './features/Tour/tour-add/tour-add.component';
import { TourUpdateComponent } from './features/Tour/tour-update/tour-update.component';

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
        path: 'admin-tour',
        component: TourListComponent
    }
    ,
    {
        path: 'admin-addtour',
        component: TourAddComponent
    }
    ,
    {
        path: 'admin-tourdetails/:id',
        component: TourUpdateComponent
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
    },
    {
        path: 'search',
        component: ListTourSearchComponent
    }
    ,
    {
        path: 'adminUser',
        component: ManageUserComponent
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
    }
];
