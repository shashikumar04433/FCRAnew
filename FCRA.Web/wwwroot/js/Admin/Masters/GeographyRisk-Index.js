$(function () {
    $(document).on('click', '.chkSelectAll', selectDeselectTableCheckBox)
 
});

function selectDeselectTableCheckBox() {
    var isSelected = $(this).is(':checked');
    $(this).closest('table').find('tbody input[type="checkbox"]').prop('checked', isSelected);
}