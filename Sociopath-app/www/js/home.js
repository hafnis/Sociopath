module.exports = {
	
	init: function(page) {
		require('./api');
		var homeViewModel = require('./ViewModels/FeedViewModel'),
			messages;
		
		api.get('api/feed', {userid: 5}).done(function (data) {
		   console.log(data);
		   messages = data;
		   ko.applyBindings(new homeViewModel(messages), page);
		});
	
	}
}

