import { User } from "../../auth/models/user.model";
import { TourGet } from "../../Tour/models/tour-get.model";

export enum StatusType {
    Submited = 0,      // Đã gửi
    Pending = 1,       // Đang chờ duyệt
    Confirmed = 2,     // Đã xác nhận
    Canceled = 3,      // Đã hủy
    Available = 4,     // Có sẵn
    Unavailable = 5    // Không khả dụng
}


export interface Booking {
    bookingId: number;
    bookingDate: string;
    pickDate: string;
    startTime: string;
    personNumber: number;
    notes?: string;
    status: StatusType;
    tour?: TourGet;
    userId: string;
    tourId: number;
    user?: User;
}

