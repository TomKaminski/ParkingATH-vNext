(function () {
    'use strict';

    function adminPriceCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Administracja - Cennik');

        getList();

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getPricesListLoader', true);
               },
               function (data) {
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getPricesListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getPricesListLoader', false);
               },
               apiFactory.apiEnum.GetAdminPricesList);
        }
    }

    angular.module('portalApp').controller('adminPriceCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminPriceCtrl]);
})();
