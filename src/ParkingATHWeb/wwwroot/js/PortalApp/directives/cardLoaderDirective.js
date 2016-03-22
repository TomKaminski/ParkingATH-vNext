angular.module('portalApp').directive('cardLoader', ['$timeout','$rootScope',function ($timeout, $rootScope) {

    return {
        restrict: 'E',
        replace: true,
        scope: {
            loadname: '@'

        },
        template: '<div class="card-loader-wrapper"><div class="preloader-wrapper active"><div class="spinner-layer"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div></div>',
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
}]);