(function () {
    'use strict';

    function statisticsCtrl(breadcrumbService, chartJsOptionsFactory) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('statistics');

        self.testData = {
            labels: [1,2,3,4,5,6,7],
            data: [[1,2,3,4,5,6,7]],
            options: chartJsOptionsFactory.getDefaultLineOptions()
        }

        var currentTime = new Date();
        self.currentTime = currentTime;
        self.month = ['Januar', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        self.monthShort = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        self.weekdaysFull = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        self.weekdaysLetter = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
        self.disable = [false, 1, 7];
        self.today = 'Today';
        self.clear = 'Clear';
        self.close = 'Close';
        var days = 15;
        self.minDate = (new Date(self.currentTime.getTime() - (1000 * 60 * 60 * 24 * days))).toISOString();
        self.maxDate = (new Date(self.currentTime.getTime() + (1000 * 60 * 60 * 24 * days))).toISOString();
        self.onStart = function () {
            console.log('onStart');
        };
        self.onRender = function () {
            console.log('onRender');
        };
        self.onOpen = function () {
            console.log('onOpen');
        };
        self.onClose = function () {
            console.log('onClose');
        };
        self.onSet = function () {
            console.log('onSet');
        };
        self.onStop = function () {
            console.log('onStop');
        };
    }

    angular.module('portalApp').controller('statisticsCtrl', ['breadcrumbService', 'chartJsOptionsFactory', statisticsCtrl]);
})();
