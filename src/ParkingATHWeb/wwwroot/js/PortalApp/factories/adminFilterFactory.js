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

        function getFilteredItems(shouldFilter, searchText, pageSize, currentPage) {
            var filteredItems = !shouldFilter ? $filter('filter')(filterData, filterBy) : filterData;

            if (searchText !== "") {
                filteredItems = $filter('filter')(filteredItems, searchText);
            }

            totalPages = Math.ceil(filteredItems.length / pageSize);
            tableOfPages = createPageArray(totalPages, currentPage);

            if (currentPage > tableOfPages.length) {
                currentPage = 1;
            }

            return filteredItems.slice(currentPage * pageSize - pageSize, currentPage * pageSize);
        }

        function createPageArray(totalPages, currentPage) {
            var arrayOfPages = [];
            var start = currentPage - 3 < 1 ? 1 : currentPage - 3;
            var end = currentPage + 3 > totalPages ? totalPages : currentPage + 3;
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
            getTotalPages: getTotalPages
        }
    }

    angular.module('portalApp').factory('adminFilterFactory', adminFilterFactory);
})();