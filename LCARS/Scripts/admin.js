$("#updateRedAlert").click(function () {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/UpdateRedAlert",
        dataType: "json",
        data: "{ 'isEnabled':" + $("#isEnabled").is(":checked") + ", 'targetDate': '" + $("#targetDay").val() + "/" + $("#targetMonth").val() + "/" + $("#targetYear").val() + " " + $("#targetHour").val() + ":" +  $("#targetMinute").val() + "', 'alertType': '" + $("#alertType").val() + "' }",
        success: function(data) {
            if (data) {
                alert("Done");
            } else {
                alert("Balls");
            }
        }
    });
});