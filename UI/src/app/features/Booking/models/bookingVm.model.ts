import { User } from "../../auth/models/user.model";
import { TourGet } from "../../Tour/models/tour-get.model";
import { Booking } from "./booking.model";

export interface BookingVM {
    booking: Booking;
    user: User;
    tour: TourGet;
}