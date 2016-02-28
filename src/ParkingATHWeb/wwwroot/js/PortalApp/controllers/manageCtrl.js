(function () {
    'use strict';

    function manageController(breadcrumbService, loadingContentService, apiFactory) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('account');
        self.model = {};
        getSettingsIndexData();

        self.sendChargesModel = {
            ReceiverEmail: "",
            AmountOfCharges: null,
            Password: "",
            disableButton: false
        }
        self.changePasswordModel = {
            OldPassword: "",
            Password: "",
            RepeatPassword: "",
            disableButton: false
        }
        self.changeEmailModel = {
            NewEmail: "",
            NnewEmailRepeat: "",
            Password: "",
            disableButton: false
        }
        self.changeInfoModel = {
            Name: "",
            LastName: "",
            disableButton: false
        }

        self.activeTab = 1;

        self.setActiveTab = function (tabNumber) {
            self.activeTab = tabNumber;
        }

        self.sendCharges = function () {
            loadingContentService.setIsLoading('sendCharges', true);
            self.sendChargesModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.SendCharges, self.sendChargesModel).then(function (data) {
                if (data.IsValid) {
                    self.sendChargesForm.$setUntouched();
                    self.sendChargesForm.$setPristine();
                    self.sendChargesModel = {
                        ReceiverEmail: "",
                        AmountOfCharges: null,
                        Password: "",
                        disableButton: false
                    }
                    self.model.Charges = data.Result;
                }
                loadingContentService.setIsLoading('sendCharges', false);
                self.sendChargesModel.disableButton = false;
                showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('sendCharges', false);
                self.sendChargesModel.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        self.changeInfo = function () {
            loadingContentService.setIsLoading('changeInfo', true);
            self.changeInfoModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.ChangeUserInfo, self.changeInfoModel).then(function (data) {
                if (data.IsValid) {
                    self.changeInfoForm.$setUntouched();
                    self.changeInfoForm.$setPristine();
                    self.changeInfoModel = {
                        Name: data.Result.Name,
                        LastName: data.Result.LastName,
                        disableButton: false
                    }
                    self.model.Name = data.Result.Name;
                    self.model.LastName = data.Result.LastName;
                }
                loadingContentService.setIsLoading('changeInfo', false);
                self.changeInfoModel.disableButton = false;
                showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('changeInfo', false);
                self.changeInfoModel.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        self.changeEmail = function () {
            loadingContentService.setIsLoading('changeEmail', true);
            self.changeEmailModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.ChangeEmail, self.changeEmailModel).then(function (data) {
                if (data.IsValid) {
                    self.changeEmailForm.$setUntouched();
                    self.changeEmailForm.$setPristine();
                    self.changeEmailModel = {
                        NewEmail: "",
                        NewEmailRepeat: "",
                        Password: "",
                        disableButton: false
                    }
                    self.model.Email = data.Result.Email;
                }
                loadingContentService.setIsLoading('changeEmail', false);
                self.changeEmailModel.disableButton = false;
                showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('changeEmail', false);
                self.changeEmailModel.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        self.changePassword = function () {
            loadingContentService.setIsLoading('changePassword', true);
            self.changePasswordModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.ChangePassword, self.changePasswordModel).then(function (data) {
                if (data.IsValid) {
                    self.changePasswordForm.$setUntouched();
                    self.changePasswordForm.$setPristine();
                    self.changePasswordModel = {
                        OldPassword: "",
                        Password: "",
                        RepeatPassword: "",
                        disableButton: false
                    }
                }
                loadingContentService.setIsLoading('changePassword', false);
                self.changePasswordModel.disableButton = false;
                showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('changePassword', false);
                self.changePasswordModel.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        function getSettingsIndexData() {
            loadingContentService.setIsLoading('settingsMainLoading', true);
            apiFactory.get(apiFactory.apiEnum.GetSettingsIndexData, {}).then(function (data) {
                if (data.IsValid) {
                    self.model = data.Result;
                    self.changeInfoModel = {
                        Name: data.Result.Name,
                        LastName: data.Result.LastName,
                        disableButton: false
                    }
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
