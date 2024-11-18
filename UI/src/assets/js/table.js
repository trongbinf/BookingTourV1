/* global XLSX */

var btnXlsx = document.querySelectorAll('.action')[0];

btnXlsx.onclick = () => exportData('xlsx');

function exportData(type) {
    const fileName = 'exported-sheet.' + type;
    const table = document.getElementById("tableExcel");
    const wb = XLSX.utils.table_to_book(table);
    XLSX.writeFile(wb, fileName);
}