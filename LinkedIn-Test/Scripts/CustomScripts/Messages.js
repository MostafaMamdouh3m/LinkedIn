class MessageUser {
    constructor(name, id, picLink) {
        this.name = name;
        this.id = id;
        this.picture = picLink;
    }

    getMessage(body, date) {
        var msg = "<div><div class=\"message_board_pics\"><div><img src=\"" + this.picture + "\"></div></div><div class=\"message_board_body\"><div><p><span>" + this.name + "</span>&nbsp;</p><p>&nbsp;<span>" + date + "</span></p></div><div>" + body + "</div></div></div>";
        return msg;
    }
}

$(document).ready(function () {

    var pic = $("#nav_profile_img img")[0].src;
    var id = $("#nav_profile_menu_profile").attr("userId");
    var name = $("#nav_profile_name p:first-child")[0].innerText;
    var user = new MessageUser(name, id, pic);
    var tempChats = "";

    //-------------------------------------------------------------------------------------------------- Event Listeners

    
    $("#message_board_attach button").on("click", function () {


        var reciver = GetActivePaneId();
        var sender = $("#nav_profile_menu_profile").attr("userId");

        var currentDate = new Date();
        var message = $("#message_board_new textarea").val();
        var data = { Body: message, Date: currentDate.toISOString(), Fk_Sender: sender, Fk_Reciver: reciver };
        $("#message_board_new textarea").val("");

        $.ajax({
            url: "/Messages/AjaxSendMessage",
            type: 'POST',
            data: data,
            success: function (result) {
                var ampm = currentDate.getHours() >= 12 ? 'PM' : 'AM';
                $("#message_board_messages").append(user.getMessage(message, currentDate.getHours() + ":" + currentDate.getMinutes() + " " + ampm));
                CollectMessages();
                $(".chatter_pane[userId=\"" + GetActivePaneId() + "\"] .chatter_pane_info div:nth-child(2)").html("<span>You : </span>" + message);

            }
        });

    });
    $("#message_board_new textarea").keyup(function (e) {
        var key = e.which || e.keyCode;
        if (key === 13) {
            $("#message_board_attach button").trigger("click");
        }
        if ($("#message_board_new textarea").val() != "") {
            $("#message_board_attach button").prop('disabled', false);
            $("#message_board_attach button").removeClass("disabled");
        }
        else {
            $("#message_board_attach button").prop('disabled', true);
            $("#message_board_attach button").addClass("disabled");
        }
    });
    $("#message_board_title div:nth-child(2)").on("click", function () {
        CheckMessagesUpdate();
    });
    $("#message_chatters_search input").keyup(function () {

        if ($("#message_chatters_search input").val() != "") {

            if (tempChats == "") {
                tempChats = $("#message_chatters_contacts").html();
            }

            $.ajax({
                url: "/Messages/SearchMessages",
                type: 'POST',
                data: { str: $("#message_chatters_search input").val() },
                success: function (result) {
                    $("#message_chatters_contacts").replaceWith(result);
                }
            });
        }
        else {

            console.log(tempChats);
            $("#message_chatters_contacts").html(tempChats);
            tempChats = "";

        }

    });
    $("#load_more_panes").on("click", function () {
        $.ajax({
            url: "/Messages/AjaxLoadMoreChats",
            type: 'POST',
            success: function (result) {
                console.log("Working");
            },
            error: function () {
                $.confirm({
                    title: 'ERROR',
                    content: 'Bad things happened \0/',
                    draggable: true,
                    dragWindowGap: 0
                });
            }
        });
    });


    //-------------------------------------------------------------------------------------------------- functions Calls

    CollectMessages();
    AddActiveAtTopPane();
    $("#message_board_attach button").prop('disabled', true);
    $("#message_board_attach button").addClass("disabled");
    setInterval(CheckMessagesUpdate, 1500);




});


function LoadChat(userId) {

    SetActivePane(userId);

    $.ajax({
        url: "/Messages/AjaxLoadChat",
        type: 'POST',
        data: { Id: userId },
        success: function (result) {
            $('#message_board_messages').replaceWith(result);
            CollectMessages();
        }
    });

    $.ajax({
        url: "/Messages/GetChatInfo",
        type: 'POST',
        data: { Id: userId },
        success: function (result) {
            $("#message_board_title").replaceWith(result);
        }
    });

}
function CheckMessagesUpdate() {

    var Id = GetActivePaneId();

    $.ajax({
        url: "/Messages/CheckChats",
        type: 'POST',
        success: function (result) {
            if (result == "True") {
                UpdateChat(Id);
                UpdateChatPanes(Id);
            }
        }
    });
}
function UpdateChat(Id) {

    var dat = { Id : Id }
   
    $.ajax({
        url: "/Messages/UpdateCurrentChat",
        type: 'POST',
        data: dat,
        success: function (result) {
            $('#message_board_messages').append(result);
            CollectMessages();
        }
    })
}
function UpdateChatPanes(Id) {
    $.ajax({
        url: "/Messages/UpdateChats",
        type: 'POST',
        success: function (result) {
            $("#message_chatters_contacts").replaceWith(result);
            SetActivePane(Id);
        }
    });
}
function CollectMessages() {
    var messages = $("#message_board_messages>div");
    for (var i = 0; i < messages.length - 1; i++) {

        // if name == name of the next element
        if (messages[i].children[1].children[0].children[0].children[0].innerText == messages[i + 1].children[1].children[0].children[0].children[0].innerText) {

            // add the current message to the next div
            var perantElements = messages[i + 1].children[1].children;
            var takenElements = messages[i].children[1].children;

            for (var j = 1; j < takenElements.length;) {
                var insertingElement = takenElements[1];
                var length = perantElements.length;
                var insertingBefore = perantElements[length - 1];
                messages[i + 1].children[1].insertBefore(insertingElement, insertingBefore);
            }

            // remove the current elemet
            messages[i].remove();
        }
        $("#message_board_messages").scrollTop(1000000);

    }
}
function AddActiveAtTopPane() {
    if ($(".chatter_pane").length != 0) {
        $(".chatter_pane")[0].classList.add("active");
        $(".chatter_pane")[0].children[0].classList.add("active");
    }
}
function GetActivePaneId() {
    var panes = $(".chatter_pane");
    var reciver;
    for (var i = 0; i < panes.length; i++) {
        if (panes[i].classList.contains("active")) {
            reciver = panes[i].getAttribute("userId");
            break;
        }
    }
    return reciver;
}
function ClearActivePanes() {
    var panes = $(".chatter_pane");
    for (let i = 0; i < panes.length; i++) {
        panes[i].classList.remove("active");
        panes[i].children[0].classList.remove("active");
    }
}
function SetActivePane(Id) {
    ClearActivePanes();
    $("[userId =" + Id + "]").addClass("active");
    $("[userId =" + Id + "] .chatter_pane_sidebar").addClass("active");
}
