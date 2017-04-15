/// <reference path="../bower_components/jquery/dist/jquery.js" />
/// <reference path="~/bower_components/jquery-ui/jquery-ui.min.js

$(document).ready(function()
{
    // Link the chat
    createConnection();

    // If the content is empty, load in the announcements content.
    if ($("#content").is(":empty"))
    {
        $("#content").load("/Announcement/Announcements", function() {
            initialiseDates();
        });
    }

    // On click of a section button, load the partial view from the controller.
    $(".standard-panel").click(function ()
    {
        $(".standard-panel").css({ "background-color": "#0078d7", "color": "#FFFFFF" });
        $(this).css({ "background-color": "#FFFFFF", "color": "#0078d7" });
        $("#content").load($(this).data("url"), function ()
        {
            if ($(".graph-container").length)
            {
                var data = $.ajax({
                    url: $(".graphMyTasks").data("url"),
                    success: (function (data, status)
                    {
                        createGraph(data);
                    })
                });
            }
            // Hacky, but initialise the date controls
            initialiseDates();
        });
    });

    // When a modal is shown, make it draggable.
    $(document).on("shown.bs.modal", ".modal", function() {
        makeDraggableModal();
    })
})

// When hooking into the submit event, manually remove the bootstrap modal form.
function clearModal()
{
    $('body').removeClass('modal');
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