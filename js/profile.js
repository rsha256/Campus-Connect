function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(document).ready(() => {
	console.log("ok")
	var url = "api/user/get?user=" + getParameterByName('user');
	console.log(url);
	$.getJSON(url, function(data) {	
		console.log(data);
		$("#name").text(data.name);
		$("#campus").text(data.campus);
		$("#email").text(data.email);
		$("#tags").text(data.tags);
    });
});

$("#submit").click(() => {
	$(location).attr('href',"http://very.hardcoded.software/edit.html?user=" + getParameterByName('user'));	
});

$("#logout").click(() => {
	$(location).attr('href',"http://very.hardcoded.software/");	
});