function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$("#back").click(() => {
	$(location).attr('href',"http://very.hardcoded.software/profile.html?user=" + getParameterByName('user'));	
});


$("#submit").click(() => {
	var url = "api/user/getTags?user=" + getParameterByName('user') + "&tags=" + $("#tags").val();
	console.log(url);
	$.getJSON(url, function(data) {	
		for (var i = 0; i < data.length; i++) {
			var entry = document.createElement('li');
			var dat = data[i];
			
			entry.innerHTML =
				'<div class="popout">' + 
				'<h5>' + dat.name + '</h5>' +
				'<h6>' + dat.email+ '</h6>' + 
				'<h6>' + dat.tags + '</h6>' + 
				'<br />' + 
				'</div>'
			document.getElementById('list').appendChild(entry);
		}
    });
});