import { Category } from "../../category/model/category.model";

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
    personNumber: number;
    duration: string;
    created?: Date;
    status: boolean;
    category?: Category;
}