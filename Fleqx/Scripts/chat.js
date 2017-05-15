$(function()
{
    $(".chat-container").hide();
    $(".content-container").css("margin-right", 0);
    $(".show-chat").show();

    // Show the chat
    $(".chat-container .panel-heading").click(function()
    {
        $(".chat-container").hide();
        $(".content-container").css("margin-right", 0);
        $(".show-chat").show();
    })

    // Hide the chat
    $(".show-chat").click(function()
    {
        $(".chat-container").show();
        $(".content-container").css("margin-right", 300);
        $(".show-chat").hide();
    })
});

// Create the connection to the server for the chat.
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
            if (e.keyCode == 13 && $("#message").val() != "") {
                // Save the last message sent.
                chat.server.save($("#message").val());

                // Clear the text box.
                $("#message").val("").focus();
            }
        }));
    });
};

// Get all the current chat messages.
function getMessages() {
    $(".messages-container").load("/Fleqx/Chat/ChatMessages", { call: "chat" }, function(response, status, xhr)
    {
        if (status == "error")
        {
            alert("There was a problem refreshing the chat, make sure the client is connected");
        }
    });
}