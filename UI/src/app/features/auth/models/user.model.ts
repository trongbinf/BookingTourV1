
export interface User {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    roles: string;
    status: boolean;
    bookings?: Bookings[];
}

export enum StatusType {
    Pending = 3,
    Confirmed = 2,
    Canceled = 1
}

export interface Bookings {
    bookingId: number;
    bookingDate: Date;
    status: StatusType;
    notes: string;
    tourId: number;
}