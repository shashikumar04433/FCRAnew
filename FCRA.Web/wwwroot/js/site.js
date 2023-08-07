// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.
var fcraApp = {
    getAjaxRequest: function (url, data, success, fail, excluedLoader) {
        var globalRequest = true;
        if (excluedLoader != undefined && excluedLoader != null && excluedLoader == true)
            globalRequest = false;
        $.ajax({
            url: url,
            method: "GET",
            data: data,
            global: globalRequest,
            success: function (response) {
                if (success !== undefined && typeof success == 'function')
                    success(response);
            },
            error: function (a, b, c, d) {
                if (fail !== undefined && typeof fail == 'function')
                    fail(a, b, c, d);
            }
        });
    },
    postAjaxRequest: function (url, data, success, fail, excluedLoader) {
        var globalRequest = true;
        if (excluedLoader != undefined && excluedLoader != null && excluedLoader == true)
            globalRequest = false;
        $.ajax({
            url: url,
            method: "POST",
            data: data,
            global: globalRequest,
            success: function (response) {
                if (success !== undefined && typeof success == 'function')
                    success(response);
            },
            error: function (a, b, c, d) {
                if (fail !== undefined && typeof fail == 'function')
                    fail(a, b, c, d);
            }
        });
    },
    showToast: function (alertTitle, alertMessage) {
        let toast = {
            title: alertTitle,
            message: alertMessage,
            timeout: 5000
        }
        Toast.create(toast);
    }
};



$(function () {
    var currentTick = (new Date()).getTime();
    showAppMessages();
    $(document).on('click', '.aLogout', function () {
        $('#frmLogout').submit();
    });
    hideLoader();
    $(document).ajaxStart(function () {
        showLoader();
    });
    $(document).ajaxComplete(function (event, xhr, options) {

        if (xhr.responseText === '#InvalidSessionProcess#') {
            window.location.href = appHostURL + 'Account/Login?se=y';
            return;
        }
        else if (xhr.responseText === '#UnauthorizedProcess#') {
            window.location.href = appHostURL + 'Account/UnAuthorized';
            return;
        }
        hideLoader();
        setTimeout(function () {
            currentTick = (new Date()).getTime();
            $('input').prop("autocomplete", currentTick);
        }, 500);
    });
    $('form').not('form[target="_blank"],form.noclick').on('submit', function () {
        var that = this;
        showLoader();
        setTimeout(function () {
            if ($(that).find('span.field-validation-error').length > 0)
                hideLoader();
        }, 50);
    });
    $('a').not('a[target="_blank"],a[href = "#"],a[data-toggle="tab"],a[data-toggle="collapse"],a.noclick,a.jstree-anchor').on('click', function (event) {
        if (event.ctrlKey)
            return;
        showLoader();
    });
    $('input').prop("autocomplete", currentTick);
    $(document).on('keyup', '.modal .modal-body input[type="text"],.modal .modal-body input[type="number"],.modal .modal-body input[type="date"]',
        function (e) {
            e.preventDefault();
            e.stopPropagation();
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13 && !$(this).hasClass('ignoreenter')) {
                $(this).closest('div.modal').find('div.modal-footer button.btn-primary').click();
            }
        });
    $(document).on('click', 'div.modal button.close', function () {
        $(this).closest('div.modal').modal('hide');
    });
    if ($('table#example').length > 0) {
        $('table#example').DataTable({
            "language": {
                // "lengthMenu": '_MENU_ bản ghi trên trang',
                "search": '<i class="fa fa-search"></i>',
                "searchPlaceholder": "type here to search...",
                //"paginate": {
                //    "previous": '<i class="fa fa-angle-left"></i>',
                //    "next": '<i class="fa fa-angle-right"></i>'
                //}
            },
            rowCallback: function (row, data, displayNum, displayIndex, dataIndex) {
                $('td:first', row).html(displayIndex + 1);
            },
        });
    }
    //$('li.menu').on('click', function () {
    //    $(this).find('.submenu').toggleClass('d-none');
    //    $(this).find('a span i').toggleClass('d-none');
    //});
    disableViewModeControls();
    resetMenuItems();
});

function showAppMessages() {
    var successMessage = $('div.appMessage div.success').html();
    var errorMessage = $('div.appMessage div.error').html();

    if (successMessage)
        $('div.appMessage div.success').toast('show');
    if (errorMessage)
        $('div.appMessage div.error').toast('show');
}

function showLoader() {
    $('div.page-loader').fadeIn();
}
function hideLoader() {
    $('div.page-loader').fadeOut('slow');
}

function disableViewModeControls() {
    if ($('#divViewContentRenderBody form').length > 0) {
        if ($('#divViewContentRenderBody form button[type=submit]').length == 0) {
            $('#divViewContentRenderBody form input,#divViewContentRenderBody form select').prop('disabled', true);
        }
    }
}

function changeToUpperCase(ele) {
    var p = ele.selectionStart;
    ele.value = ele.value.toUpperCase();
    ele.setSelectionRange(p, p);
}

function resetMenuItems() {
    if ($('.menu a.active').length === 0)
        return;
    //$('.main-menu-content li').removeClass('open');
    $('.menu a.active').closest('ul').closest('li').click();
}