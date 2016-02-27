angular.module('portalApp').directive('requestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common[$http.defaults.xsrfHeaderName] = attrs.requestVerificationToken || "no request verification token";
    };
}]);