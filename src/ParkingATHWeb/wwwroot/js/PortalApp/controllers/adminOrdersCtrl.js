(function () {
    'use strict';

    function adminOrdersCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Admin - Zamówienia');
    }

    angular.module('portalApp').controller('adminOrdersCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminOrdersCtrl]);
})();
