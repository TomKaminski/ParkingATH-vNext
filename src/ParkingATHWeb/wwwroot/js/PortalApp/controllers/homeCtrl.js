(function () {
    'use strict';

    function homepageController(chartJsOptionsFactory, breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('dashboard');
        self.clientsBarSparklineData = [70, 80, 65, 78, 58, 80, 78, 80, 70, 50, 75, 65, 80, 70, 65, 90, 65, 80, 70, 65, 90];

        self.lineChartData = {
            labels: ['14.02', '15.02', '16.02', '17.02', '18.02', '19.02', '20.02'],
            data: [[2, 3, 5, 6, 1, 0, 3]],
            options: chartJsOptionsFactory.getDefaultLineOptions()
        };
    }

    angular.module('portalApp').controller('homeCtrl', ['chartJsOptionsFactory', 'breadcrumbService', homepageController]);
})();
