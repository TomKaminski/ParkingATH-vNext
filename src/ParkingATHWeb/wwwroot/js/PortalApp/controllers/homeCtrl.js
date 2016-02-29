(function () {
    'use strict';

    function homepageController(chartJsOptionsFactory, breadcrumbService, apiFactory, weatherInfoFactory, loadingContentService, notificationService, userProfileService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('dashboard');

        self.weatherModel = {}
        self.sendMessageModel = {};
        self.userChargesModel = {}

        getWeatherData();
        getUserChargesData();

        function getWeatherData() {
            loadingContentService.setIsLoading('weatherLoading', true);
            apiFactory.get(apiFactory.apiEnum.GetWeatherData, {}).then(function (data) {
                self.weatherModel = data;
                self.weatherModel.weatherInfo = weatherInfoFactory.getWeatherInfo(data.WeatherInfo);
                loadingContentService.setIsLoading('weatherLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('weatherLoading', false);
            });
        }

        function getUserChargesData() {
            loadingContentService.setIsLoading('userChargesLoading', true);
            apiFactory.get(apiFactory.apiEnum.GetUserChargesData, {}).then(function (data) {
                self.userChargesModel = data;
                userProfileService.userData.charges = self.userChargesModel.chargesLeft;
                self.userChargesModel.lineChartData.data = [data.lineChartData.data];
                self.userChargesModel.lineChartData.options = chartJsOptionsFactory.getDefaultLineOptions();
                loadingContentService.setIsLoading('userChargesLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('userChargesLoading', false);
            });
        }

        self.userCharges = function() {
            return userProfileService.userData.charges;
        }

        self.SendQuickMessage = function () {
            loadingContentService.setIsLoading('sendQuickMessage', true);
            self.sendMessageModel.disableButton = true;
            apiFactory.post(apiFactory.apiEnum.SendQuickMessage, { Text: self.sendMessageModel.text }).then(function (data) {
                if (data.IsValid) {
                    self.sendMessageModel.text = "";
                }
                loadingContentService.setIsLoading('sendQuickMessage', false);
                self.sendMessageModel.disableButton = false;
                notificationService.showNotifications(data);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('sendQuickMessage', false);
                self.sendMessageModel.disableButton = false;
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
            });

        }
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', 'apiFactory', 'weatherInfoFactory', 'loadingContentService', 'notificationService','userProfileService', homepageController]);
})();
