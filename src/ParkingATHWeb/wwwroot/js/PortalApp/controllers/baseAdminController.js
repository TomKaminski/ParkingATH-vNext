(function () {
    'use strict';

    function baseAdminController($scope, adminFilterFactory) {
        this.baseInit = function (filterByProperty) {
            adminFilterFactory.setFilterBy(filterByProperty);
        }

        this.totalPages = function() {
            return adminFilterFactory.getTotalPages();
        }

        this.filterList = function () {
            this.filteredList = adminFilterFactory.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = adminFilterFactory.getPages();
        }

        this.onPageSizeChange = function () {
            if (isNaN(this.pageSize) || this.pageSize < 1) {
                this.pageSize = 10;
            }
            this.currentPage = 1;
            this.setPageSize(this.pageSize);
        }

        this.onTextChange = function () {
            this.currentPage = 1;
            this.filterList();
        }

        this.setPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > this.totalPages())
                page = this.totalPages();

            this.currentPage = page;

            this.filteredList = adminFilterFactory.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = adminFilterFactory.getPages();
        }

        this.setPageSize = function (size) {
            this.pageSize = parseInt(size);

            if (this.pageSize == undefined || this.pageSize === 0) {
                this.pageSize = 10;
            }

            this.filteredList = adminFilterFactory.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = adminFilterFactory.getPages();
        }

        this.initCtrl = function(data, custom) {
            this.shouldFilter = false;
            this.searchText = "";
            this.pageSize = 8;
            this.currentPage = 1;

            custom();

            adminFilterFactory.setFilterData(data);
            this.filteredList = adminFilterFactory.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = adminFilterFactory.getPages();
        }
    }

    angular.module('portalApp')
        .controller('baseAdminController', ['$scope', 'adminFilterFactory', baseAdminController]);
})();