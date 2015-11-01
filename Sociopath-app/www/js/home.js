
var home = {

	init: function(page) {
		
		var homeViewModel = {
			message: "labas as krabas"
		};
		
		ko.applyBindings(homeViewModel, page);
	}
}