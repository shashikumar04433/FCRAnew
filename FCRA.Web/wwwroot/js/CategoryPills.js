var isDataChanged = false;
var isIgnoreDataChange = false;
var dataSaveAlert = 'You have some unsaved changes, are you sure to continue?';
$(function () {
    ShowHideRiskAssessment();
    $(document).on('click', 'div.divCategoryGroup button', changeCategoryButtonSatate);
    $(document).on('click', 'button.stage', loadRiskTypes);
    $(document).on('click', 'button.risktype', loadGeographicsPresence);
    $(document).on('click', 'button.geoPresence', loadCustomerSegment);
    $(document).on('click', 'button.customersegment', loadBusinessSegment);
    $(document).on('click', 'button.businesssegment', loadCategoryContainerContent);
    setTimeout(function () { 
        $('button.stage:first').click();
    }, 50);
})

function ShowHideRiskAssessment() {
    var buttonCount = $('.divCategoryGroup').find('button').length;
    if (buttonCount == 0) {
        $('.divCategoryGroup').html('<h3>No record found.</h3>')
    }
}

function loadRiskTypes() {
    if (isDataChanged) {
        if (!confirm(dataSaveAlert))
            return;
    }
    isDataChanged = false;
    $('#divRiskResponseContainer,.cardcontainer.risktype,.cardcontainer.geopresence,.cardcontainer.customersegment,.cardcontainer.businesssegment').html('')
    var isExclude = $(this).attr('data-ex');
    if (isExclude == 'true') {
        loadCategoryContainerContent();
        return;
    }
    var idEnc = $(this).attr('data-id');
    fcraApp.postAjaxRequest(getRiskTypeListUrl,
        { xid: idEnc, hasProgress: bHasCategoryProgress },
        function (response) {
            $('.cardcontainer.risktype').html(response);
            if ($('.cardcontainer.risktype button').length == 0) {
                $('.cardcontainer.risktype').html('<h3>Unauthorized Access.</h3>');
                return;
            }
            $('.cardcontainer.risktype button.risktype:first').click();
        }
    );
}

function loadGeographicsPresence() {
    if (isDataChanged) {
        if (!confirm(dataSaveAlert))
            return;
    }
    isDataChanged = false;
    $('#divRiskResponseContainer,.cardcontainer.geopresence,.cardcontainer.customersegment,.cardcontainer.businesssegment').html('')
    var isExclude = $(this).attr('data-ex');
    if (isExclude == 'true') {
        loadCategoryContainerContent();
        return;
    }
    var idEnc = $(this).attr('data-id');
    fcraApp.postAjaxRequest(getGeographicsPresenceListUrl,
        { xid: idEnc, hasProgress: bHasCategoryProgress },
        function (response) {
            $('.cardcontainer.geopresence').html(response);
            if ($('.cardcontainer.geopresence button').length == 0) {
                $('.cardcontainer.geopresence').html('<h3>Unauthorized Access.</h3>');
                return;
            }
            $('.cardcontainer.geopresence button.geoPresence:first').click();
        }
    );
}

function loadCustomerSegment() {
    if (isDataChanged) {
        if (!confirm(dataSaveAlert))
            return;
    }
    isDataChanged = false;
    $('#divRiskResponseContainer,.cardcontainer.customersegment,.cardcontainer.businesssegment').html('')
    var isExclude = $(this).attr('data-ex');
    if (isExclude == 'true') {
        loadCategoryContainerContent();
        return;
    }
    var idEnc = $(this).attr('data-id');
    fcraApp.postAjaxRequest(getCustomerSegmentListUrl,
        { xid: idEnc, hasProgress: bHasCategoryProgress },
        function (response) {
            $('.cardcontainer.customersegment').html(response);
            if ($('.cardcontainer.customersegment button').length == 0) {
                $('.cardcontainer.customersegment').html('<h3>Unauthorized Access.</h3>');
                return;
            }
            $('.cardcontainer.customersegment button.customersegment:first').click();
        }
    );
}

function loadBusinessSegment() {
    if (isDataChanged) {
        if (!confirm(dataSaveAlert))
            return;
    }
    isDataChanged = false;
    $('#divRiskResponseContainer,.cardcontainer.businesssegment').html('')
    var isExclude = $(this).attr('data-ex');
    if (isExclude == 'true') {
        loadCategoryContainerContent();
        return;
    }
    var idEnc = $(this).attr('data-id');
    fcraApp.postAjaxRequest(getBusinessSegmentListUrl,
        { xid: idEnc, hasProgress: bHasCategoryProgress },
        function (response) {
            $('.cardcontainer.businesssegment').html(response);
            if ($('.cardcontainer.businesssegment button').length == 0) {
                $('.cardcontainer.businesssegment').html('<h3>Unauthorized Access.</h3>');
                return;
            }
            $('.cardcontainer.businesssegment button.businesssegment:first').click();
        }
    );
}

function changeCategoryButtonSatate() {
    var that = $(this);
    $(this).closest('div').find('button').removeClass('active').addClass('disabled');
    that.removeClass('disabled').addClass('active');
}

