﻿namespace BookingTour.Model.ViewModel
{
    public class ChangePasswordVm
    {
        public string Email {  get; set; }
        public string OldPassword { get; set; }       
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
