/// <reference path="~/bower_components/jquery/dist/jquery.js
/// 
/// 
/// <reference path="~/Scripts/jquery.signalR-2.2.1.min.js

function createConnection() {
	var chat = $.connection.chatHub;
	getMessages();
	var messagesContainer = $(".messages-container");
	messagesContainer.scrollTop(messagesContainer.prop("scrollHeight"));

	// Called when the hub is finished, to refresh the chat.
	chat.client.update = function() {
		getMessages();
	}

	$.connection.hub.start().done(function()
	{
		// Fetch the initial messages.
		getMessages();
		$("#message").on("keyup",
		(function(e) {
			if (e.keyCode == 13) {
				//  Save the last message sent.
				chat.server.save($("#message").val());

				// Clear the text box.
				$("#message").val("").focus();
			}
		}));
	});
};

// Get all the current chat messages
function getMessages() {
	$(".messages-container").load("/Chat/ChatMessages", function(response, status, xhr) {
		if (status == "error")
		{
			alert("There was a problem refreshing the chat, make sure the client is connected");
		}
	});
}