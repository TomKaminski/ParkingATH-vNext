function setStatus() {
    var attrData = getCurrentForm();
    var windowWidth = $(window).width();
    if (windowWidth > 992 && attrData === "login-form") {
        $('.card-content.switcher-box').css("margin-left", "50%");
    } else if (windowWidth <= 992 && attrData === "login-form") {
        $('.card-content.switcher-box').css("margin-left", "0%");
    }
}

function getCurrentForm() {
    return $('#switcher-box').attr('data-status');
}

function setCurrentForm(formId) {
    $('#switcher-box').attr('data-current-form', formId);
}

function afterSuccessLogin(data) {
    if (data.IsValid) {
        smartAjax.clearValues();
    }
    smartAjax.setupAfterSubmit(data);
    if (data.IsValid === true) {
        window.location.replace(data.ReturnUrl);
    } else {
        smartAjax.showNotifications(data);
    }
}

function resizeContainer() {
    $(".main-container > .row").height($('.register-content > .row').height() + 80);
}

$(document).ready(function () {
    resizeContainer();
    $(window).resize(function () {
        resizeContainer();
    });

    $('#moveRegister, #moveRegister2').click(function () {
        smartAjax.clearFormErrors(getCurrentForm());
        if (smartAjax.checkIfNotDisabled(this)) {
            setCurrentForm("register-form");
            smartAjax.setCurrentForm(getCurrentForm());
            $('.card-content.switcher-box').animate({
                'marginLeft': "0"
            });

            $('.inner-box').animate({
                'marginLeft': "100%"
            });
        }
    });

    $('#moveLogin, #moveLogin2').click(function () {
        smartAjax.clearFormErrors(getCurrentForm());
        if (smartAjax.checkIfNotDisabled(this)) {
            setCurrentForm("login-form");
            smartAjax.setCurrentForm(getCurrentForm());
            if ($(window).width() <= 992) {
                $('.card-content.switcher-box').animate({
                    'marginLeft': "0%"
                });
            } else {
                $('.card-content.switcher-box').animate({
                    'marginLeft': "50%"
                });
            }
            $('.inner-box').animate({
                'marginLeft': "0"
            });
        }
    });

    $('#moveForgot').click(function () {
        smartAjax.clearFormErrors(getCurrentForm());
        if (smartAjax.checkIfNotDisabled(this)) {
            setCurrentForm("forgot-form");
            smartAjax.setCurrentForm(getCurrentForm());
            if ($(window).width() <= 992) {
                $('.card-content.switcher-box').animate({
                    'marginLeft': "0"
                });
            } else {
                $('.card-content.switcher-box').animate({
                    'marginLeft': "50%"
                });
            }
            $('.inner-box').animate({
                'marginLeft': "-100%"
            });
        }
    });

    setStatus();
    $(window).resize(function () {
        setStatus();
    });

    $('#register-form').ajaxForm(smartAjax.defaultOptions);
    $('#forgot-form').ajaxForm(smartAjax.defaultOptions);

    $('#login-form').ajaxForm({
        beforeSubmit: smartAjax.beforeSubmit,
        success: afterSuccessLogin,
        error: smartAjax.onError
    });
});