import { Booking } from "../../Booking/models/booking.model";

export interface User {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    roles: string;
    status: boolean;
    bookings?: Booking[];
}

export interface Bookings {
    bookingId: number;
    bookingDate: Date;
    status: StatusType;
    notes: string;
    tourId: number;
}
