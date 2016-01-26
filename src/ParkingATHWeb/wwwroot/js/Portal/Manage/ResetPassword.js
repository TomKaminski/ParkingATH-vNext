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

function clearFormErrors() {
    $('#reset-password-form').each(function () {
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
    $('#reset-password-form').each(function () {
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

function onError() {
    $('.preloader-wrapper').hide();
    $('.smart-move-btn, .smart-form-btn').removeClass('disabled');
    var $toastContentError = $('<span>Wystąpił błąd serwera - spróbuj później.</span>');
    Materialize.toast($toastContentError, 8000);
}

function checkIfNotDisabled(button) {
    return $(button).hasClass('disabled') ? false : true;
}

$(document).ready(function () {

    $('.smart-move-btn').click(function (e) {
        if (!checkIfNotDisabled(this))
            e.preventDefault();
    });

    $('.smart-form-btn').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('disabled')) {
            return;
        } else {
            var formId = $(this).data('form-id');
            $('#' + formId).submit();
        }
    });

    $('#reset-password-form').ajaxForm({
        beforeSubmit: beforeSubmit,
        success: afterSuccess,
        error: onError
    });
});