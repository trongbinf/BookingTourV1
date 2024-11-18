import { User } from "../../auth/models/user.model";
import { Tour } from "../../Tour/models/tour.model";

export enum StatusType {
    Submited,     // Đã gửi
    Pending,      // Đang chờ duyệt
    Confirmed,    // Đã xác nhận
    Canceled,     // Đã hủy
    Available,    // Có sẵn
    Unavailable   // Không khả dụng
}


export interface Booking {
    bookingId: number;
    bookingDate: string;
    pickDate: string;
    startTime: string;
    personNumber: number;
    notes?: string;
    status: StatusType;
    tour?: Tour;
    userId: string;
    user?: User;
}