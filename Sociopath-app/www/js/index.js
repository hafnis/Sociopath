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
