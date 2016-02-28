(function () {
    'use strict';

    function apiFactory($http, $q) {

        var portalPrefix = "Portal/";

        var apiEnum = {
            SaveSidebarState: "Konto/SaveSidebarState",
            GetUserChargesData: "Home/GetUserChargesData",
            GetWeatherData: "Home/GetWeatherData",
            SendQuickMessage: "Wiadomosci/SendQuickMessage",
            GetSettingsIndexData: "Konto/GetSettingsIndexData",
            ChangePassword: "Konto/ChangePassword",
            ChangeEmail: "Konto/ChangeEmail",
            SendCharges: "Konto/SendCharges",
            ChangeUserInfo: "Konto/ChangeUserInfo"
        }

        function get(apiUrl, options) {
            var defered = $q.defer();
            $http.get(portalPrefix + apiUrl, { params: options })
                .success(function (data) {
                    defered.resolve(data);
                }).error(function () {
                    defered.reject(false);
                });
            return defered.promise;
        }

        function post(apiUrl, options) {
            var defered = $q.defer();
            $http.post(portalPrefix + apiUrl, options)
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