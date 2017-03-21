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
                // Reload the announcements with the refreshed data
                $("#content").empty();
                $("#content").load("/Announcement/Announcements", function()
                {
                    $(".date").datepicker({ dateFormat: "yy-mm-dd" });
                });
                $("#announcementModal").modal("toggle");

                // Remove the modal.
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
            }
        })
    });
});