function apiStatus(data) {

    if (data.status === "good") {
        window.location = "/";
    } else {
        $("#gitStatus").text("GitHub Status: " + data.status);
        $("#gitStatusMessage").text(data.body);
        $("#gitStatusDate").text(moment(data.created_on).format("DD MMMM YYYY hh:mm"));
    }
}