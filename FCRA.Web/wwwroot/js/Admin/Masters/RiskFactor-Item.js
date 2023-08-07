$(function () {
    $(document).on('change', '#StageId', loadRiskType);
    $(document).on('change', '#RiskTypeId', loadGeographicsPresence);
    $(document).on('change', '#GeographicPresenceId', loadCustomerSegment);
    $(document).on('change', '#CustomerSegmentId', loadBusinessSegment);
    $(document).on('change', 'input[name="IsExcludedInRisk"]', function () { setRiskRequirementVisibility(true); });
    scaleTypeChange();
    setRiskRequirementVisibility(false);
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});

function scaleTypeChange() {
    var scaletype = $('#ScaleType').val();
    $('.scaleType').addClass('d-none');
    $('.scaleType.scaleType' + scaletype).removeClass('d-none');
}

function getIsExcluded(obj) {
    var ex = $(obj).find('option:selected').attr('data-ex');
    return ex === 'true';
}

function loadRiskType() {
    $('#RiskTypeId,#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))){
        return;
    }
    var id = $('#StageId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getRiskTypeOptionsUrl,
        { stageId: id },
        function (response) {
            $('#RiskTypeId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadGeographicsPresence() {
    $('#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))){
        return;
    }
    var id = $('#RiskTypeId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getGeographicPresenceOptionsUrl,
        { riskTypeId: id },
        function (response) {
            $('#GeographicPresenceId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadCustomerSegment() {
    $('#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))){
        return;
    }
    var id = $('#GeographicPresenceId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getCustomerSegmentOptionsUrl,
        { gId: id },
        function (response) {
            $('#CustomerSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadBusinessSegment() {
    $('#BusinessSegmentId').html('').closest('div').addClass('d-none');
    var id = $('#CustomerSegmentId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getBusinessSegmentOptionsUrl,
        { cId: id },
        function (response) {
            $('#BusinessSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}


function setRiskRequirementVisibility(isChange) {
    $('div.riskTypeParameter').addClass('d-none');
    var selectedValue = $('input[name="IsExcludedInRisk"]:checked').val().toLowerCase();
    if (selectedValue == 'false') {
        $('div.riskTypeParameter').removeClass('d-none');
    }
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