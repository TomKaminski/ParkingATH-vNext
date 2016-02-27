(function () {
    'use strict';

    function sidebarStateService(apiFactory, $timeout) {
        var sidebarTimer;
        var sidebarInitialState;
        var currentSidebarState = null;

        this.setInitialState = function (state) {
            sidebarInitialState = state;
            currentSidebarState = state;
        }
        this.changeState = function () {
            currentSidebarState = !currentSidebarState;
            $timeout.cancel(sidebarTimer);
            sidebarTimer = $timeout(function () {
                if (currentSidebarState !== sidebarInitialState) {
                    postSidebarChangeMessage(currentSidebarState);
                }
            }, 10000);
        }

        function postSidebarChangeMessage(sidebarShrinked) {
            apiFactory.post(apiFactory.apiEnum.SaveSidebarState, { sidebarShrinked: sidebarShrinked }).then(function (data) {
                if (data.IsValid === false) {
                    console.log(data.ValidationErrors);
                }
            }, function (e) {
                console.log(e);
            });
        }
    }

    angular.module('portalApp').service('sidebarStateService', ['apiFactory', '$timeout', sidebarStateService]);
})();