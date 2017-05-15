$(function()
{
    $(".list-header-activity").on("click", "*", function()
    {
        $(".dropdown-menu").empty();

        $.ajax({
            url: "/Fleqx/Activity/GetRecentActivity",
            success: function(data)
            {
                var parsedData = JSON.parse(data);
                parsedData.reverse();
                parsedData.forEach(function(activity)
                {
                    $(".dropdown-menu").append("<li class=\"dropdown-menu-item hvr-bounce-to-right\"><div class=\"username\">" + activity.UserName + " - " + jQuery.timeago(activity.Date) + "</div>" + activity.ActivityContent + "</li>");
                })
            }
        })

    })
})

function updateActivity(activityType)
{
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Fleqx/Activity/SaveActivity",
        data: JSON.stringify({activityType: activityType }),
        success: function()
        {
        }
    });
}