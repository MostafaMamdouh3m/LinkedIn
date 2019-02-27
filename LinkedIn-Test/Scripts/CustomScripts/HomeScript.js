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