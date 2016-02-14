(function () {
    'use strict';

    function messagesCtrl(breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('messages');
    }

    angular.module('portalApp').controller('messagesCtrl', ['breadcrumbService', messagesCtrl]);
})();