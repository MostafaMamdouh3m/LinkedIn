function Editeducation(element) {

    $.ajax({
        url: "/Profile/EditEducationAjax/" + element.id,
        method: "GET",
        success: function (result) {
            $('#edit_education').replaceWith(result);
            $("#edit_education").modal("toggle");
        }
    });
}

$(document).on("click", "#Edit_Education_Submit", function () {



    $.ajax({

        url: '/Profile/EditEducationAjax/',
        method: "POST",
        data: $("#edit_education form").serialize(),
        success: function (result) {
            alert(data.success);
            $("#edit_education").modal("toggle");
            $('#education_data').replaceWith(result);

        }
    });
});


function Deleteducation(element) {

    $.ajax({
        url: "/Profile/DeleteEducationAjax/" + element.id,
        method: "GET",
        success: function (result) {
            $('#delete_education').replaceWith(result);
            $("#delete_education").modal("toggle");

        }
    });
}

$(document).on("click", "#Delete_Education_Submit", function () {

    $.ajax({

        url: '/Profile/DeleteEducationAjax/',
        method: "POST",
        data: $("#delete_education form").serialize(),
        success: function (result) {
            alert(data.success);
            $("#delete_education").modal("toggle");
            $('#education_data').replaceWith(result);

        }
    });
});