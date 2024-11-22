import { Activity } from "../../activities/models/activity.model";
import { Booking } from "../../Booking/models/booking.model";
import { Category } from "../../category/model/category.model";
import { DateStart } from "../../dateStarts/models/date-start.model";
import { Review } from "../../reviews/model/review.model";
import { TourGet } from "./tour-get.model";




export interface TourVm {
    tour: TourGet;
    dateStarts: DateStart[];
    activities: Activity[];
    bookings: Booking[];
    reviews: Review[];
    category: Category;
    mainImage: string;
    detailImages: string[];
}