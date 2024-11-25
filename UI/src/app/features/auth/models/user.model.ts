import { StatusType } from "../../Booking/models/booking.model";

export interface User {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    roles: string;
    status: boolean;
    bookings?: Bookings[];
}

export interface Bookings {
    bookingId: number;
    bookingDate: Date;
    status: StatusType;
    notes: string;
    tourId: number;
}
