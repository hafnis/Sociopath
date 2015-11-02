module.exports = {
	
	init: function(page) {
		
		var homeViewModel = require('./ViewModels/FeedViewModel');
		var messages = [
			{message:'this is message 1 this is message 1 this is message 1 this is message 1 this is message 1', time: '10:00', networks: ['facebook', 'twitter']},
			{message:'this is message 2 this is message 2 this is message 2 this is message 2 this is message 2', time: '11:00', networks: ['twitter']},
			{message:'this is message 3 this is message 3 this is message 3 this is message 3 this is message 3', time: '12:00', networks: ['facebook', 'twitter']},
			{message:'this is message 4 this is message 4 this is message 4 this is message 4 this is message 4', time: '13:00', networks: ['facebook']},
			{message:'this is message 5 this is message 5 this is message 5 this is message 5 this is message 5', time: '14:00', networks: ['facebook', 'twitter']},
			{message:'this is message 6 this is message 6 this is message 6 this is message 6 this is message 6', time: '15:00', networks: ['twitter']}
		];
		ko.applyBindings(new homeViewModel(messages), page);
	}
}

