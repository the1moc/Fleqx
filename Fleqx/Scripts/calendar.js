
function createCalendar(data)
{
    var parsedData = JSON.parse(data.toLowerCase());
    $("#calendar").fullCalendar({
        editable: true,
        aspectRatio: 2.2,
        events: parsedData
    });

    //$("#calendar").css("max-height", "800px");
    //$("#calendar").css("max-width", "800px");

    //$(".calendar-container").css("max-height", "800px");
}