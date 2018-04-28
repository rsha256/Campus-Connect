function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$("#submit").click(() => {
	var xhr = new XMLHttpRequest();
	xhr.open("POST", "api/user/add", true);
	xhr.setRequestHeader('Content-Type', 'application/json');
	xhr.send(JSON.stringify({
		username: $("#user").val(),
		name: $("#name").val(),
		campus: $("#campus").val(),
		email: $("#email").val(),
		tags: $("#tags").val()
	}));
	xhr.onload = function() {
		alert(this.responseText);
	}	
});

$("#tags").on('input', () => {
	if ($("#tags").val().includes(' '))
		$("#tags").val($("#tags").data('in'));
		
	$("#tags").data('in', $("#tags").val());
});

$("#back").click(() => {
	$(location).attr('href',"http://very.hardcoded.software");	
});

$(document).ready(() => {
	var url = "api/user/get?user=" + getParameterByName('user');
	$.getJSON(url, function(data) {	
		console.log(data);
		$("#user").val(data.name);
		$("#campus").val(data.campus);
		$("#email").val(data.email);
		$("#tags").val(data.tags);
    });
});