(function () {
    'use strict';

    function redirectService() {

        var redirectTo = "";

        this.setRedirect = function(redTo) {
            redirectTo = redTo;
        }

        this.redirectTo = function () {
            return redirectTo;
        }
    }

    angular.module('portalApp').service('redirectService', redirectService);
})();