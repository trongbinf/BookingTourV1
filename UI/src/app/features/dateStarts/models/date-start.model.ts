import { Tour } from "../../Tour/models/tour.model";


export enum TypeStatus {
    Pending = 'Pending',
    Confirmed = 'Confirmed',
    Canceled = 'Canceled'
}

export interface DateStart {
    dateStartId: number;
    startDate: Date;
    typeStatus: TypeStatus;
    tourId: number;
    tour?: Tour;
}