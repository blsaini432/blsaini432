var app = angular.module("myapp", ['angularUtils.directives.dirPagination']);
app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {

    $scope.fillList = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Viewallmember.aspx/BindCustomers',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.Employees = response.d;
        })
    };
    $scope.fillList();
    $("#loader").hide();

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

    //read data by id start

    $scope.Read = function (id) {
        //alert("test");
        ReadRecord(id);
    }
    function ReadRecord(id) {
        //alert("n");
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/BindCustomersByMsrno',
            method: "POST",
            data: { msrno: id }
        }).success(function (response) {
            $("#loader").hide();
            $scope.fulldetails = response.d;
        })
    };

    //read data by id end

    //services start

    //Active DMR Start
    $scope.ServiceDMR = function (id, actions) {

        ReadService(id, actions);
    }
    function ReadService(id, actions) {
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/updatedmr',
            method: "POST",
            data: { msrno: id, action: actions }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillList();

        })
    };
    //Active DMR End

    //Active DMR Start
    $scope.ServiceRecharge = function (id, actions) {

        ReadrechargeService(id, actions);
    }
    function ReadrechargeService(id, actions) {
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/updaterecharge',
            method: "POST",
            data: { msrno: id, action: actions }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillList();

        })
    };
    //Active DMR End

    ///Active AEPS Start

    $scope.ServiceAEPS = function (id, actions) {
        ReadServiceaeps(id, actions);
    }
    function ReadServiceaeps(id, actions) {
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/updateaeps',
            method: "POST",
            data: { msrno: id, action: actions }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillList();

        })
    };



    $scope.ServiceAEPSPayout = function (id, actions) {
        ReadServiceaepspayout(id, actions);
    }
    function ReadServiceaepspayout(id, actions) {
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/updateaepspayout',
            method: "POST",
            data: { msrno: id, action: actions }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillList();

        })
    };

    //Active AEPS End


    //services end

  








    //update bank

    $scope.ServiceBank = function (id) {

        ReadServiceBank(id);
    }
    function ReadServiceBank(id) {
        var ID = id;
        $http({
            url: '../Administrator/Viewallmember.aspx/updatebankdetails',
            method: "POST",
            data: { msrno: id, bankac: $scope.bankac, bankname: $scope.bankname, bankifsc: $scope.bankifsc }
        }).success(function (response) {
            showSwal('success-message');
        })
    };



    //update bank

});




