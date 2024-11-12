import { Booking } from "../../Booking/models/booking.model";

export interface LoginResponse {
    token: string;
    email: string;
    role: string;
    bookings: Booking[]
}