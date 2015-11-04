module.exports = {
	
	init: function(page) {
		
		var settingsViewModel = require('./ViewModels/SettingsViewModel');
		
		ko.applyBindings(new settingsViewModel([{connected: true, enabled: true, name: 'Facebook'}, {connected: true, enabled: false, name: 'Twitter'}]), page);
	}
}