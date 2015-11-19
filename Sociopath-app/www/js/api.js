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