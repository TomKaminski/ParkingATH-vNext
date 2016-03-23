(function () {
    'use strict';

    function adminGateUsageCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Admin - Wyjazdy');
    }

    angular.module('portalApp').controller('adminGateUsageCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminGateUsageCtrl]);
})();
