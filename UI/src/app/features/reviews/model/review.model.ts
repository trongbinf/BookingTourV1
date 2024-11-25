import { User } from "../../auth/models/user.model";
import { Booking, StatusType } from "../../Booking/models/booking.model";
import { Tour } from "../../Tour/models/tour.model";

export interface Review {
    reviewId: number;
    rating: number;
    comment?: string;
    reviewDate: Date;
    status: StatusType;
    tour?: Tour;
    user?: User;
    booking: Booking;
}