import { Activity } from "../../activities/models/activity.model";
import { Booking } from "../../Booking/models/booking.model";
import { DateStart } from "../../dateStarts/models/date-start.model";
import { Category } from "../../category/model/category.model";
import { Review } from "../../reviews/model/review.model";
import { TourGet } from "./tour-get.model";

export interface Tour {
    tour: TourGet;
    category: Category;
    dateStarts: DateStart[];
    activities: Activity[];
    bookings: Booking[];
    reviews: Review[];
}