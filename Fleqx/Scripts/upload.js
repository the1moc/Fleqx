function initialiseUploads()
{
    $("#fileUpload").fileinput({
        maxFileCount: 5,
        uploadUrl: "/Fleqx/File/UploadFile",
        uploadAsync: false,
        success: function()
        {
            updateActivity("8");
        },
        error: function()
        {
            displayPopup("Error uploading the file", "error");
        }
    });

    $('#fileUpload').on('filebatchuploadcomplete', function(event, data, previewId, index)
    {
        updateActivity("8");
        displayPopup("File has been uploaded", "success");
    });
}