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
    userId: string;
}