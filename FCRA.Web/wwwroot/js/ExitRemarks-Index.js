$(function () {
    $('#btnSearchAuditTrail').hide();
    $(document).on('click', '#btnSearchAuditTrail', showAuditTrail);
    $(document).on('change', '#dataAuditTrailId', showhideSearchbutton);
});

function showhideSearchbutton() {
    if ($('#dataAuditTrailId option:selected').val() != '' && $('#dataAuditTrailId option:selected').val() != undefined) {
        $('#btnSearchAuditTrail').show();
    }
    else {
        $('#btnSearchAuditTrail').hide();
    }
}

function showAuditTrail() {
    var objectname = $('#dataAuditTrailId option:selected').val();
    fcraApp.postAjaxRequest(getAuditTrailUrl,
        {
            Objectname: objectname
        },
        function (response) {
            $('#divAuditTrailModal tbody').html(response);
            $('#divAuditTrailModal').modal('show');
        });

}
