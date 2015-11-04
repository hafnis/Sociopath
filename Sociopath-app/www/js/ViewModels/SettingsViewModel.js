module.exports = function (networks) {
	var self = this;
	
	self.networks = ko.observableArray(networks);
	
	self.goBack = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );	
	}
	
	return self;
};