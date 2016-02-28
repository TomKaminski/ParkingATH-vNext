(function () {
    'use strict';

    function manageController(breadcrumbService) {
        var self = this;
        breadcrumbService.setOuterBreadcrumb('account');

        self.sendChargesModel = {
            receiverEmail: "",
            amountOfCharges: null,
            password: "",
            disableButton: false
        }

        self.changePasswordModel = {
            oldPassword: "",
            newPassword: "",
            repeatNewPassword: "",
            disableButton: false
        }

        self.changeEmailModel = {
            newEmail: "",
            newEmailRepeat: "",
            confirmPassword: "",
            disableButton: false
        }

        self.changeInfoModel = {
            firstName: "Tomasz",
            lastName: "Kamiński",
            disableButton: false
        }

        self.activeTab = 1;

        self.setActiveTab = function(tabNumber) {
            self.activeTab = tabNumber;
        }
    }

    angular.module('portalApp').controller('manageCtrl', ['breadcrumbService', manageController]);
})();
