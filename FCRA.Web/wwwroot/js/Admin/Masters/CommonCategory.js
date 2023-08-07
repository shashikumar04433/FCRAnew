$(function () {
    $(document).on('change', '#StageId', loadRiskType);
    $(document).on('change', '#RiskTypeId', loadGeographicsPresence);
    $(document).on('change', '#GeographicPresenceId', loadCustomerSegment);
    $(document).on('change', '#CustomerSegmentId', loadBusinessSegment);
    $(document).on('change', '#BusinessSegmentId', loadRiskFactors);
    $(document).on('change', '#RiskFactorId', changeRiskFactor);
});

function getIsExcluded(obj) {
    var ex = $(obj).find('option:selected').attr('data-ex');
    return ex === 'true';
}
function loadRiskType() {
    $('#RiskTypeId,#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
        return;
    }

    if ($('#RiskTypeId').length == 0)
        return;
    var id = $('#StageId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getRiskTypeOptionsUrl,
        { stageId: id },
        function (response) {
            $('#RiskTypeId').html(response).closest('div').removeClass('d-none');
        },
        function () { });
}

function loadGeographicsPresence() {
    $('#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
        return;
    }

    if ($('#GeographicPresenceId').length == 0)
        return;
    var id = $('#RiskTypeId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getGeographicPresenceOptionsUrl,
        { riskTypeId: id },
        function (response) {
            $('#GeographicPresenceId').html(response).closest('div').removeClass('d-none');
        },
        function () { });
}

function loadCustomerSegment() {
    $('#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
        return;
    }
    if ($('#CustomerSegmentId').length == 0)
        return;
    var id = $('#GeographicPresenceId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getCustomerSegmentOptionsUrl,
        { gId: id },
        function (response) {
            $('#CustomerSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { });
}

function loadBusinessSegment() {
    $('#BusinessSegmentId').html('').closest('div').addClass('d-none');
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
        return;
    }
    if ($('#BusinessSegmentId').length == 0)
        return;
    var id = $('#CustomerSegmentId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getBusinessSegmentOptionsUrl,
        { cId: id },
        function (response) {
            $('#BusinessSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { });
}

function loadRiskFactors() {
    if ($('#RiskFactorId').length == 0)
        return;
    $('#RiskFactorId').html('');
    var stage = null;
    var riskType = null;
    var geoPresence = null;
    var customerSegment = null;
    var businessSegment = null;
    stage = $('#StageId').val();
    if (!stage)
        return;
    if (!$('#RiskTypeId').closest('div').hasClass('d-none')) {
        riskType = $('#RiskTypeId').val();
    }

    if (!$('#GeographicPresenceId').closest('div').hasClass('d-none')) {
        geoPresence = $('#GeographicPresenceId').val();
    }

    if (!$('#CustomerSegmentId').closest('div').hasClass('d-none')) {
        customerSegment = $('#CustomerSegmentId').val();
    }
    if (!$('#BusinessSegmentId').closest('div').hasClass('d-none')) {
        businessSegment = $('#BusinessSegmentId').val();
    }

    fcraApp.getAjaxRequest(getRiskFactorOptionsUrl,
        { stageId: stage, riskTypeId: riskType, geoPresenceId: geoPresence, customerSegmentId: customerSegment, businessSegmentId: businessSegment },
        function (response) {
            $('#RiskFactorId').html(response);
        },
        function () { });
}

function changeRiskFactor() {
    var that = $('#RiskFactorId option:selected');
    $('#IsExcludedInRisk').val(that.attr('data-ex'));
    $('#RiskRangeParameter').val(that.attr('data-param'));
    
    loadSubFactor();
}

function loadSubFactor() {
    if ($('#RiskSubFactorId').length == 0)
        return;
    $('#RiskSubFactorId').html('');
    var id = $('#RiskFactorId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getRiskSubFactorOptionsUrl,
        { rId: id },
        function (response) {
            $('#RiskSubFactorId').html(response).closest('div').removeClass('d-none');
        },
        function () { });
}