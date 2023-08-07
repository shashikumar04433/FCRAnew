$(function () {
    $(document).on('change', '#RiskTypeId,#RiskFactorId,#RiskSubFactorId', function () { $('#divMappingContainer').html(''); });
    $(document).on('click', '#btnSearchMapping', loadProductMapping);
    $(document).on('click', '#btnUpdateMapping', updateProductServiceMapping);
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});

function loadProductMapping() {
    var riskFactorId = $('#RiskFactorId').val();
    var riskSubFactorId = $('#RiskSubFactorId').val();
    if (!riskFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Factor');
        return;
    }
    if (!riskSubFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Sub Factor');
        return;
    }
    fcraApp.getAjaxRequest(getProductMappingUrl,
        { riskFactorId: riskFactorId, riskSubFactorId: riskSubFactorId },
        function (response) {
            $('#divMappingContainer').html(response);
        },
        function () { }
    );
}

function updateProductServiceMapping() {
    var riskFactorId = $('#RiskFactorId').val();
    var riskSubFactorId = $('#RiskSubFactorId').val();
    var scaleType = $('#ScaleType').val();
    if (!riskFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Factor');
        return;
    }
    if (!riskSubFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Sub Factor');
        return;
    }
    var list = [];
    $('input.chkSelect:checked').each(function () {
        var tr = $(this).closest('tr');
        list.push({
            RiskFactorId: $(this).attr('data-factorid'),
            RiskSubFactorId: $(this).attr('data-subfactorid'),
            ProductId: $(this).attr('data-productid'),
            IsSelected: true,
            //RiskRating: tr.find('select.ddlRating option:selected').val(),
            ScaleRange2: tr.find('input.txtScaleRange2').val(),
            ScaleRange3: tr.find('input.txtScaleRange3').val(),
            ScaleRange4: scaleType == 4 || scaleType == 5 ? tr.find('input.txtScaleRange4').val() : null,
            ScaleRange5: scaleType == 5 ? tr.find('input.txtScaleRange5').val() : null
        });
    });
    fcraApp.postAjaxRequest(updateProductMappingUrl,
        { mappings: list, riskFactorId: riskFactorId, riskSubFactorId: riskSubFactorId },
        function (response) {
            if (response)
                fcraApp.showToast('Alert', 'Mappings updated');
            else
                fcraApp.showToast('Alert', 'Mappings not updated, please try again');
        },
        function () { }
    );
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