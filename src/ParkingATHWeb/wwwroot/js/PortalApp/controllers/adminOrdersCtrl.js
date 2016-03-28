(function () {
    'use strict';

    function adminOrdersCtrl($controller, $scope, breadcrumbService, apiFactory, loadingContentService, notificationService) {
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
                   self.initCtrl(data.Result.ListItems, function () {
                       self.orderDateRangeModel = {
                           DateFrom: data.Result.DateFrom,
                           DateTo: data.Result.DateTo
                       }
                   });
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

    angular.module('portalApp').controller('adminOrdersCtrl', ['$controller', '$scope', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminOrdersCtrl]);
})();
