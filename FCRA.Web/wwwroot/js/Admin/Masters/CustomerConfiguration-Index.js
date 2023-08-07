$(function () {
    showConfiguration();
    $(document).on('click', '#btnUpdateCustomerConfig', updateconfiguration);
});

function updateconfiguration() {
    var items = [];
    $('#tblcustomerConfig tbody tr').each(function (i, tr) {
        items.push({
            FieldId: $(tr).attr('data-fieldid'),
            Visible: $(tr).find('input[datatype="Visible"]').is(':checked')
        });
    });
    fcraApp.postAjaxRequest(updateCustomerConfigurationUrl,
        { model: items },
        function (response) {
            alert(response.message);
        }
    );
}
function showConfiguration() {
    fcraApp.getAjaxRequest(getCustomerConfigurationUrl,
        { },
        function (response) {
            $("#divConfigurationContainer").html(response);
        }
    )
}