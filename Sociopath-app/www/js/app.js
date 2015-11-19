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
		$.mobile.loading("show");
		api.post('api/feed', {message: self.newMessage(), userid: window.localStorage.getItem('sociopath_userId')}).done(function(data) {
			self.messages.unshift(data);
			self.newMessage('');
			$('#new-message').popup('close');
			$.mobile.silentScroll(0);
			$.mobile.loading("hide");
		});
	}
	
	return self;
};

 
},{"../api":3}],2:[function(require,module,exports){
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
},{"../api":3}],3:[function(require,module,exports){
module.exports = {
	
	get: function(url, data) {
		return $.ajax({
			type: 'GET',
			url: 'http://192.168.43.195/' + url,
			data: data
		});
	},
	
	post: function(url, data) {
		return $.ajax({
			type: 'POST',
			url: 'http://192.168.43.195/' + url,
			data: JSON.stringify(data),
			contentType: 'application/json'
		});
	},
	
	put: function(url, data) {
		return $.ajax({
			type: 'PUT',
			url: 'http://192.168.43.195/' + url,
			data: JSON.stringify(data),
			contentType: 'application/json'
		});
	}
}
},{}],4:[function(require,module,exports){
module.exports = {
	
	init: function(page) {
		require('./api');
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
	
		$( ":mobile-pagecontainer" ).on( "pagecontainerchange", function( event, ui ) {
			if (ui.options.target == 'home.html' && ui.options.reloadPage) {
				home.init(ui.toPage[0]);
			} else if (ui.options.target == 'settings.html' && ui.options.reloadPage) {
				settings.init(ui.toPage[0]);
			}
		} );	
	
		$('.facebookLogin').on('click', function() {
			OAuth.initialize('bQveABZX4h316Hug8fo7MP_mqZw');
			OAuth.popup('facebook').done(function(data) {
				data.me().done(function(me) {
					var request = { 
						provider: 1,
						token: data.access_token,
						externalid: me.id
					};		
					$.mobile.loading("show");
					api.post('api/users/', request).done(function(user) {
						window.localStorage.setItem("sociopath_userId", user.UserId);
						$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", reloadPage: true } );
						$.mobile.loading("hide");
					});					
				});
			});
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
					$.mobile.loading("show");
					api.post('api/users/', request).done(function(user) {	
						window.localStorage.setItem("sociopath_userId", user.UserId);
						$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", reloadPage: true } );
						$.mobile.loading("hide");
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


ko.bindingHandlers.jqmChecked = {
    init: ko.bindingHandlers.checked.init,
    update: function (element, valueAccessor) {
        //KO v3 and previous versions of KO handle this differently
        //KO v3 does not use 'update' for 'checked' binding
        if (ko.bindingHandlers.checked.update) 
            ko.bindingHandlers.checked.update.apply(this, arguments); //for KO < v3, delegate the call
        else 
            ko.utils.unwrapObservable(valueAccessor()); //for KO v3, force a subscription to get further updates

        if ($(element).data("mobile-checkboxradio")) //calling 'refresh' only if already enhanced by JQM
            $(element).checkboxradio('refresh');
    }
};

},{"./api":3,"./home":4,"./settings":6}],6:[function(require,module,exports){
module.exports = {
	
	init: function(page) {
		require('./api');
		var settingsViewModel = require('./ViewModels/SettingsViewModel');
		
		api.get('api/users/'+window.localStorage.getItem('sociopath_userId')).done(function (data) {
			ko.applyBindings(new settingsViewModel(data), page);
		});
		
		
	}
}

},{"./ViewModels/SettingsViewModel":2,"./api":3}]},{},[5]);
