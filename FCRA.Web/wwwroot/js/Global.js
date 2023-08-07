$(window).on('load', function () {
  
});

$(function () {
    //loadUserNotifications();
    $(document).on('click', '.menu-title', function () {
        $(this).closest('li').find('ul.submenu').toggleClass('d-none');
        $(this).find('span i').toggleClass('d-none');
    });
    $(document).on('click', '.submenu-title', function () {
        $(this).closest('li').find('ul.subsubmenu').toggleClass('d-none');
        $(this).find('span i').toggleClass('d-none');
    });
    $(document).on('click', '.aExitRemarks', showExitRemarksModal);
    $(document).on('click', '#btnExitRemarksSaveModal', saveExitRemarksLog);
});

function loadUserNotifications() {
    //fcraApp.getAjaxRequest(userNotificationGetUrl, null, function (response) {
    //    $('#liDropDownNotification').html(response);
    //});


}

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})();

function showExitRemarksModal() {
    $('#divAddExitRemarksModal #txtExitRemarks').val('')
    $('#divAddExitRemarksModal').modal('show');
}

function saveExitRemarksLog() {
    if (!$('#divAddExitRemarksModal #txtExitRemarks').val()) {
        fcraApp.showToast('Alert', 'Please enter remarks');
        return;
    }
    fcraApp.postAjaxRequest(addExitRemarksUrl,
        { remarks: $('#divAddExitRemarksModal #txtExitRemarks').val() },
        function (response) {
            if (response == 1) {
                fcraApp.showToast('Alert', 'Remarks added successfully!');
                $('#divAddExitRemarksModal').modal('hide');
            }
            if (response == -1) {
                fcraApp.showToast('Alert', 'Please enter remarks');
            }
            if (response == 0) {
                fcraApp.showToast('Alert', 'Please try again.');
            }
        }
    );
}