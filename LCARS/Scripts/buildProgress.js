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
                    $("*[data-typeid='" + value.TypeId + "']").html("<div class=\"buildStatusProgress\"><div style=\"width: " + value.Progress.Percentage + "%\">" + value.Progress.Percentage + "%</div></div>");
                }
            });
        });

    setTimeout("getProgress()", 100);
}

getProgress();