(function () {
    'use strict';

    function adminPriceCtrl($filter,breadcrumbService, apiFactory, loadingContentService, notificationService) {

        var self = this;
        breadcrumbService.setOuterBreadcrumb('Administracja - Cennik');

        getList();

        self.prcList = {};

        self.createPrcStart = function() {
            self.prcCreateModel = {
                MinCharges: "",
                PricePerCharge: ""
            }
        }

        self.createPrc = function () {
            apiFactory.genericPost(
             function () {
                 self.prcCreateModel.disableButton = true;
                 loadingContentService.setIsLoading('createPrcAdmin', true);
             },
             function (data) {
                 self.prcList.push(data.Result);
                 $('#createPrcModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('createPrcAdmin', false);
                 notificationService.showNotifications(data);
                 self.prcCreateModel.disableButton = false;
             },
             function () {
                 self.prcCreateModel.disableButton = false;
                 loadingContentService.setIsLoading('createPrcAdmin', false);
             },
             apiFactory.apiEnum.CreateAdminPrc, self.prcCreateModel);
        }


        self.prcEditStart = function (id) {
            var prc = $filter('getById')(self.prcList, id);
            self.prcEditModel = {
                MinCharges: prc.MinCharges,
                PricePerCharge: prc.PricePerCharge,
                disableButton: false,
                Id: prc.Id
            }
        }

        self.editPrc = function () {
            apiFactory.genericPost(
             function () {
                 self.prcEditModel.disableButton = true;
                 loadingContentService.setIsLoading('editPrcAdmin', true);
             },
             function () {
                 var item = $filter('getById')(self.prcList, self.prcEditModel.Id);
                 item.MinCharges = self.prcEditModel.MinCharges;
                 item.PricePerCharge = self.prcEditModel.PricePerCharge;
                 $('#editPrcModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('editPrcAdmin', false);
                 notificationService.showNotifications(data);
                 self.prcEditModel.disableButton = false;
             },
             function () {
                 self.prcEditModel.disableButton = false;
                 loadingContentService.setIsLoading('editPrcAdmin', false);
             },
             apiFactory.apiEnum.EditAdminprc, self.prcEditModel);
        }

        self.deletePrcStart = function (id) {
            self.deletePrcModel = $filter('getById')(self.prcList, id);
        }

        self.deletePrc = function () {
            apiFactory.genericPost(
             function () {
                 self.deletePrcProgress = true;
                 loadingContentService.setIsLoading('deletePrc', true);
             },
             function () {

                 var item = $filter('getById')(self.prcList, self.deletePrcModel.Id);
                 self.prcList.splice(self.prcList.indexOf(item), 1);
                 $('#deletePrcModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('deletePrc', false);
                 notificationService.showNotifications(data);
                 self.deletePrcProgress = false;

             },
             function () {
                 self.deletePrcProgress = false;
                 loadingContentService.setIsLoading('deletePrc', false);
             },
             apiFactory.apiEnum.DeleteAdminPrc, { Id: self.deletePrcModel.Id });
        }

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getPricesListLoader', true);
               },
               function (data) {
                   self.prcList = data.Result;
               },
               function (data) {
                   console.log(data);
                   loadingContentService.setIsLoading('getPricesListLoader', false);
                   notificationService.showNotifications(data);
               },
               function () {
                   loadingContentService.setIsLoading('getPricesListLoader', false);
               },
               apiFactory.apiEnum.GetAdminPricesList);
        }
    }

    angular.module('portalApp').controller('adminPriceCtrl', ['$filter', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', adminPriceCtrl]);
})();
