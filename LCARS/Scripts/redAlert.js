var _targetTime;

function getTimeRemaining(isEnabled, targetTime) {

	if (targetTime != undefined)
		_targetTime = targetTime;

	var rightNow = new Date();

	if (!isEnabled || _targetTime < rightNow) {
	    $.ajax({
	        type: "POST",
	        contentType: "application/json",
	        data: "{ 'isEnabled':false,'targetDate':'June 1, 2050 00:00:00', 'alertType': 'Beer' }",
	        url: "/RedAlert/Update",
	        success: function() {

	            var returnUrl = getUrlVars()["returnUrl"];

	            if (returnUrl == null) {
	                window.location = "/";
	            } else {
	                window.location = "/" + returnUrl;
	            }
	        },
	        error: function () {
	            location.reload();
	        }
	    });
	}

	var differenceDays = (_targetTime.getTime() - rightNow.getTime()) / 1000 / 60 / 60 / 24;
	var differenceHours = (_targetTime.getTime() - rightNow.getTime()) / 1000 / 60 / 60;
	var differenceMinutes = (_targetTime.getTime() - rightNow.getTime()) / 1000 / 60;
	var differenceSeconds = (_targetTime.getTime() - rightNow.getTime()) / 1000;
	var differenceMilliseconds = (_targetTime.getTime() - rightNow.getTime()).toString();

	//$("daysRemaining").html(Math.floor(DifferenceDays));

	var hours = Math.floor(differenceHours - ((24 * Math.ceil(differenceDays) - 24)));

	if (hours < 10) {
		hours = "0" + hours;
	} else {
		hours = hours.toString();
	}
	var minutes = Math.ceil(differenceMinutes - ((60 * Math.ceil(differenceHours) - 60)));

	if (minutes < 10) {
		minutes = "0" + minutes;
	} else {
		minutes = minutes.toString();
	}

	var seconds = Math.ceil(differenceSeconds - ((60 * Math.ceil(differenceMinutes) - 60)));

	if (seconds < 10) {
		seconds = "0" + seconds;
	} else {
		seconds = seconds.toString();
	}

	var milliseconds;
	
	if (differenceMilliseconds.length > 2) {
		milliseconds = differenceMilliseconds.substr(differenceMilliseconds.length - 3, differenceMilliseconds.length - 1);
	}
	else if (differenceMilliseconds.length > 1) {
		milliseconds = differenceMilliseconds.substr(differenceMilliseconds.length - 2, differenceMilliseconds.length - 1);
	}
	else {
		milliseconds = differenceMilliseconds.substr(differenceMilliseconds.length - 1, differenceMilliseconds.length - 1);
	}

	$("#hoursRemaining").html(hours);
	$("#minutesRemaining").html(minutes);
	$("#secondsRemaining").html(seconds);
	$("#millisecondsRemaining").html(milliseconds);

	setTimeout("getTimeRemaining(true)", 25);
}

var highlightNumber = 1;

function setHighlight() {

	$(".destruct-bar-large").removeClass("destruct-highlight");
	$(".destruct-bar-medium").removeClass("destruct-highlight");
	$(".destruct-bar-small").removeClass("destruct-highlight");
	$(".destruct-clock").removeClass("destruct-highlight-text");

	switch (highlightNumber) {
		case 1:
			$(".destruct-bar-large").addClass("destruct-highlight");
			break;
		case 2:
			$(".destruct-bar-medium").addClass("destruct-highlight");
			break;
		case 3:
			$(".destruct-bar-small").addClass("destruct-highlight");
			break;
		case 4:
			$(".destruct-clock").addClass("destruct-highlight-text");
			break;
	}

	highlightNumber++;

	if (highlightNumber === 5)
		highlightNumber = 1;

	setTimeout("setHighlight()", 500);
}

setHighlight();