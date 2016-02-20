angular.module('portalApp', ['ui.router', 'chart.js'])
 .config(['ChartJsProvider', function (ChartJsProvider) {
     // Configure all charts
     ChartJsProvider.setOptions({
         colours: ['#FFFFFF'],
         responsive: false
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

        $urlRouterProvider.otherwise("/");

        $stateProvider.state('dashboard', {
            url: "/",
            templateUrl: '/Portal/Dashboard',
            controller: 'homeCtrl',
            controllerAs: 'homeCtrl'
        })
        .state('account', {
                url: '/Konto',
                templateUrl: '/Portal/Konto',
                controller: 'manageCtrl',
                controllerAs: 'manageCtrl'
            })
        .state('shop', {
            url: '/Sklep',
            templateUrl: '/Portal/Sklep',
            controller: 'shopCtrl',
            controllerAs: 'shopCtrl'
        })
        .state('statistics', {
            url: '/Statystyki',
            templateUrl: '/Portal/Statystyki',
            controller: 'statisticsCtrl',
            controllerAs: 'statisticsCtrl'
        })
        .state('messages', {
            url: '/Wiadomosci',
            templateUrl: '/Portal/Wiadomosci',
            controller: 'messagesCtrl',
            controllerAs: 'messagesCtrl'
        });

        $locationProvider.html5Mode(false).hashPrefix('!');
    }]);