$("#submit").click(() => {
	$(location).attr('href',"http://very.hardcoded.software/profile.html?user=" + $("#user").val());	
});

$("#signup").click(() => {
	$(location).attr('href',"http://very.hardcoded.software/signup.html");	
});