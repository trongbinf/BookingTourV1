export enum StatusType {
    Submited,     // Đã gửi
    Pending,      // Đang chờ duyệt
    Confirmed,    // Đã xác nhận
    Canceled,     // Đã hủy
    Available,    // Có sẵn
    Unavailable   // Không khả dụng
}

export interface CreateBooking {
    pickDate: string;
    startTime: string;
    bookingDate: string;
    personNumber: number;
    notes?: string;
    status: StatusType;
    tourId: number;
    userId: string;
}





