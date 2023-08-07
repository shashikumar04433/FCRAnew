$(function () {
    $(document).on('change', '#ddlVersion1', Version1Change);
    $(document).on('change', '#ddlVersion2', Version2Change);
    $(document).on('click', '#btnRiskCompare', getRiskCompare);
});

function Version1Change() {
    $('.thYear1').html($('#ddlVersion1  option:selected').text());
    $('#ddlVersion2').html($('#ddlVersion1').html());

    var version1 = [];
    $('select[name="version2"] option').each(function () {
        version1.push($(this).val());
    });
    var updatedValue = version1.filter(function (item) {
        return item <= $('#ddlVersion1').val();
    });
    for (var i in updatedValue) {
        $("select#ddlVersion2 option[value=" + updatedValue[i] + "]").remove();
    }


    
    //$('#ddlVersion2 option[value="' + $(this).val() + '"]').remove();
    $('#ddlVersion2').change();
}

function Version2Change() {
    $('.thYear2').html($('#ddlVersion2  option:selected').text());
}

function getRiskCompare() {
    if (parseInt($('#ddlVersion2').val()) <= parseInt($('#ddlVersion1').val())) {
        alert("Please select Latest Reported greater than Last Reported");
        return false;
    }
    var groups = $('#ddlLevel').val().join(',');
    fcraApp.postAjaxRequest(GetRiskFactorComparisonUrl,
        { fromVersionId: $('#ddlVersion1').val(), toVersionId: $('#ddlVersion2').val(), riskTpye: $('#ddlRiskType').val(), group: groups },
        function (response) {
            $('.riskComparePartial').html(response);
            $('.thYear1').html($('#ddlVersion1  option:selected').text());
            $('.thYear2').html($('#ddlVersion2  option:selected').text());
        }
    );
}
