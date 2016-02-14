(function () {
    'use strict';

    function manageController(breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('account');
    }

    angular.module('portalApp').controller('manageCtrl', ['breadcrumbService', manageController]);
})();
