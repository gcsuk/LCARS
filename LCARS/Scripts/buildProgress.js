function getProgress() {

    var buildTypeIds = [];

    $("div.build").each(function () {
        buildTypeIds.push($(this).data("typeid"));
    });

    $.get("/Builds/Status/",
        $.param({ buildTypeIds: buildTypeIds }, true),
        function (data) {
            $.each(data, function (index, value) {

                if (value.Progress === null) {

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

    setTimeout("getProgress()", 1000);
}

getProgress();