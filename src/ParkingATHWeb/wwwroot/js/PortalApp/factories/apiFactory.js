(function () {
    'use strict';

    function apiFactory($http, $q) {

        var apiEnum = {
            SaveSidebarState: "Portal/Account/SaveSidebarState",
            GetUserChargesData: "Portal/Home/GetUserChargesData",
            GetWeatherData: "Portal/Home/GetWeatherData",
            SendQuickMessage: "Portal/Message/SendQuickMessage",
            GetSettingsIndexData: "Portal/Account/GetSettingsIndexData",
            ChangePassword: "Portal/Account/ChangePassword",
            ChangeEmail: "Portal/Account/ChangeEmail",
            SendCharges: "Portal/Account/SendCharges",
            ChangeUserInfo: "Portal/Account/ChangeUserInfo",
            UploadProfilePhoto: "Portal/Account/UploadProfilePhoto",
            GetDefaultChartData: "Portal/Statistics/GetDefaultChartData",
            DeleteProfilePhoto: "Portal/Account/DeleteProfilePhoto",
            GetChartData: "Portal/Statistics/GetChartData",
            GetUserMessagesClusters: "Portal/Message/GetUserMessagesClusters",
            ReplyPortalMessage: "Portal/Message/ReplyPortalMessage",
            FakeDeleteCluster: "Portal/Message/FakeDeleteCluster",
            SetDisplayed: "Portal/Message/SetDisplayed",
            GetShopPrices: "Portal/Shop/GetPrices",
            GetUserOrders: "Portal/Shop/GetUserOrders",
            ProcessPayment: "Portal/Payment/ProcessPayment",
            GetAdminUserList: "Admin/AdminUser/List",
            DeleteAdminUser: "Admin/AdminUser/Delete",
            RecoverAdminUser: "Admin/AdminUser/RecoverUser",
            GetAdminOrderList: "Admin/AdminOrder/List",
            GetAdminGtList: "Admin/AdminGateUsage/List",
            GetAdminPricesList: "Admin/AdminPriceTreshold/List"
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

        function post(apiUrl, data, options) {
            var defered = $q.defer();
            if (options != undefined) {
                $http.post(apiUrl, data, options)
                    .success(function(data) {
                        defered.resolve(data);
                    }).error(function(err) {
                        defered.reject(err);
                    });
            } else {
                $http.post(apiUrl, data)
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