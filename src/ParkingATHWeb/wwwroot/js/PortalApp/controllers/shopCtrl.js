(function () {
    'use strict';

    function shopCtrl(breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('shop');
    }

    angular.module('portalApp').controller('shopCtrl', ['breadcrumbService', shopCtrl]);
})();