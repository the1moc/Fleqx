$(function()
{

	// Show the modal edit form for a task.
	$(document).on("click", ".task-item", function()
	{
		$(".modal-form-section").load("/Task/GetEditModalView", { taskId: this.id }, function()
		{
			$("#editTaskModal").modal();

			// Hide these fields if the task is not in the closed state.
			if($("#task-state-selection").val() != 3)
			{
				$("#actual-days").hide();
				$("#actual-finish").hide();
			}

			$(".date").datepicker();
		});
	})

	// Show the modal add form for a task.
	$(document).on("click", ".add-task", function()
	{
		// Determine which of the add buttons was pressed.
		var state;
		switch ($(this).attr("id"))
		{
			case "1":
				state = 1;
				break;
			case "2":
				state = 2;
				break;
			default:
				state = 3;
				break;
		}
		$(".modal-form-section").load("/Task/GetAddModalView", function()
		{
			$("#addTaskModal").modal();

			// Set the state of the task to add.
			$("#task-state-selection").val(state);

			// Hide these fields if the task is not in the closed state.
			if ($("#task-state-selection").val() != 3)
			{
				$("#actual-days").hide();
				$("#actual-finish").hide();
			}

			$(".date").datepicker();
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

	$(document).on("change", "#task-state-selection", function()
	{
		if ($(this).val() == 3)
		{
			$("#actual-days").show();
			$("#actual-finish").show();
		}
		else
		{
			$("#actual-days").hide();
			$("#actual-finish").hide();
		}
	});
});