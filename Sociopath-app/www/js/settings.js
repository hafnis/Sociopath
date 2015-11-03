module.exports = {
	
	init: function(page) {
		
		var settingsViewModel = require('./ViewModels/SettingsViewModel');

		ko.applyBindings(new settingsViewModel(), page);
	}
}