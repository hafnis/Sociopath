module.exports = function (messages) {
	var self = this;
	require('../api');
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", reloadPage: true } );	
	}

	self.newMessage = ko.observable('');
	
	self.send = function() {
		$.mobile.loading("show");
		api.post('api/feed', {message: self.newMessage(), userid: window.localStorage.getItem('sociopath_userId')}).done(function(data) {
			self.messages.unshift(data);
			self.newMessage('');
			$('#new-message').popup('close');
			$.mobile.silentScroll(0);
			$.mobile.loading("hide");
		});
	}
	
	return self;
};

 