﻿var app = angular.module("Homeapp", ['angucomplete-alt']);

app.controller("HomeController", ['$scope', '$http', function ($scope,$http) {
    $scope.countries = [];
    $scope.SelectedCountries = null;

    $scope.AfterSelectedCoutries = function (selected) {
        if(selected){
            $scope.SelectedCountries = selected.originalObject;
        }
    }

    $http.get("../Administrator/AddFund.aspx/getmember").then(function (d) {
        $scope.countries = d.data;
    }, function (error) {
        alert('Failed');
    })

}])