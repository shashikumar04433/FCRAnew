var oTable = null;
$(function () {
    oTable = $('#tblQuestions').DataTable({
        rowCallback: function (row, data, displayNum, displayIndex, dataIndex) {
            $('td:first', row).html(displayIndex + 1);
        },
});
    $(document).on('change', '#ddlProducts', filterGrid);
});

function filterGrid() {
    if (oTable) {
        if ($('#ddlProducts').val())
            oTable.column(1).search($('#ddlProducts option:selected').text()).draw();
        else
            oTable.search('').draw();
    }
}