function Editeducation(element) {

    $.ajax({
        url: "/Profile/EditAjax/" + Number($(element).attr('id').match(/\d+/)[0]),
        method: "GET",
        success: function (result) {
            $('#edit_education').replaceWith(result);
            $("#edit_education").modal("toggle");
        }
    });
}
    
    

