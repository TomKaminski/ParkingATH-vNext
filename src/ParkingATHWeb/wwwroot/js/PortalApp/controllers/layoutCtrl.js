(function () {
    'use strict';

    function layoutController(sidebarStateService, $timeout, breadcrumbService, userProfileService) {
        var self = this;

        //Date and time
        self.date = null;
        self.time = null;
        setDateAndTime();

        self.init = function (sidebarShrinked, name, lastName, photoId) {
            sidebarStateService.setInitialState((sidebarShrinked === "True"));
            userProfileService.userData = {
                name: name,
                lastName: lastName,
                profilePhotoPath: photoId === "" ? userProfileService.setProfilePhotoPath(null) : userProfileService.setProfilePhotoPath(photoId),
                charges: 0
            }
        }

        self.userPhotoNavbar = function () {
            return userProfileService.userData.profilePhotoPath;
        }

        self.userInitials = function () {
            return userProfileService.getInitials();
        }

        //TODO
        //self.innerBreadcrumb = function () {
        //    return breadcrumbService.getInnerBreadcrumb();
        //}

        self.outerBreadcrumb = function () {
            return breadcrumbService.getOuterBreadcrumb();
        }

        self.changeSidebarState = function () {
            sidebarStateService.changeState();
        }

        function setDateAndTime() {
            var date = new Date();
            var year = date.getFullYear();
            var month = date.getMonth();
            var months = new Array('Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień');
            var d = date.getDate();
            var day = date.getDay();
            var days = new Array('Niedziela', 'Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota');
            var h = date.getHours();
            if (h < 10) {
                h = "0" + h;
            }
            var m = date.getMinutes();
            if (m < 10) {
                m = "0" + m;
            }
            var s = date.getSeconds();
            if (s < 10) {
                s = "0" + s;
            }
            self.date = days[day] + ', ' + d + ' ' + months[month] + ' ' + year;
            self.time = h + ':' + m + ':' + s;
            $timeout(function () {
                setDateAndTime();
            }, 1000);
        }
    }

    angular.module('portalApp').controller('layoutCtrl', ['sidebarStateService', '$timeout', 'breadcrumbService', 'userProfileService', layoutController]);
})();
