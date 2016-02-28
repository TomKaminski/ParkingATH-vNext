(function () {
    'use strict';

    function manageController(breadcrumbService, loadingContentService, apiFactory) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('account');
        self.model = {};
        getSettingsIndexData();

        self.sendChargesModel = {
            receiverEmail: "",
            amountOfCharges: null,
            password: "",
            disableButton: false
        }
        self.changePasswordModel = {
            oldPassword: "",
            newPassword: "",
            repeatNewPassword: "",
            disableButton: false
        }
        self.changeEmailModel = {
            newEmail: "",
            newEmailRepeat: "",
            confirmPassword: "",
            disableButton: false
        }
        self.changeInfoModel = {
            firstName: "Tomasz",
            lastName: "Kamiński",
            disableButton: false
        }

        self.activeTab = 1;

        self.setActiveTab = function(tabNumber) {
            self.activeTab = tabNumber;
        }

        function getSettingsIndexData() {
            loadingContentService.setIsLoading('settingsMainLoading', true);
            apiFactory.get(apiFactory.apiEnum.GetSettingsIndexData, {}).then(function (data) {
                if (data.IsValid) {
                    self.model = data.Result;
                } else {
                    showNotifications(data);
                }
                loadingContentService.setIsLoading('settingsMainLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('settingsMainLoading', false);
            });
        }

        function showNotifications(model) {
            for (var i = 0; i < model.SuccessNotifications.length; i++) {
                var $toastContent = $('<span>' + model.SuccessNotifications[i] + '</span>');
                Materialize.toast($toastContent, 8000, 'toast-green');
            }
            for (var j = 0; j < model.ValidationErrors.length; j++) {
                var $toastContentError = $('<span>' + model.ValidationErrors[j] + '</span>');
                Materialize.toast($toastContentError, 8000, 'toast-red');
            }
        }
    }

    angular.module('portalApp').controller('manageCtrl', ['breadcrumbService', 'loadingContentService', 'apiFactory', manageController]);
})();
