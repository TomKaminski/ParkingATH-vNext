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
                console.log("got weather");
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
                console.log("got user");

                self.userChargesModel = data;
                self.userChargesModel.lineChartData.data = [data.lineChartData.data];
                self.userChargesModel.lineChartData.options = chartJsOptionsFactory.getDefaultLineOptions();
                loadingContentService.setIsLoading('userChargesLoading', false);
            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('userChargesLoading', false);
            });
        }

        self.SendQuickMessage = function() {
         
        }
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', 'apiFactory', 'weatherInfoFactory', 'loadingContentService', homepageController]);
})();
