(function () {
    'use strict';

    function adminFilterFactory($filter) {

        var filterData = {}
        var tableOfPages = [];
        var filterBy = {};
        var totalPages = null;

        function setFilterBy(fB) {
            filterBy = fB;
        }

        function setFilterData(data) {
            filterData = data;
        }

        function getFilterData() {
            return filterData;
        }

        function getPages() {
            return tableOfPages;
        }

        function getTotalPages() {
            return totalPages;
        }

        function filterInstant(data, filterModel) {
            var filteredItems = data;
            if (filterModel.searchText !== "") {
                filteredItems = $filter('filter')(filteredItems, filterModel.searchText);
            }

            var tempTotalPages = Math.ceil(filteredItems.length / filterModel.pageSize);
            var tempTableOfPages = createPageArray(tempTotalPages, filterModel.currentPage);

            if (filterModel.currentPage > tempTotalPages) {
                filterModel.currentPage = 1;
            }

            var items = filteredItems.slice(filterModel.currentPage * filterModel.pageSize - filterModel.pageSize, filterModel.currentPage * filterModel.pageSize);
            return {
                items: items,
                tableOfPages: tempTableOfPages,
                totalPages: tempTotalPages
            }
        }

        function getFilteredItems(shouldFilter, searchText, pageSize, currentPage) {
            var filteredItems = !shouldFilter ? $filter('filter')(filterData, filterBy) : filterData;

            if (searchText !== "") {
                filteredItems = $filter('filter')(filteredItems, searchText);
            }

            totalPages = Math.ceil(filteredItems.length / pageSize);
            tableOfPages = createPageArray(totalPages, currentPage);

            if (currentPage > totalPages) {
                currentPage = 1;
            }

            return filteredItems.slice(currentPage * pageSize - pageSize, currentPage * pageSize);
        }

        function createPageArray(totalPgs, currentPage) {
            var arrayOfPages = [];
            var start = currentPage - 3 < 1 ? 1 : currentPage - 3;
            var end = currentPage + 3 > totalPgs ? totalPgs : currentPage + 3;
            for (var i = start; i <= end; i++) {
                arrayOfPages.push(i);
            }
            return arrayOfPages;
        }


        return {
            setFilterBy: setFilterBy,
            getPages: getPages,
            getFilteredItems: getFilteredItems,
            setFilterData: setFilterData,
            getFilterData: getFilterData,
            getTotalPages: getTotalPages,
            filterInstant: filterInstant
        }
    }

    angular.module('portalApp').factory('adminFilterFactory', ['$filter', adminFilterFactory]);
})();