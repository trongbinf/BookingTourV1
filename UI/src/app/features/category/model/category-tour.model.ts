import { Tour } from "../../Tour/models/tour.model";

export interface CategoryTours {
    id: number,
    name: string,
    tours?: Tour[]
}