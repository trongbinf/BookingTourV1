export interface ActivityAdd {
    activityType: ActivityType;
    activityName: string;
    description: string;
    location: string;
    duration: string;
    tourId: number;
}

export enum ActivityType {
    Services,
    Rules,
    Schedule
}