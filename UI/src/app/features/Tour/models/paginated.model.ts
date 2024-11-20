export interface PaginatedResponse<T> {
    items: T[]; // Danh sách các mục (ở đây là Tour[])
    currentPage: number; // Trang hiện tại
    totalCount: number; // Tổng số mục
    pageSize: number; // Số mục trên mỗi trang
    totalPages: number; // Tổng số trang
    hasPreviousPage: boolean; // Có trang trước không
    hasNextPage: boolean; // Có trang tiếp theo không
}
