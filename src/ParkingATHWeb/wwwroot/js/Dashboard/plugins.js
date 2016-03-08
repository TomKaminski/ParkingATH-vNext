$(function () {

    "use strict";

    /*Preloader*/
    $(window).load(function () {
        setTimeout(function () {
            $('body').addClass('loaded');
        }, 200);
    });

    // Materialize Tabs
    $('.tab-demo').show().tabs();
    $('.tab-demo-active').show().tabs();

    // Materialize scrollSpy
    $('.scrollspy').scrollSpy();

    // Materialize tooltip
    $('.tooltipped').tooltip({
        delay: 50
    });

    // Materialize sideNav  

    //Main Left Sidebar Menu
    $('.sidebar-collapse').sideNav({
        edge: 'left', // Choose the horizontal origin    
    });

    // Perfect Scrollbar
    $('select').not('.disabled').material_select();
    var leftnav = $(".page-topbar").height();
    var leftnavHeight = window.innerHeight - leftnav;
    $('.leftside-navigation').height(leftnavHeight).perfectScrollbar({
        suppressScrollX: true
    });

    // Detect touch screen and enable scrollbar if necessary
    function is_touch_device() {
        try {
            document.createEvent("TouchEvent");
            return true;
        }
        catch (e) {
            return false;
        }
    }
    if (is_touch_device()) {
        $('#nav-mobile').css({
            overflow: 'auto'
        })
    }

}); // end of document ready