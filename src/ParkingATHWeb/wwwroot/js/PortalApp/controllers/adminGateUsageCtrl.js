(function () {
    'use strict';

    function adminGateUsageCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Administracja - Wyjazdy');

           getList();

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getGateUsagesListLoader', true);
               },
               function (data) {
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getGateUsagesListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getGateUsagesListLoader', false);
               },
               apiFactory.apiEnum.GetAdminGtList);
        }
    }

    angular.module('portalApp').controller('adminGateUsageCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminGateUsageCtrl]);
})();
