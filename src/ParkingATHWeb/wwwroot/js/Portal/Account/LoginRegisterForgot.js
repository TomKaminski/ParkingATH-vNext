// 0 - LOGIN
// 1 - register
// 2 - forgot

function showNotifications(model) {
    for (var i = 0; i < model.SuccessNotifications.length; i++) {
        var $toastContent = $('<span>' + model.SuccessNotifications[i] + '</span>');
        Materialize.toast($toastContent, 8000, 'toast-green');
    }
    for (var j = 0; j < model.ValidationErrors.length; j++) {
        var $toastContentError = $('<span>' + model.ValidationErrors[j] + '</span>');
        Materialize.toast($toastContentError, 8000);
    }
}

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

function clearValues() {
    $('#register-form, #login-form, #forgot-form').each(function () {
        $(this).find('input[type=text], input[type=password]').each(function () {
            $(this).val(null);
        });
        $(this).find('label').each(function () {
            $(this).removeClass('active');
        });
    });
}

function setupBeforeSubmit() {
    $('.preloader-wrapper').show();
    resizeContainer();
    $('.smart-move-btn, .smart-form-btn').addClass('disabled');
}

function setupAfterSubmit() {
    $('.preloader-wrapper').hide();
    $('.smart-move-btn, .smart-form-btn').removeClass('disabled');
}

function beforeSubmit() {
    setupBeforeSubmit();
}

function afterSuccess(data) {
    if (data.IsValid) {
        clearValues();
    }
    setupAfterSubmit(data);
    showNotifications(data);
}

function afterSuccessLogin(data) {
    if (data.IsValid) {
        clearValues();
    }
    setupAfterSubmit(data);
    if (data.IsValid === true) {
        window.location.replace(data.ReturnUrl);
    } else {
        showNotifications(data);
    }
}

function onError() {
    $('.preloader-wrapper').hide();
    $('.smart-move-btn, .smart-form-btn').removeClass('disabled');
    var $toastContentError = $('<span>Wystąpił błąd serwera - spróbuj później.</span>');
    Materialize.toast($toastContentError, 8000);
}

function checkIfNotDisabled(button) {
    return $(button).hasClass('disabled') ? false : true;
}

function resizeContainer() {
    $(".main-container > .row").height($('.register-content > .row').height() + 80);
}

$(document).ready(function () {
    $('.preloader-wrapper').hide();
    resizeContainer();
    $(window).resize(function () {
        resizeContainer();
    });

    $('#moveRegister, #moveRegister2').click(function () {
        clearFormErrors();
        if (checkIfNotDisabled(this)) {
            $('#switcher-box').attr('data-status', 1);
            $('.card-content.switcher-box').animate({
                'marginLeft': "0" 
            });

            $('.inner-box').animate({
                'marginLeft': "100%" 
            });
        }
    });

    $('.smart-form-btn').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('disabled')) {
            return;
        } else {
            var formId = $(this).data('form-id');
            $('#'+formId).submit();
        }
    });

    $('#moveLogin, #moveLogin2').click(function () {
        clearFormErrors();
        if (checkIfNotDisabled(this)) {
            $('#switcher-box').attr('data-status', 0);
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
        clearFormErrors();
        if (checkIfNotDisabled(this)) {
            $('#switcher-box').attr('data-status', 2);
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

    var options = {
        beforeSubmit: beforeSubmit,
        success: afterSuccess,
        error: onError
    }

    $('#register-form').ajaxForm(options);

    $('#login-form').ajaxForm({
        beforeSubmit: beforeSubmit,
        success: afterSuccessLogin,
        error: onError
    });

    $('#forgot-form').ajaxForm(options);


});