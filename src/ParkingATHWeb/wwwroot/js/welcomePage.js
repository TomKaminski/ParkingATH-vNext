﻿
    $(window).load(function () {
        setTimeout(function () {
            $('#status').fadeOut();

            $('#preloader').delay(300).fadeOut('slow');

        }, 1500);
    });

    $(document).ready(function () {

        /* ---------------------------------------------- /*
		 * Smooth scroll / Scroll To Top
		/* ---------------------------------------------- */

        $('a[href*="#"]').bind("click", function (e) {

            var anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $(anchor.attr('href')).offset().top - 65
            }, 1000);
            e.preventDefault();
        });

        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.scroll-up').fadeIn();
            } else {
                $('.scroll-up').fadeOut();
            }
        });

        /* ---------------------------------------------- /*
		 * Navbar
		/* ---------------------------------------------- */

        $('.header').sticky({
            topSpacing: 0
        });

        $('body').scrollspy({
            target: '.navbar-custom',
            offset: 70
        });


        /* ---------------------------------------------- /*
		 * Home BG
		/* ---------------------------------------------- */

        $(".screen-height").height($(window).height());
        $(".screen-height-60").height(($(window).height()) * 0.6);

        $(window).resize(function () {
            $(".screen-height").height($(window).height());
            $(".screen-height-60").height(($(window).height()) * 0.6);
        });

        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
            $('#home').css({ 'background-attachment': 'scroll' });
        } else {
            $('#home').parallax('50%', 0.1);
        }


        /* ---------------------------------------------- /*
		 * WOW Animation When You Scroll
		/* ---------------------------------------------- */

        wow = new WOW({
            mobile: false
        });
        wow.init();
    });
