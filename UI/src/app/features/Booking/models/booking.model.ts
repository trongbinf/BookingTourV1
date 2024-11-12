import { User } from "../../auth/models/user.model";
import { Tour } from "../../Tour/models/tour.model";

export enum StatusType {
    Pending = 'Pending',
    Confirmed = 'Confirmed',
    Canceled = 'Canceled'
}

export interface Booking {
    bookingId: number;
    bookingDate: Date;
    notes?: string;
    status: StatusType;
    tour?: Tour;
    user?: User;
}