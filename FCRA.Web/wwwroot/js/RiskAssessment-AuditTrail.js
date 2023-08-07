$(document).on('click', '#btnAuditTrail', showAuditTrail);

function showAuditTrail() {
    debugger;
    var objectId = parseInt($('#hdCustomerId').val());
    fcraApp.postAjaxRequest(getAuditTrailUrl,
        {
            objectId: objectId
        },
        function (response) {
            debugger;
            $('#divAuditTrailModal tbody').html(response);
            $('#divAuditTrailModal').modal('show');
        });

}