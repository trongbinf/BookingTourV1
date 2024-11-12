export interface Tour {
    tourId: number;
    tourName: string;
    description: string;
    mainImage?: string;
    otherImage?: string;
    city: string;
    country: string;
    isFullDay: boolean;
    price: number;
    vehicleType: string;
    created?: Date;
    status: boolean;
    categoryId: number;
    dateStarts?: [];
    activities?: [];
    bookings?: [];
    reviews?: [];
}