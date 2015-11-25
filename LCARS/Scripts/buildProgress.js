function getProgress() {

    var buildTypeIds = [];

    $("div.build").each(function () {
        buildTypeIds.push($(this).data("typeid"));
    });

    $.get("/Builds/Status/",
        $.param({ buildTypeIds: buildTypeIds }, true),
        function (data) {
            $.each(data, function (index, value) {

                if (value.Status == null) { // Can't connect to TC

                    $("*[data-typeid='" + value.TypeId + "']").html("<div class=\"buildStatusProgress unreachable\">Unreachable</div>");

                } else if (value.Progress === null) { // Not running

                    var versionNumber = "";

                    if (value.Status === "SUCCESS") {
                        versionNumber = " - " + value.Number;
                    }

                    $("*[data-typeid='" + value.TypeId + "']").html("<div class=\"buildStatusProgress " + value.Status.toLowerCase() + "\">" + value.Status + versionNumber + "</div>");
                } else {

                    var progressBackground = "buildingA";

                    if ($("*[data-typeid='" + value.TypeId + "'] div").attr("class") == undefined || $("*[data-typeid='" + value.TypeId + "'] div").attr("class").indexOf("buildingA") > -1) {
                        progressBackground = "buildingB";
                    }

                    $("*[data-typeid='" + value.TypeId + "']").html("<div class=\"buildStatusProgress " + progressBackground + "\">" + value.Progress.Percentage + "%</div>");
                }
            });
        });

    setTimeout("getProgress()", 3000);
}

$(document).ready(function() {
    getProgress();
});