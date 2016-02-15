function setDigitColor() {
    var rowOne = Math.floor(Math.random() * 5) + 1;
    var rowTwo = rowOne + 1;

    $(".digits tr").removeClass("white");
    $(".digits tr:nth-child(" + rowOne + ")").addClass("white");
    $(".digits tr:nth-child(" + rowTwo + ")").addClass("white");

    setTimeout(setDigitColor, 1000);
};

setDigitColor();

function gitStatus(data) {

    var $gitStatus = $("#gitStatus");

    if ($gitStatus.length > 0) {
        if (data.status === "good") {
            window.location = "/";
        }
        else {
            $("#gitStatus").html("GitHub Status: <span class=\"status\">" + data.status + "</span>");
            $("#gitStatusMessage").text(data.body);
            $("#gitStatusDate").text(moment(data.created_on).format("DD MMMM YYYY HH:mm"));
        }
    }
    else {
        if (data.status !== "good") {
            window.location = "/Git/Alert";
        }
    }
}