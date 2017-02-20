// Show the modal edit form for a task.
$(document).on("click", ".task-item", function()
{
	$(".modal-form-section").load("/Task/GetEditModalView", { taskId: this.id }, function()
	{
		$("#editTaskModal").modal();
	});
})

// Show the modal add form for a task.
$(document).on("click", ".add-task", function()
{
	$(".modal-form-section").load("/Task/GetAddModalView", function()
	{
		$("#addTaskModal").modal();
	});
})

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
			// Reload the tasks with the refreshed data
			$("#content").empty();
			$("#content").load("/Task/Tasks");
			$("#editTaskForm").modal("toggle");
			clearModal();
		},
		error: function(jqXHR, textStatus, errorThrown)
		{
			alert(errorThrown);
		}
	})
});

// Hook into the submit event for the add task form.
$(document).on("submit", "#addTaskForm", function(event)
{
	event.preventDefault();

	$.ajax({
		type: "POST",
		url: "/Task/Add",
		data: $(this).serialize(),
		success: function()
		{
			// Reload the tasks with the refreshed data
			$("#content").empty();
			$("#content").load("/Task/Tasks");
			$("#editTaskForm").modal("toggle");
			clearModal();
		},
		error: function(jqXHR, textStatus, errorThrown)
		{
			alert(errorThrown);
		}
	})
});