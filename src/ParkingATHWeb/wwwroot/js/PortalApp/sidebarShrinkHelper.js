$(document).ready(function() {
    initSidebarOnStart(isSidebarShrinked());

    $(".toggle-side-nav-shrinked").click(function() {
        toggleSidebarAnimation();
    });

    $(window).resize(function () {
        if ($(window).width() < 993) {
            $("#main").css('padding-left', '0');
        } else {
            setTimeout(function() {
                if (isSidebarShrinked()) {
                    $("#slide-out").css('width', shrinkedWidth);
                    $("#main").css('padding-left', shrinkedWidth);
                } else {
                    $("#slide-out").css('width', expandWidth);
                    $("#main").css('padding-left', expandWidth);
                }
            }, 50);

        }
    });
});

var shrinkedWidth = "120px";
var expandWidth = "240px";

function initSidebarOnStart(isShrinked) {
    if (isShrinked) {
        animateShink();
    } else {
        animateExpand();
    }
}

function isSidebarShrinked() {
    return $("#slide-out").hasClass('shrinked');
}

function toggleSidebarAnimation() {
    if (isSidebarShrinked()) {
        animateExpand();
        toggleShrinkedClass(false);
    } else {
        animateShink();
        toggleShrinkedClass(true);
    }
}

function toggleShrinkedClass(shouldShrink) {
    if (shouldShrink) {
        $("#slide-out").addClass('shrinked');
    } else {
        $("#slide-out").removeClass('shrinked');
    }
}

function animateShink() {
    $(".hide-on-shrinked").hide();
    $(".expand-on-shrinked").css('width', '100%');
    $("#slide-out").css('width', shrinkedWidth);
    if ($(window).width() >= 993) {
        $("#main").css('padding-left', shrinkedWidth);
    }
}

function animateExpand() {
    $(".hide-on-shrinked").show();
    $(".expand-on-shrinked").css('width', '');
    $("#slide-out").css('width', expandWidth);
    if ($(window).width() >= 993) {
        $("#main").css('padding-left', expandWidth);
    }
}

