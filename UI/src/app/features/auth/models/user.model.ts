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




