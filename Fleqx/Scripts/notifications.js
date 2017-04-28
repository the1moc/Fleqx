function displayPopup(message, type)
{
    $.notify({
        message: message
    }, {
        type: type,
        showProgressbar: true,
        delay: 2000,
        placement: {
            from: "bottom",
            align: "left"
        },
        timer: 100,
        offset: {
            y: 5
        },
        animate: {
            enter: 'animated fadeIn',
            exit: 'animated fadeOut'
        }
    });

}