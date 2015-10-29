function updateStatus(tenant, environment, currentStatus) {
	$.ajax({
		type: "POST",
		contentType: "application/json",
		data: "{ 'tenant':'" + tenant + "','environment':'" + environment + "','currentStatus':'" + currentStatus + "' }",
		url: "/Environments/UpdateStatus",
		success: function () {
			location.reload();
		},
		error: function () {
		    location.reload();
		}
	});
}