function setStatus() {
    var attrData = getCurrentForm();
    var windowWidth = $(window).width();
    if (windowWidth > 992 && (attrData === "login-form" || attrData === "forgot-form")) {
        $('.card-content.switcher-box').css("margin-left", "50%");
    } else if (windowWidth <= 992 && (attrData === "login-form" || attrData === "forgot-form")) {
        $('.card-content.switcher-box').css("margin-left", "0%");
    }
}

function getCurrentForm() {
    return $('#switcher-box').attr('data-current-form');
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


function manageTabIndexAttr() {
    var currentForm = getCurrentForm();
    var selector = resolverSelectorForTabIndex(currentForm);
    $(selector).find('button, a, input').each(function () {
        $(this).attr('tabIndex', '-1');
    });
    $('#'+currentForm).find('button, a, input').each(function () {
        $(this).removeAttr('tabindex');
    });
}

function resolverSelectorForTabIndex(currentForm) {
    switch (currentForm) {
        case "login-form":
            {
                return "#register-form, #forgot-form";
            }
        case "register-form":
            {
                return "#login-form, #forgot-form";
            }
        case "forgot-form":
            {
                return "#register-form, #login-form";
            }
    };
}

$(document).ready(function () {
    resizeContainer();
    $(window).resize(function () {
        resizeContainer();
    });
    manageTabIndexAttr();

    $('#moveRegister, #moveRegister2').click(function () {
        if (smartAjax.checkIfNotDisabled(this)) {
            smartAjax.clearFormErrors(getCurrentForm());
            setCurrentForm("register-form");
            smartAjax.setCurrentForm(getCurrentForm());
            
            $('.card-content.switcher-box').animate({
                'marginLeft': "0"
            });

            $('.inner-box').animate({
                'marginLeft': "100%"
            });
            manageTabIndexAttr();
        }
    });

    $('#moveLogin, #moveLogin2').click(function () {
        if (smartAjax.checkIfNotDisabled(this)) {
            smartAjax.clearFormErrors(getCurrentForm());
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
            manageTabIndexAttr();
        }
    });

    $('#moveForgot').click(function () {
        if (smartAjax.checkIfNotDisabled(this)) {
            smartAjax.clearFormErrors(getCurrentForm());
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
            manageTabIndexAttr();
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