﻿function Editeducation(element) {

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
            console.log(result);
            $("#edit_education").modal("toggle");
            $('#education_data').replaceWith(result);

        }
    });
});


function Deleteducation(element) {

    $.ajax({
        //alert('done'),
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
            alert('done')
            console.log(result);
            $("#delete_education").modal("toggle");
            $('#education_data').replaceWith(result);
        }
    });
});


function Editskill(element) {

    $.ajax({
        url: "/Profile/EditSkillAjax/" + element.id,
        method: "GET",
        success: function (result) {
            $('#edit_skill').replaceWith(result);
            $("#edit_skill").modal("toggle");
        }
    });
}

$(document).on("click", "#Edit_Skill_Submit", function () {

    $.ajax({

        url: '/Profile/EditSkillAjax/',
        method: "POST",
        data: $("#edit_skill form").serialize(),
        success: function (result) {
            console.log(result);
            $("#edit_skill").modal("toggle");
            $('#skill_data').replaceWith(result);

        }
    });
});


function Deleteskill(element) {

    $.ajax({
        //alert('done'),
        url: "/Profile/DeleteSkillAjax/" + element.id,
        method: "GET",
        success: function (result) {

            $('#delete_skill').replaceWith(result);
            $("#delete_skill").modal("toggle");

        }
    });
}


$(document).on("click", "#Delete_Skill_Submit", function () {

    $.ajax({
        url: '/Profile/DeleteSkillAjax/',
        method: "POST",
        data: $("#delete_skill form").serialize(),
        success: function (result) {
            alert('done')
            console.log(result);
            $("#delete_skill").modal("toggle");
            $('#skill_data').replaceWith(result);
        }
    });
});