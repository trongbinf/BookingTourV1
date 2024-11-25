import { StatusType } from "../../Booking/models/booking.model";

export interface CretaReview {
    tourId?: number | null;
    userId: string | null;
    bookingId?: number | null;
    rating: number;
    comment?: string;
    reviewDate: string;
    status: StatusType;
}