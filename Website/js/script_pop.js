// For popup1
function loadPopup() {
    closeloading(); // fadeout loading
    $("#toPopup").fadeIn(0500); // fadein popup div
    $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
    $("#backgroundPopup").fadeIn(0001);
}

function disablePopup() {
    $("#toPopup").fadeOut("normal");
    $("#backgroundPopup").fadeOut("normal");
}
function loading() {
    $("div.loader").show();
}
function closeloading() {
    $("div.loader").fadeOut('normal');
}
// for popup2
function loadPopup1() {
    closeloading(); // fadeout loading
    $("#toPopup1").fadeIn(0500); // fadein popup div
    $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
    $("#backgroundPopup").fadeIn(0001);
}

function disablePopup1() {
    $("#toPopup1").fadeOut("normal");
    $("#backgroundPopup").fadeOut("normal");
}
function loading() {
    $("div.loader").show();
}
function closeloading() {
    $("div.loader").fadeOut('normal');
}
// for popup3
function loadPopup2() {
    closeloading(); // fadeout loading
    $("#toPopup2").fadeIn(0500); // fadein popup div
    $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
    $("#backgroundPopup").fadeIn(0001);
}

function disablePopup2() {
    $("#toPopup2").fadeOut("normal");
    $("#backgroundPopup").fadeOut("normal");
}
function loading() {
    $("div.loader").show();
}
function closeloading() {
    $("div.loader").fadeOut('normal');
}
// for popup4
function loadPopup3() {
    closeloading(); // fadeout loading
    $("#toPopup3").fadeIn(0500); // fadein popup div
    $("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
    $("#backgroundPopup").fadeIn(0001);
}

function disablePopup3() {
    $("#toPopup3").fadeOut("normal");
    $("#backgroundPopup").fadeOut("normal");
}
function loading() {
    $("div.loader").show();
}
function closeloading() {
    $("div.loader").fadeOut('normal');
}
$(document).ready(function () {
    $("a.topopup").click(function () {
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup(); // function show popup 
        }, 500); // .5 second
        return false;
    });

    /* event for close the popup */
    $("div.close").hover(
					function () {
					    $('span.ecs_tooltip').show();
					},
					function () {
					    $('span.ecs_tooltip').hide();
					}
				);

    $("div.close").click(function () {
        disablePopup();  // function close pop up
    });

    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup();  // function close pop up
        }
    });

    $("div#backgroundPopup").click(function () {
        disablePopup();  // function close pop up
    });

    //--------------------------Recover Password Popup2----------------------------------

    $("a.topopup1").click(function () {
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup1(); // function show popup 
        }, 500); // .5 second
        return false;
    });

    /* event for close the popup */
    $("div.close").hover(
					function () {
					    $('span.ecs_tooltip').show();
					},
					function () {
					    $('span.ecs_tooltip').hide();
					}
				);

    $("div.close").click(function () {
        disablePopup1();
    });

    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup1();
        }
    });

    $("div#backgroundPopup").click(function () {
        disablePopup1();
    });

    //--------------------------Recover Password Popup3----------------------------------

    $("a.topopup2").click(function () {
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup2(); // function show popup 
        }, 500); // .5 second
        return false;
    });

    /* event for close the popup */
    $("div.close").hover(
					function () {
					    $('span.ecs_tooltip').show();
					},
					function () {
					    $('span.ecs_tooltip').hide();
					}
				);

    $("div.close").click(function () {
        disablePopup2();
    });

    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup2();
        }
    });

    $("div#backgroundPopup").click(function () {
        disablePopup2();
    });

    //--------------------------Recover Password Popup4----------------------------------

    $("a.topopup3").click(function () {
        loading(); // loading
        setTimeout(function () { // then show popup, deley in .5 second
            loadPopup3(); // function show popup 
        }, 500); // .5 second
        return false;
    });

    /* event for close the popup */
    $("div.close").hover(
					function () {
					    $('span.ecs_tooltip').show();
					},
					function () {
					    $('span.ecs_tooltip').hide();
					}
				);

    $("div.close").click(function () {
        disablePopup3();
    });

    $(this).keyup(function (event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            disablePopup3();
        }
    });

    $("div#backgroundPopup").click(function () {
        disablePopup3();
    });
});


