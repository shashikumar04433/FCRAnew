function DownloadUpdatedFile(vId) {
    var url = SummaryVersionReportUrl;
    fcraApp.postAjaxRequest(url,
        { versionId: vId},
        function () {
            $("#summaryVersionRepoLink")[0].click();
        }
    );
}

function DownloadUpdatedExcelFile(vId) {
    var url = DownloadUrl;
    fcraApp.postAjaxRequest(url,
        { versionId: vId },
        function () {
            
        }
    );
}