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
            console.log(result);
            $("#delete_education").modal("toggle");
            $('#education_data').replaceWith(result);
        }
    });
});

//      Experience      //
function Editexperience(element) {
    $.ajax({
        url: "/Profile/EditWorkplaceAjax/" + element.id,
        method: "GET",
        success: function (result) {
            $('#edit_experience').replaceWith(result);
            $('#edit_experience').modal("toggle");
        }
    });
}

$(document).on("click", "#Edit_Experience_Submit", function () {
    $.ajax({
        url: "/Profile/EditWorkplaceAjax",
        method: "POST",
        data: $("#edit_experience form").serialize(),
        success: function (result) {
            $("#edit_experience").modal("toggle");
            $('#experience_data').replaceWith(result);
        }
    });
});


function Deleteexperience(element) {
    $.ajax({
        url: "/Profile/DeleteWorkplaceAjax/" + element.id,
        method: "GET",
        success: function (result) {
            $('#delete_experience').replaceWith(result);
            $("#delete_experience").modal("toggle");
        }
    });

}
$(document).on("click", "#Delete_Experience_Submit", function () {
    $.ajax({
        url: "/Profile/DeleteWorkplaceAjax",
        method: "POST",
        data: $("#edit_experience form").serialize(),
        success: function (result) {
            console.log(result);
            $("#delete_experience").modal("toggle");
            $('#experience_data').replaceWith(result);
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
            $("#delete_skill").modal("toggle");
            $('#skill_data').replaceWith(result);
        }
    });
});


$("#add-skill-search").focus(function () {

    $("#profile_skill_menu_underlay").removeClass("hidden");
    $.ajax({
        url: "/Profile/SearchSkills",
        type: 'POST',
        data: { str: $("#add-skill-search").val() },
        success: function (result) {
            $("#profile_skill_menu").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
});

$("#add-skill-search").focusout(function () {
    setTimeout(temp, 150);
    function temp() {
        $("#profile_skill_menu_underlay").addClass("hidden");
    }
});

$("#add-skill-search").keyup(function () {

    $("#profile_skill_menu_underlay").removeClass("hidden");
    $.ajax({
        url: "/Profile/SearchSkills",
        type: 'POST',
        data: { str: $("#add-skill-search").val() },
        success: function (result) {
            $("#profile_skill_menu").html(result);
        },
        error: function (error) {
            
        }
    });

});

function setSkillText(name) {
    $("#add-skill-search").val(name);
    $("#profile_skill_menu_underlay").addClass("hidden");
}

function sendFriendRequest(Id) {

    $.ajax({
        url: "/Profile/FriendRequest",
        type: 'POST',
        data: { Id: Id },
        success: function (result) {
            if ($("#profile_friend").hasClass("sent") || $("#profile_friend").hasClass("friend")) {
                $("#profile_friend").removeClass("sent");
                $("#profile_friend").removeClass("friend");
                $("#profile_friend").text("Send Friend Request");
            }
            else {
                $("#profile_friend").addClass("sent");
                $("#profile_friend").text("Remove Request");
            }
        },
        error: function (error) {

        }
    });
}

$(document).on("click", "#profile_message_modal [type='submit']", function () {

    var message = $("#profile_message_modal textarea").val();
    if (message != "") {
        var currentDate = new Date();
        var reciver = $("#profile_message").attr("userId");
        var sender = $("#nav_profile_menu_profile").attr("userId");
        var data = { Body: message, Date: currentDate.toISOString(), Fk_Sender: sender, Fk_Reciver: reciver };

        $.ajax({
            url: "/Profile/SendMessage",
            type: 'POST',
            data: data,
            success: function (result) {
                $("#profile_message_modal").modal("toggle");
            }
        });
    }
});