var app = (function() {
	
	var self = this;
	
	self.home = require('./home');
	
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
			if (ui.options.target == 'home.html') {
					home.init(ui.toPage[0]);
			}
		} );	
	
		$('.facebookLogin').on('click', function() {
			var fbLoginSuccess = function (userData) {
				$( ":mobile-pagecontainer" ).pagecontainer( "change", "home.html", { role: "page", refresh: true } );		
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

