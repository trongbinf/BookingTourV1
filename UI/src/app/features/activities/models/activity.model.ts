import { Tour } from "../../Tour/models/tour.model";

export interface Activity {
    activityId: number;
    activityType: ActivityType;
    activityName: string;
    description: string;
    tourId: number;
    tour?: Tour;
}

export enum ActivityType {
    Services = 'Services',
    Rules = 'Rules',
    Schedule = 'Schedule'
}