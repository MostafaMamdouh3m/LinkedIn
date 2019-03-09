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

                var hours = currentDate.getHours() > 12 ? currentDate.getHours() - 12 : currentDate.getHours();
                var hours = hours >= 10 ? hours : "0" + hours;
                var mints = currentDate.getMinutes() >= 10 ? currentDate.getMinutes() : "0" + currentDate.getMinutes();
                var ampm = currentDate.getHours() >= 12 ? 'PM' : 'AM';

                $("#message_board_messages").append(user.getMessage(message, hours + ":" + mints + " " + ampm));
                CollectMessages();
                $(".chatter_pane[userId=\"" + GetActivePaneId() + "\"] .chatter_pane_info div:nth-child(2)").html("<span>You : </span>" + message);
                $(".chatter_pane[userId=\"" + GetActivePaneId() + "\"] .chatter_pane_time").text(hours + ":" + mints + " " + ampm);

            }
        });

    });
    $("#message_board_new textarea").keyup(function (e) {
        var key = e.which || e.keyCode;
        if (key === 13 && GetActivePaneId() != undefined) {
             $("#message_board_attach button").trigger("click");

        }
        if ($("#message_board_new textarea").val() != "" && GetActivePaneId() != undefined) {
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

            $("#message_chatters_contacts").html(tempChats);
            tempChats = "";

        }

    });
    $("#load_more_panes").on("click", function () {

        var chats = $("#load_more_panes > div");
        var Id = GetActivePaneId();

        $.ajax({
            url: "/Messages/AjaxLoadMoreChats",
            type: 'POST',
            data: { currentChatsNumber: chats.length-1 },
            beforeSend: function () {
                $("#chat_loader").css("display", "block");
                $("#load_more_panes p").css("display", "none");
            },
            success: function (result) {
                if (result != "") {
                    $("#message_chatters_contacts").replaceWith(result);
                    SetActivePane(Id);
                }
            },
            error: function (error) {
                $.confirm({
                    title: 'Encountered an error!',
                    content: 'Something went downhill, this may be serious',
                    type: 'red',
                    typeAnimated: true,
                    buttons: {
                        close: function () {}
                    }
                });
            },
            complete: function () {
                setTimeout(temp, 500);
                function temp() {
                    $("#load_more_panes p").css("display", "block");
                    $("#chat_loader").css("display", "none");
                }
            }
        });
    });
    $("#message_chatters_title div").on("click", function () {

        $("#message_board_title").toggle("hidden");
        $("#message_board_messages").toggle("hidden");
        $("#search_users").toggle("nonActive");
        $("#search_users_board").toggle("nonActive");
        $("#search_users input").val("");


    });
    $("#search_users input").keyup(function () {

        if ($("#search_users input").val() != "") {

            $.ajax({
                url: "/Messages/SearchUsers",
                type: 'POST',
                data: { str: $("#search_users input").val() },
                success: function (result) {
                    $("#search_users_board").html(result);
                }
            });
        }
        else {
            $("#search_users_board").html('<div id="messages_no_user_found">Enter name in the search bar</div>');
        }

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
function NewMessage(Id) {

    var panes = $("#message_chatters_contacts")[0].children;
    console.log(panes)

    $.ajax({
        url: "/Messages/StartNewMessage",
        type: 'POST',
        data: {Id : Id},
        success: function (result) {
            $("#message_chatters_title div").trigger("click");
            if (IsChatPaneExists(Id)) {
                $(".chatter_pane[userId='" + Id + "']").trigger("click");
            }
            else {
                $(result).insertBefore(panes[0]);
                $(".chatter_pane[userId='" + Id + "']").trigger("click");
            }
        }
    })
}
function IsChatPaneExists(Id) {
    var panes = $("#message_chatters_contacts")[0].children;
    for (var i = 0; i < panes.length; i++) {
        if (panes.attr("userId") == Id) {
            return true;
        }
    }
    return false;
}