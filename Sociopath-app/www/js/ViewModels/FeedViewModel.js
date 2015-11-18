module.exports = function (messages) {
	var self = this;
	require('../api');
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", reloadPage: true } );	
	}

	self.newMessage = ko.observable('');
	
	self.send = function() {
		api.post('api/feed', {message: self.newMessage(), userid: 5}).done(function(data) {
			self.messages.unshift(data);
			self.newMessage('');
			$('#new-message').popup('close');
			$.mobile.silentScroll(0);
		});
	}
	
	return self;
};