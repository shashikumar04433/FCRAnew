$(function () {   
    $(document).on('change', 'input[name="RiskRangeParameter"]', function () { setPrametersVisibility(true); });
    $(document).on('change', 'input[name="IsExcludedInRisk"]', function () { setRiskRequirementVisibility(true); });
    $(document).on('change', '#StageId', loadRiskType);
    $(document).on('change', '#RiskTypeId', loadGeographicsPresence);
    $(document).on('change', '#GeographicPresenceId', loadCustomerSegment);
    $(document).on('change', '#CustomerSegmentId', loadBusinessSegment);
    $(document).on('change', '#BusinessSegmentId', loadRiskFactors);
    $(document).on('change', '#RiskFactorId', changeRiskFactor);
    setRiskRequirementVisibility(false);
    setPrametersVisibility(false);
    scaleTypeChange();
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
    showhideAttachmentMandatory();
    $(document).on('change', '#isAttachmentApplicable', isAttachmentApplicableChange);
});


function scaleTypeChange() {
    return;
    var scaletype = $('#ScaleType').val();
    $('.scaleType').addClass('d-none');
    $('.scaleType.scaleType' + scaletype).removeClass('d-none');
}


function setPrametersVisibility(isChange) {
    $('div.riskType').addClass('d-none');
    var selectedValue = $('input[name="RiskRangeParameter"]').val();
    var selectedValueRequirement = $('input[name="IsExcludedInRisk"]').val().toLowerCase();
    if (selectedValueRequirement == 'false')
        $('div.riskType' + selectedValue).removeClass('d-none');
    if (isChange) {
        $('div.riskType input,div.riskType select').val('');
        $('div.riskTypeParameter input[type="text"]').val('');
    }
}

function setRiskRequirementVisibility(isChange) {
    $('div.riskType,div.riskTypeParameter').addClass('d-none');
    var selectedValue = $('input[name="IsExcludedInRisk"]').val().toLowerCase();
    if (selectedValue == 'false') {
        $('div.riskType,div.riskTypeParameter').removeClass('d-none');
        setPrametersVisibility(isChange);
        if (isChange) {
            $('div.riskTypeParameter input[type="text"]').val('');
        }
    }
    scaleTypeChange();
}

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
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
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
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
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
    $('#RiskFactorId').html('');
    if (getIsExcluded($(this))) {
        loadRiskFactors();
        return;
    }
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

function loadRiskFactors() {
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
        function () { }
    )
}

function changeRiskFactor() {
    var that = $('#RiskFactorId option:selected');
    $('#IsExcludedInRisk').val(that.attr('data-ex'));
    $('#RiskRangeParameter').val(that.attr('data-param'));
    setRiskRequirementVisibility(true);
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

function showhideAttachmentMandatory() {
    if ($('#isAttachmentApplicable').is(":checked")) {
        $('#divMandatoryAttachment').show();
    }
    else {
        $('#divMandatoryAttachment').hide();
    }
}

function isAttachmentApplicableChange() {
    if ($(this).is(":checked")) {
        $('#divMandatoryAttachment').show();
    }
    else {
        $('#divMandatoryAttachment').hide();
        $('#isAttachmentMandatory').prop("checked", false);
    }
}