(function () {
    'use strict';

    function adminUserCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('Admin - Użytkownicy');
    }

    angular.module('portalApp').controller('adminUserCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminUserCtrl]);
})();
