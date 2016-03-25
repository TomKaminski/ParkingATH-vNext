(function () {
    'use strict';

    function adminUserCtrl($controller, $scope, breadcrumbService, apiFactory, loadingContentService, notificationService, adminFilterFactory) {
        angular.extend(this, $controller('baseAdminController', { $scope: $scope }));

        var self = this;

        self.baseInit({});
        self.filteredList = {};

        breadcrumbService.setOuterBreadcrumb('Administracja - Użytkownicy');
        getList();

        init();

        function init() {
            self.shouldFilter = false;
            self.searchText = "";
            self.pageSize = 8;
            self.currentPage = 1;

            adminFilterFactory.setFilterData(getTestUserData());
            self.filteredList = adminFilterFactory.getFilteredItems(self.shouldFilter, self.searchText, self.pageSize, self.currentPage);
            self.tableOfPages = adminFilterFactory.getPages();
        }

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getUserListLoader', true);
               },
               function (data) {
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


        function getTestUserData() {
            var testArray = [];
            for (var i = 0; i < 100; i++) {
                testArray.push({
                    Id: i + 1,
                    Initials: "Tomasz Kamiński" + i,
                    CreateDateLabel: "25.06.1993 15:05",
                    IsAdmin: i % 2 === 0 ? true : false,
                    IsDeleted: i % 5 === 0 ? true : false,
                    OrdersCount: 50 + i,
                    Charges: 100 + i,
                    GateUsagesCount: 100 + 1
                });
            }
            return testArray;
        }
    }

    angular.module('portalApp').controller('adminUserCtrl', ['$controller', '$scope', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService','adminFilterFactory', adminUserCtrl]);
})();
