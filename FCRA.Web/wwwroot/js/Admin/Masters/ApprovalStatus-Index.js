$(function () {
    $(document).on('click', '#btnStatusSearch', loadApprovalStatus);
    loadApprovalStatus();
    $(document).on('change', '#ddlApprovalStatus', function () {
        $("#divApprovalStatusContainer").html('');
    });
});

function loadApprovalStatus() {
    var status = $('#ddlApprovalStatus option:selected').val();
    fcraApp.postAjaxRequest(GetApprovalStatusUrl,
        { status: status },
        function (response) {
            $('#divApprovalStatusContainer').html(response);
        }
    );
}