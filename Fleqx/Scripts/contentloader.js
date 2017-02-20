/// <reference path="~/bower_components/jquery/dist/jquery.js
/// <reference path="~/bower_components/jquery-ui/jquery-ui.min.js

$(document).ready(function()
{
	// Link the chat
	createConnection();

	// If the content is empty, load in the announcements content.
	if ($("#content").is(":empty")) {
		$("#content").load("/Announcement/Announcements");
	}

	// On click of a section button, load the partial view from the controller.
	$(".panel-button").click(function() {
		$("#content").load($(this).data("url"));
	});

	// Hide the show chat button
	if ($(".chat-container").is(":visible"))
	{
		$(".show-chat").hide();
	}

	// Hook into the submit event for the announcement form.
	$(document).on("submit", "#announcementForm", function(event)
	{
		event.preventDefault();

		$.ajax({
			type: "POST",
			url: "/Announcement/CreateAnnouncement",
			data: $(this).serialize(),
			success: function()
			{
				// Reload the announcements with the refreshed data
				$("#content").empty();
				$("#content").load("/Announcement/Announcements");
				$("#announcementModal").modal("toggle");

				// Remove the modal.
				clearModal();
			}
		})
	});

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

	// When a modal is shown, make it draggable.
	$(document).on("shown.bs.modal", ".modal", function()
	{
		makeDraggableModal();
	})
})

// When hooking into the submit event, manually remove the bootstrap modal form.
function clearModal()
{
	$('body').removeClass('modal-open');
	$('.modal-backdrop').remove();
}

// Make a modal form draggable.
function makeDraggableModal()
{
	// Make all generated modal forms draggable.
	$(".modal-dialog").draggable({
		handle: ".modal-header"
	});
}