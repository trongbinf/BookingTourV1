import { Tour } from "../../Tour/models/tour.model";

export interface Activity {
    activityId: number;
    activityType: ActivityType;
    activityName: string;
    description: string;
    tour?: Tour;
}

export enum ActivityType {
    Services = 'Services',
    Rules = 'Rules',
    Schedule = 'Schedule'
}