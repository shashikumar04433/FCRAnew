var oInputQuestion = null;
var oTblCountryVolume = null;
var scaleType = null;
var buttonType = null;
$(function () {
    $(document).on('keyup change', 'input.riskWeightage', calculateScore);
    $(document).on('change', 'select.riskWeightageSelect', calculateScoreSelect);
    $(document).on('blur', 'input.riskWeightage', validateScore);
    $(document).on('keyup change', 'input.ratingScore', calculateRatingScore);
    $(document).on('click', '#btnSaveResponse', showSaveResponseModal);
    $(document).on('click', '#btnSaveResponseModal', saveResponseRemarks);
    $(document).on('keyup change', 'input.productVolume', calculateProductVolume);
    $(document).on('keyup change', 'input.txtSubFactorVolume', calculateSubFactorVolumeScore);
    $(document).on('click', '.btnQuestion', showAddQuestionModal);
    $(document).on('click', '#btnAddQuestion', addQuestionRating);
    $(document).on('click', '.btnCountryVolume', showCountryVolumeModal);
    $(document).on('click', '#btnCountriesVolumeModal', addCountryVolume);
    scaleType = $('#ScaleType').val();
    $(document).on('click', '.btnChangeImageCustom', showSubFactorFileSelectionModal);
    $(document).on('click', '#btnChangeSubFactorFileModal', updateSelectedFile);
    $(document).on('click', '#btnSubmitResponse', showSaveApproveResponseModal);
    $(document).on('click', '#btnSaveApproveResponseModal', saveApproveResponseRemarks);
    $(document).on('click', '#btnApproveSubmit', showSaveApproveResponseModal);
    $(document).on('click', '#btnApproveCancel', showSaveApproveResponseModal);
    $(document).on('click', '#btnAuditTrail', showAuditTrail);
});


function validateScore() {
    var that = $(this);
    var value = that.val();
    if (value && !isNaN(value)) {
        var decimalVal = parseFloat(value);
        if (decimalVal > 100) {
            that.val('100');
        }
    }
}

function calculateScore() {
    if (!isIgnoreDataChange)
        isDataChanged = true;
    var that = $(this);
    var s2 = parseFloat(that.attr('data-s2'));
    var s3 = parseFloat(that.attr('data-s3'));
    var s4 = parseFloat(that.attr('data-s4'));
    var s5 = parseFloat(that.attr('data-s5'));
    var value = that.val();
    var score = '-';
    var scoreNumber = null;
    var className = '';
    var rowClassName = '';
    if (value && !isNaN(value) && parseFloat(value) > 0) {
        var decimalVal = parseFloat(value);
        if (decimalVal < s2)
            scoreNumber = '1';
        else if (decimalVal < s3)
            scoreNumber = '2';
        else if (scaleType == '3') {
            if (decimalVal >= s3)
                scoreNumber = '3';
            else
                scoreNumber = '2';
        }
        else if ((scaleType == '4' || scaleType == '5') && decimalVal < s4) {
            scoreNumber = '3';
        }
        else if (scaleType == '4') {
            if (decimalVal >= s4)
                scoreNumber = '4';
            else
                scoreNumber = '3';
        }
        else if (scaleType == '5' && decimalVal < s5) {
            scoreNumber = '4';
        }
        else if (scaleType == '5' && decimalVal >= s5) {
            scoreNumber = '5';
        }
    }
    if (scoreNumber) {
        score = scoreNumber;
        className = 'riskScore' + scoreNumber;
        rowClassName = 'riskScore' + scoreNumber + 'TR';
    }

    that.closest('tr').removeClass('riskScore1TR riskScore2TR riskScore4TR riskScore5TR');
    var td = that.closest('tr').find('.riskScore');
    td.html(score);
    td.removeClass('riskScore1 riskScore2 riskScore3 riskScore4 riskScore5');
    if (className) {
        td.addClass(className);
        that.closest('tr').addClass(rowClassName);
        that.closest('tr').find('.factorTotalWeightedScore');
    }
    setSubFactorWeightedScore(that.closest('tr'), score);
    calculateSubFactorWeightedScore(that.closest('table'));
}

function calculateScoreSelect() {
    if (!isIgnoreDataChange)
        isDataChanged = true;
    var that = $(this);
    var rating = parseInt(that.find('option:selected').attr('data-rating'));
    var score = '-';
    var className = '';
    var rowClassName = '';
    if (rating && rating > 0) {
        score = rating;
        className = 'riskScore' + rating;
        rowClassName = 'riskScore' + rating + 'TR';
    }
    that.closest('tr').removeClass('riskScore1TR riskScore2TR riskScore4TR riskScore5TR');
    var td = that.closest('tr').find('.riskScore');
    td.html(score);
    td.removeClass('riskScore1 riskScore2 riskScore3 riskScore4 riskScore5');
    if (className) {
        td.addClass(className);
        that.closest('tr').addClass(rowClassName);
    }
    setSubFactorWeightedScore(that.closest('tr'), score);
    calculateSubFactorWeightedScore(that.closest('table'));
}

function setSubFactorWeightedScore(tr, score) {
    var percent = parseFloat($(tr).find('.riskWeightedScore').attr('data-weightedpercent'));
    if (score && !isNaN(score)) {
        $(tr).find('.riskWeightedScore').html(parseFloat(score) * percent / 100);
    }
    else {
        $(tr).find('.riskWeightedScore').html('');
    }
}

function calculateRatingScore() {
    if (!isIgnoreDataChange)
        isDataChanged = true;
    var that = $(this);
    var tr = that.closest('tr');
    var s2 = parseFloat(tr.attr('data-s2'));
    var s3 = parseFloat(tr.attr('data-s3'));
    var s4 = parseFloat(tr.attr('data-s4'));
    var s5 = parseFloat(tr.attr('data-s5'));
    var score = '-';
    var className = '';
    var rowClassName = '';
    var totalScore = 0.00;
    var ratingName = '';
    var rating = 0;
    tr.find('input.ratingScore').each(function () {
        var ratingVal = $(this).val();
        if (ratingVal && !isNaN(ratingVal)) {
            totalScore += parseFloat(ratingVal);
        }
    });
    if (totalScore > 0) {
        if (totalScore < s2) {
            score = 10;
            ratingName = 'Low';
            rating = 1;
        } else if (totalScore < s3) {
            score = 20;
            ratingName = 'Medium';
            rating = 2;
        } else if (scaleType == '3') {
            if (totalScore >= s3) {
                score = 30;
                ratingName = 'High';
                rating = 3;
            }
            else {
                score = 20;
                ratingName = 'Medium';
                rating = 2;
            }
        }
        else if ((scaleType == '4' || scaleType == '5') && totalScore < s4) {
            score = 30;
            ratingName = 'High';
            rating = 3;
        } else if (scaleType == '4') {
            if (totalScore >= s4) {
                score = 40;
                ratingName = 'High';
                rating = 4;
            }
            else {
                score = 30;
                ratingName = 'High';
                rating = 3;
            }
        } else if (scaleType == '5' && totalScore < s5) {
            score = 40;
            ratingName = 'High';
            rating = 4;
        } else if (scaleType == '5' && totalScore >= s5) {
            score = 50;
            ratingName = 'Critical';
            rating = 5;
        }
    }
    if (rating > 0) {
        className = 'riskScore' + rating;
        rowClassName = 'riskScore' + rating + 'TR';
    }
    var volumeInput = tr.find('input.productVolume');
    if (totalScore > 0) {
        tr.find('td.tdTotalScore').html(totalScore);
        volumeInput.attr('data-totalscore', totalScore);
    }
    else {
        tr.find('td.tdTotalScore').html('');
        volumeInput.attr('data-totalscore', '');
    }
    tr.find('td.tdFinalScore').html(score);
    if (score && !isNaN(score)) {
        volumeInput.attr('data-finalscore', score).attr('data-riskrating', rating);
    }
    else {
        volumeInput.attr('data-finalscore', '').attr('data-riskrating', '');
    }
    var td = tr.find('td.tdRiskRating');
    td.html(ratingName);
    td.removeClass('riskScore1 riskScore2 riskScore3 riskScore4 riskScore5');
    tr.removeClass('riskScore1TR riskScore2TR riskScore4TR riskScore5TR');
    if (className) {
        td.addClass(className);
        tr.addClass(rowClassName);
    }
    calculateWeightedScore(that);
    setColorToProductRating(that);
}

function setColorToProductRating(input) {
    if (!input.val()) {
        input.removeClass('riskScore1 riskScore2 riskScore3 riskScore4 riskScore5');
        return;
    }
    var hasMultiQuestion = input.attr('data-questions').indexOf(',') != -1;
    var inputVal = parseFloat(input.val());
    if (inputVal <= 0)
        return;
    inputClass = '';
    if (hasMultiQuestion) {
        if (inputVal <= 2)
            inputClass = 'riskScore1';
        else if (inputVal <= 4)
            inputClass = 'riskScore2';
        else
            inputClass = 'riskScore3';
    } else {
        if (inputVal == 1)
            inputClass = 'riskScore1';
        else if (inputVal == 2)
            inputClass = 'riskScore2';
        else
            inputClass = 'riskScore3';
    }
    if (inputClass)
        input.addClass(inputClass);
}

function calculateProductVolume() {
    if (!isIgnoreDataChange)
        isDataChanged = true;
    var that = $(this);
    calculateWeightedScore(that);
}
function calculateWeightedScore(that) {
    var table = that.closest('table')
    var totalVolume = 0.00;
    table.find('tbody input.productVolume').each(function (i, item) {
        var volume = $(item).val();
        if (volume && !isNaN(volume) && parseFloat(volume) > 0)
            totalVolume += parseFloat(volume);
    });
    table.find('th.thTotalVolume').html(totalVolume.toFixed(2));

    //all high rating volume
    var totalHighVolume = 0.00;
    table.find('td.riskScore3').each(function (i, td) {
        var volume = $(td).closest('tr').find('input.productVolume').val();
        if (volume && !isNaN(volume) && parseFloat(volume) > 0)
            totalHighVolume += parseFloat(volume);
    });
    var sfResponseFactor = totalHighVolume * 100.00 / totalVolume;
    var inputWeightage = table.closest('tr').prev().find('input.riskWeightage');
    inputWeightage.val(sfResponseFactor.toFixed(2));
    inputWeightage.keyup();
}

function calculateSubFactorWeightedScore(sfTable) {
    var totalWeightedScore = 0.00;
    $(sfTable).find('.riskWeightedScore').each(function (i, td) {
        var score = $(td).html();
        if (score && !isNaN(score))
            totalWeightedScore += parseFloat(score);
    });
    sfTable.find('.factorTotalWeightedScore').attr('data-totalweightedscore', totalWeightedScore).html(totalWeightedScore.toFixed(2));
}

function calculateSubFactorVolumeScore() {
    var that = $(this);
    var total = 0.00;
    var inputs = that.closest('table').find('input.txtSubFactorVolume');
    inputs.each(function () {
        var val = $(this).val();
        if (val && !isNaN(val) && parseFloat(val) > 0) {
            total += parseFloat(val);
        }
    });
    var totalWeightedScore = 0.00;
    inputs.each(function () {
        var inputVal = $(this).val();
        var weight = 0.00;
        var weightedScore = 0.00;
        var score = parseFloat($(this).attr('data-score'));
        if (inputVal && !isNaN(inputVal) && parseFloat(inputVal) > 0) {
            weight = (parseFloat(inputVal) * 100 / total).toFixed(2);
            weightedScore = (score * (weight / 100)).toFixed(2);
            totalWeightedScore += parseFloat(weightedScore);
        }
        $(this).attr('data-weight', weight);
        $(this).attr('data-weightedscore', weightedScore);
        $(this).closest('tr').find('td.tdVolumeWeight').text(weight);
        $(this).closest('tr').find('td.tdVolumeWeightedScore').text(weightedScore);
    });
    that.closest('table').find('td.tdTotalVolumeWeightedScore').text(totalWeightedScore.toFixed(2));
    that.closest('table').closest('tr').prev().find('input.riskWeightage').val(totalWeightedScore.toFixed(2)).change();
}

function showSaveResponseModal() {
    var model = getSaveResponseModel();
    if (model.RiskScoreResponses.length == 0 && model.RiskSubFactorResponses.length == 0) {
        fcraApp.showToast('Alert', 'No response to update');
        return;
    }
    $('#divSaveResponseRemarksModal #txtSaveResponseRemarks').val('')
    $('#divSaveResponseRemarksModal').modal('show');
}

function saveResponseRemarks() {
    if (!$('#divSaveResponseRemarksModal #txtSaveResponseRemarks').val()) {
        fcraApp.showToast('Alert', 'Please enter remarks');
        return;
    }

    if (!confirm('Are you sure to update?')) {
        return;
    }

    fcraApp.postAjaxRequest(addExitRemarksUrl,
        { remarks: $('#divSaveResponseRemarksModal #txtSaveResponseRemarks').val() },
        function (response) {
            if (response == 1) {
                $('#divSaveResponseRemarksModal').modal('hide');
                saveResponse();
            }
            if (response == -1) {
                fcraApp.showToast('Alert', 'Please enter remarks');
            }
            if (response == 0) {
                fcraApp.showToast('Alert', 'Please try again.');
            }
        }
    );


}

function getSaveResponseModel() {
    var model = {
        RiskFactorResponses: [],
        RiskSubFactorResponses: [],
        RiskScoreProductVolumRatingResponses: [],
        RiskScoreResponses: [],
        VolumeMappings: []
    };
    var riskFactorId = null;
    if ($('#hdRiskFactorId').length > 0 && $('#hdRiskFactorId').val())
        riskFactorId = $('#hdRiskFactorId').val();
    $('th.factorTotalWeightedScore').each(function (i, element) {
        var score = $(element).attr('data-totalweightedscore');
        if (score && !isNaN(score) && parseFloat(score) > 0) {
            model.RiskFactorResponses.push({
                RiskFactorId: $(element).attr('data-factorid'),
                TotalWeightedScore: parseFloat(score)
            });
        }
    })
    $('input.riskWeightage').each(function (i, element) {
        var tr = $(element).closest('tr');

        if (tr.attr('data-isAttachmentMandatory') == 'True' && tr.attr('data-attachmentCount') <= 0) {
            alert("Please select Attachment for " + tr.attr('data-SubfactorName'));
            return;
        }
        var response = $(element).val();
        if (response && !isNaN(response) && parseFloat(response) > 0) {
            model.RiskSubFactorResponses.push({
                RiskFactorId: tr.attr('data-factorid'),
                RiskSubFactorId: tr.attr('data-subfactorid'),
                RiskRangeParameter: tr.attr('data-riskrangeparameter'),
                Score: tr.find('td.riskScore').text(),
                Assumptions: tr.find('.txtAssumptions').val(),
                Response: parseFloat(response)
            });
        }
    });
    $('select.riskWeightageSelectPre').each(function (i, element) {
        var tr = $(element).closest('tr');
        var response = $(element).val();
        if (response && !isNaN(response) && parseInt(response) > 0) {
            model.RiskSubFactorResponses.push({
                RiskFactorId: tr.attr('data-factorid'),
                RiskSubFactorId: tr.attr('data-subfactorid'),
                RiskRangeParameter: tr.attr('data-riskrangeparameter'),
                Score: tr.find('td.riskScore').text(),
                Assumptions: tr.find('.txtAssumptions').val(),
                PreDefinedParameterId: response
            });
        }
    });
    $('select.riskWeightageSelectDescription').each(function (i, element) {
        var tr = $(element).closest('tr');
        var response = $(element).val();
        if (response && response != '') {
            model.RiskSubFactorResponses.push({
                RiskFactorId: tr.attr('data-factorid'),
                RiskSubFactorId: tr.attr('data-subfactorid'),
                RiskRangeParameter: tr.attr('data-riskrangeparameter'),
                Score: tr.find('td.riskScore').text(),
                Assumptions: tr.find('.txtAssumptions').val(),
                ResponseDescription: response
            });
        }
    });
    $('select.riskWeightageSelectScale').each(function (i, element) {
        var tr = $(element).closest('tr');
        var response = $(element).val();
        if (response && !isNaN(response) && parseInt(response) > 0) {
            model.RiskSubFactorResponses.push({
                RiskFactorId: tr.attr('data-factorid'),
                RiskSubFactorId: tr.attr('data-subfactorid'),
                RiskRangeParameter: tr.attr('data-riskrangeparameter'),
                Score: tr.find('td.riskScore').text(),
                Assumptions: tr.find('.txtAssumptions').val(),
                Response: response
            });
        }
    });

    $('input.productVolume').each(function (i, element) {
        var volume = $(element).val();
        var tr = $(element).closest('tr');
        var totalScore = $(element).attr('data-totalscore');
        if ((volume && !isNaN(volume) && parseFloat(volume) > 0) || (totalScore && !isNaN(totalScore) && parseFloat(totalScore) > 0)) {
            model.RiskScoreProductVolumRatingResponses.push({
                RiskFactorId: $(element).attr('data-factorid'),
                RiskSubFactorId: $(element).attr('data-subfactorid'),
                ProductId: $(element).attr('data-productid'),
                Volume: volume,
                Value: tr.find('.productValue').val(),
                TotalScore: totalScore,
                FinalScore: $(element).attr('data-finalscore'),
                RiskRating: $(element).attr('data-riskrating')
            });
        }
    });
    $('input.ratingScore').each(function (i, element) {
        var response = $(element).val();
        if (response && !isNaN(response) && parseFloat(response) > 0) {
            model.RiskScoreResponses.push({
                RiskFactorId: $(element).attr('data-factorid'),
                RiskSubFactorId: $(element).attr('data-subfactorid'),
                ProductId: $(element).attr('data-productid'),
                RiskCriteriaId: $(element).attr('data-criteriaid'),
                Score: response,
                QuestionIds: $(element).attr('data-questions'),
                Answers: $(element).attr('data-answers')
            });
        }
    });

    $('table.tblSubFactorVolumeDetails').each(function () {
        var riskFactorId = $(this).attr('data-factorid');
        var riskSubFactorId = $(this).attr('data-subfactorid');
        var volume1Input = $(this).find('tr.trVolume1 input.txtSubFactorVolume');
        var volume2Input = $(this).find('tr.trVolume2 input.txtSubFactorVolume');
        var volume3Input = $(this).find('tr.trVolume3 input.txtSubFactorVolume');
        var volume4Input = $(this).find('tr.trVolume4 input.txtSubFactorVolume');
        var volume5Input = $(this).find('tr.trVolume5 input.txtSubFactorVolume');
        var countries = $(this).attr('data-countries');
        var ratings = $(this).attr('data-ratings');
        var volumes = $(this).attr('data-volume');

        var volume1 = volume1Input.val();
        var volume2 = volume2Input.val();
        var volume3 = volume3Input.val();
        var volume4 = volume4Input.val();
        var volume5 = volume5Input.val();
        if ((volume1 && !isNaN(volume1)) || (volume2 && !isNaN(volume2)) || (volume3 && !isNaN(volume3))
            || (volume4 && !isNaN(volume4)) || (volume5 && !isNaN(volume5))) {
            model.VolumeMappings.push({
                RiskFactorId: riskFactorId,
                RiskSubFactorId: riskSubFactorId,

                Score1: volume1Input.attr('data-score'),
                Volume1: parseFloat(volume1),
                Weight1: volume1Input.attr('data-weight'),
                WeightedScore1: volume1Input.attr('data-weightedscore'),

                Score2: volume2Input.attr('data-score'),
                Volume2: parseFloat(volume2),
                Weight2: volume2Input.attr('data-weight'),
                WeightedScore2: volume2Input.attr('data-weightedscore'),

                Score3: volume3Input.attr('data-score'),
                Volume3: parseFloat(volume3),
                Weight3: volume3Input.attr('data-weight'),
                WeightedScore3: volume3Input.attr('data-weightedscore'),

                Score4: volume4Input.length > 0 ? volume4Input.attr('data-score') : null,
                Volume4: volume4Input.length > 0 ? parseFloat(volume4) : null,
                Weight4: volume4Input.length > 0 ? volume4Input.attr('data-weight') : null,
                WeightedScore4: volume4Input.length > 0 ? volume4Input.attr('data-weightedscore') : null,

                Score5: volume5Input.length > 0 ? volume5Input.attr('data-score') : null,
                Volume5: volume5Input.length > 0 ? parseFloat(volume5) : null,
                Weight5: volume5Input.length > 0 ? volume5Input.attr('data-weight') : null,
                WeightedScore5: volume5Input.length > 0 ? volume5Input.attr('data-weightedscore') : null,

                Countries: countries,
                CountryWiseRating: ratings,
                CountryWiseVolume: volumes
            });
        }
    });
    return model;
}

function saveResponse() {
    var model = getSaveResponseModel();
    if (model.RiskScoreResponses.length == 0 && model.RiskSubFactorResponses.length == 0) {
        fcraApp.showToast('Alert', 'No response to update');
        return;
    }
    var riskFactorId = null;
    if ($('#hdRiskFactorId').length > 0 && $('#hdRiskFactorId').val())
        riskFactorId = $('#hdRiskFactorId').val();

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

    fcraApp.postAjaxRequest(saveResponseUrl,
        { items: model, stageId: stage, riskTypeId: riskType, geoPresenceId: geoPresence, customerSegmentId: customerSegment, businessSegmentId: businessSegment, riskFactor: riskFactorId },
        function (response) {
            fcraApp.showToast('Alert', response.message);
            if (response.status)
                isDataChanged = false;
        }
    );
}

function showAddQuestionModal() {
    oInputQuestion = $(this).closest('td').find('input.ratingScore');

    var riskFactorId = oInputQuestion.attr('data-factorid');
    var riskSubFactorId = oInputQuestion.attr('data-subfactorid');
    var productId = oInputQuestion.attr('data-productid');
    var criteriaId = oInputQuestion.attr('data-criteriaid');
    var questions = oInputQuestion.attr('data-questions');
    var answers = oInputQuestion.attr('data-answers');

    fcraApp.postAjaxRequest(getQuestionRiskCriteriaMappingUrl,
        {
            riskFactorId: riskFactorId, riskSubFactorId: riskSubFactorId, productId: productId
            , riskCriteriaId: criteriaId, questionId: questions, answers: answers
        },
        function (response) {
            $('#tblSelectedQuestions tbody').html(response);
            $('#divAddQuestionModal').modal('show');
        });

}

function addQuestionRating() {
    var qIds = [];
    var aIds = [];
    var total = 0;
    $('#tblSelectedQuestions tbody tr select.ddlQuestionRating').each(function () {
        qIds.push(parseInt($(this).attr('data-qid')));
        aIds.push(parseInt($(this).val()));
        total += parseInt($(this).val());
    });
    oInputQuestion.attr('data-questions', qIds.join(','));
    oInputQuestion.attr('data-answers', aIds.join(','));
    oInputQuestion.val(total).change();
    $('#divAddQuestionModal').modal('hide');
}

function showCountryVolumeModal() {
    oTblCountryVolume = $(this).closest('table');
    var countries = oTblCountryVolume.attr('data-countries');
    var ratings = oTblCountryVolume.attr('data-ratings');
    var volumes = oTblCountryVolume.attr('data-volume');

    fcraApp.postAjaxRequest(getCountriesUrl,
        {
            countries: countries, ratings: ratings, volumes: volumes
        },
        function (response) {
            $('#divCountriesVolumeModal tbody').html(response);
            $('#divCountriesVolumeModal').modal('show');
        });
}

function addCountryVolume() {
    var countries = [];
    var ratings = [];
    var volume = [];
    var volume1 = 0.00;
    var volume2 = 0.00;
    var volume3 = 0.00;
    var volume4 = 0.00;
    var volume5 = 0.00;

    $('.txtSubFactorCountryVolume').each(function () {
        var val = $(this).val();
        if (val && !isNaN(val) && parseFloat(val) > 0) {
            volume.push(parseFloat(val));
            countries.push($(this).attr('data-countryid'));
            var rating = $(this).attr('data-rating');
            ratings.push(rating);
            if (rating == 1) {
                volume1 += parseFloat(val);
            } else if (rating == 2) {
                volume2 += parseFloat(val);
            } else if (rating == 3) {
                volume3 += parseFloat(val);
            } else if (rating == 4) {
                volume4 += parseFloat(val);
            } else if (rating == 5) {
                volume5 += parseFloat(val);
            }
        }
    });
    if (oTblCountryVolume && countries.length > 0) {
        oTblCountryVolume.attr('data-countries', countries.join(','));
        oTblCountryVolume.attr('data-ratings', ratings.join(','));
        oTblCountryVolume.attr('data-volume', volume.join(','));
    }
    oTblCountryVolume.find('tr.trVolume1 input.txtSubFactorVolume').val(volume1).change();
    oTblCountryVolume.find('tr.trVolume2 input.txtSubFactorVolume').val(volume2).change();
    oTblCountryVolume.find('tr.trVolume3 input.txtSubFactorVolume').val(volume3).change();
    oTblCountryVolume.find('tr.trVolume4 input.txtSubFactorVolume').val(volume4).change();
    oTblCountryVolume.find('tr.trVolume5 input.txtSubFactorVolume').val(volume5).change();
    $('#divCountriesVolumeModal').modal('hide');
}

function showSubFactorFileSelectionModal() {
    currentButton = $(this);
    var subfactorid = currentButton.closest('td').attr('data-subfactorid');
    $('#hdSubfactorIdModal').val(subfactorid);
    $('#divChangeRiskSubFactorFileModal').modal('show');

}

function updateSelectedFile() {
    var files = $("#divChangeRiskSubFactorFileModal #fileCustomModal").get(0).files;
    if (!files || files.length === 0) {
        alert('Please select File');
        return;
    }
    var subfactorIdModal = $('#divChangeRiskSubFactorFileModal #hdSubfactorIdModal').val();
    var fileList = []

    var formData = new FormData();
    formData.append("RiskSubFactorId", subfactorIdModal);
    for (var i = 0; i < files.length; i++) {
        formData.append("FormFile", files[i]);
    }

    var requestUrl = SubFactorTempFileItemAddUrl;

    $.ajax({
        url: requestUrl,
        type: "POST",
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {

            $('#divChangeRiskSubFactorFileModal').modal('hide');
            currentButton = null;
        }
    });
}

function showSaveApproveResponseModal() {
    buttonType = $(this).val();
    $('#divSaveApproveRemarksModal #txtSaveApproveResponseRemarks').val('');
    $('#divSaveApproveRemarksModal').modal('show');
}


function saveApproveResponseRemarks() {
    var approvalStatus = $('#hdApprovalStatus').val();
    if (!$('#divSaveApproveRemarksModal #txtSaveApproveResponseRemarks').val()) {
        fcraApp.showToast('Alert', 'Please enter remarks');
        return;
    }

    if (!confirm('Are you sure to update?')) {
        return;
    }
    var UserType = $('#hdUserType').val();
    var iscreateVersion = $('#hdcreateVersion').val();
    if (UserType && !isNaN(UserType) && buttonType != null && buttonType == '1' && UserType == 5 && iscreateVersion == 'True') {
        saveApprovedResponse();
    }
    saveApprovalResponse();


}

function saveApprovalResponse() {
    var model = getSaveApproveResponseModel();
    var pendingWithUserType = model.ApprovalHistory[0].PendingWithUserType;
    fcraApp.postAjaxRequest(SubmitApprovalRequestUrl,
        { modeldata: model, PendingWithUserType: pendingWithUserType },
        function (response) {
            fcraApp.showToast('Alert', response.message);
            $('#divSaveApproveRemarksModal').modal('hide');
            location.reload();
        }
    );
}

function getSaveApproveResponseModel() {
    var riskFactorId = null;
    if ($('#hdRiskFactorId').length > 0 && $('#hdRiskFactorId').val())
        riskFactorId = $('#hdRiskFactorId').val();

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

    var model = {
        ApprovalHistory: [],
        StageId: stage,
        RiskTypeId: riskType,
        GeographicPresenceId: geoPresence,
        CustomerSegmentId: customerSegment,
        BusinessSegmentId: businessSegment
    };
    var UserType = $('#hdUserType').val();
    var status = 0;
    var pendingWithUserType = 0
    if (UserType && !isNaN(UserType)) {
        if (buttonType != null && buttonType == '1') {
            status = 1;
            pendingWithUserType = 0;
        }
        else if (buttonType != null && buttonType == '0') {
            status = 0;
            pendingWithUserType = 0;
        }
        else if (buttonType == '2') {
            status = 1;
            pendingWithUserType = 4;
        }
    }
    model.ApprovalHistory.push({
        Status: status,
        Remark: $('#divSaveApproveRemarksModal #txtSaveApproveResponseRemarks').val(),
        PendingWithUserType: pendingWithUserType
    });

    return model;
}

function saveApprovedResponse() {
    var riskFactorId = null;
    if ($('#hdRiskFactorId').length > 0 && $('#hdRiskFactorId').val())
        riskFactorId = $('#hdRiskFactorId').val();

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

    fcraApp.postAjaxRequest(saveApprovedResponseUrl,
        { stageId: stage, riskTypeId: riskType, geoPresenceId: geoPresence, customerSegmentId: customerSegment, businessSegmentId: businessSegment, riskFactor: riskFactorId },
        function (response) {
            $('#divSaveApproveRemarksModal').modal('hide');
            location.reload();
        }
    );
}

function showAuditTrail() {
    var objectIds = parseInt($('#hdCustomerId').val());
    fcraApp.postAjaxRequest(getRiskAssessmentAuditTrailurl,
        {
            objectId: objectIds
        },
        function (response) {
            $('#divAuditTrailModal .modal-body').html(response);
            $('#divAuditTrailModal').modal('show');
        });

}