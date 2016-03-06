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
        self.month = ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'];
        self.monthShort = ['Sty', 'Lu', 'Mar', 'Kwi', 'Maj', 'Cze', 'Li', 'Sie', 'Wrz', 'Paź', 'Li', 'Gr'];
        self.weekdaysFull = ['Niedziela', 'Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota'];
        self.weekdaysLetter = ['N', 'P', 'W', 'Ś', 'C', 'P', 'S'];
        self.disable = [false, 1, 7];
        self.today = 'Dzisiaj';
        self.clear = 'Wyczyść';
        self.close = 'Zamknij';
        var days = 9999;
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
