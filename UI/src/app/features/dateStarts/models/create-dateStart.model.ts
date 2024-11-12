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
}