export interface CretaReview {
    tourId: number;
    userId: string;
    rating: number;
    comment?: string;
    reviewDate: Date;
}