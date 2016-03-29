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

        self.deletePhotoModel = {
            disableButton: false
        }

        self.setActiveTab = function (tabNumber) {
            self.activeTab = tabNumber;
        }

        function genericForm(loadingName, model, apiEnumName, successFunction, formModel) {
            apiFactory.genericPost(
                function () {
                    loadingContentService.setIsLoading(loadingName, true);
                    model.disableButton = true;
                },
                function (data) {
                    formModel.$setUntouched();
                    formModel.$setPristine();

                    successFunction(data);
                },
                function (data) {
                    loadingContentService.setIsLoading(loadingName, false);
                    model.disableButton = false;
                    notificationService.showNotifications(data);
                },
                function () {
                    loadingContentService.setIsLoading(loadingName, false);
                    model.disableButton = false;
                },
                apiEnumName, model);
        }

        self.sendCharges = function () {
            genericForm('sendCharges', self.sendChargesModel, apiFactory.apiEnum.SendCharges, function (data) {
                self.model.Charges = data.Result;
                userProfileService.setCharges(data.Result);
                self.sendChargesModel = {
                    disableButton: false,
                    ReceiverEmail: "",
                    AmountOfCharges: "",
                    Password: ""
                }
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
            genericForm('changeEmail', self.changeEmailModel, apiFactory.apiEnum.ChangeEmail, function(data) {
                self.model.Email = data.Result.Email;
                self.changeEmailModel = {
                    disableButton: false,
                    NewEmail: "",
                    NewEmailRepeat: "",
                    Password: ""
                }
            }, self.changeEmailForm);
        }

        self.changePassword = function () {
            genericForm('changePassword', self.changePasswordModel, apiFactory.apiEnum.ChangePassword, function () { self.changePasswordModel = {
                disableButton: false,
                RepeatPassword: "",
                Password: "",
                OldPassword: ""
            }}, self.changePasswordForm);
        }

        self.uploadFile = function () {
            var form = document.getElementById('upload-photo-form');
            var formData = new FormData(form);
            apiFactory.genericPost(
              function () {
                  self.uploadPhotoModel.oldValue = self.uploadPhotoModel.profileFile;
                  loadingContentService.setIsLoading('uploadPhoto', true);
                  self.uploadPhotoModel.disableButton = true;
              },
              function (data) {
                  userProfileService.setProfilePhotoPath(data.Result);
              },
              function (data) {
                  loadingContentService.setIsLoading('uploadPhoto', false);
                  notificationService.showNotifications(data);
                  self.uploadPhotoModel.disableButton = false;
              },
              function () {
                  self.uploadPhotoModel.oldValue = null;
                  self.uploadPhotoModel.disableButton = false;
                  loadingContentService.setIsLoading('uploadPhoto', false);
              },
              apiFactory.apiEnum.UploadProfilePhoto, formData, {
                  withCredentials: true,
                  headers: { 'Content-Type': undefined },
                  transformRequest: angular.identity
              });
        }

        self.deleteProfilePhoto = function () {
            apiFactory.genericPost(
             function () {
                 self.deletePhotoModel.disableButton = true;
             },
             function (data) {
                 userProfileService.setProfilePhotoPath(data.Result);
             },
             function (data) {
                 notificationService.showNotifications(data);
                 self.deletePhotoModel.disableButton = false;
             },
             function () {
                 self.deletePhotoModel.disableButton = false;

             },
             apiFactory.apiEnum.DeleteProfilePhoto, {});
        }

        self.isDefaultPhotoSet = function () {
            return userProfileService.userData.profilePhotoPath.endsWith("avatar-placeholder.jpg");
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
            apiFactory.genericGet(
             function () {
                 loadingContentService.setIsLoading('settingsMainLoading', true);
             },
             function (data) {
                 self.model = data.Result;
                 userProfileService.setInitials(data.Result.Name, data.Result.LastName);
                 userProfileService.setCharges(data.Result.Charges);
                 self.changeInfoModel = {
                     Name: data.Result.Name,
                     LastName: data.Result.LastName,
                     disableButton: false
                 }
             },
             function (data) {
                 notificationService.showNotifications(data);
                 loadingContentService.setIsLoading('settingsMainLoading', false);

             },
             function () {
                 loadingContentService.setIsLoading('settingsMainLoading', false);
             },
             apiFactory.apiEnum.GetSettingsIndexData);
        }
    }

    angular.module('portalApp').controller('manageCtrl', ['breadcrumbService', 'loadingContentService', 'apiFactory', 'notificationService', 'userProfileService', manageController]);
})();
