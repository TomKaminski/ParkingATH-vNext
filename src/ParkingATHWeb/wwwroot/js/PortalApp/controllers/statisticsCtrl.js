(function () {
    'use strict';

    function statisticsCtrl(breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('statistics');
    }

    angular.module('portalApp').controller('statisticsCtrl', ['breadcrumbService', statisticsCtrl]);
})();
