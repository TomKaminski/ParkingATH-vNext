(function () {
    'use strict';

    function shopCtrl(breadcrumbService, apiFactory, loadingContentService, notificationService, payuReturnService) {
        var self = this;
        self.charges = 1;
        self.prices = [];
        self.userOrders = [];

        if (payuReturnService.shouldRedirectToShop()) {
            if (payuReturnService.shouldShowShopError()) {
                var $toastContent = $('<span>Wystapił błąd podczas autoryzacji płatności, spróbuj ponownie.</span>');
                Materialize.toast($toastContent, 8000, 'toast-red');
            } else {
                var $toastContentSuccess = $('<span>Dziękujemy za złożenie zamówienia, wyjazdy będą dodane do Twojej puli w ciągu najbliższych chwil!</span>');
                Materialize.toast($toastContentSuccess, 8000, 'toast-green');
            }

            payuReturnService.isFromShop = false;
            payuReturnService.isErrorFromShop = false;
        }

        getPricesData();
        getUserOrdersData();

        breadcrumbService.setOuterBreadcrumb('shop');

        self.currentComputed = {
            forCharge: 0,
            total: 0
        }


        self.chargesInputOnChange = function () {
            var slider = $("#order-slider").data("ionRangeSlider");
            slider.update({
                from: parseInt(self.charges)
            });

            computePrices();
        }

        self.sliderOnChange = function () {
            computePrices();
        }

        function computePrices() {
            if (!computeFromUp()) {
                self.currentComputed = {
                    forCharge: self.defaultPrice.PricePerCharge,
                    total: self.defaultPrice.PricePerCharge * self.charges
                }
            }
        }

        function computeFromUp() {
            if (self.prices.length > 0) {
                for (var i = self.prices.length; i > 0; i--) {
                    var price = self.prices[i - 1];
                    if (price.MinCharges <= self.charges) {
                        self.currentComputed = {
                            forCharge: price.PricePerCharge,
                            total: price.PricePerCharge * self.charges
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        self.buyCharges = function () {
            apiFactory.genericPost(
              function () {
                  self.disableButton = true;
                  loadingContentService.setIsLoading('buyChargesLoader', true);
              },
              function (data) {
                  console.log(data);
                  window.location.replace(data.Result.RedirectUri);
              },
              function (data) {
                  console.log(data);
                  self.disableButton = false;
                  loadingContentService.setIsLoading('buyChargesLoader', false);
                  notificationService.showNotifications(data);
              },
              function () {
                  self.disableButton = false;
                  loadingContentService.setIsLoading('buyChargesLoader', false);
              },
              apiFactory.apiEnum.ProcessPayment, { Charges: self.charges });
        }

        function getPricesData() {
            apiFactory.genericPost(
               function () {
                   loadingContentService.setIsLoading('getPricesLoader', true);
               },
               function (data) {
                   initPricesData(data.Result);
                   self.currentComputed = {
                       forCharge: self.defaultPrice.PricePerCharge,
                       total: self.defaultPrice.PricePerCharge
                   }
               },
               function (data) {
                   loadingContentService.setIsLoading('getPricesLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getPricesLoader', false);
               },
               apiFactory.apiEnum.GetShopPrices);
        }

        function getUserOrdersData() {
            apiFactory.genericPost(
               function () {
                   loadingContentService.setIsLoading('getOrdersLoader', true);
               },
               function (data) {
                   self.userOrders = data.Result;
                   console.log(data);
               },
               function (data) {
                   loadingContentService.setIsLoading('getOrdersLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getOrdersLoader', false);
               },
               apiFactory.apiEnum.GetUserOrders);
        }


        function initPricesData(data) {
            self.defaultPrice = data[0];
            if (data.length > 1) {
                self.prices = data.slice(1);
            }
        }
    }

    angular.module('portalApp').controller('shopCtrl', ['breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', 'payuReturnService', shopCtrl]);
})();