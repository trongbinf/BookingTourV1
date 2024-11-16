import { Tour } from "../../Tour/models/tour.model";

export interface Activity {
    activityId: number;
    activityType: ActivityType;
    activityName: string;
    description: string;
    location: string;
    duration: string;
    tourId: number;
}

export enum ActivityType {
    Services = 'Services',
    Rules = 'Rules',
    Schedule = 'Schedule'
}