module.exports = function (messages) {
	var self = this;
	
	self.messages = ko.observableArray(messages);
	
	return self;
};