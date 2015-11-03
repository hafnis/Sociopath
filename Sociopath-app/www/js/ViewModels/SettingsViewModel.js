module.exports = function () {
	var self = this;
	
	self.goBack = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );	
	}
	
	return self;
};