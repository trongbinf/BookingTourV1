export interface PagedResponse<T> {
    items: T[];
    currentPage: number;
    totalCount: number;
    pageSize: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}