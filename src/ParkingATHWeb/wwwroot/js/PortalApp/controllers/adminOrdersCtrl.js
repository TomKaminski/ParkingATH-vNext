(function () {
    'use strict';

    function adminOrdersCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Administracja - Zamówienia');

        getList();

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', true);
               },
               function (data) {
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getOrderListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', false);
               },
               apiFactory.apiEnum.GetAdminOrderList);
        }
    }

    angular.module('portalApp').controller('adminOrdersCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminOrdersCtrl]);
})();
