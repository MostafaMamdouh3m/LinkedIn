$("#Post_board_attach button").on("click", function () {


       var currentDate = new Date();
       var data = { Body: "Hello World ", Date: currentDate.toISOString(), isShared: 0, LikeCount: 0, CommentCount: 0, Media: "", Fk_PostOwner: "", Fk_SharedPost: "" };


       $.ajax({
           url: "/Messages/AjaxSendMessage",
           type: 'POST',
           data: data,
           success: function (result) {

           }
       });

})

$("#TextArea_post").keyup(function () {
    if ($("#TextArea_post").val() != "") {
        $("#btnSubmit").prop('disabled', false);
        $("#btnSubmit").removeClass("disabled");
    }
    else {
        $("#btnSubmit").prop('disabled', true);
        $("#btnSubmit").addClass("disabled");
    }
});

$("#btnSubmit").on("click", function () {
    var currentDate = new Date();

    var post = document.getElementById("TextArea_post").value;
    var data = { Body: post, Date: currentDate.toISOString(), isShared: 0, LikeCount: 0, CommentCount: 0, Media: "", Fk_PostOwner: "", Fk_SharedPost: "" };
    $.ajax({
        url: "/Home/AjaxAddPost",
        type: 'POST',
        data: data,
        success: function (result) {
            $("#Posts").prepend(result);
        }
    });
    $("#TextArea_post").val('');
})

function WriteComment(id) {
    if ($(`#TextArea-${id}`).val() != '') {
        var currentDate = new Date();
        var CommentBody = $(`#TextArea-${id}`).val();
        var FK_PostID = id
        var data = { Body: CommentBody, Date: currentDate.toISOString(), FK_postId: FK_PostID, Fk_CommentOwner: "" };
        $.ajax({
            url: "/Home/AjaxAddComment",
            type: 'POST',
            data: data,
            success: function (result) {
                $(`#ListOfCommnets-${id}`).append(result);

            }
        });
    }
    $(`#TextArea-${id}`).val('');

}

function addLike(id) {

   
   
    var data = { Fk_Post: id };
    var element = $(`#like-count-${id}`);
    var linkeNumder = new Number(element.text());

    $.ajax({
        url: "/Home/AjaxAddLike",
        type: 'POST',
        data: data,
        success: function () {
            if (element.attr("isLiked") == "false") {
                element.attr("isLiked", "true");
                linkeNumder++;
            }
            else {
                element.attr("isLiked", "false");
                linkeNumder--;
            }
            element.text(linkeNumder);
        },
        error: function (error) {
            console.log(error);
        }
    });


}


function acceptRequest(Id) {
    $.ajax({
        url: "/Home/AcceptRequest",
        type: 'POST',
        data: { Id : Id },
        success: function (result) {
            $("[requestId='" + Id + "']").remove();
        }
    });
}

function removeRequest(Id) {
    $.ajax({
        url: "/Home/RemoveRequest",
        type: 'POST',
        data: { Id: Id },
        success: function (result) {
            $("[requestId='" + Id + "']").remove();
        }
    });
}