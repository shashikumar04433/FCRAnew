$(function () {
    $(document).on('click','.btnSelectCustomer', setCustomer)
});

function setCustomer() {
    var that = $(this);
    $('#customerId').val(that.attr('data-customer'));
    $('#customerName').val(that.attr('data-name'));
    $('#customerScale').val(that.attr('data-scale'));
    $('#frmCustomerSelection').submit();
}