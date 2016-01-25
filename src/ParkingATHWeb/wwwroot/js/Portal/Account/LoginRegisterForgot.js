// 0 - LOGIN
// 1 - register
// 2 - forgot

$(document).ready(function () {
    $(".main-container > .row").height($('.register-content > .row').height() + 50);

    $(window).resize(function () {
        $(".main-container > .row").height($('.register-content > .row').height() + 50);
    });


    $('#moveRegister, #moveRegister2').click(function () {
        clearFormErrors();
        $('#switcher-box').attr('data-status', 1);
        $('.card-content.switcher-box').animate({
            'marginLeft': "0" //moves left
        });

        $('.inner-box').animate({
            'marginLeft': "100%" //moves right
        });
    });

    $('#moveLogin, #moveLogin2').click(function () {
        clearFormErrors();
        $('#switcher-box').attr('data-status', 0);
        if ($(window).width() <= 992) {
            $('.card-content.switcher-box').animate({
                'marginLeft': "0%" //moves right
            });
        } else {
            $('.card-content.switcher-box').animate({
                'marginLeft': "50%" //moves right
            });
        }

        $('.inner-box').animate({
            'marginLeft': "0" //moves right
        });
    });

    $('#moveForgot').click(function () {
        clearFormErrors();
        $('#switcher-box').attr('data-status', 2);
        if ($(window).width() <= 992) {
            $('.card-content.switcher-box').animate({
                'marginLeft': "0" //moves right
            });
        } else {
            $('.card-content.switcher-box').animate({
                'marginLeft': "50%" //moves right
            });
        }

        $('.inner-box').animate({
            'marginLeft': "-100%" //moves right
        });
    });

    setStatus();
    $(window).resize(function () {
        setStatus();
    });

    function setStatus() {
        var attrData = getCurrentStatus();
        var windowWidth = $(window).width();
        if (windowWidth > 992 && attrData === 0) {
            $('.card-content.switcher-box').css("margin-left", "50%");
        } else if (windowWidth <= 992 && attrData === 0) {
            $('.card-content.switcher-box').css("margin-left", "0%");
        }
    }

    function getCurrentStatus() {
        return parseInt($('#switcher-box').attr('data-status'));
    }

    $('#register-form').ajaxForm(function (data) {
        alert(data);
    });

    $('#login-form').ajaxForm(function (data) {
        console.log(data);
        if (data.IsValid === true) {
            window.location.replace(data.ReturnUrl);
        } else {
            alert(data.ValidationErrors);
        }
    });

    $('#forgot-form').ajaxForm(function (data) {
        console.log(data);
        alert(data);
    });

    function clearFormErrors() {
        $('#register-form, #login-form, #forgot-form').each(function () {
            $(this).find('input[type=text], input[type=password]').each(function () {
                $(this).removeClass('input-validation-error').removeClass('valid');
                $(this).val(null);
            });
            $(this).find('span.field-validation-error').each(function () {
                $(this).remove();
            });
            $(this).find('label').each(function () {
                $(this).removeClass('active');
            });
        });
    }
});