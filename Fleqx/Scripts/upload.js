//$(function()
//{
//    $(document).on("click", ".file-list-item", function()
//    {
//        $.ajax({
//            url: "/File/DownloadFile",
//            data: { fileId: $(this).find(".file-info-header-id .file-info").text() },
//            type: "GET",
//            success: function(data)
//            {

//            },
//            error: function(error)
//            {

//            }
//        })
//    })
//})

function initialiseUploads()
{
    $("#fileUpload").fileinput({
        maxFileCount: 5,
        uploadUrl: "/File/UploadFile",
        uploadAsync: false
    });

    $('#fileUpload').on('filebatchuploadcomplete', function(event, data, previewId, index)
    {
        displayPopup("File has been uploaded", "success");
    });
}