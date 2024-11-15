export interface TourGet {
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
}