/**
 * Created by mercatilorenzo on 12/10/15.
 */
var DreamAppCtrls;
DreamAppCtrls = angular.module('DAWievControllers', ['ngRoute', 'ui.bootstrap', 'DAResources']);
//<editor-fold desc="APP MAIN">
/** Menu Top controller **/
DreamAppCtrls.controller('menuCtrl', ['$scope', '$route', 'appResources', '$location', '$window', function ($scope, $route, appResources, $location, $window, $routeParams) {
    $scope.new = true;
    $scope.dealer = {};
    $scope.dealerData = {};
    appResources.checkDealer().then(function (resp) {
        if (!resp.data) {
            appResources.getDealerData().then(function (res) {
                $scope.dealerData = res.data;
            });
            $scope.new = false;
        }
    })
}]);
/** DashBoard controller **/
DreamAppCtrls.controller('mainCtrl', ['$scope', '$route', 'appResources', '$location', '$window', function ($scope, $route, appResources, $location, $window, $routeParams) {
    $scope.new = true;
    $scope.dealer = {};
    appResources.checkDealer().then(function (resp) {
        if (!resp.data) {
            $scope.new = false;
        }
    });
    $scope.creaDealer = function () {
        appResources.creaDealer($scope.dealer).then(function (resp) {
            if (resp.data) {
                $window.open("http://dreamcars.dreamdesign.it/Admin", "_self");
            }
        })
    }
}]);
//</editor-fold>

//<editor-fold desc="CARS">
/** Stock controller **/
DreamAppCtrls.controller('stockCtrl', ['$scope', '$http', '$modal', 'appResources', '$route', function ($scope, $http, $modal, appResources, $routeParams) {
    $scope.ids = $routeParams.current.params.type;
    $scope.tipol = function (id, tipo) {
        switch (id) {
            case '0':
                tipo = "Tutto";
                break;
            case '1':
                tipo = "Nuovo";
                break;
            case '2':
                tipo = "Usato";
                break;
            case '3':
                tipo = "Km0";
                break;
        }
        return tipo;
    };
    $scope.venduta = false;
    $scope.tipologia = $scope.tipol($scope.ids);
    appResources.getAuto($scope.ids, $scope.venduta).then(function (response) {
        $scope.data = response.data;
    });
    $scope.open = function (size) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: '/app/Template/Modals/CreateCarTmpl.html',
            controller: 'CreateCarModalCtrl',
            size: size
        });


    };

}]);
DreamAppCtrls.controller('carDetailsCtrl', ['$scope', '$filter', '$http', 'appResources', '$location', '$route', function ($scope, $filter, $http, appResources, $location, $routeParams) {
    $scope.idCar = $routeParams.current.params.id;
    appResources.getAutoDetails($scope.idCar).then(function (response) {
        $scope.dataCar = response.data;
        $scope.tot1 = $scope.dataCar.PrezzoDealer - $scope.dataCar.PrezzoAcquisto - $scope.dataCar.TotaleRipristini;
        $scope.tot2 = $scope.dataCar.PrezzoVendita - $scope.dataCar.PrezzoAcquisto - $scope.dataCar.TotaleRipristini;
        var myEl1 = angular.element(document.querySelector('#back1'));
        var myEl2 = angular.element(document.querySelector('#back2'));
        if ($scope.tot1 > 0) {
            myEl1.addClass('bg-green-800');
        } else {
            myEl1.addClass('bg-red-800');
        }
        if ($scope.tot2 > 0) {
            myEl2.addClass('bg-green-800');
        } else {
            myEl2.addClass('bg-red-800');
        }

        // GESTIONE CAMPI SELECT ANNO/MESE MOTORE CON angular-xeditable 'http://vitalets.github.io/angular-xeditable/'

        $scope.showStatusMese = function () {
            var selected = $filter('filter')($scope.mesiMotore, {value: $scope.dataCar.Mese});
            return ($scope.dataCar.Mese && selected.length) ? selected[0].text : 'Not set';
        };
        $scope.showStatusAnno = function () {
            var selected = $filter('filter')($scope.anniMotore, {value: $scope.dataCar.Anno});
            return ($scope.dataCar.Anno && selected.length) ? selected[0].text : 'Not set';
        };

    });
    $scope.salva = function () {
        appResources.updateAuto($scope.dataCar).then(function (resp) {

            $location.path('/carDetails/' + $scope.dataCar.Id);
        })
    };
    $scope.mesiMotore = [];
    for (var i = 1; i <= 12; i++) {
        $scope.mesiMotore.push({value: i, text: i});
    }
    $scope.anniMotore = [];
    for (var i = 1900; i <= 2015; i++) {
        $scope.anniMotore.push({value: i, text: i});
    }


}]);
DreamAppCtrls.controller('venduteCtrl', ['$scope', '$http', 'appResources', '$route', function ($scope, $http, appResources, $routeParams) {
    appResources.getAuto(0, true).then(function (response) {
        $scope.data = response.data;
    });
}]);
//</editor-fold>

//<editor-fold desc="CLIENTI">
/** Clienti controller **/
DreamAppCtrls.controller('clientiAllCtrl', ['$scope', '$http', '$modal', 'appResources', '$route', function ($scope, $http, $modal, appResources, $routeParams) {
    $scope.siAzienda = $routeParams.current.params.azienda;
    appResources.getClientiAll($scope.siAzienda).then(function (response) {
        $scope.data = response.data;
    });
    $scope.open = function (size) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: '/app/Template/Modals/CreateClienteTmlp.html',
            controller: 'CreateClienteModalCtrl',
            size: size
        });


    };
}]);
DreamAppCtrls.controller('clienteCtrl', ['$scope', '$http', 'appResources', '$route', function ($scope, $http, appResources, $routeParams) {
    $scope.idCliente = $routeParams.current.params.id;
    appResources.getCliente($scope.idCliente).then(function (response) {
        $scope.data = response.data;
    });

}]);
//</editor-fold>

//<editor-fold desc="CREATE MODALS">
/** Modal Create controllers **/
DreamAppCtrls.controller('CreateCarModalCtrl', ['$scope', 'appResources', '$location', '$modalInstance', function ($scope, appResources, $location, $modalInstance) {
    $scope.car = {};
    $scope.idadd = 0;
    $scope.submitForm = function () {
        appResources.creaAuto($scope.car.nome).then(function (resp) {
            $scope.idadd = resp.data;
            $location.path('/carDetails/' + $scope.idadd);
            $modalInstance.dismiss('cancel');
        })
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}]);
DreamAppCtrls.controller('CreateClienteModalCtrl', ['$scope', 'appResources', '$location', '$modalInstance', function ($scope, appResources, $location, $modalInstance) {
    $scope.cliente = {};
    $scope.idadd = 0;
    $scope.submitForm = function () {
        appResources.creaCliente($scope.cliente.nome, $scope.cliente.azienda).then(function (resp) {
            $scope.idadd = resp.data;
            $location.path('/cliente/' + $scope.idadd);
            $modalInstance.dismiss('cancel');
        })
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}]);
//</editor-fold>
