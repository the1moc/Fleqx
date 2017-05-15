$(function()
{
    // Show the modal edit for account details.
    $(document).on("click", ".account-details", function ()
    {
        $(".modal-form-section").load("/Fleqx/Account/Edit", function()
        {
            $("#accountEditModal").modal();
        });
    })

    // Show the modal add form for a new user.
    $(document).on("click", ".account-new", function ()
    {
        $(".modal-form-section").load("/Fleqx/Account/Signup", function()
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
            url: "/Fleqx/Security/Signup",
            data: $(this).serialize(),
            success: function ()
            {
                updateActivity("6");
                // Reload the tasks with the refreshed data
                $("#registerModal").modal("hide");
                displayPopup("The user has been added to the application.", "success");
                clearModal();
            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                displayPopup(errorThrown, "danger");
            }
        })
    });

    // Hook into the submit event for the account edit form.
    $(document).on("submit", "#accountEditForm", function (event)
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/Fleqx/Account/Edit",
            data: $(this).serialize(),
            success: function ()
            {
                updateActivity("7");
                // Reload the tasks with the refreshed data
                $("#accountEditModal").modal("hide");
                displayPopup("Your information has been updated.");
                clearModal();
            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                displayPopup(errorThrown, "danger");
            }
        })
    });
});