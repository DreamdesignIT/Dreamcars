/**
 * Created by mercatilorenzo on 22/09/15.
 */
angular.module("DAResources", [])
    .factory("appResources", function ($http, $q) {
        return {
            checkDealer: function () {
                return $http.get("/Umbraco/Api/AdminApi/checkDealer");
            },
            creaDealer: function(dealer){
                return   $http.post("/Umbraco/Api/AdminApi/creaDealer/", angular.toJson(dealer))
            },
            getAuto: function (tipo, venduta) {
                return $http.get("/Umbraco/Api/AdminApi/getAuto/?tipo=" + tipo + "&venduta=" + venduta);
            },
            getDealerData: function () {
                return  $http.get("/Umbraco/Api/AdminApi/getDealerData");
            },
            getAutoDetails: function (id) {
                return $http.get("/Umbraco/Api/AdminApi/getAutoDetails?id=" + id);
            },
            getClientiAll: function (azienda) {
                return $http.get("/Umbraco/Api/AdminApi/getClientiAll?azienda=" + azienda);
            },
            getCliente: function (id) {
                return $http.get("/Umbraco/Api/AdminApi/getCliente?id=" + id);
            },
            creaAuto: function(nome){
                return $http.get("/Umbraco/Api/AdminApi/creaAuto?name=" + nome);
            },
            updateAuto: function(dataCar){
                return $http.post("/Umbraco/Api/AdminApi/updateAuto/", angular.toJson(dataCar))
            },
            creaCliente: function(cliente, azienda){
                return $http.get("/Umbraco/Api/AdminApi/creaCliente?name=" + cliente + "&azienda=" + azienda)
            },
            updateCliente: function(cliente){
                return $http.post("/Umbraco/Api/AdminApi/updateCliente", angular.toJson(cliente))
            },
            creaRipristino: function(ripristino){
                return $http.get("/Umbraco/Api/AdminApi/creaRipristino?name=" + ripristino)
            },
            updateRipristino: function(ripristino){
                return $http.post("/Umbraco/Api/AdminApi/updateCliente", angular.toJson(ripristino))
            }

        };
    });

