var app = angular.module("dmrApp", ['angularUtils.directives.dirPagination']);
app.controller("dmrCntrl", function ($scope, $http) {

    //Pagination start
    $scope.currentPage = 1;
    $scope.pageSize = 20;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
    //pagination end
    
    //DMTReport
    $scope.fillDMTreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Retailer/myDMRTransactions.aspx/fillDMTreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.DMTReport = response.d;
        })
    };
    $scope.fillDMTreport();
    $("#loader").hide();
    //

    $scope.fillDMTreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Retailer/myDMRTransactions.aspx/fillDMTreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.DMTReport = response.d;
        })
    };
    $("#loader").hide();

    // DMTReport
});
