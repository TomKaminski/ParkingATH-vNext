(function () {
    'use strict';

    function statisticsCtrl(breadcrumbService, chartJsOptionsFactory, apiFactory, loadingContentService, notificationService) {
        var self = this;
        initDatePicker();
        breadcrumbService.setOuterBreadcrumb('statistics');

        loadingContentService.setIsLoading('statisticsDefaultDataLoader', true);
        getDefaultData();

        function getDefaultData() {
            apiFactory.genericPost(
              function () {
                  loadingContentService.setIsLoading('statisticsDefaultDataLoader', true);
              },
              function () {
              },
              function (data) {
                  initModelsWithPreferenceData(data.SecondResult);
                  drawCharts(data.Result);
                  loadingContentService.setIsLoading('statisticsDefaultDataLoader', false);
                  notificationService.showNotifications(data);
              },
              function () {
                  loadingContentService.setIsLoading('statisticsDefaultDataLoader', false);

              },
              apiFactory.apiEnum.GetDefaultChartData, {});
        }

        self.getGateUsagesChartData = function () {
            apiFactory.genericPost(function () {
                self.gateUsagesModel.disableButton = true;
                loadingContentService.setIsLoading('gateUsagesChartAjax', true);
            }, function () {
            }, function (data) {
                drawGateUsagesChart(data.Result);
                loadingContentService.setIsLoading('gateUsagesChartAjax', false);
                notificationService.showNotifications(data);
                self.gateUsagesModel.disableButton = false;
            }, function () {
                loadingContentService.setIsLoading('gateUsagesChartAjax', false);
                self.gateUsagesModel.disableButton = false;

            },
            apiFactory.apiEnum.GetChartData, { StartDate: self.gateUsagesModel.startDate, EndDate: self.gateUsagesModel.endDate, Granuality: self.gateUsagesModel.granuality, Type: 0 });

        }

        self.getOrdersChartData = function () {
            apiFactory.genericPost(function () {
                self.ordersModel.disableButton = true;
                loadingContentService.setIsLoading('ordersChartAjax', true);
            }, function () {
            }, function (data) {
                drawOrdersChart(data.Result);
                loadingContentService.setIsLoading('ordersChartAjax', false);
                notificationService.showNotifications(data);
                self.ordersModel.disableButton = false;
            }, function () {
                loadingContentService.setIsLoading('ordersChartAjax', false);
                self.ordersModel.disableButton = false;
            },
            apiFactory.apiEnum.GetChartData, { StartDate: self.ordersModel.startDate, EndDate: self.ordersModel.endDate, Granuality: self.ordersModel.granuality, Type: 1 });
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

    angular.module('portalApp').controller('statisticsCtrl', ['breadcrumbService', 'chartJsOptionsFactory', 'apiFactory', 'loadingContentService', 'notificationService', statisticsCtrl]);
})();
