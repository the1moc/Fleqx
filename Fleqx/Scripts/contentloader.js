/// <reference path="../bower_components/jquery/dist/jquery.js" />
/// <reference path="~/bower_components/jquery-ui/jquery-ui.min.js

$(document).ready(function()
{
    // Link the chat
    createConnection();

    setInterval(function ()
    {
        setupTime();
    }, 500);

    $(".navbar").css("width", window.innerWidth - 150);
    $(window).on("resize", function ()
    {
        $(".navbar").css("width", window.innerWidth - 150);
    });

    // On click of a section button, load the partial view from the controller.
    $(".standard-panel").click(function ()
    {
        $("#content").load($(this).data("url"), function ()
        {
            $(".content-title").text($(".section-title").text());
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

            if ((".calendar-container").length)
            {
                var data = $.ajax({
                    url: "Calendar/GetCalendarTasks",
                    success: (function (data, status)
                    {
                        createCalendar(data);
                    })
                });
            }
            // Hacky, but initialise the date controls
            initialiseDates();
        });
    });

    // If the content is empty, load in the announcements content.
    if ($("#content").is(":empty")) {
        $(".announcements-panel").click();
    }

    // When a modal is shown, make it draggable.
    $(document).on("shown.bs.modal", ".modal", function()
    {
        makeDraggableModal();
    });

    $(document).on("hidden.bs.modal", ".modal", function()
    {
        $(".modal-form-section").empty();
    })

})

// When hooking into the submit event, manually remove the bootstrap modal form.
function clearModal()
{
    $('body').removeClass('modal');
    $('.modal-backdrop').remove();
    $(".modal-form-section").empty();
}

// Make a modal form draggable.
function makeDraggableModal()
{
    // Make all generated modal forms draggable.
    $(".modal-dialog").draggable({
        handle: ".modal-header"
    });
}

function setupTime()
{
    var time = moment().format("dddd wo MMMM - HH:mm");
    $(".time-display").text(time);
}