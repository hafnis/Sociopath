(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
module.exports = function (messages) {
	var self = this;
	require('../api');
	self.messages = ko.observableArray(messages);
	
	self.openSettings = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "settings.html", { role: "page", reloadPage: true } );	
	}

	self.newMessage = ko.observable('');
	
	self.send = function() {
		api.post('api/feed', {message: self.newMessage(), userid: 5}).done(function(data) {
			self.messages.unshift(data);
			self.newMessage('');
			$('#new-message').popup('close');
			$.mobile.silentScroll(0);
		});
	}
	
	return self;
};
},{"../api":3}],2:[function(require,module,exports){
module.exports = function (networks) {
	var self = this;
	
	self.networks = ko.observableArray(networks);
	
	self.goBack = function() {
		$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page" } );	
	}
	
	return self;
};
},{}],3:[function(require,module,exports){
module.exports = {
	
	get: function(url, data) {
		return $.ajax({
			type: 'GET',
			url: 'http://192.168.0.103/' + url,
			data: data
		});
	},
	
	post: function(url, data) {
		return $.ajax({
			type: 'POST',
			url: 'http://192.168.0.103/' + url,
			data: JSON.stringify(data),
			contentType: 'application/json'
		});
	},
	
	put: function(url, data) {
		$.ajax({
			type: 'PUT',
			url: '192.168.0.103/' + url,
			data: data
		}).done(function(data) {
			console.log(data);
			return data;
		});	
	}
}
},{}],4:[function(require,module,exports){
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


},{"./ViewModels/FeedViewModel":1,"./api":3}],5:[function(require,module,exports){
var app = (function() {
	
	var self = this;
	
	self.home = require('./home');
	self.settings = require('./settings');
	self.api = require('./api');
	
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
			} else if (ui.options.target == 'settings.html' && ui.options.reloadPage) {
				settings.init(ui.toPage[0]);
			}
		} );	
	
		$('.facebookLogin').on('click', function() {
			var fbLoginSuccess = function (userData) {
				console.log(userData);
				$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", reloadPage: true } );		
			}
			
			var loginError = function (error) {
				console.log(error);
			}

			facebookConnectPlugin.login(["publish_actions"],
				fbLoginSuccess,
				loginError
			);
		});
		
		$('.twitterLogin').on('click', function() {
			OAuth.initialize('bQveABZX4h316Hug8fo7MP_mqZw');
			OAuth.popup('twitter', {cache: false})
				.done(function(result) {
					var request = { 
						provider: 2,
						secret: result.oauth_token_secret,
						token: result.oauth_token
					};			
					api.post('api/users/', request).done(function() {										
						$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", reloadPage: true } );
					});

				})
				.fail(function (err) {
				  alert(result);
				});
		});
	}
	return self;
})();

app.initialize();

    $(document).on('focus', 'input, textArea', function () {
        $('div[data-role="footer"]').hide();
    })  

    $(document).on('blur', 'input, textarea', function() {
        setTimeout(function() {
            window.scrollTo(document.body.scrollLeft, document.body.scrollTop);
            $('div[data-role="footer"]').show();
        }, 0);
    });


},{"./api":3,"./home":4,"./settings":6}],6:[function(require,module,exports){
module.exports = {
	
	init: function(page) {
		
		var settingsViewModel = require('./ViewModels/SettingsViewModel');
		
		ko.applyBindings(new settingsViewModel([{connected: true, enabled: true, name: 'Facebook'}, {connected: true, enabled: false, name: 'Twitter'}]), page);
	}
}
},{"./ViewModels/SettingsViewModel":2}]},{},[5]);
