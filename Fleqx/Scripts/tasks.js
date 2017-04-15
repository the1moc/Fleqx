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

            // Hide these fields if the task is in the open state.
            if ($("#task-state-selection").val() == 1) {
                $("#actual-start").hide();
            }
            initialiseDates();
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

            // Hide these fields if the task is in the open state.
            if ($("#task-state-selection").val() == 1) {
                $("#actual-start").hide();
            }

            initialiseDates();
        });
    })

    // Hook into the submit event for the edit task form.
    $(document).on("submit", "#editTaskForm", function(event)
    {
        event.preventDefault();
        plugEmptyDates();

        $.ajax({
            type: "POST",
            url: "/Task/Edit",
            data: $(this).serialize(),
            success: function()
            {
                // Reload the tasks with the refreshed data
                $("#content").empty();
                $("#content").load("/Task/Tasks", function ()
                {
                    initialiseDates();
                });
                $("#editTaskModal").modal("hide");

                displayPopup("Task has been changed.");
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
        plugEmptyDates();

        $.ajax({
            type: "POST",
            url: "/Task/Add",
            data: $(this).serialize(),
            success: function()
            {
                // Reload the tasks with the refreshed data
                $("#content").empty();
                $("#content").load("/Task/Tasks", function ()
                {
                    initialiseDates();
                });
                $("#addTaskModal").modal("hide");

                displayPopup("New task has been added.");
            },
            error: function(jqXHR, textStatus, errorThrown)
            {
                alert(errorThrown);
            }
        })
    });

    // Hook into the submit event for the task filter form.
    $(document).on("submit", "#taskFilterForm", function(event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Task/FilterTasks",
            data: $(this).serialize(),
            success: function(data, status)
            {
                // Reload the announcements with the new data.
                $("#content").empty();
                $("#content").html(data);
                initialiseDates();

                displayPopup("Filter has been applied.");
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

        // Hide these fields if the task is in the open state.
        if ($("#task-state-selection").val() == 1) {
            $("#actual-start").hide();
        }
        else {
            $("#actual-start").show();
        }
    });
});

function plugEmptyDates()
{
    $(".date").each(function (index)
    {
        if ($(this).val() == "") {
            $(this).val("9999-12-31");
        }
    });
}

function initialiseDates()
{
    $(".date").datepicker({ dateFormat: "yy-mm-dd" });
    $(".date").each(function (index)
    {
        if ($(this).val() == "9999-12-31") {
            $(this).val("");
        }
    })
}