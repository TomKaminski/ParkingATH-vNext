(function () {
    'use strict';

    function homepageController(chartJsOptionsFactory, breadcrumbService, apiFactory, weatherInfoFactory, loadingContentService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('dashboard');

        self.model = getDashboardData();

        function getDashboardData() {
            loadingContentService.setIsLoading(true);
            apiFactory.get(apiFactory.apiEnum.GetDashboardData, {}).then(function (data) {
                self.model = data;
                self.model.weatherData.weatherInfo = weatherInfoFactory.getWeatherInfo(data.weatherData.WeatherInfo);
                self.model.lineChartData.data = [data.lineChartData.data];
                self.model.lineChartData.options = chartJsOptionsFactory.getDefaultLineOptions();
                loadingContentService.setIsLoading(false);

            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading(false);

            });
        }
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', 'apiFactory', 'weatherInfoFactory', 'loadingContentService', homepageController]);
})();
