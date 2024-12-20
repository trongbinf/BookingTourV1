export enum StatusType {
    Submited,     // Đã gửi
    Pending,      // Đang chờ duyệt
    Confirmed,    // Đã xác nhận
    Canceled,     // Đã hủy
    Available,
    Unavailable,        // Có sẵn
    Completed     // Đã hoàn thành
}

export interface CreateBooking {
    pickDate: string;
    startTime: string;
    bookingDate: string;
    personNumber: number;
    notes?: string;
    status: StatusType;
    tourId: number;
    userId: string | null;
}





