module.exports = function (messages) {
	var self = this;
	
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", reloadPage: true } );	
	}

	self.newMessage = ko.observable('');
	
	self.send = function() {
		self.messages.unshift({message:self.newMessage(), time: '10:00', networks: ['facebook', 'twitter']});
		self.newMessage('');
		$('#new-message').popup('close');
		$.mobile.silentScroll(0);
	}
	
	return self;
};