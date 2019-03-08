$(document).ready(function(){

    let icons_itemlist = $(".nav_icon");
    let icons_svgs = $(".nav_icon svg");
    let icons_paras = $(".nav_icon p");
    let menu_opened = false;
    let AddAntiForgeryToken = function (data) {
        data.__RequestVerificationToken = $('#token').attr("val");
        return data;
    };

    for (let i = 0; i < icons_itemlist.length; i++) {
        $(icons_itemlist[i]).hover(function(){
            $(icons_svgs[i]).css('color', 'white');
            $(icons_paras[i]).css('color', 'white');
            $(icons_itemlist[i]).css('cursor', 'pointer');
        },
        function(){
            $(icons_svgs[i]).css('color', '#c7d1d8');
            $(icons_paras[i]).css('color', '#c7d1d8');
            $(icons_itemlist[i]).css('cursor', 'none');

        });
    }

    $("#nav_linkedLogo").on("click", function () {
        window.location.href = "/Home/Index";
    });

    $(icons_itemlist[0]).on("click", function () {
        window.location.href = "/Home/Index";
    });

    $(icons_itemlist[1]).on("click", function () {
        window.location.href = "/Mynetwork/Index";
    });

    $(icons_itemlist[2]).on("click", function () {
        window.location.href = "/Messages/Index";
    });

    $(icons_itemlist[3]).on("click", function () {
        window.location.href = "/Notifications/Index";
    });

    $(icons_itemlist[4]).on("click", function () {
        $(".nav_profile_menu").toggleClass("hide_menu");
        menu_opened = !menu_opened;
    });

    $("#nav_profile_view").on("click", function () {
        window.location.href = "/Profile/Index";
    });

    $("#lnk_signOut").on("click", function () {
        $.ajax({
            url: "/Account/LogOff",
            type: 'POST',
            //data: AddAntiForgeryToken({ id: parseInt($(this).attr("title")) })
            success: function (result) {
                window.location.href = "/home/Index";
            }
        });
    });

    


    $("#msg_header").on("click", function(){
        $("#msg_mainBody").toggleClass("collapsed");
        $("#msg_board").toggleClass("collapsed");
    });

    $("#nav_search input").focus(function(){
        $("#nav_search svg").attr("class", "fouced");
        $("#nav_search_icon_background").toggleClass("fouced");
        $("#nav_search input").toggleClass("fouced");
        $("#nav_search input").attr("placeholder", "");

        if ($("#nav_search input").val() != "") {
            $("#nav_search_result_underlay").removeClass("hidden");
        }
    });

    $("#nav_search input").focusout(function(){
        $("#nav_search svg").attr("class", "");
        $("#nav_search_icon_background").toggleClass("fouced");
        $("#nav_search input").toggleClass("fouced");
        $("#nav_search input").attr("placeholder", "Search");

        $("#nav_search_result_underlay").addClass("hidden");
    });

    $("#nav_search input").keyup(function () {

        $("#nav_search_result_underlay").removeClass("hidden");
        if ($("#nav_search input").val() != "") {

            $.ajax({
                url: "/Home/Search",
                type: 'POST',
                data: { str: $("#nav_search input").val() },
                success: function (result) {
                    $("#nav_search_result_underlay").replaceWith(result);
                    $("#nav_search_result_underlay").removeClass("hidden");
                }
            });

        }
        else {
            $("#nav_search_result_board").html("<div>No Results</div>");
        }

    });



});