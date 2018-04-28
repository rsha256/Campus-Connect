$("#submit").click(() => {
	var url = "/user/get";
	
	$.getJSON(url, {
		data: $("#user").val()
	})
    .done(data => {	
		alert(data);
    });
});