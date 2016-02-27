(function () {
    'use strict';

    function apiFactory($http, $q) {

        var apiEnum = {
            SaveSidebarState: "Portal/Konto/SaveSidebarState",
            GetUserChargesData: "Portal/Home/GetUserChargesData",
            GetWeatherData: "Portal/Home/GetWeatherData",
            SendQuickMessage: "Portal/Wiadomosci/SendQuickMessage"
        }

        function get(apiUrl, options) {
            var defered = $q.defer();
            $http.get(apiUrl, { params: options })
                .success(function (data) {
                    defered.resolve(data);
                }).error(function () {
                    defered.reject(false);
                });
            return defered.promise;
        }

        function post(apiUrl, options) {
            var defered = $q.defer();
            $http.post(apiUrl, options)
                .success(function (data) {
                    defered.resolve(data);
                }).error(function (err) {
                    defered.reject(err);
                });
            return defered.promise;
        }

        return {
            get: get,
            apiEnum: apiEnum,
            post: post
        };
    }

    angular.module('portalApp').factory('apiFactory', ['$http', '$q', apiFactory]);
})();