(function () {
    'use strict';

    function adminUserCtrl($filter, $controller, $scope, breadcrumbService, apiFactory, loadingContentService, notificationService, adminFilterFactory) {
        angular.extend(this, $controller('baseAdminController', { $scope: $scope }));

        var self = this;

        self.baseInit({});
        self.filteredList = {};

        breadcrumbService.setOuterBreadcrumb('Administracja - Użytkownicy');
        getList();

        self.userEditStart = function (id) {
            var user = $filter('getById')(adminFilterFactory.getFilterData(), id);
            self.userEditModel = {
                Name: user.Name,
                Id: user.Id,
                disableButton: false,
                LastName: user.LastName,
                Charges: user.Charges,
                Email: user.Email,
                OldEmail: user.Email
            }
        }

        self.editUser = function () {
            apiFactory.genericPost(
             function () {
                 loadingContentService.setIsLoading('editUserAdmin', true);
             },
             function () {
                 var item = $filter('getById')(adminFilterFactory.getFilterData(), self.userEditModel.Id);
                 item.Name = self.userEditModel.Name;
                 item.LastName = self.userEditModel.LastName;
                 item.Charges = self.userEditModel.Charges;
                 item.Email = self.userEditModel.Email;
                 item.Initials = item.Name + " " + item.LastName;
                 $('#editUserModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('editUserAdmin', false);
                 notificationService.showNotifications(data);
             },
             function () {
                 loadingContentService.setIsLoading('editUserAdmin', false);
             },
             apiFactory.apiEnum.EditAdminUser, self.userEditModel);
        }

        self.userDetails = function(id) {
            self.userDetailsModel = $filter('getById')(adminFilterFactory.getFilterData(), id);
        }

        self.deleteUserStart = function (id) {
            self.deleteUserModel = $filter('getById')(adminFilterFactory.getFilterData(), id);
        }

        self.deleteUser = function () {
            apiFactory.genericPost(
             function () {
                 loadingContentService.setIsLoading('deleteUser', true);
             },
             function () {
                 var item = $filter('getById')(adminFilterFactory.getFilterData(), self.deleteUserModel.Id);
                 item.IsDeleted = true;
                 $('#deleteModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('deleteUser', false);
                 notificationService.showNotifications(data);
             },
             function () {
                 loadingContentService.setIsLoading('deleteUser', false);
             },
             apiFactory.apiEnum.DeleteAdminUser, { Id: self.deleteUserModel.Id });
        }

        self.recoverUserStart = function (id) {
            self.recoverUserModel = $filter('getById')(adminFilterFactory.getFilterData(), id);
            loadingContentService.setIsLoading('recoverUser', false);
        }

        self.recoverUser = function () {
            apiFactory.genericPost(
             function () {
                 loadingContentService.setIsLoading('recoverUser', true);
             },
             function () {
                 var item = $filter('getById')(adminFilterFactory.getFilterData(), self.recoverUserModel.Id);
                 item.IsDeleted = false;
                 $('#recoveryModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('recoverUser', false);
                 notificationService.showNotifications(data);
             },
             function () {
                 loadingContentService.setIsLoading('recoverUser', false);
             },
             apiFactory.apiEnum.RecoverAdminUser, { Id: self.recoverUserModel.Id });
        }


        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getUserListLoader', true);
               },
               function (data) {
                   self.initCtrl(data.Result, function() {});
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getUserListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getUserListLoader', false);
               },
               apiFactory.apiEnum.GetAdminUserList);
        }
    }

    angular.module('portalApp').controller('adminUserCtrl', ['$filter', '$controller', '$scope', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', 'adminFilterFactory', adminUserCtrl]);
})();
