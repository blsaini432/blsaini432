var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {

    //loadmemberdata
    $scope.filladmindashboard = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Dashboard.aspx/Bindmemberdata',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.admindashboard = response.d;
        })
        $timeout(function () {
            $scope.filladmindashboard();
        }, 5000)
    };
    $scope.filladmindashboard();
    $("#loader").hide();
    //loadmemberdata
});




