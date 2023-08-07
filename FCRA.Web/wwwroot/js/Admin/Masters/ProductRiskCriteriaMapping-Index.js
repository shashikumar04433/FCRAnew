var oTableQuestionList = null;
var oBtnQuestion = null;
$(function () {
    $(document).on('change', '#RiskTypeId,#RiskFactorId,#RiskSubFactorId', function () { $('#divMappingContainer').html(''); });
    $(document).on('click', '#btnSearchMapping', loadCriteriaMapping);
    $(document).on('click', '#btnUpdateMapping', updateCriteriaMapping);
    $(document).on('change', '.chkSelect', chkSelectUpdate);
    $(document).on('click', '.btnQuestion', showAddQuestionModal);
    $(document).on('click', '.btnShowQuestionListModal', showAddQuestionListModal);
    $(document).on('click', '#btnAddQuestionFromList', addQuestionFromList);
    $(document).on('click', '#btnAddQuestion', addQuestion);
    $(document).on('click', '.btnRemoveQuestion', removeQuestion);
});

function loadCriteriaMapping() {
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
    fcraApp.getAjaxRequest(getCriteriaMappingUrl,
        { riskFactorId: riskFactorId, riskSubFactorId: riskSubFactorId },
        function (response) {
            $('#divMappingContainer').html(response);
        },
        function () { }
    );
}

function updateCriteriaMapping() {
    var riskFactorId = $('#RiskFactorId').val();
    if (!riskFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Factor');
        return;
    }
    var riskSubFactorId = $('#RiskSubFactorId').val();
    if (!riskSubFactorId) {
        fcraApp.showToast('Alert', 'Please select any Risk Sub Factor');
        return;
    }
    var list = [];
    $('input.chkSelect:checked').each(function () {
        var item = {
            RiskCriteriaId: $(this).attr('data-criteriaid'),
            RiskFactorId: $(this).attr('data-factorid'),
            RiskSubFactorId: $(this).attr('data-subfactorid'),
            ProductId: $(this).attr('data-productid'),
            IsSelected: true
        };
        var qIds = $(this).closest('td').find('.btnQuestion').attr('data-questions');
        if (qIds)
            item.QuestionIds = qIds;
        list.push(item);
    });

    fcraApp.postAjaxRequest(updateCriteriaMappingUrl,
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

function chkSelectUpdate() {
    if ($(this).is(':checked')) {
        $(this).closest('td').find('.btnQuestion').removeClass('v-none');
    }
    else {
        $(this).closest('td').find('.btnQuestion').addClass('v-none');
    }
}

function showAddQuestionModal() {
    var that = oBtnQuestion = $(this);
    if (!that.closest('td').find('.chkSelect').is(':checked')) {
        return;
    }
    var productId = oBtnQuestion.attr('data-productid');
    var questionId = oBtnQuestion.attr('data-questions');
    fcraApp.postAjaxRequest(getProductQuestionsAddedUrl,
        { productId: productId, qIds: questionId },
        function (response) {
            $('#tblSelectedQuestions tbody').html(response);
            $('#divAddQuestionModal').modal('show');
        });

}

function showAddQuestionListModal() {
    if (!oBtnQuestion)
        return;
    if (oTableQuestionList)
        oTableQuestionList.destroy();
    var productId = oBtnQuestion.attr('data-productid');
    var ids = [];
    $('#tblSelectedQuestions tbody tr').each(function () {
        if ($(this).attr('data-id') && !isNaN($(this).attr('data-id'))) {
            var id = parseInt($(this).attr('data-id'));
            if (ids.indexOf(id) === -1)
                ids.push(id);
        }
    });
    fcraApp.postAjaxRequest(getProductQuestionsUrl,
        { productId: productId, excludedQuestions: ids.join(',') },
        function (response) {
            $('#tblQuestionList tbody').html(response);
            oTableQuestionList = $('#tblQuestionList').DataTable();
            $('#divAddQuestionListModal').modal('show');
        });
}

function addQuestionFromList() {
    var selectedChks = $('.chkSelectQuestion:checked');
    if (selectedChks.length == 0) {
        fcraApp.showToast('Alert', 'Please select any Question');
        return;
    }
    var html = '';
    $(selectedChks).each(function () {
        html += '<tr data-id="' + $(this).attr('data-id') + '">';
        html += '<td>' + $(this).closest('tr').find('td').eq(1).text() + '</td>';
        html += '<td>' + $(this).closest('tr').find('td').eq(2).text() + '</td>';
        html += '<td><button type="button" class="btn btn-outline-danger rounded-5 min-w-0 btnRemoveQuestion" title="Remove Questions"><i class="fas fa-trash text-danger" ></i></button></td>';
        html += '</tr>'
    });
    $('#tblSelectedQuestions tbody').append(html);
    $('#divAddQuestionListModal').modal('hide');
}

function addQuestion() {
    var ids = [];
    $('#tblSelectedQuestions tbody tr').each(function () {
        if ($(this).attr('data-id') && !isNaN($(this).attr('data-id'))) {
            var id = parseInt($(this).attr('data-id'));
            if (id && ids.indexOf(id) === -1)
                ids.push(id);
        }
    });
    oBtnQuestion.attr('data-questions', ids.join(','));
    $('#divAddQuestionModal').modal('hide');
}
function removeQuestion() {
    $(this).closest('tr').remove();
}