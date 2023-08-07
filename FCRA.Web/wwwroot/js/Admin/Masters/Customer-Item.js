$(function () {
    $(document).on('click', '.btnSave', checkFormIsValid);
    $(document).on('click', '.btnAddLocation', addLocationSet);
    $(document).on('click', '.btnAddCountry', addCountrySet);
    $(document).on('click', '.location-container .location-item button.delete', function () {
        $(this).closest('.location-item').remove();
        reOrderContainer($('.location-container .location-item'));
    });
    $(document).on('click', '.country-container .country-item button.delete', function () {
        $(this).closest('.country-item').remove();
        reOrderContainer($('.country-container .country-item'));
    });
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
    $(document).on('click', '#btnAuditTrail1', showAuditTrail);
});

function checkFormIsValid() {
    if ($('#frmEmployee').valid())
        return;
    var span = $('span.field-validation-error').eq(0);
    var id = $(span).closest('.content').attr('id');
    $('div.step[data-target="#' + id + '"] button.step-trigger').click();
    $(span).closest('div').find('input,select').focus();
}

function addLocationSet() {
    //destroySelect2('.location-container .location-item');
    if ($('.location-container .location-item').length > 0) {
        addNewContainerSet('.location-container', '.location-container .location-item');
        resetFormValidation('.location-container .location-item');
        return;
    }

    fcraApp.getAjaxRequest(getLocationTemplateUrl, { countryId: $('#Id').val() }
        , function (response) {
            $('.location-container').append(response);
            resetFormValidation('.location-container .location-item');
        });
}

function addCountrySet() {
   // destroySelect2('.country-container .country-item');
    if ($('.country-container .country-item').length > 0) {
        addNewContainerSet('.country-container', '.country-container .country-item');
        resetFormValidation('.country-container .country-item');
        return;
    }

    fcraApp.getAjaxRequest(getCountryTemplateUrl, { countryId: $('#Id').val() }
        , function (response) {
            $('.country-container').append(response);
            resetFormValidation('.country-container .country');
        });
}

function destroySelect2(itemSelector) {
    $(itemSelector + ' select.select2-hidden-accessible').select2('destroy');
}

function initSelect2(itemSelector) {
    $(itemSelector + ' select.select2').each(function () {
        var $this = $(this);
        $this.select2({
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });
}

function addNewContainerSet(container, itemSelector) {
    $(container).append($(itemSelector).eq(0).clone());
    reOrderContainer($(itemSelector));
    var lastItem = $(itemSelector).eq($(itemSelector).length - 1);
    $(lastItem).find('input[data-date]').attr('data-date', '').attr('value', '');
    $(lastItem).find('input,select').val('');
    $(lastItem).find('input[type="hidden"].id').val('0');
    $(lastItem).find('div.d-none').removeClass('d-none');
    $(lastItem).find('.download').remove();
}

function reOrderContainer(items) {
    var regexName = /\[([0-9])*\]/g;
    var regexId = /\_([0-9])*\_/g;
    $(items).each(function (i, cont) {
        $(cont).find('input,select').each(function (j, ele) {
            var name = $(ele).attr('name');
            var id = $(ele).attr('id');
            $(ele).attr('name', name.replace(regexName, '[' + i + ']'));
            $(ele).attr('id', id.replace(regexId, '_' + i + '_'));
        });
        $(cont).find('span[data-valmsg-for]').each(function (j, ele) {
            var varFor = $(ele).attr('data-valmsg-for');
            $(ele).attr('data-valmsg-for', varFor.replace(regexName, '[' + i + ']'));
        });
        $(cont).find('.spCount').text(i + 1);
    });
}

function resetFormValidation(itemSelector) {
    $('#frmCustomer').removeData("validator");
    $('#frmCustomer').removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse('#frmCustomer');
    //initSelect2(itemSelector);
   // $("input[type=date]").trigger("change");
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