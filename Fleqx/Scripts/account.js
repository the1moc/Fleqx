$(function()
{
    // Show the modal edit for account details.
    $(document).on("click", ".account-details", function ()
    {
        $(".modal-form-section").load("/Account/Edit", function()
        {
            $("#accountEditModal").modal();
        });
    })

    // Show the modal add form for a new user.
    $(document).on("click", ".account-new", function ()
    {
        $(".modal-form-section").load("/Account/Signup", function ()
        {
            $("#registerModal").modal();
        });
    })

    // Hook into the submit event for the signup form.
    $(document).on("submit", "#signupForm", function (event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Security/Signup",
            data: $(this).serialize(),
            success: function ()
            {
                // Reload the tasks with the refreshed data
                $("#registerModal").modal("hide");
                displayPopup("The user has been added to the application.");
            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                alert(errorThrown);
            }
        })
    });

    // Hook into the submit event for the account edit form.
    $(document).on("submit", "#accountEditForm", function (event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Account/Edit",
            data: $(this).serialize(),
            success: function ()
            {
                // Reload the tasks with the refreshed data
                $("#accountEditModal").modal("hide");
                displayPopup("Your information has been updated.");
            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                alert(errorThrown);
            }
        })
    });
});