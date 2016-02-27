(function () {
    'use strict';

    function homepageController(chartJsOptionsFactory, breadcrumbService, apiFactory, weatherInfoFactory, loadingContentService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('dashboard');

        self.weatherModel = {}
        self.sendMessageModel = {};
        self.userChargesModel = {}

        getWeatherData();
        getUserChargesData();

        function getWeatherData() {
            loadingContentService.setIsLoading('weatherLoading',true);
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
                self.userChargesModel.lineChartData.data = [data.lineChartData.data];
                self.userChargesModel.lineChartData.options = chartJsOptionsFactory.getDefaultLineOptions();
                loadingContentService.setIsLoading('userChargesLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('userChargesLoading', false);
            });
        }

        self.SendQuickMessage = function () {
            if (!self.sendMessageModel.disableButton) {
                loadingContentService.setIsLoading('sendQuickMessage', true);
                self.sendMessageModel.disableButton = true;
                apiFactory.post(apiFactory.apiEnum.SendQuickMessage, { Text: self.sendMessageModel.text }).then(function (data) {
                    if (data.IsValid) {
                        self.sendMessageModel.text = "";
                    }
                    loadingContentService.setIsLoading('sendQuickMessage', false);
                    self.sendMessageModel.disableButton = false;
                    showNotifications(data);
                }, function (e) {
                    console.log(e);
                    loadingContentService.setIsLoading('sendQuickMessage', false);
                    self.sendMessageModel.disableButton = false;
                });
            }
        }

        function showNotifications(model) {
            for (var i = 0; i < model.SuccessNotifications.length; i++) {
                var $toastContent = $('<span>' + model.SuccessNotifications[i] + '</span>');
                Materialize.toast($toastContent, 8000, 'toast-green');
            }
            for (var j = 0; j < model.ValidationErrors.length; j++) {
                var $toastContentError = $('<span>' + model.ValidationErrors[j] + '</span>');
                Materialize.toast($toastContentError, 8000, 'toast-red');
            }
        }
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', 'apiFactory', 'weatherInfoFactory', 'loadingContentService', homepageController]);
})();
