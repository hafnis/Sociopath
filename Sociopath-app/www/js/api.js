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