﻿(function () {
    'use strict';

    function adminUserCtrl($filter, $controller, $scope, breadcrumbService, apiFactory, loadingContentService, notificationService, adminFilterFactory) {
        angular.extend(this, $controller('baseAdminController', { $scope: $scope }));

        var self = this;

        self.baseInit({});
        self.filteredList = {};

        breadcrumbService.setOuterBreadcrumb('Administracja - Użytkownicy');
        getList();

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

        self.onPageSizeChange = function () {
            if (isNaN(self.pageSize) || self.pageSize < 1) {
                self.pageSize = 10;
            }
            self.currentPage = 1;
            self.setPageSize(self.pageSize);
        }

        self.onTextChange = function () {
            self.currentPage = 1;
            self.filterList();
        }

        function init(data) {
            self.shouldFilter = false;
            self.searchText = "";
            self.pageSize = 8;
            self.currentPage = 1;

            adminFilterFactory.setFilterData(data);
            self.filteredList = adminFilterFactory.getFilteredItems(self.shouldFilter, self.searchText, self.pageSize, self.currentPage);
            self.tableOfPages = adminFilterFactory.getPages();
        }

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getUserListLoader', true);
               },
               function (data) {
                   init(data.Result);
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