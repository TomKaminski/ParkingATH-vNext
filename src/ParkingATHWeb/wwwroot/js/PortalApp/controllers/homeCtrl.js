(function () {
    'use strict';

    function homepageController(chartJsOptionsFactory, breadcrumbService, apiFactory, weatherInfoFactory, loadingContentService, notificationService, userProfileService, payuReturnService, $state, $location, redirectService) {
        var self = this;

        breadcrumbService.setOuterBreadcrumb('dashboard');

        self.weatherModel = {}
        self.sendMessageModel = {};
        self.userChargesModel = {};

        getWeatherData();
        getUserChargesData();

        function getWeatherData() {
            apiFactory.genericGet(
                function () {
                    loadingContentService.setIsLoading('weatherLoading', true);
                },
                function () {
                },
                function (data) {
                    self.weatherModel = data;
                    self.weatherModel.weatherInfo = weatherInfoFactory.getWeatherInfo(data.WeatherInfo);
                    loadingContentService.setIsLoading('weatherLoading', false);
                },
                function () {
                    loadingContentService.setIsLoading('weatherLoading', false);
                },
                apiFactory.apiEnum.GetWeatherData);
        }

        function getUserChargesData() {
            apiFactory.genericGet(
              function () {
                  loadingContentService.setIsLoading('userChargesLoading', true);
              },
              function () {
              },
              function (data) {
                  self.userChargesModel = data;
                  userProfileService.userData.charges = self.userChargesModel.chargesLeft;
                  self.userChargesModel.lineChartData.data = [data.lineChartData.Data];
                  self.userChargesModel.lineChartData.options = chartJsOptionsFactory.getDefaultLineOptions();
                  loadingContentService.setIsLoading('userChargesLoading', false);
              },
              function () {
                  loadingContentService.setIsLoading('userChargesLoading', false);
              },
              apiFactory.apiEnum.GetUserChargesData);
        }

        self.userCharges = function () {
            return userProfileService.userData.charges;
        }

        self.SendQuickMessage = function () {
            apiFactory.genericPost(
              function () {
                  loadingContentService.setIsLoading('sendQuickMessage', true);
              },
              function () {
                  self.sendMessageModel.text = "";
              },
              function (data) {
                  loadingContentService.setIsLoading('sendQuickMessage', false);
                  self.sendMessageModel.disableButton = false;
                  notificationService.showNotifications(data);
              },
              function () {
                  loadingContentService.setIsLoading('sendQuickMessage', false);
                  self.sendMessageModel.disableButton = false;
              },
              apiFactory.apiEnum.SendQuickMessage, { Text: self.sendMessageModel.text });
        }
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', 'apiFactory', 'weatherInfoFactory', 'loadingContentService', 'notificationService', 'userProfileService', 'payuReturnService', '$state', '$location', 'redirectService', homepageController]);
})();
