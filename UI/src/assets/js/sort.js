// Trạng thái sắp xếp hiện tại
const sortState = {
    column: null, // Cột đang sắp xếp
    isAscending: true // Hướng sắp xếp: true = tăng dần, false = giảm dần
};

function sortTable(column) {
    const tableBody = document.getElementById("table-body");
    const rows = Array.from(tableBody.querySelectorAll("tr"));

    // Cập nhật trạng thái sắp xếp
    if (sortState.column === column) {
        sortState.isAscending = !sortState.isAscending; // Đổi hướng sắp xếp
    } else {
        resetSortIcons(); // Reset icon khi chọn cột khác
        sortState.column = column;
        sortState.isAscending = true; // Mặc định tăng dần khi chuyển cột
    }

    const isAscending = sortState.isAscending;

    // Cập nhật icon sắp xếp
    updateSortIcon(column, isAscending);

    // Sắp xếp các hàng
    rows.sort((a, b) => {
        const cellA = a.querySelector(`.${column}`).textContent.trim();
        const cellB = b.querySelector(`.${column}`).textContent.trim();

        if (!isNaN(cellA) && !isNaN(cellB)) {
            // Nếu là số
            return isAscending ? cellA - cellB : cellB - cellA;
        } else {
            // Nếu là chuỗi
            return isAscending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
        }
    });

    // Gắn lại các hàng đã sắp xếp vào bảng
    rows.forEach(row => tableBody.appendChild(row));
}

// Cập nhật icon cho cột hiện tại
function updateSortIcon(column, isAscending) {
    const th = document.querySelector(`[data-sort="${column}"]`);
    if (th) {
        th.classList.remove("sort-default", "asc", "desc");
        th.classList.add(isAscending ? "asc" : "desc");
    }
}

// Reset icon về trạng thái mặc định
function resetSortIcons() {
    const allTh = document.querySelectorAll("th[data-sort]");
    allTh.forEach(th => {
        th.classList.remove("asc", "desc");
        th.classList.add("sort-default");
    });
}

// Thêm sự kiện click cho từng tiêu đề
document.querySelector('[data-sort="id"]').addEventListener("click", () => sortTable("id"));
document.querySelector('[data-sort="userName"]').addEventListener("click", () => sortTable("userName"));
document.querySelector('[data-sort="email"]').addEventListener("click", () => sortTable("email"));
