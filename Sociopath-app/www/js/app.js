(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
module.exports = function (messages) {
	var self = this;
	
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", refresh: true } );	
	}
	
	return self;
};
},{}],2:[function(require,module,exports){
module.exports = function () {
	var self = this;
	
	self.goBack = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );	
	}
	
	return self;
};
},{}],3:[function(require,module,exports){
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


},{"./ViewModels/FeedViewModel":1}],4:[function(require,module,exports){
var app = (function() {
	
	var self = this;
	
	self.home = require('./home');
	self.settings = require('./settings');
	
    self.initialize = function() {
        this.bindEvents();
    },

    self.bindEvents = function() {
        document.addEventListener('deviceready', this.onDeviceReady, false);
    },

    self.onDeviceReady =  function() {
        app.receivedEvent('deviceready');
    },

	
    self.receivedEvent = function(id) {

		$( ":mobile-pagecontainer" ).on( "pagecontainerload", function( event, ui ) {
			if (ui.options.target == 'home.html' && ui.options.reloadPage) {
				home.init(ui.toPage[0]);
			} else if (ui.options.target == 'settings.html') {
				settings.init(ui.toPage[0]);
			}
		} );	
	
		$('.facebookLogin').on('click', function() {
			var fbLoginSuccess = function (userData) {
				$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", reloadPage: true } );		
			}
			
			var loginError = function (error) {
				console.log(error);
			}

			facebookConnectPlugin.login(["public_profile"],
				fbLoginSuccess,
				loginError
			);
		});
	}
	return self;
})();

app.initialize();


},{"./home":3,"./settings":5}],5:[function(require,module,exports){
module.exports = {
	
	init: function(page) {
		
		var settingsViewModel = require('./ViewModels/SettingsViewModel');

		ko.applyBindings(new settingsViewModel(), page);
	}
}
},{"./ViewModels/SettingsViewModel":2}]},{},[4]);
