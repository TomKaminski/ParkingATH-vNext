var smartAjax = function () {
    var currentForm;

    var setCurrentForm = function (id) {
        currentForm = id;
    }

    var showNotifications = function (model) {
        for (var i = 0; i < model.SuccessNotifications.length; i++) {
            var $toastContent = $('<span>' + model.SuccessNotifications[i] + '</span>');
            Materialize.toast($toastContent, 8000, 'toast-green');
        }
        for (var j = 0; j < model.ValidationErrors.length; j++) {
            var $toastContentError = $('<span>' + model.ValidationErrors[j] + '</span>');
            Materialize.toast($toastContentError, 8000);
        }
    }

    var clearFormErrors = function () {
        $('#' + currentForm).each(function () {
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

    var clearValues = function () {
        $('#' + currentForm).each(function () {
            $(this).find('input[type=text], input[type=password]').each(function () {
                $(this).val(null);
            });
            $(this).find('label').each(function () {
                $(this).removeClass('active');
            });
        });
    }

    function setupBeforeSubmit() {
        $('#' + currentForm + ' .preloader-wrapper').show();
        $('#' + currentForm + ' .smart-move-btn,' + '#' + currentForm + ' .smart-form-btn').addClass('disabled');
    }

    var setupAfterSubmit = function () {
        $('#' + currentForm + ' .preloader-wrapper').hide();
        $('#' + currentForm + ' .smart-move-btn,' + '#' + currentForm + ' .smart-form-btn').removeClass('disabled');
    }

    var beforeSubmit = function () {
        setupBeforeSubmit();
    }

    var afterSuccess = function (data) {
        if (data.IsValid) {
            clearValues();
        }
        setupAfterSubmit();
        showNotifications(data);
    }

    var onError = function () {
        setupAfterSubmit();
        var $toastContentError = $('<span>Wystąpił błąd serwera - spróbuj później.</span>');
        Materialize.toast($toastContentError, 8000);
    }

    var defaultOptions = {
        beforeSubmit: beforeSubmit,
        success: afterSuccess,
        error: onError
    }

    var checkIfNotDisabled = function (button) {
        return $(button).hasClass('disabled') ? false : true;
    }

    return {
        defaultOptions: defaultOptions,
        onError: onError,
        afterSuccess: afterSuccess,
        beforeSubmit: beforeSubmit,
        clearValues: clearValues,
        clearFormErrors: clearFormErrors,
        showNotifications: showNotifications,
        setCurrentForm: setCurrentForm,
        checkIfNotDisabled: checkIfNotDisabled,
        setupAfterSubmit: setupAfterSubmit
    }
}();

$(document).ready(function () {
    $('.preloader-wrapper').hide();

    $('.smart-form-btn').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('disabled')) {
            return;
        } else {
            var formId = $(this).data('form-id');
            smartAjax.setCurrentForm(formId);
            $('#' + formId).submit();
        }
    });

    $('.smart-move-btn').click(function (e) {
        if (!smartAjax.checkIfNotDisabled(this))
            e.preventDefault();
    });
});