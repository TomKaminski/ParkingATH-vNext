angular.module('portalApp').directive('loader', function ($timeout, $rootScope) {

    return {
        restrict: 'E',
        replace: true,
        scope: {
            loadname: '@'
        },
        templateUrl: '../../../templates/loader.html',
        link: function ($scope, element) {
            $scope.$watch(function () {
                return $rootScope.loadingContainer[$scope.loadname];
            }, function () {
                if ($rootScope.loadingContainer[$scope.loadname]) {
                    $timeout(function () {
                        element.removeClass('ng-hide');
                    });
                } else {
                    $timeout(function () {
                        element.addClass('ng-hide');
                    });
                }
            });
        }
    };
});