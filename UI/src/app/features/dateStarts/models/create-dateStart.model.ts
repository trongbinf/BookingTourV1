export enum TypeStatus {
    Submited = 0,     // Đã gửi 0
    Pending = 1,      // Đang chờ duyệt 1
    Confirmed = 2,    // Đã xác nhận 2
    Canceled = 3,     // Đã hủy 3
    Available = 4,    // Có sẵn 4
    Unavailable = 5 // Không khả dụng 5
}

export interface DateStart {
    dateStartId: number;
    startDate: Date;
    typeStatus: TypeStatus;
    tourId: number;
}

