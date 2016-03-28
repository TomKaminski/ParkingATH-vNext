(function () {
    'use strict';

    function statisticsCtrl(breadcrumbService, chartJsOptionsFactory, apiFactory, loadingContentService, notificationService, adminFilterFactory) {
        var self = this;
        initDatePicker();
        breadcrumbService.setOuterBreadcrumb('statistics');

        self.totalGtList = {};
        self.totalOrderList = {};

        self.orderListFilterModel = {
            currentPage:1,
            pageSize: 10,
            searchText: ""
        }

        self.gtListFilterModel = {
            currentPage:1,
            pageSize: 10,
            searchText: ""
        }

        loadingContentService.setIsLoading('statisticsDefaultDataLoader', true);
        getDefaultData();
        getOrderList();
        getGtList();

        self.refreshOrderList = function () {
            getOrderList(self.orderDateRangeModel);
        }

        function getOrderList(requestModel) {
            apiFactory.genericPost(
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', true);
               },
               function (data) {
                   self.orderDateRangeModel = {
                       DateFrom: data.Result.DateFrom,
                       DateTo: data.Result.DateTo
                   }
                   self.totalOrderList = data.Result.ListItems;
                   self.orderListViewModel = adminFilterFactory.filterInstant(self.totalOrderList, self.orderListFilterModel);
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getOrderListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', false);
               },
               apiFactory.apiEnum.OrderDateRangeList, requestModel);
        }

        self.onOrderPageSizeChange = function () {
            if (isNaN(self.orderListFilterModel.pageSize) || self.orderListFilterModel.pageSize < 1) {
                self.orderListFilterModel.pageSize = 10;
            }
            self.orderListFilterModel.currentPage = 1;
            self.setOrderPageSize(self.orderListFilterModel.pageSize);
        }

        self.onOrderTextChange = function () {
            self.orderListFilterModel.currentPage = 1;
            self.orderListViewModel = adminFilterFactory.filterInstant(self.totalOrderList, self.orderListFilterModel);
        }

        self.setOrderPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > self.orderListViewModel.totalPages)
                page = self.orderListViewModel.totalPages;

            self.orderListFilterModel.currentPage = page;

            self.orderListViewModel = adminFilterFactory.filterInstant(self.totalOrderList, self.orderListFilterModel);
        }

        self.setOrderPageSize = function (size) {
            self.orderListFilterModel.pageSize = parseInt(size);

            if (self.orderListFilterModel.pageSize == undefined || self.orderListFilterModel.pageSize === 0) {
                self.orderListFilterModel.pageSize = 10;
            }

            self.orderListViewModel = adminFilterFactory.filterInstant(self.totalOrderList, self.orderListFilterModel);
        }


        self.refreshGtList = function () {
            getGtList(self.orderDateRangeModel);
        }

        function getGtList(requestModel) {
            apiFactory.genericPost(
               function () {
                   loadingContentService.setIsLoading('getGtListLoader', true);
               },
               function (data) {
                   self.gtDateRangeModel = {
                       DateFrom: data.Result.DateFrom,
                       DateTo: data.Result.DateTo
                   }
                   self.totalGtList = data.Result.ListItems;
                   self.gtListViewModel = adminFilterFactory.filterInstant(self.totalGtList, self.gtListFilterModel);
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getGtListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getGtListLoader', false);
               },
               apiFactory.apiEnum.GtDateRangeList, requestModel);
        }

        self.onGtPageSizeChange = function () {
            if (isNaN(self.gtListFilterModel.pageSize) || self.gtListFilterModel.pageSize < 1) {
                self.gtListFilterModel.pageSize = 10;
            }
            self.gtListFilterModel.currentPage = 1;
            self.setGtPageSize(self.gtListFilterModel.pageSize);
        }

        self.onGtTextChange = function () {
            self.gtListFilterModel.currentPage = 1;
            self.gtListViewModel = adminFilterFactory.filterInstant(self.totalGtList, self.gtListFilterModel);
        }

        self.setGtPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > self.gtListViewModel.totalPages)
                page = self.gtListViewModel.totalPages;

            self.gtListFilterModel.currentPage = page;

            self.gtListViewModel = adminFilterFactory.filterInstant(self.totalGtList, self.gtListFilterModel);
        }

        self.setGtPageSize = function (size) {
            self.gtListFilterModel.pageSize = parseInt(size);

            if (self.gtListFilterModel.pageSize == undefined || self.gtListFilterModel.pageSize === 0) {
                self.gtListFilterModel.pageSize = 10;
            }

            self.gtListViewModel = adminFilterFactory.filterInstant(self.totalGtList, self.gtListFilterModel);
        }

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
            if (el.classList) {
                el.classList.remove(className);
            }
            else if (hasClass(el, className)) {
                var reg = new RegExp('(\\s|^)' + className + '(\\s|$)');
                el.className = el.className.replace(reg, ' ');
            }
        }
    }

    angular.module('portalApp').controller('statisticsCtrl', ['breadcrumbService', 'chartJsOptionsFactory', 'apiFactory', 'loadingContentService', 'notificationService', 'adminFilterFactory', statisticsCtrl]);
})();
