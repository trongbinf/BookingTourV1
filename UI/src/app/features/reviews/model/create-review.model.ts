import { User } from "../../auth/models/user.model";
import { Tour } from "../../Tour/models/tour.model";

export interface CretaReview {
    tourId: number;
    userId: string;
    rating: number;
    comment?: string;
    reviewDate: Date;
    tour?: Tour;
    user?: User;
}