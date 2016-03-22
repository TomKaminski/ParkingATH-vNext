angular.module('portalApp', ['ui.router', 'chart.js', 'ngMessages','ui.materialize','perfect_scrollbar'])
 .config(['ChartJsProvider', function (ChartJsProvider) {
     // Configure all charts
     ChartJsProvider.setOptions({
         colours: ['#2e6cb2'],
         responsive: true
     });
     // Configure all line charts
     ChartJsProvider.setOptions('Line', {
         datasetFill: false
     });
 }])
.run(['$rootScope', '$state', '$stateParams',
	function ($rootScope, $state, $stateParams) {
	    $rootScope.$state = $state;
	    $rootScope.$stateParams = $stateParams;
	}
])

    .config(['$stateProvider', '$locationProvider', '$urlRouterProvider', function ($stateProvider, $locationProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/Portal");

        $stateProvider.state('dashboard', {
            url: "/Portal",
            templateUrl: '/Portal/Dashboard',
            controller: 'homeCtrl',
            controllerAs: 'homeCtrl'
        })
        .state('account', {
            url: '/Portal/Konto',
                templateUrl: '/Portal/Account',
                controller: 'manageCtrl',
                controllerAs: 'manageCtrl'
            })
        .state('shop', {
            url: '/Portal/Sklep',
            templateUrl: '/Portal/Shop',
            controller: 'shopCtrl',
            controllerAs: 'shopCtrl'
        })
        .state('statistics', {
            url: '/Portal/Statystyki',
            templateUrl: '/Portal/Statistics',
            controller: 'statisticsCtrl',
            controllerAs: 'statCtrl'
        })
        .state('messages', {
            url: '/Portal/Wiadomosci',
            templateUrl: '/Portal/Message',
            controller: 'messagesCtrl',
            controllerAs: 'msgCtrl'
        });

        $locationProvider.html5Mode(true);
    }]);

Date.prototype.addDays = function(days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};