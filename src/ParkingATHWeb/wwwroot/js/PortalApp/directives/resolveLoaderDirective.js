angular.module('portalApp').directive('resolveLoader', function ($rootScope, $timeout) {

    return {
        restrict: 'E',
        replace: true,
        template: '<div class="progress ng-hide"><div class="indeterminate"></div></div>',
        link: function (scope, element) {

            $rootScope.$on('$stateChangeStart', function (event, currentRoute, previousRoute) {
                $timeout(function () {
                    element.removeClass('ng-hide');
                });
            });

            $rootScope.$on('$stateChangeSuccess', function () {
                $timeout(function () {
                    element.addClass('ng-hide');
                });
            });

            $rootScope.$on('$stateChangeError', function () {
                alert('ROUTE ERROR!!!!');
                $timeout(function () {
                    element.addClass('ng-hide');
                });
            });
        }
    };
});