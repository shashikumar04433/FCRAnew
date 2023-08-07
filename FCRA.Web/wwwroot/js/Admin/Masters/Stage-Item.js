$(function () {
    scaleTypeChange();
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});

function scaleTypeChange() {
    var scaletype = $('#ScaleType').val();
    $('.scaleType').addClass('d-none');
    $('.scaleType.scaleType' + scaletype).removeClass('d-none');
}

function showAuditTrail() {
    var object = $('#hdTitle').val();
    var objectId = $('#hdId').val();
    fcraApp.postAjaxRequest(getAuditTrailUrl,
        {
            objectId: objectId, objectname: object
        },
        function (response) {
            $('#divAuditTrailModal tbody').html(response);
            $('#divAuditTrailModal').modal('show');
        });

}