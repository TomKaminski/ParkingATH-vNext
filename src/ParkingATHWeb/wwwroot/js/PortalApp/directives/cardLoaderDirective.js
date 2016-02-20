angular.module('portalApp').directive('cardLoader', function ($timeout, $rootScope) {

    return {
        restrict: 'E',
        replace: true,
        template: '<div class="card-loader-wrapper"><div class="preloader-wrapper active"><div class="spinner-layer spinner-white-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div></div>',
        link: function (scope, element) {
            scope.$watch(function () {
                return $rootScope.isContentLoading;
            }, function () {
                if ($rootScope.isContentLoading) {
                    $timeout(function() {
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