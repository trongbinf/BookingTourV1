import { Activity } from "../../activities/models/activity.model";
import { Booking } from "../../Booking/models/booking.model";
import { DateStart } from "../../dateStarts/models/date-start.model";
import { Category } from "../../category/model/category.model";
import { Review } from "../../reviews/model/review.model";

export interface Tour {
    tourId: number;
    tourName: string;
    description: string;
    mainImage?: string;
    otherImage?: string;
    city: string;
    country: string;
    isFullDay: boolean;
    price: number;
    vehicleType: string;
    created?: Date;
    status: boolean;
    category?: Category;
    dateStarts?: DateStart[];
    activities?: Activity[];
    bookings?: Booking[];
    reviews?: Review[];
}