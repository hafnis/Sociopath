module.exports = {
	
	init: function(page) {
		var api = require('./api');
		var $ = require('jquery');
		$.mobile = require('jquery-mobile');
		var homeViewModel = require('./ViewModels/FeedViewModel'),
			messages;
			$.mobile.loading("show");
		api.get('api/feed', {userid: window.localStorage.getItem("sociopath_userId")}	).done(function (data) {
		   messages = data;
		   ko.applyBindings(new homeViewModel(messages), page);
		   $.mobile.loading("hide");
		});
	
	}
}



