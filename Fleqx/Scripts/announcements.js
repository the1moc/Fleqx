$(function()
{
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
                updateActivity("3");
                // Reload the announcements with the refreshed data
                $("#content").empty();
                $("#content").load("/Announcement/Announcements", function()
                {
                    $(".date").datepicker({ dateFormat: "yy-mm-dd" });
                });
                $("#announcementModal").modal("hide");
                displayPopup("New announcement has been added.", "success");
                clearModal();
            }
        })
    });

    // Hook into the submit event for the announcement edit form.
    $(document).on("submit", "#announcementEditForm", function(event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Announcement/Edit",
            data: $(this).serialize(),
            success: function()
            {
                updateActivity("4");
                // Reload the announcements with the refreshed data
                $("#content").empty();
                $("#content").load("/Announcement/Announcements", function()
                {
                    initialiseDates();
                });
                $("#announcementEditModal").modal("hide");
                displayPopup("Announcement was has been updated.", "success");
                clearModal();
            }
        })
    });

    // Hook into the submit event for the announcement edit form.
    $(document).on("submit", "#announcementDeleteForm", function(event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Announcement/Delete",
            data: $(this).serialize(),
            success: function()
            {
                updateActivity("5");
                // Reload the announcements with the refreshed data
                $("#content").empty();
                $("#content").load("/Announcement/Announcements", function()
                {
                    initialiseDates();
                });
                $("#announcementDeleteModal").modal("hide");
                displayPopup("Announcement has been deleted", "success");
                clearModal();
            }
        })
    });

    // Hook into the submit event for the announcement filter form.
    $(document).on("submit", "#announcementFilterForm", function(event)
    {
        event.preventDefault();
        $.ajax({
            type: "POST",
            url: "/Announcement/FilterAnnouncements",
            data: $(this).serialize(),
            success: function(data, status)
            {
                // Reload the announcements with the new data.
                $("#content").empty();
                $("#content").html(data);
                $(".date").datepicker({ dateFormat: "yy-mm-dd" });
                displayPopup("Filter applied.", "success");
            }
        })
    });

    $(document).on("hidden.bs.modal", "#announcementModal", function()
    {
        clearModal();
    })

    $(document).on("hidden.bs.modal", "#announcementEditModal", function()
    {
        clearModal();
    })

    // Show the modal add form for a new announcement
    $(document).on("click", ".add-announcement", function()
    {
        $(".modal-form-section").load("/Announcement/GetAddModalView", function()
        {
            $("#announcementModal").modal();

            initialiseDates();
        });
    })

    // Show the modal edit form for an announcement.
    $(document).on("click", ".edit-announcement", function()
    {
        $(".modal-form-section").load("/Announcement/GetEditModalView", { announcementId: this.id }, function()
        {
            $("#announcementEditModal").modal();

            initialiseDates();
        });
    })

    // Show the modal edit form for an announcement.
    $(document).on("click", ".delete-announcement", function()
    {
        $(".modal-form-section").load("/Announcement/GetDeleteModalView", { announcementId: this.id }, function()
        {
            $("#announcementDeleteModal").modal();

            initialiseDates();
        });
    })

    // Show the modal edit form for an announcement.
    $(document).on("click", ".cancel-form", function()
    {
        clearModal();
    })
});