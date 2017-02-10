/// <reference path="~/bower_components/jquery/dist/jquery.js

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
				clearModal();
			}
		})
	});

	// Hook into the submit event for the edit task form.
	$(document).on("submit", "#editTaskForm", function(event)
	{
		event.preventDefault();

		$.ajax({
			type: "POST",
			url: "/Task/Edit",
			data: $(this).serialize(),
			success: function()
			{
				// Reload the announcements with the refreshed data
				$("#content").empty();
				$("#content").load("/Task/Tasks");
				$("#editTaskForm").modal("toggle");
				clearModal();
			}
		})
	});

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

// When hooking into the submit event, manually remove the bootstrap modal form.
function clearModal()
{
	$('body').removeClass('modal-open');
	$('.modal-backdrop').remove();
}