(function () {
    'use strict';

    function apiFactory($http, $q) {

        var portalPrefix = "Portal/";

        var apiEnum = {
            SaveSidebarState: "Account/SaveSidebarState",
            GetUserChargesData: "Home/GetUserChargesData",
            GetWeatherData: "Home/GetWeatherData",
            SendQuickMessage: "Message/SendQuickMessage",
            GetSettingsIndexData: "Account/GetSettingsIndexData",
            ChangePassword: "Account/ChangePassword",
            ChangeEmail: "Account/ChangeEmail",
            SendCharges: "Account/SendCharges",
            ChangeUserInfo: "Account/ChangeUserInfo",
            UploadProfilePhoto: "Account/UploadProfilePhoto",
            GetDefaultChartData: "Statistics/GetDefaultChartData",
            DeleteProfilePhoto: "Account/DeleteProfilePhoto",
            GetChartData: "Statistics/GetChartData",
            GetUserMessagesClusters: "Message/GetUserMessagesClusters",
            ReplyPortalMessage: "Message/ReplyPortalMessage",
            FakeDeleteCluster: "Message/FakeDeleteCluster",
            SetDisplayed: "Message/SetDisplayed",
            GetShopPrices: "Shop/GetPrices",
            GetUserOrders: "Shop/GetUserOrders",
            ProcessPayment: "Payment/ProcessPayment"
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

        function genericPost(funcBefore, funcAfterValid, funcAfter, funcError, apiEnum, postData, options) {
            funcBefore();
            post(apiEnum, postData, options).then(function (data) {
                if (data.IsValid) {
                    funcAfterValid(data);
                }
                funcAfter(data);
            }, function (e) {
                console.log(e);
                funcError();
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        function genericGet(funcBefore, funcAfterValid, funcAfter, funcError, apiEnum, params) {
            funcBefore();
            get(apiEnum, params).then(function (data) {
                if (data.IsValid) {
                    funcAfterValid(data);
                }
                funcAfter(data);
            }, function (e) {
                console.log(e);
                funcError();
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });
        }

        return {
            get: get,
            apiEnum: apiEnum,
            post: post,
            genericPost: genericPost,
            genericGet: genericGet
        };
    }

    angular.module('portalApp').factory('apiFactory', ['$http', '$q', apiFactory]);
})();