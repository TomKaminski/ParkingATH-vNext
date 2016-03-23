(function () {
    'use strict';

    function adminPriceCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Admin - Cennik');
    }

    angular.module('portalApp').controller('adminPriceCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminPriceCtrl]);
})();
