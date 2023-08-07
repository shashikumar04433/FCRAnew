var isUpdate = 0;
var addControlContainer = null;

$(function () {
    $(document).on('change', '#StageId', loadRiskType);
    $(document).on('change', '#RiskTypeId', loadGeographicsPresence);
    $(document).on('change', '#GeographicPresenceId', loadCustomerSegment);
    $(document).on('change', '#CustomerSegmentId', loadBusinessSegment);
    $(document).on('click', '#btnSearchApprovalMatrix', searchApprovalMatrix);
    $(document).on('click', '.btnAddApprover', addApprover);
    $(document).on('click', '#btnAddRoleMoreUser', addApproverUser);
    $(document).on('click', '#btnAddRoleUser', addApproverUserClose);
    $(document).on('click', '.sortableApprover li button', removeApprover);
    $(document).on('click', '#btnUpdate', saveMatrix);
    dragula([document.getElementById('draggable-list')]);
});


function getIsExcluded(obj) {
    var ex = $(obj).find('option:selected').attr('data-ex');
    return ex === 'true';
}

function loadRiskType() {
    $('#RiskTypeId,#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))) {
        return;
    }
    var id = $('#StageId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getRiskTypeOptionsUrl,
        { stageId: id },
        function (response) {
            $('#RiskTypeId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadGeographicsPresence() {
    $('#GeographicPresenceId,#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))) {
        return;
    }
    var id = $('#RiskTypeId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getGeographicPresenceOptionsUrl,
        { riskTypeId: id },
        function (response) {
            $('#GeographicPresenceId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadCustomerSegment() {
    $('#CustomerSegmentId,#BusinessSegmentId').html('').closest('div').addClass('d-none');
    if (getIsExcluded($(this))) {
        return;
    }
    var id = $('#GeographicPresenceId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getCustomerSegmentOptionsUrl,
        { gId: id },
        function (response) {
            $('#CustomerSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function loadBusinessSegment() {
    $('#BusinessSegmentId').html('').closest('div').addClass('d-none');
    var id = $('#CustomerSegmentId').val();
    if (!id)
        return;
    fcraApp.getAjaxRequest(getBusinessSegmentOptionsUrl,
        { cId: id },
        function (response) {
            $('#BusinessSegmentId').html(response).closest('div').removeClass('d-none');
        },
        function () { }
    )
}

function addApprover() {
    $("#divRoleModal #UserId").val('');
    $("#divRoleModal").modal('show');
}

function addApproverUser() {
    addApprovers(true);
};

function addApproverUserClose() {
    addApprovers(false);
};

function removeApprover() {
    $(this).closest('li').remove();
}

function addApprovers(addMore) {
    var userId = $("#divRoleModal #UserId").val();
    if (!userId) {
        alert('Please select User');
        return;
    }
    if ($('ul.sortableApprover li[UserId="' + userId + '"]').length > 0) {
        alert($("#divRoleModal #UserId option:selected").text() + ' already added!');
        return;
    }

    var sortable = $('ul.sortableApprover');
    var userHtml = ''
    if (userId)
        userHtml = ' , User: <b>' + $("#divRoleModal #UserId option:selected").text() + '</b>';
    console.log(userId);

    var liHtml = '<li class="list-group-item draggable" sequenceno="1" userid="' + userId + '" title="Drag to reorder">Level:<b><span id="spSequence">1</span></b> ' + userHtml + '<button class="btn btn-secondary float-end p-1">x</button></li>';

    sortable.append(liHtml);
    reOrder(sortable);
    if (!addMore)
        $("#divRoleModal").modal('hide');
    else {
        $("#divRoleModal #UserId").val('');
    }
}

function reOrder(element) {
    $(element).find('li').each(function (i, ele) {
        $(ele).attr('sequenceno', i + 1).find('span#spSequence').text(i + 1);
    });
}

function searchApprovalMatrix() {
    var stage = null;
    var riskType = null;
    var geoPresence = null;
    var customerSegment = null;
    var businessSegment = null;
    stage = $('#StageId').val();
    if (!stage)
        return;
    if ($('#RiskTypeId').val()) {
        riskType = $('#RiskTypeId').val();
    }

    if ($('#GeographicPresenceId').val()) {
        geoPresence = $('#GeographicPresenceId').val();
    }

    if ($('#CustomerSegmentId').val()) {
        customerSegment = $('#CustomerSegmentId').val();
    }
    if ($('#BusinessSegmentId').val()) {
        businessSegment = $('#BusinessSegmentId').val();
    }

    fcraApp.getAjaxRequest(GetApprovalMatrixUrl,
        { StageId: stage, RiskTypeId: riskType, GeographicPresenceId: geoPresence, CustomerSegmentId: customerSegment, BusinessSegmentId: businessSegment },
        function (response) {
            $("#divPermissionsContainer").html(response);
        }
    )
}

function saveMatrix() {
    var validate = validateMatrix();
    if (!validate.status) {
        alert(validate.message);
        if (validate.errorControl)
            $(validate.errorControl).focus();
        return;
    }

    if (!confirm('Please review matrix before saving. Are you sure you want to continue?')) {
        return;
    }

    $.ajax({
        url: saveApprovalMatrixUrl,
        type: 'POST',
        data: { model: getUpdateItems() },
        success: function (response) {
            if (response.status) {
                fcraApp.showToast('alert', response.message);
                $("#StageId, #RiskTypeId, #GeographicPresenceId, #CustomerSegmentId, #BusinessSegmentId").val('');
                $("#divPermissionsContainer").html('');
            }
            else {
                fcraApp.showToast('alert', response.message);
            }
        }
    })
}

function validateMatrix() {
    var isValidApprover = validateApprover();
    if (!isValidApprover.status)
        return isValidApprover;

    return { status: true, message: '' };
}

function validateApprover() {
    var hasError = false;
    var message = '';
    if ($('ul.sortableApprover li').length === 0) {
        message = 'Please add atleast one approver!';
        hasError = true;
    }
    return { status: !hasError, message: message };
}

function getUpdateItems() {
    var items = [];
    var stage = null;
    var riskType = null;
    var geoPresence = null;
    var customerSegment = null;
    var businessSegment = null;
    stage = $('#StageId').val();
    if ($('#RiskTypeId').val()) {
        riskType = $('#RiskTypeId').val();
    }

    if ($('#GeographicPresenceId').val()) {
        geoPresence = $('#GeographicPresenceId').val();
    }

    if ($('#CustomerSegmentId').val()) {
        customerSegment = $('#CustomerSegmentId').val();
    }
    if ($('#BusinessSegmentId').val()) {
        businessSegment = $('#BusinessSegmentId').val();
    }
    $(".sortableApprover li").each(function (i, ele) {
        items.push({
            StageId: stage,
            RiskTypeId: riskType,
            GeographicPresenceId: geoPresence,
            CustomerSegmentId: customerSegment,
            BusinessSegmentId: businessSegment,
            SequenceNo: $(this).attr('sequenceno'),
            UserId: $(this).attr('userid'),
            IsActive: true
        });
    });
    return items;
}