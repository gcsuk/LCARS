function getDeployments() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "/Deployments/GetStatus",
        success: function (data) {

            // Loop through each deployment
            $.each(data, function (key, value) {

                // Get the div container
                var deployment = $("div").find("[data-project-id='" + value.ProjectId + "'][data-environment-id='" + value.EnvironmentId + "']");

                // Populate it
                deployment.html(value.ReleaseVersion);

                // Style it
                deployment.removeClass("loading");
                deployment.addClass(value.State.toLowerCase());

                if (value.State == "Executing") {
                    deployment.html("RUNNING");
                }
            });

            $("div.loading").addClass("none");
            $("div.loading").removeClass("loading");
        },
        error: function () {
            location.reload();
        }
    });
}