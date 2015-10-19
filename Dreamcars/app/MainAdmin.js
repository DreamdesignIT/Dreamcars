/**
 * Created by mercatilorenzo on 20/09/15. Prova git
 */

var DreamApp = angular.module('DreamApp', ['ngRoute', 'DAWievControllers', 'xeditable']);

    DreamApp.config(function($routeProvider) {
        $routeProvider
            .when('/stock/:type', {
                templateUrl: '/app/Template/stock.html',
                controller: 'stockCtrl'
            })
            .when('/carDetails/:id', {
                templateUrl: '/app/Template/carDetails.html',
                controller: 'carDetailsCtrl'
            })
            .when('/vendute', {
                templateUrl: '/app/Template/vendute.html',
                controller: 'venduteCtrl'
            })
            .when('/clientiAll/:azienda', {
                templateUrl: '/app/Template/clientiAll.html',
                controller: 'clientiAllCtrl'
            })
            .when('/cliente/:id', {
                templateUrl: '/app/Template/cliente.html',
                controller: 'clienteCtrl'
            })
            .when('/', {
                templateUrl: '/app/Template/dashboard.html',
                controller: 'mainCtrl'
            });

    });

DreamApp.run(function(editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});