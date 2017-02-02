/// <reference path="~/bower_components/jquery/dist/jquery.js

$(document).ready(function()
{
	// Link the chat
	createConnection();

	if ($("#content").is(":empty")) {
		$("#content").load("/Announcement/Announcements");
	}

	// On selection of a section button, load the partial view from the controller.
	$(".panel-button").click(function() {
		$("#content").load($(this).data("url"));
	});

	// Hide the show chat button
	if ($(".chat-container").is(":visible"))
	{
		$(".show-chat").hide();
	}

	// Show the modal form for a task.
	$(document).on("click", ".task-item", function()
	{
		$(".modal-form-section").load("/Task/GetModalView", { taskId: this.id }, function() {
			$("#editTaskModal").modal();
		});
	})

	// Had the chat
	$(".chat-container .panel-heading").click(function()
	{
		$(".chat-container").hide();
		$(".content-container").css("margin-right", 0);
		$(".show-chat").show();
	})

	// Show the chat
	$(".show-chat").click(function()
	{
		$(".chat-container").show();
		$(".content-container").css("margin-right", 300);
		$(".show-chat").hide();
	})
})

function parseContent(content)
{
	// Parse the text area content.
	return $.parseHTML(content);
}