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
			} else if (ui.options.target == 'settings.html' && ui.options.reloadPage) {
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

