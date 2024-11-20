import { Category } from '../../category/model/category.model';
import { Activity } from '../../activities/models/activity.model';
import { Booking } from '../../Booking/models/booking.model';
import { Review } from '../../reviews/model/review.model';
import { DateStart } from '../../dateStarts/models/date-start.model';

export interface CreateTour {
  tourId?: number;
  tourName: string;
  description: string;
  mainImage?: File | null;
  detailImages?: File[] | null;
  city: string;
  country: string;
  duration: string;
  isFullDay: boolean;
  price: number;
  personNumber: number;
  status: boolean;
  categoryId: number;
  dateStarts?: DateStart[] | null;
  activities?: Activity[] | null;
  bookings?: Booking[] | null;
  reviews?: Review[] | null;
  category?: Category;
}
