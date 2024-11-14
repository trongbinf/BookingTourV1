import { Booking } from "../../Booking/models/booking.model";

export interface User {
    email: string;
    role: string;
    bookings: Booking[];
}