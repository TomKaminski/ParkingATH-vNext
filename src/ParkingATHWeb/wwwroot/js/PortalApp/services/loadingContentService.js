(function () {
    'use strict';

    function loadingContentService($rootScope) {
        this.setIsLoading = function(loading) {
            $rootScope.isContentLoading = loading;
        }

        this.isContentLoading = function() {
            return $rootScope.isContentLoading;
        }
    }

    angular.module('portalApp').service('loadingContentService', loadingContentService);
})();