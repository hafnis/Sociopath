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

