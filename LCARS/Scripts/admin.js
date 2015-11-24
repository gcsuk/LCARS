$("#updateRedAlert").click(function () {
    $(".confirmation").hide();
    $(".error").hide();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/UpdateRedAlert",
        dataType: "json",
        data: "{ 'isEnabled':" + $("#isEnabled").is(":checked") + ", 'targetDate': '" + $("#targetDay").val() + "/" + $("#targetMonth").val() + "/" + $("#targetYear").val() + " " + $("#targetHour").val() + ":" +  $("#targetMinute").val() + "', 'alertType': '" + $("#alertType").val() + "' }",
        success: function(data) {
            if (data) {
                $(".confirmation").show();
            } else {
                $(".error").show();
            }
        }
    });
});

$("#hasDeadline").click(function () {
    if ($(this).prop("checked")) {
        $("#deadlineDay").removeAttr("disabled");
        $("#deadlineMonth").removeAttr("disabled");
        $("#deadlineYear").removeAttr("disabled");
        $("#deadlineHour").removeAttr("disabled");
        $("#deadlineMinute").removeAttr("disabled");
    } else {
        $("#deadlineDay").attr("disabled", "disabled");
        $("#deadlineMonth").attr("disabled", "disabled");;
        $("#deadlineYear").attr("disabled", "disabled");
        $("#deadlineHour").attr("disabled", "disabled");
        $("#deadlineMinute").attr("disabled", "disabled");
    }
});

$("#screens").on("click", function (e) {
    $(".confirmation").hide();
    $(".error").hide();
    getScreen($(e.target).attr("data-button-id"));
});

function getScreen(id) {
    $.get("/Admin/GetScreen/" + id, function (data) {
        $("#id").val(data.Id);
        $("#name").val(data.Name);

        var boards;

        if (data.Boards.length) {
            var source = $("#boardsTemplate").html();
            var template = Handlebars.compile(source);

            boards = template(data);
        } else {
            boards = "Blank";
        }

        $("#boards").html(borads);
    });
};

$("#updateScreen").click(function () {

    $(".confirmation").hide();
    $(".error").hide();

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/UpdateScreen",
        dataType: "json",
        data: "{ 'id':" + $("#id").val() + ", 'name':'" + $("#name").val() + "' }",
        success: function (data) {

            if (data) {
                $("<div class=\"screen-button apricot\" data-button-id=\"" + $("#id").val() + "\">" + $("#id").val() + "</div>").insertBefore("#new");
            } else {
                $("div[data-button-id='" + $("#id").val() + "'").html($("#id").val());
            }

            $(".confirmation").show();
        },
        error: function () {
            $(".error").show();
        }
    });
});

$("#deleteScreen").click(function () {
    $(".error").hide();

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "/Admin/DeleteScreen",
        data: JSON.stringify({ id: $("#id").val() }),
        success: function (data) {
            if (data) {
                window.location.reload();
            } else {
                $(".error").show();
            }
        }
    });
});

$("#addBoard").click(function () {

    $(".confirmation").hide();
    $(".error").hide();

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/AddBoard",
        dataType: "json",
        data: "{ 'screenId':" + $("#id").val() + ", 'categoryId':" + $("#categories").val() + ", 'argument':'" + $("#argument").val() + "' }",
        success: function (data) {
            getScreen(data);
        },
        error: function () {
            $(".error").show();
        }
    });
});

$("#boards").on("click", function (e) {

    $(".confirmation").hide();
    $(".error").hide();

    var index = $(e.target).parent().parent().attr("data-index");

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/DeleteBoard",
        dataType: "json",
        data: "{ 'screenId':" + $("#id").val() + ", 'boardIndex':" + index + "}",
        success: function (data) {
            getScreen(data);
        },
        error: function () {
            $(".error").show();
        }
    });
});

$("#issues").on("click", function (e) {
    $(".confirmation").hide();
    $(".error").hide();
    getIssueQuery($(e.target).attr("data-button-id"));
});

function getIssueQuery(id) {
    $.get("/Admin/GetIssueQuery/" + id, function (data) {
        $("#id").val(data.Id);
        $("#name").val(data.Name);
        $("#jql").val(data.Jql);

        if (($("#hasDeadline").prop("checked") && data.Deadline == null) || (!$("#hasDeadline").prop("checked") && data.Deadline != null)) {
            $("#hasDeadline").click(); // Checks the box and also fires the click event to enable the combos
        }

        if (data.Deadline != null) {
            $("#deadlineDay").val(data.DeadlineDay);
            $("#deadlineMonth").val(data.DeadlineMonth);
            $("#deadlineYear").val(data.DeadlineYear);
            $("#deadlineHour").val(data.DeadlineHour);
            $("#deadlineMinute").val(data.DeadlineMinute);
        }
    });
};

$("#updateIssueQuery").click(function () {

    $(".confirmation").hide();
    $(".error").hide();

    var deadline = null;

    if ($("#hasDeadline").prop("checked")) {
        deadline = "'" + $("#deadlineDay").val() + "/" + $("#deadlineMonth").val() + "/" + $("#deadlineYear").val() + " " + $("#deadlineHour").val() + ":" + $("#deadlineMinute").val() + "'";
    }

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Admin/UpdateIssueQuery",
        dataType: "json",
        data: "{ 'id':" + $("#id").val() + ", 'name':'" + $("#name").val() + "', 'deadline': " + deadline + ", 'jql': '" + $("#jql").val().replace(/'/g, '"') + "' }",
        success: function (data) {
            if (data) {
                $("<div class=\"issue-button apricot\" data-button-id=\"" + $("#id").val() + "\">" + $("#name").val() + "</div>").insertBefore("#new");
            } else {
                $("div[data-button-id='" + $("#id").val() + "'").html($("#name").val());
            }

            $(".confirmation").show();
        },
        error: function () {
            $(".error").show();
        }
    });
});

$("#deleteIssueQuery").click(function () {
    $(".error").hide();

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "/Admin/DeleteIssueQuery",
        data: JSON.stringify({ id: $("#id").val() }),
        success: function (data) {
            if (data) {
                window.location.reload();
            } else {
                $(".error").show();
            }
        }
    });
});