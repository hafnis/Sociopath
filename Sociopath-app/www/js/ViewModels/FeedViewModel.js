module.exports = function (messages) {
	var self = this;
	
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", reloadPage: true } );	
	}
	
	return self;
};