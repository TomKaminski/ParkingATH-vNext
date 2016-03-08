(function () {
    'use strict';

    function statisticsCtrl(breadcrumbService, chartJsOptionsFactory, apiFactory, loadingContentService, notificationService) {
        var self = this;
        initDatePicker();
        breadcrumbService.setOuterBreadcrumb('statistics');

        loadingContentService.setIsLoading('statisticsDefaultDataLoader', true);
        getDefaultData();

        function getDefaultData() {
            loadingContentService.setIsLoading('statisticsDefaultDataLoader', true);
            apiFactory.post(apiFactory.apiEnum.GetDefaultChartData, {}).then(function (data) {
                initModelsWithPreferenceData(data.SecondResult);
                drawCharts(data.Result);
                loadingContentService.setIsLoading('statisticsDefaultDataLoader', false);
                notificationService.showNotifications(data);

            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('statisticsDefaultDataLoader', false);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');

            });
        }

        self.getGateUsagesChartData = function () {
            self.gateUsagesModel.disableButton = true;
            loadingContentService.setIsLoading('gateUsagesChartAjax', true);
            var model = { StartDate: self.gateUsagesModel.startDate, EndDate: self.gateUsagesModel.endDate, Granuality: self.gateUsagesModel.granuality, Type: 0 };
            apiFactory.post(apiFactory.apiEnum.GetChartData, model).then(function (data) {
                drawGateUsagesChart(data.Result);
                loadingContentService.setIsLoading('gateUsagesChartAjax', false);
                notificationService.showNotifications(data);
                self.gateUsagesModel.disableButton = false;

            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('gateUsagesChartAjax', false);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
                self.gateUsagesModel.disableButton = false;

            });
        }

        self.getOrdersChartData = function () {
            self.ordersModel.disableButton = true;

            loadingContentService.setIsLoading('ordersChartAjax', true);
            var model = { StartDate: self.ordersModel.startDate, EndDate: self.ordersModel.endDate, Granuality: self.ordersModel.granuality, Type: 1 };
            apiFactory.post(apiFactory.apiEnum.GetChartData, model).then(function (data) {
                drawOrdersChart(data.Result);
                loadingContentService.setIsLoading('ordersChartAjax', false);
                notificationService.showNotifications(data);
                self.ordersModel.disableButton = false;

            }, function (e) {
                console.log(e);
                loadingContentService.setIsLoading('ordersChartAjax', false);
                Materialize.toast("Wystąpił błąd podczas łączenia się z serwerem.", 8000, 'toast-red');
                self.ordersModel.disableButton = false;

            });
        }

        self.testData = {
            labels: [1, 2, 3, 4, 5, 6, 7],
            data: [[1, 2, 3, 4, 5, 6, 7]],
            options: chartJsOptionsFactory.getDefaultLineOptions()
        }

        function drawGateUsagesChart(data) {
            self.gateUsagesModel.data = [data.Data];
            self.gateUsagesModel.labels = data.Labels;
        }

        function drawOrdersChart(data) {
            self.ordersModel.data = [data.Data];
            self.ordersModel.labels = data.Labels;
        }

        function drawCharts(data) {
            self.gateUsagesModel.data = [data.gateUsagesData.Data];
            self.ordersModel.data = [data.ordersData.Data];

            self.gateUsagesModel.labels = data.gateUsagesData.Labels;
            self.ordersModel.labels = data.ordersData.Labels;

            self.gateUsagesModel.options = chartJsOptionsFactory.getDefaultLineOptions();
            self.ordersModel.options = chartJsOptionsFactory.getDefaultLineOptions();
        }

        function initModelsWithPreferenceData(userPreferenceModel) {
            self.gateUsagesModel = {
                endDate: userPreferenceModel.EndDate,
                startDate: userPreferenceModel.StartDate,
                granuality: userPreferenceModel.Granuality,
                labelStartDate: userPreferenceModel.LabelStartDate,
                labelEndDate: userPreferenceModel.LabelEndDate
            }
            self.ordersModel = {
                endDate: userPreferenceModel.EndDate,
                startDate: userPreferenceModel.StartDate,
                granuality: userPreferenceModel.Granuality,
                labelStartDate: userPreferenceModel.LabelStartDate,
                labelEndDate: userPreferenceModel.LabelEndDate
            }
        }

        function initDatePicker() {
            self.month = ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'];
            self.monthShort = ['Sty', 'Lu', 'Mar', 'Kwi', 'Maj', 'Cze', 'Li', 'Sie', 'Wrz', 'Paź', 'Li', 'Gr'];
            self.weekdaysFull = ['Niedziela', 'Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota'];
            self.weekdaysLetter = ['N', 'P', 'W', 'Ś', 'C', 'P', 'S'];
            self.disable = [false, 1, 7];
            self.today = 'Dzisiaj';
            self.clear = 'Wyczyść';
            self.close = 'Zamknij';
            self.onClose = function (iconId) {
                removeClass(document.getElementById(iconId), "active");
            };
        }

        function removeClass(el, className) {
            if (el.classList)
                el.classList.remove(className)
            else if (hasClass(el, className)) {
                var reg = new RegExp('(\\s|^)' + className + '(\\s|$)');
                el.className = el.className.replace(reg, ' ');
            }
        }
    }

    angular.module('portalApp').controller('statisticsCtrl', ['breadcrumbService', 'chartJsOptionsFactory', 'apiFactory', 'loadingContentService','notificationService', statisticsCtrl]);
})();
