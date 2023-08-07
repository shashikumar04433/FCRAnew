$(function () {
    $("#CompanyId").on('change', loadRoles);
    $(document).on('click', '#btnSave', saveUser);
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});

function loadRoles() {
    var companyId = $("#CompanyId").val();
    if (!companyId) {
        $("#RoleId").html('');
        return;
    }

    fcraApp.getAjaxRequest(getRolesOptionsUrl,
        { companyId: companyId },
        function (response) {
            $('#RoleId').html(response);
        },
        function () { }
    )
}

function saveUser() {
    if (!$('#frmUser').valid())
        return;
    var userType = $("#RoleId option:selected").attr('data-utype');
    if ($('#RoleId').val()  && userType != 1 && !$('#CustomerId').val()) {
        fcraApp.showToast('Alert', 'Please select any Coustomer');
        return;
    }

    $('#frmUser').submit();
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