module.exports = {
	
	get: function(url, data) {
		return $.ajax({
			type: 'GET',
			url: 'http://sociopath.azurewebsites.net/' + url,
			data: data
		});
	},
	
	post: function(url, data) {
		return $.ajax({
			type: 'POST',
			url: 'http://sociopath.azurewebsites.net/' + url,
			data: JSON.stringify(data),
			contentType: 'application/json'
		});
	},
	
	put: function(url, data) {
		return $.ajax({
			type: 'PUT',
			url: 'http://sociopath.azurewebsites.net/' + url,
			data: JSON.stringify(data),
			contentType: 'application/json'
		});
	}
}