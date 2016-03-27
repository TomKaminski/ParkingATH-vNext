(function () {
    'use strict';

    function adminOrdersCtrl($filter, $controller, $scope, breadcrumbService, apiFactory, loadingContentService, notificationService, adminFilterFactory) {
        angular.extend(this, $controller('baseAdminController', { $scope: $scope }));
        var self = this;

        self.baseInit({});
        self.filteredList = {};
        initDatePicker();
        breadcrumbService.setOuterBreadcrumb('Administracja - Zamówienia');

        getList();

        self.refreshList = function () {
            getList(self.orderDateRangeModel);
        }

        function getList(requestModel) {
            apiFactory.genericPost(
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', true);
               },
               function (data) {
                   init(data.Result);
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getOrderListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getOrderListLoader', false);
               },
               apiFactory.apiEnum.GetAdminOrderList, requestModel);
        }

        self.onPageSizeChange = function () {
            if (isNaN(self.pageSize) || self.pageSize < 1) {
                self.pageSize = 10;
            }
            self.currentPage = 1;
            self.setPageSize(self.pageSize);
        }

        self.onTextChange = function () {
            self.currentPage = 1;
            self.filterList();
        }

        function init(data) {
            self.shouldFilter = false;
            self.searchText = "";
            self.pageSize = 8;
            self.currentPage = 1;

            self.orderDateRangeModel = {
                DateFrom: data.DateFrom,
                DateTo: data.DateTo
            }
            
            adminFilterFactory.setFilterData(data.ListItems);
            self.filteredList = adminFilterFactory.getFilteredItems(self.shouldFilter, self.searchText, self.pageSize, self.currentPage);
            self.tableOfPages = adminFilterFactory.getPages();
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

    angular.module('portalApp').controller('adminOrdersCtrl', ['$filter', '$controller', '$scope', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', 'adminFilterFactory', adminOrdersCtrl]);
})();
