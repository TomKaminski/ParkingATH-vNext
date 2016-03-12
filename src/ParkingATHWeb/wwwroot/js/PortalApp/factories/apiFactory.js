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
            ChangeUserInfo: "Konto/ChangeUserInfo",
            UploadProfilePhoto: "Konto/UploadProfilePhoto",
            GetDefaultChartData: "Statystyki/GetDefaultChartData",
            DeleteProfilePhoto: "Konto/DeleteProfilePhoto",
            GetChartData: "Statystyki/GetChartData",
            GetUserMessagesClusters: "Wiadomosci/GetUserMessagesClusters"
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

        function post(apiUrl, data, options) {
            var defered = $q.defer();
            if (options != undefined) {
                $http.post(portalPrefix + apiUrl, data, options)
                    .success(function(data) {
                        defered.resolve(data);
                    }).error(function(err) {
                        defered.reject(err);
                    });
            } else {
                $http.post(portalPrefix + apiUrl, data)
               .success(function (data) {
                   defered.resolve(data);
               }).error(function (err) {
                   defered.reject(err);
               });
            }
           
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