module.exports = {
	
	init: function(page) {
		require('./api');
		var settingsViewModel = require('./ViewModels/SettingsViewModel');
		
		api.get('api/users/'+window.localStorage.getItem('sociopath_userId')).done(function (data) {
			ko.applyBindings(new settingsViewModel(data), page);
		});	
		
	}
}
