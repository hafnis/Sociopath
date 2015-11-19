module.exports = function (data) {
	var self = this;
	require('../api');
	self.IsTwitterConnected = data.IsTwitterConnected;
	self.IsFacebookConnected = data.IsFacebookConnected;
	self.IsFacebookEnabled = ko.observable(data.IsFacebookEnabled);
	self.IsTwitterEnabled = ko.observable(data.IsTwitterEnabled);
	
	self.save = function() {
		$.mobile.loading("show");
		var request = { UserId: window.localStorage.getItem('sociopath_userId'), IsFacebookEnabled: self.IsFacebookEnabled(), IsTwitterEnabled: self.IsTwitterEnabled()};
		api.put('api/users/', request).done(function() {
			$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );
			$.mobile.loading("hide");			
		});
	};
	
	self.goBack = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );	
	}
	
	return self;
};