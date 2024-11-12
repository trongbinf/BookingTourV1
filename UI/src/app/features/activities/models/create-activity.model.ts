export interface Activity {
    activityId: number;
    activityType: ActivityType;
    activityName: string;
    description: string;
    tourId: number;
}

export enum ActivityType {
    Services = 'Services',
    Rules = 'Rules',
    Schedule = 'Schedule'
}