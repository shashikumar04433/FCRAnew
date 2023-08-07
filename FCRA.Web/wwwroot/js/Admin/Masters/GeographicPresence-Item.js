$(function () {
    $(document).on('change', '#StageId', loadRiskType);
    scaleTypeChange();
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});

function scaleTypeChange() {
    var scaletype = $('#ScaleType').val();
    $('.scaleType').addClass('d-none');
    $('.scaleType.scaleType' + scaletype).removeClass('d-none');
}

function loadRiskType() {
    $('#RiskTypeId').html('');
    var id = $('#StageId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getRiskTypeOptionsUrl,
        { stageId: id },
        function (response) {
            $('#RiskTypeId').html(response);
        },
        function () { }
    )
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