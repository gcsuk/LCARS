function updateStatus(tenant, dependency, environment, currentStatus) {
	$.ajax({
		type: "POST",
		contentType: "application/json",
		data: "{ 'tenant':'" + tenant + "','dependency':'" + dependency + "','environment':'" + environment + "','currentStatus':'" + currentStatus + "' }",
		url: "/Home/UpdateStatus",
		success: function () {
			location.reload();
		},
		error: function (xhr, thrownError) {
			alert(thrownError);
		}
	});
}

function setDigitColor () {
	var rowOne = Math.floor(Math.random() * 6) + 1;
	var rowTwo = rowOne + 1;

	$(".digits tr").removeClass("white");
	$(".digits tr:nth-child(" + rowOne + ")").addClass("white");
	$(".digits tr:nth-child(" + rowTwo + ")").addClass("white");

	setTimeout(setDigitColor, 1000);
};

setDigitColor();