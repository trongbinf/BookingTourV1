import { User } from "../../auth/models/user.model";
import { Tour } from "../../Tour/models/tour.model";

export enum StatusType {
    Pending = 'Pending',
    Confirmed = 'Confirmed',
    Canceled = 'Canceled'
}
export interface CreateBooking {
    bookingDate: Date;
    notes?: string;
    status: StatusType;
    tourId: number;
    tour?: Tour;
    userId: string;
    user?: User;
}