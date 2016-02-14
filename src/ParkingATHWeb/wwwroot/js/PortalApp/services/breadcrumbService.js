(function () {
    'use strict';

    function breadcrumbService() {

        var outerBreadcrumb = {};

        this.setOuterBreadcrumb = function(name) {
            var isInnerBreadcrumb = getInnerBreadcrumb();
            outerBreadcrumb = {
                current: !isInnerBreadcrumb,
                uiLink: name
            }
            switch (name) {
                case 'dashboard':
                    {
                        outerBreadcrumb.displayName = 'Strona główna';
                        break;
                    }
                case 'account':
                    {
                        outerBreadcrumb.displayName = 'Konto użytkownika';
                        break;
                    }
                case 'shop':
                    {
                        outerBreadcrumb.displayName = 'Sklep z wyjazdami';
                        break;
                    }
                case 'statistics':
                    {
                        outerBreadcrumb.displayName = 'Statystyki';
                        break;
                    }
                case 'messages':
                    {
                        outerBreadcrumb.displayName = 'Wiadomości';
                        break;
                    }
            }
        }

        this.getOuterBreadcrumb = function() {
            return outerBreadcrumb;
        }

        function getInnerBreadcrumb() {
            return false;
        }
    }

    angular.module('portalApp').service('breadcrumbService', breadcrumbService);
})();