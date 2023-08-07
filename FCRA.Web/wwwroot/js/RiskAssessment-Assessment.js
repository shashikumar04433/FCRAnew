var oInputQuestion = null;
var oTblCountryVolume = null;
$(function () {
    $(document).on('keyup change', 'input.riskWeightage', calculateScore);
    $(document).on('change', 'select.riskWeightageSelect', calculateScoreSelect);
    $(document).on('blur', 'input.riskWeightage', validateScore);
    $(document).on('keyup change', 'input.ratingScore', calculateRatingScore);
    $(document).on('click', '#btnSaveResponse', saveResponse);
    $(document).on('keyup change', 'input.productVolume', calculateProductVolume);
    $(document).on('keyup change', 'input.txtSubFactorVolume', calculateSubFactorVolumeScore);
    $(document).on('click', '.btnQuestion', showAddQuestionModal);
    $(document).on('click', '#btnAddQuestion', addQuestionRating);
    $(document).on('click', '.btnCountryVolume', showCountryVolumeModal);
    $(document).on('click', '#btnCountriesVolumeModal', addCountryVolume);

    $('select').change();
    $('input').keyup();
    $('tr.trDisabled input,tr.trDisabled select').attr('readonly', true);
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
    var that = $(this);
    var low = parseFloat(that.attr('data-low'));
    var mediumMin = parseFloat(that.attr('data-mediummin'));
    var mediumMax = parseFloat(that.attr('data-mediummax'));
    var high = parseFloat(that.attr('data-high'));
    var value = that.val();
    var score = '-';
    var className = '';
    var rowClassName = '';
    if (value && !isNaN(value)) {
        var decimalVal = parseFloat(value);
        if (decimalVal <= low) {
            score = '1';
            className = 'riskScore1';
            rowClassName = 'riskScore1TR';
        }
        else if (decimalVal >= high) {
            score = '3';
            className = 'riskScore3';
            rowClassName = 'riskScore3TR';

        }
        else {
            score = '2';
            className = 'riskScore2';
            rowClassName = 'riskScore2TR';

        }
    }
    that.closest('tr').removeClass('riskScore1TR riskScore3TR riskScore2TR');
    var td = that.closest('tr').find('.riskScore');
    td.html(score);
    td.removeClass('riskScore1 riskScore3 riskScore2');
    if (className) {
        td.addClass(className);
        that.closest('tr').addClass(rowClassName);
        that.closest('tr').find('.factorTotalWeightedScore');
    }
    setSubFactorWeightedScore(that.closest('tr'), score);
    calculateSubFactorWeightedScore(that.closest('table'));
}

function calculateScoreSelect() {
    var that = $(this);
    var rating = parseInt(that.find('option:selected').attr('data-rating'));
    var score = '-';
    var className = '';
    var rowClassName = '';
    if (rating && rating > 0) {
        if (rating == 1) {
            score = '1';
            className = 'riskScore1'
            rowClassName = 'riskScore1TR';
        }
        else if (rating == 3) {
            score = '3';
            className = 'riskScore3'
            rowClassName = 'riskScore3TR';
        }
        else {
            score = '2';
            className = 'riskScore2'
            rowClassName = 'riskScore2TR';
        }
    }
    that.closest('tr').removeClass('riskScore1TR riskScore3TR riskScore2TR');
    var td = that.closest('tr').find('.riskScore');
    td.html(score);
    td.removeClass('riskScore1 riskScore3 riskScore2');
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
    var that = $(this);
    var tr = that.closest('tr');
    var low = parseFloat(tr.attr('data-low'));
    var high = parseFloat(tr.attr('data-high'));
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
        if (totalScore <= low) {
            score = 10;
            className = 'riskScore1';
            rowClassName = 'riskScore1TR';
            ratingName = 'Low';
            rating = 1;
        }
        else if (totalScore >= high) {
            score = 30;
            className = 'riskScore3';
            rowClassName = 'riskScore3TR';
            ratingName = 'High';
            rating = 3;
        } else {
            score = 20;
            className = 'riskScore2';
            rowClassName = 'riskScore2TR';
            ratingName = 'Medium';
            rating = 2;
        }
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
    td.removeClass('riskScore1 riskScore3 riskScore2');
    tr.removeClass('riskScore1TR riskScore3TR riskScore2TR');
    if (className) {
        td.addClass(className);
        tr.addClass(rowClassName);
    }
    calculateWeightedScore(that);
}

function calculateProductVolume() {
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

function saveResponse() {
    debugger;
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

    $('input.productVolume').each(function (i, element) {
        var volume = $(element).val();
        var totalScore = $(element).attr('data-totalscore');

        if ((volume && !isNaN(volume) && parseFloat(volume) > 0) || (totalScore && !isNaN(totalScore) && parseFloat(totalScore) > 0)) {
            model.RiskScoreProductVolumRatingResponses.push({
                RiskFactorId: $(element).attr('data-factorid'),
                RiskSubFactorId: $(element).attr('data-subfactorid'),
                ProductId: $(element).attr('data-productid'),
                Volume: volume,
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
        var lowVolumeInput = $(this).find('tr.trLow input.txtSubFactorVolume');
        var mediumVolumeInput = $(this).find('tr.trMedium input.txtSubFactorVolume');
        var highVolumeInput = $(this).find('tr.trHigh input.txtSubFactorVolume');
        var countries = $(this).attr('data-countries');
        var ratings = $(this).attr('data-ratings');
        var volumes = $(this).attr('data-volume');

        var lowVolume = lowVolumeInput.val();
        var mediumVolume = mediumVolumeInput.val();
        var highVolume = highVolumeInput.val();
        if ((lowVolume && !isNaN(lowVolume)) || (mediumVolume && !isNaN(mediumVolume)) || (highVolume && !isNaN(highVolume))) {
            model.VolumeMappings.push({
                RiskFactorId: riskFactorId,
                RiskSubFactorId: riskSubFactorId,
                LowRiskScore: lowVolumeInput.attr('data-score'),
                LowRiskVolume: parseFloat(lowVolume),
                LowRiskWeight: lowVolumeInput.attr('data-weight'),
                LowRiskWeightedScore: lowVolumeInput.attr('data-weightedscore'),
                MediumRiskScore: mediumVolumeInput.attr('data-score'),
                MediumRiskVolume: parseFloat(mediumVolume),
                MediumRiskWeight: mediumVolumeInput.attr('data-weight'),
                MediumRiskWeightedScore: mediumVolumeInput.attr('data-weightedscore'),
                HighRiskScore: highVolumeInput.attr('data-score'),
                HighRiskVolume: parseFloat(highVolume),
                HighRiskWeight: highVolumeInput.attr('data-weight'),
                HighRiskWeightedScore: highVolumeInput.attr('data-weightedscore'),
                Countries: countries,
                CountryWiseRating: ratings,
                CountryWiseVolume: volumes
            });
        }
    });

    if (model.RiskScoreResponses.length == 0 && model.RiskSubFactorResponses.length == 0) {
        fcraApp.showToast('Alert', 'No response to update');
        return;
    }

    if (!confirm('Are you sure to update?')) {
        return;
    }

    fcraApp.postAjaxRequest(saveResponseUrl,
        { items: model, riskType: $('#hdTypeId').val(), riskFactor: riskFactorId },
        function (response) {
            fcraApp.showToast('Alert', response.message);
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
    var lowVolume = 0.00;
    var mediumVolume = 0.00;
    var highVolume = 0.00;
    $('.txtSubFactorCountryVolume').each(function () {
        var val = $(this).val();
        if (val && !isNaN(val) && parseFloat(val) > 0) {
            volume.push(parseFloat(val));
            countries.push($(this).attr('data-countryid'));
            var rating = $(this).attr('data-rating');
            ratings.push(rating);
            if (rating == 1) {
                lowVolume += parseFloat(val);
            } else if (rating == 2) {
                mediumVolume += parseFloat(val);
            } else if (rating == 3) {
                highVolume += parseFloat(val);
            }
        }
    });
    if (oTblCountryVolume && countries.length > 0) {
        oTblCountryVolume.attr('data-countries', countries.join(','));
        oTblCountryVolume.attr('data-ratings', ratings.join(','));
        oTblCountryVolume.attr('data-volume', volume.join(','));
    }
    oTblCountryVolume.find('tr.trLow input.txtSubFactorVolume').val(lowVolume).change();
    oTblCountryVolume.find('tr.trMedium input.txtSubFactorVolume').val(mediumVolume).change();
    oTblCountryVolume.find('tr.trHigh input.txtSubFactorVolume').val(highVolume).change();
    $('#divCountriesVolumeModal').modal('hide');
}