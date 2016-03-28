(function () {
    'use strict';

    function adminPriceCtrl($filter, breadcrumbService, apiFactory, loadingContentService, notificationService, adminFilterFactory) {

        var self = this;
        breadcrumbService.setOuterBreadcrumb('Administracja - Cennik');

        getList();

        self.prcList = {};
        self.shouldFilter = false;

        self.createPrcStart = function () {
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
                 if (data.SecondResult.Recovered === true) {
                     var item = $filter('getById')(adminFilterFactory.getFilterData(), self.deletePrcModel.Id);
                     item.IsDeleted = false;
                     self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);
                 } else if (data.SecondResult.ReplacedDefault === true) {
                     var totalData = adminFilterFactory.getFilterData();

                     for (var i = 0; i < totalData.length; i++) {
                         if (totalData[i].MinCharges === 0 && !totalData[i].IsDeleted) {
                             totalData[i].IsDeleted = true;
                         }
                     }

                     totalData.push(data.Result);
                     adminFilterFactory.setFilterData(totalData);
                     self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);

                 } else {
                     var dt = adminFilterFactory.getFilterData();
                     dt.push(data.Result);
                     adminFilterFactory.setFilterData(dt);
                     self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);
                 }

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
                 var dt = adminFilterFactory.getFilterData();
                 var item = $filter('getById')(dt, self.deletePrcModel.Id);
                 item.IsDeleted = true;
                 adminFilterFactory.setFilterData(dt);
                 self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);
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

        self.recoverPrcStart = function (id) {
            self.recoverPrcModel = $filter('getById')(self.prcList, id);
        }

        self.recoverPrc = function () {
            apiFactory.genericPost(
             function () {
                 self.recoverPrcProgress = true;
                 loadingContentService.setIsLoading('recoverPrc', true);
             },
             function () {
                 var totalData = adminFilterFactory.getFilterData();

                 var item = $filter('getById')(totalData, self.recoverPrcModel.Id);

                 if (item.MinCharges === 0) {
                     for (var i = 0; i < totalData.length; i++) {
                         if (totalData[i].MinCharges === 0 && !totalData[i].IsDeleted) {
                             totalData[i].IsDeleted = true;
                         }
                     }
                 }
                 item.IsDeleted = false;
                 adminFilterFactory.setFilterData(totalData);
                 self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);
                 $('#recoverPrcModal').closeModal();
             },
             function (data) {
                 loadingContentService.setIsLoading('recoverPrc', false);
                 notificationService.showNotifications(data);
                 self.recoverPrcProgress = false;

             },
             function () {
                 self.recoverPrcProgress = false;
                 loadingContentService.setIsLoading('recoverPrc', false);
             },
             apiFactory.apiEnum.RecoverAdminPrc, { Id: self.recoverPrcModel.Id });
        }

        self.toggleFilter = function () {
            self.prcList = adminFilterFactory.getFilteredItems(self.shouldFilter, "", adminFilterFactory.getFilterData().length, 1);
        }

        function getList() {
            apiFactory.genericGet(
               function () {
                   loadingContentService.setIsLoading('getPricesListLoader', true);
               },
               function (data) {
                   adminFilterFactory.setFilterBy({ IsDeleted: false });
                   adminFilterFactory.setFilterData(data.Result);
                   self.prcList = adminFilterFactory.getFilteredItems(false, "", data.Result.length, 1);
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

    angular.module('portalApp').controller('adminPriceCtrl', ['$filter', 'breadcrumbService', 'apiFactory', 'loadingContentService', 'notificationService', 'adminFilterFactory', adminPriceCtrl]);
})();


//self.prcEditStart = function (id) {
//    var prc = $filter('getById')(self.prcList, id);
//    self.prcEditModel = {
//        MinCharges: prc.MinCharges,
//        PricePerCharge: prc.PricePerCharge,
//        disableButton: false,
//        Id: prc.Id
//    }
//}
//self.editPrc = function () {
//    apiFactory.genericPost(
//     function () {
//         self.prcEditModel.disableButton = true;
//         loadingContentService.setIsLoading('editPrcAdmin', true);
//     },
//     function () {
//         var item = $filter('getById')(self.prcList, self.prcEditModel.Id);
//         item.MinCharges = self.prcEditModel.MinCharges;
//         item.PricePerCharge = self.prcEditModel.PricePerCharge;
//         $('#editPrcModal').closeModal();
//     },
//     function (data) {
//         loadingContentService.setIsLoading('editPrcAdmin', false);
//         notificationService.showNotifications(data);
//         self.prcEditModel.disableButton = false;
//     },
//     function () {
//         self.prcEditModel.disableButton = false;
//         loadingContentService.setIsLoading('editPrcAdmin', false);
//     },
//     apiFactory.apiEnum.EditAdminprc, self.prcEditModel);
//}