function updateStatus(tenant, dependency, environment, currentStatus) {
	$.ajax({
		type: "POST",
		contentType: "application/json",
		data: "{ 'tenant':'" + tenant + "','dependency':'" + dependency + "','environment':'" + environment + "','currentStatus':'" + currentStatus + "' }",
		url: "/Home/UpdateStatus",
		success: function () {
			location.reload();
		},
		error: function () {
		    location.reload();
		}
	});
}