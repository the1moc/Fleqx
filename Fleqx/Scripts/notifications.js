function displayPopup(message)
{
    $("#notification").html("<strong>Success! </strong> " + message);
    $("#notification").removeClass("hidden");
    setTimeout(function ()
    {
        $("#notification").addClass("hidden");
    }, 500)

}