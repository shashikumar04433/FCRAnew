(function (H) {
    H.wrap(H.Chart.prototype, 'getContainer', function (proceed) {
        proceed.apply(this);

        var chart = this,
            renderer = chart.renderer,
            defOptions = chart.options.defs || [],
            i = defOptions.length,
            def,
            D;

        while (i--) {
            def = defOptions[i];
            var D = renderer.createElement('marker').attr({
                id: def.id,
                viewBox: "0 0 10 10",
                refX: 1,
                refY: 5,
                markerWidth: 1,
                markerHeight: 1,
                orient: 'auto',
                fill: 'inherit',
            }).add(renderer.defs);
            renderer.createElement('path').attr({
                d: def.path,
                fill: 'white'
            }).add(D);
        }
    });

    H.wrap(H.Series.prototype, 'drawGraph', function (proceed) {
        proceed.apply(this);
        if (this.options.endMarker) {
            this.graph.element.setAttribute('marker-end', this.options.endMarker);
        }
    });
})(Highcharts);
$(function () {
    $(document).on('click', '.aDLink', loadData);
    $(document).on('change', '#ddlRiskType,#ddlLevel', loadData)
    loadData();
    //DownloadUpdatedFile();
});

function loadData() {
    var type = $('.aDLink.active').attr('data-type');
    var groups = $('#ddlLevel').val().join(','); 
    if (!groups) {
        $('#divContent').html('');
        return;
    }
    var url = type == '1' ? dashboardDetailsUrl : (type == '3' ? comparisonSummaryUrl : summaryDetailsUrl);
    fcraApp.postAjaxRequest(url,
        { riskTpye: $('#ddlRiskType').val(), group: groups, selectedriskFactorId: 0 },
        function (response) {
            $('#divContent').html(response);
            if (type == '1')
                createChart();
        }
    );
}


function loadDrilledData(groups, selectedriskFactorId = 0, countryId = 0, CustomerSegmentName = '') {
    var type = $('.aDLink.active').attr('data-type');
    if (!groups) {
        $('#divContent').html('');
        return;
    }
    var url = type == '1' ? dashboardDetailsUrl : (type == '3' ? comparisonSummaryUrl : summaryDetailsUrl);
    fcraApp.postAjaxRequest(url,
        { riskTpye: $('#ddlRiskType').val(), group: groups, selectedriskFactorId: selectedriskFactorId, countryId: countryId, customerSegmentName: CustomerSegmentName, isDrillDown: true },
        function (response) {
            if (groups == '2,3') {
                $('#GeographicContent').html(response);
                $('#divGeograhic').modal('show');
                return;
            }
            if (groups == '2,3,4') {
                $('#summaryJuriContent').html(response);
                $('#divsummaryJuri').modal('show');
                return;
            }
            if (groups == '2,3,4,5') {
                $('#SummaryBusinessSegmentContent').html(response);
                $('#divSummaryBusinessSegment').modal('show');
                return;
            }
            if (groups == '2,3,4,5,6') {
                $('#SummaryResponseDetailContent').html(response);
                $('#divSummaryResponseDetail').modal('show');
                return;
            }

            $('#divContent').html(response);
        }
    );
}

function createChart() {
    var riskAssessment = parseFloat($('#hiddRiskAssessment').val());
    var controlAssessment = parseFloat($('#hiddControlAssessment').val()) ;
    $('#heatmap-chart-one').highcharts({
        defs: [{
            id: 'arrow',
            path: 'M 0 0 L 10 5 L 0 10 z',
            fill: 'inherit'
        }],
        responsive: true,
        scaleBeginAtZero: true,
        chart: {
            type: 'heatmap',
            marginTop: 0,
            marginBottom: 50,
            // backgroundColor: '#e7e6e6',
            plotBackgroundColor: {
                linearGradient: {
                    x1: 1,
                    y1: 0,
                    x2: 0,
                    y2: 1
                },
                stops: [
                    [0.23, 'rgb(240, 59, 9)'],
                    [1, 'rgb(255, 224, 80)'],
                    [2, 'rgb(13, 163, 35)'],
                    [3, 'rgb(217, 186, 50']
                ]
            }
        },
        title: {
            text: '',
            align: 'center',
            style: {
                fontWeight: '600'
            }
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        xAxis: {
            categories: ['0.00', '0.50', '1.00', '1.50', '2.00', '2.50', '3.00'],
            //categories: ['1.00', '2.00', '3.00', '4.00', '5.00'],
            title: {
                text: "Overall Inherent Risk",
                style: {
                    fontWeight: '500'
                }
            }
        },
        yAxis: {
            categories: ['0.00', '0.50', '1.00', '1.50', '2.00', '2.50', '3.00'],
            //categories: ['1.00', '2.00', '3.00', '4.00', '5.00'],
            title: {
                text: 'Overall Control Assessment',
                style: {
                    fontWeight: '500'
                }
            },

        },
        colorAxis: {
            min: 0,
            minColor: 'transparent',
            maxColor: 'transparent',
        },
        legend: {
            enabled: false,
            align: 'right',
            layout: 'vertical',
            margin: 0,
            verticalAlign: 'top',
            y: 25,
            symbolHeight: 320,
            itemStyle: {
                fontFamily: "'Barlow Semi Condensed'",
            },
        },

        plotOptions: {
            series: {
                
            }
        },
        series: [{
            type: 'heatmap',
            name: 'Risk Score',
            borderWidth: 0,
            // borderColor: '#FFFFFF',
            data: [
                //[2, 2.45, 2.12],
                [controlAssessment, riskAssessment, ''],

            ],
            dataLabels: {
                enabled: true,
                color: 'black',
                style: {
                    textShadow : 'none'
                }
            }
        }],
        tooltip: {
            enabled: true,
            headerFormat: 'Risk Score <br />',
            //pointFormat: '<b>{point.y}, {point.x}</b>'
            pointFormat: '<b>' + riskAssessment + ', ' + controlAssessment +'</b>'
        }
    });

    //$('#heatmap-chart-one').tooltip({ placement: 'bottom', trigger: 'manual' }).tooltip('show');
    //$('#heatmap-chart-two').highcharts({
    //    defs: [{
    //        id: 'arrow',
    //        path: 'M 0 0 L 10 5 L 0 10 z',
    //        fill: 'inherit'
    //    }],
    //    chart: {
    //        type: 'heatmap',
    //        marginTop: 0,
    //        marginBottom: 40,
    //        // backgroundColor: '#e7e6e6',
    //        plotBackgroundColor: {
    //            linearGradient: {
    //                x1: 1,
    //                y1: 0,
    //                x2: 0,
    //                y2: 1
    //            },
    //            stops: [
    //                [0.23, 'rgb(240, 59, 9)'],
    //                [0.5, 'rgb(255, 224, 80)'],
    //                [0.99, 'rgb(13, 163, 35)'],
    //                [1, 'rgb(217, 186, 50']
    //            ]
    //        }
    //    },
    //    responsive: true,
    //    scaleBeginAtZero: true,
    //    title: {
    //        text: '',
    //        align: 'center',
    //        style: {
    //            fontWeight: '600'
    //        }
    //    },
    //    credits: {
    //        enabled: false
    //    },
    //    exporting: {
    //        enabled: false
    //    },
    //    xAxis: {
    //        categories: ['0.50', '1.00', '1.50', '2.00', '2.50', '3.00'],
    //        title: {
    //            text: "Overall Inherent Risk",
    //            style: {
    //                fontWeight: '500'
    //            }
    //        }
    //    },
    //    yAxis: {
    //        categories: ['0.00', '0.50', '1.00', '1.50', '2.00'],
    //        title: {
    //            text: 'Overall Control Assessment',
    //            style: {
    //                fontWeight: '500'
    //            }
    //        },
    //    },
    //    colorAxis: {
    //        min: 0,
    //        minColor: 'transparent',
    //        maxColor: 'transparent',
    //    },
    //    legend: {
    //        enabled: false,
    //        align: 'right',
    //        layout: 'vertical',
    //        margin: 0,
    //        verticalAlign: 'top',
    //        y: 25,
    //        symbolHeight: 320,
    //        itemStyle: {
    //            fontFamily: "'Barlow Semi Condensed'",
    //        },
    //    },

    //    tooltip: {
    //        enabled: true,
    //        formatter: function () {
    //            return '<b>' + this.series.xAxis.categories[this.point.x] + '</b> sold <br><b>' +
    //                this.point.value + '</b> items on <br><b>' + this.series.yAxis.categories[this.point.y] + '</b>';
    //        }
    //    },
    //    plotOptions: {
    //        series: {
    //            states: {
    //                hover: {
    //                    //color: 'white'
    //                }
    //            }
    //        }
    //    },
    //    series: [{
    //        type: 'heatmap',
    //        // name: 'Current Residual Risk',
    //        borderWidth: 0,
    //        // borderColor: '#FFFFFF',
    //        data: [
    //            [0, 0, 0],
    //            [0, 1, 12],
    //            [0, 2, 8],
    //            [0, 3, 24],
    //            [0, 4, 67],
    //            [1, 0, 92],
    //            [1, 1, 58],
    //            [1, 2, 78],
    //            [1, 3, 117],
    //            [1, 4, 48],
    //            [2, 0, 35],
    //            [2, 1, 15],
    //            [2, 2, 123],
    //            [2, 3, 64],
    //            [2, 4, 52],
    //            [3, 0, 72],
    //            [3, 1, 132],
    //            [3, 2, 114],
    //            [3, 3, 19],
    //            [3, 4, 16],
    //            [4, 0, 38],
    //            [4, 1, 5],
    //            [4, 2, 8],
    //            [4, 3, 117],
    //            [4, 4, 115]
    //        ],
    //        dataLabels: {
    //            enabled: false,
    //            color: 'black',
    //            style: {
    //                textShadow: 'none'
    //            }
    //        }
    //    }]

    //});

    Highcharts.chart('aggregateinherentrisk', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        responsive: true,
        scaleBeginAtZero: true,
        colors: ['#0ea323', '#fdd83a', '#f03b09', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true,
                legend: {
                    itemStyle: {
                        fontFamily: "'Barlow Semi Condensed'",
                    },
                    // layout: number < 15 ? 'vertical' : 'horizontal',
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0,
                    width: 100,
                    itemWidth: 50
                }
            }
        },
        series: [{
            name: 'Value',
            colorByPoint: true,
            data: [{
                name: 'Low',
                y: 7.67,
            }, {
                name: 'Medium',
                y: 14.77
            }, {
                name: 'High',
                y: 70.86
            }]
        }]
    });

    Highcharts.chart('retailinherentrisk', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        responsive: true,
        scaleBeginAtZero: true,
        colors: ['#0ea323', '#fdd83a', '#f03b09', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true,
                legend: {
                    itemStyle: {
                        fontFamily: "'Barlow Semi Condensed'",
                    },
                    // layout: number < 15 ? 'vertical' : 'horizontal',
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0,
                    width: 100,
                    itemWidth: 50
                }
            }
        },
        series: [{
            name: 'Value',
            colorByPoint: true,
            data: [{
                name: 'Low',
                y: 4.67,
                 //sliced: true,
                 //selected: true
            }, {
                name: 'Medium',
                y: 44.77
            }, {
                name: 'High',
                y: 50.86
            }]
        }]
    });


    Highcharts.chart('corporateinherentrisk', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        responsive: true,
        scaleBeginAtZero: true,
        colors: ['#0ea323', '#fdd83a', '#f03b09', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true,
                legend: {
                    itemStyle: {
                        fontFamily: "'Barlow Semi Condensed'",
                    },
                    // layout: number < 15 ? 'vertical' : 'horizontal',
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0,
                    width: 100,
                    itemWidth: 50
                }
            }
        },
        series: [{
            name: 'Value',
            colorByPoint: true,
            data: [{
                name: 'Low',
                y: 14.77,
            }, {
                name: 'Medium',
                y: 15.67,
            }, {
                name: 'High',
                y: 70.86
            }]
        }]
    });

    Highcharts.chart('overallcontrolassessment', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        responsive: true,
        scaleBeginAtZero: true,
        colors: ['#0ea323', '#fdd83a', '#f03b09', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true,
                legend: {
                    itemStyle: {
                        fontFamily: "'Barlow Semi Condensed'",
                    },
                    // layout: number < 15 ? 'vertical' : 'horizontal',
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0,
                    width: 100,
                    itemWidth: 50
                }
            }
        },
        series: [{
            name: 'Controls',
            colorByPoint: true,
            data: [{
                name: 'Need Improvement',
                y: 4.67,
                // sliced: true,
                // selected: true
            }, {
                name: 'Weak',
                y: 24.77
            }, {
                name: 'Adequate',
                y: 60.86
            }]
        }]
    });

    Highcharts.chart('areawisecontrolassessment', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'column',
            zoomType: 'y',
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        responsive: true,
        scaleBeginAtZero: true,
        colors: ['#0ea323', '#fdd83a', '#f03b09', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        xAxis: {
            categories: [
                'Governance and Risk Management',
                'Policies and Procedures',
                'KYC/CDD/EDD',
                'Screening',
                'Transaction Monitoring',
                'Compliance Assurance',
                'Reporting & MIS',
                'Training, Awareness & Record Keeping'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            labels: {
                format: '{value}%'
            },
            title: {
                text: ''
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px"><b>{point.key}</b></span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}%</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    enabled: true
                }
            },
            showInLegend: true,
            legend: {
                itemStyle: {
                    fontFamily: "'Barlow Semi Condensed'",
                },

                // layout: number < 15 ? 'vertical' : 'horizontal',
                // layout: 'vertical',
                //align: 'right',
                // verticalAlign: 'middle',
                borderWidth: 0,
                width: 100,
                itemWidth: 50
            }
        },
        series: [{
            name: 'Adequate',
            data: [15, 0, 0, 38, 5, 20, 10, 90]

        }, {
            name: 'Needs Improvement',
            data: [29, 25, 20, 0, 0, 50, 70, 8]

        },
        {
            name: 'Weak',
            data: [77, 75, 80, 62, 90, 30, 20, 2]
        }
        ]
    });
}

function getSelectedItemsMultiSelect(selector) {
    var ids = [];
    $(selector).closest('span.multiselect-native-select').find('input[type="checkbox"]').each(function () {
        var isSelected = $(this).is(':checked');
        if (isSelected)
            ids.push($(this).val());
    });
    return ids.join(',');
}

function DownloadUpdatedFile() {
    var type = $('#ddlRiskType').val();
    var groups = $('#ddlLevel').val().join(',');
    var url = SummaryReportUrl;
    fcraApp.postAjaxRequest(url,
        { riskType: type, group: groups, html: $('.tblSummaryReport').html() },
        function () {
            $("#summaryRepoLink")[0].click();
        }
    );
}