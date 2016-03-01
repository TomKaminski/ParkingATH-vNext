(function () {
    'use strict';

    function manageController(breadcrumbService, loadingContentService, apiFactory, notificationService, userProfileService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('account');
        self.model = {};
        getSettingsIndexData();

        self.activeTab = 1;

        self.uploadPhotoModel = {
            disableButton: false,
            oldValue: null
        }

        self.setActiveTab = function (tabNumber) {
            self.activeTab = tabNumber;
        }

        function genericForm(loadingName, model, apiEnumName, successFunction, formModel) {
            loadingContentService.setIsLoading(loadingName, true);
            model.disableButton = true;
            apiFactory.post(apiEnumName, model).then(function (data) {
                if (data.IsValid) {
                    formModel.$setUntouched();
                    formModel.$setPristine();
                    model = {
                        disableButton: false
                    }
                    successFunction(data);
                }
                loadingContentService.setIsLoading(loadingName, false);
                model.disableButton = false;
                notificationService.showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading(loadingName, false);
                model.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        self.sendCharges = function () {
            genericForm('sendCharges', self.sendChargesModel, apiFactory.apiEnum.SendCharges, function (data) {
                self.model.Charges = data.Result;
                userProfileService.setCharges(data.Result);
            }, self.sendChargesForm);
        }

        self.changeInfo = function () {
            genericForm('changeInfo', self.changeInfoModel, apiFactory.apiEnum.ChangeUserInfo,
                function (data) {
                    self.changeInfoModel = {
                        Name: data.Result.Name,
                        LastName: data.Result.LastName,
                        disableButton: false
                    }
                    self.model.Name = data.Result.Name;
                    self.model.LastName = data.Result.LastName;
                    userProfileService.setInitials(data.Result.Name, data.Result.LastName);
                }, self.changeInfoForm);
        }

        self.changeEmail = function () {
            genericForm('changeEmail', self.changeEmailModel, apiFactory.apiEnum.ChangeEmail, function (data) { self.model.Email = data.Result.Email; }, self.changeEmailForm);
        }

        self.changePassword = function () {
            genericForm('changePassword', self.changePasswordModel, apiFactory.apiEnum.ChangePassword, function () { }, self.changePasswordForm);
        }

        self.uploadFile = function () {
            self.uploadPhotoModel.oldValue = self.uploadPhotoModel.profileFile;
            loadingContentService.setIsLoading('uploadPhoto', true);
            var form = document.getElementById('upload-photo-form');
            self.uploadPhotoModel.disableButton = true;
            var formData = new FormData(form);
            apiFactory.post(apiFactory.apiEnum.UploadProfilePhoto, formData, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).then(function (data) {
                if (data.IsValid) {
                    userProfileService.setProfilePhotoPath(data.Result);
                }
                loadingContentService.setIsLoading('uploadPhoto', false);
                notificationService.showNotifications(data);
                self.uploadPhotoModel.disableButton = false;
            }, function (e) {
                self.uploadPhotoModel.oldValue = null;
                console.log(e);
                loadingContentService.setIsLoading('uploadPhoto', false);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
                self.uploadPhotoModel.disableButton = false;
            });
        }

        self.userInitials = function () {
            return userProfileService.getInitials();
        }

        self.getCharges = function () {
            return userProfileService.userData.charges;
        }

        self.userPhoto = function () {
            return userProfileService.userData.profilePhotoPath;
        }

        function getSettingsIndexData() {
            loadingContentService.setIsLoading('settingsMainLoading', true);
            apiFactory.get(apiFactory.apiEnum.GetSettingsIndexData, {}).then(function (data) {
                if (data.IsValid) {
                    self.model = data.Result;
                    userProfileService.setInitials(data.Result.Name, data.Result.LastName);
                    userProfileService.setCharges(data.Result.Charges);
                    self.changeInfoModel = {
                        Name: data.Result.Name,
                        LastName: data.Result.LastName,
                        disableButton: false
                    }
                } else {
                    notificationService.showNotifications(data);
                }
                loadingContentService.setIsLoading('settingsMainLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('settingsMainLoading', false);
            });
        }
    }

    angular.module('portalApp').controller('manageCtrl', ['breadcrumbService', 'loadingContentService', 'apiFactory', 'notificationService', 'userProfileService', manageController]);
})();
