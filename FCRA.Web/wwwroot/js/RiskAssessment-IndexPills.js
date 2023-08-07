$(function () {
    bHasCategoryProgress = true;
    $(document).on('click', '#btnUploadExcelFile', uploadAssesmentExcel);
    $(document).on('click', '#btnSendEmailNotificationShow', showHideSendEmailModal);
    $(document).on('click', '#btnSendemail', showSendEmailMailBox);
    $(document).on('change', '#ddlUsers', showHideSendEmailButton);
    
})

function loadCategoryContainerContent() {
    if (isDataChanged) {
        if (!confirm(dataSaveAlert))
            return;
    }
    isDataChanged = false;
    var stage = null;
    var riskType = null;
    var geoPresence = null;
    var customerSegment = null;
    var businessSegment = null;

    if ($('.btnStage.active').length == 0)
        return;
    stage = $('.btnStage.active').attr('data-id');
    if ($('.btnRiskType.active').length > 0) {
        riskType = $('.btnRiskType.active').attr('data-id');
    }

    if ($('.btnGeoPresence.active').length > 0) {
        geoPresence = $('.btnGeoPresence.active').attr('data-id');
    }

    if ($('.btnCustomerSegment.active').length > 0) {
        customerSegment = $('.btnCustomerSegment.active').attr('data-id');
    }

    if ($('.btnBusinessSegment.active').length > 0) {
        businessSegment = $('.btnBusinessSegment.active').attr('data-id');
    }

    fcraApp.postAjaxRequest(getAssessmentPillsUrl,
        { stage: stage, riskType: riskType, geoPresence: geoPresence, customerSegment: customerSegment, businessSegment: businessSegment },
        function (response) {
            $('#divRiskResponseContainer').html(response);
            isIgnoreDataChange = true;
            $('select').change();
            $('input').keyup();
            isIgnoreDataChange = false;
            $('tr.trDisabled input,tr.trDisabled select').attr('readonly', true);
            if ($('#ddlUsers').length > 0)
                $('#ddlUsers').select2();
        }
    );
}

function uploadAssesmentExcel() {
    var files = $("#fileExcelFileUpload").get(0).files;
    if (!files || files.length == 0) {
        fcraApp.showToast('Alert', 'Please select a file');
        return;
    }
    var formData = new FormData();
    formData.append($("#fileExcelFileUpload").attr('name'), files[0]);
    if ($('#StageId').val())
        formData.append("stageId", $('#StageId').val());
    if ($('#RiskTypeId').val())
        formData.append("riskTypeId", $('#RiskTypeId').val());
    if ($('#GeographicPresenceId').val())
        formData.append("geoPresenceId", $('#GeographicPresenceId').val());
    if ($('#CustomerSegmentId').val())
        formData.append("customerSegmentId", $('#CustomerSegmentId').val());
    if ($('#BusinessSegmentId').val())
        formData.append("businessSegmentId", $('#BusinessSegmentId').val());

    $.ajax({
        url: uploadAssesmentExcelUrl,
        type: "POST",
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (!response || response == 0) {
                alert('Someting went wrong, please try again!');
                return;
            }
            if (response == 1) {
                fcraApp.showToast('Alert', 'Ratings has been updated successfully.');
                loadCategoryContainerContent();
                return;
            }
            if (response == -1) {
                fcraApp.showToast('Alert', 'Please select valid excel file.');
            }
            else if (response == -2) {
                fcraApp.showToast('Alert', 'No record to update.');
            }
            else if (response == -3) {
                fcraApp.showToast('Alert', 'No file uploaded.');
            }
        }
    });
}

function showHideSendEmailButton() {
    if ($('#ddlUsers option:selected').val()) {
        $('#btnSendemail').removeClass('d-none');
    } else {
        $('#btnSendemail').addClass('d-none');
    }
}
function showHideSendEmailModal() {
    $('#ddlUsers').val('').change();
    $('#divSendEmailModal').modal('show');
}

function showSendEmailMailBox() {
    var email = $('#ddlUsers option:selected').val();
    var name = $('#ddlUsers option:selected').text();
    var mailToContent = $('#divEmailContent').text();
    var date = $('#hdEmailTaskDate').val();
    var username = $('#spLoggedUser').text();
    var IsThroughSMTP = $('#hdIsThroughSMTP').val();
    if (IsThroughSMTP == 'Y') {
        mailToContent = $('#divSMTPEmailContent').html();
    }
    mailToContent = mailToContent.replace('{{name}}', name);
    mailToContent = mailToContent.replace('{{email}}', email);
    mailToContent = mailToContent.replace('{{date}}', date);
    mailToContent = mailToContent.replace('{{username}}', username);
    if (IsThroughSMTP == 'Y') {
        fcraApp.postAjaxRequest(SendEmailUrl,
            { toemail: email, subject: 'FCRA-Review Action', htmlMessage: mailToContent },
            function (response) {
                if (response) {
                    fcraApp.showToast('Alert', response.message);
                    $('#divSendEmailModal').modal('hide');
                }
            }
        );
    }
    else
        $('#aSendNotification').attr('href', mailToContent)[0].click();
}

