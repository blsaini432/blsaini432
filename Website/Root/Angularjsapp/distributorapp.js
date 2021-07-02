var app = angular.module("distributorApp", ['angularUtils.directives.dirPagination']);
app.controller("distributorCntrl", function ($scope, $http, $timeout) {

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

    //mydashboard
   
    $scope.fillrtdashboard = function () {
        alter("wefddwe")
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Dashboard.aspx/Bindmemberdata',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.rtdashboard = response.d;
        })

        $timeout(function () {
            $scope.fillrtdashboard();
        }, 10000)
    };
    $scope.fillrtdashboard();
    $("#loader").hide();
    //mydashboard

    //AEPSWallet Balance ledger
    $scope.fillaepsWalletBalance = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListaepsWalletBalance.aspx/fillaepsWalletBalance',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.AepsBalance = response.d;
        })
    };
    $scope.fillaepsWalletBalance();
    $("#loader").hide();


    $scope.fillAEPSwalletbalancesummary = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListRwalletTransaction.aspx/fillRWalletTransaction',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.AepsBalanceSummary = response.d;
        })
    };
    $scope.fillAEPSwalletbalancesummary();
    $("#loader").hide();


    //

    $scope.fillAEPSwalletbalancesummarybydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListRwalletTransaction.aspx/fillRWalletTransactionbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.AepsBalanceSummary = response.d;
        })
    };
    $("#loader").hide();



    //AEPSWallet













    //electrictyReport

    $scope.fillelectricityreport = function ()
    {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Electricitytranscation.aspx/fillelectricityreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.ElectrictyReport = response.d;
        })
    };
    $scope.fillelectricityreport();
    $("#loader").hide();
    //

    $scope.fillelectricityreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Electricitytranscation.aspx/fillelectricityreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.ElectrictyReport = response.d;
        })
    };
    $("#loader").hide();
    //ElectricityReportEnd

    //Show DMRReceipt

    //read data by id start

    $scope.Readdmrreceipt = function (id) {
        //alert("test");
        dmrreceipt(id);
    }
    function dmrreceipt(id) {
        //alert("n");
        var ID = id;
        $http({
            url: '../Distributor/DmrNewReport.aspx/loaddmrreceipt',
            method: "POST",
            data: { txnid: id }
        }).success(function (response) {
            $("#loader").hide();
            $scope.dmrreceipt = response.d;
        })
    };
    $scope.Readinstantdmrreceipt = function (id) {
        //alert("test");
        instantdmrreceipt(id);
    }
    function instantdmrreceipt(id) {
        //alert("n");
        var ID = id;
        $http({
            url: '../Distributor/DmrReport.aspx/loaddmrreceipt',
            method: "POST",
            data: { txnid: id }
        }).success(function (response) {
            $("#loader").hide();
            $scope.instantdmrreceipt = response.d;
        })
    };
    //read data by id end
    //Show DMRReceipt


    //Show RechargeReceipt

    //read data by id start

    $scope.Readrechargereceipt = function (id) {
        //alert("test");
        rechargereceipt(id);
    }
    function rechargereceipt(id) {
        //alert("n");
        var ID = id;
        $http({
            url: '../Distributor/Recharge_ListHistory.aspx/loadrechargereceipt',
            method: "POST",
            data: { txnid: id }
        }).success(function (response) {
            $("#loader").hide();
            $scope.rechargereceipt = response.d;
        })
    };
    //read data by id end
    //Show RechargeReceipt


    //FundRequest
    $scope.fillfundrequest = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListFundRequest.aspx/fillfundrequest',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.listfundrequest = response.d;
        })
    };
    $scope.fillfundrequest();
    $("#loader").hide();
    //

    $scope.fillfundrequestbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListFundRequest.aspx/fillfundrequestbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.listfundrequest = response.d;
        })
    };
    $("#loader").hide();




    $scope.ShowFundImage = function (id) {

        FundImage(id);
    }
    function FundImage(id) {
        var ID = id;
        $http({
            url: '../Distributor/ListFundRequest.aspx/ShowFundImage',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fundid: id }
        }).success(function (response) {
            $scope.fundimage = response.d;

        })
    };
    //FundRequest
    //Show Receipt

    //AEPSTransactionReport

    $scope.fillaepstransaction = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/AEPSTransactions.aspx/fillaepstransactions',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.aepstransactionsreport = response.d;
        })
    };
    $scope.fillaepstransaction();
    $("#loader").hide();
    //

    $scope.fillaepstransactionbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/AEPSTransactions.aspx/fillaepstransactionsbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.aepstransactionsreport = response.d;
        })
    };
    $("#loader").hide();

    //AEPSTransactionReport

    //read data by id start

    $scope.Read = function (id) {
        //alert("test");
        ReadRecord(id);
    }
    function ReadRecord(id) {
        //alert("n");
        var ID = id;
        $http({
            url: '../Distributor/Electricitytranscation.aspx/loadreceipt',
            method: "POST",
            data: { txnid: id }
        }).success(function (response) {
            $("#loader").hide();
            $scope.fulldetails = response.d;
        })
    };
    //read data by id end
    //Show Receipt





    //electrictyReport
    //Recharge
    $scope.fillrechargereport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Recharge_ListHistory.aspx/fillrechargereport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.RechargeReport = response.d;
        })
    };
    $scope.fillrechargereport();
    $("#loader").hide();
    //

    $scope.fillrechargereportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Recharge_ListHistory.aspx/fillrechargereportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.RechargeReport = response.d;
        })
    };
    $("#loader").hide();

    //Recharge



    //AEPSReport
    $scope.fillaepsreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/AepsNewTranscation.aspx/fillaepsreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.AepsReport = response.d;
        })
    };
    $scope.fillaepsreport();
    $("#loader").hide();
    //

    $scope.fillaepsreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/AepsNewTranscation.aspx/fillaepsreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.AepsReport = response.d;
        })
    };
    $("#loader").hide();

    // AEPSReport




    //DMTReport
    $scope.fillDMTreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/myDMRTransactions.aspx/fillDMTreport',
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
            url: '../Distributor/myDMRTransactions.aspx/fillDMTreportbydate',
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




    //UTICouponReport
    $scope.fillUTICouponreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListPurchaseCoupon.aspx/fillUTIcouponreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.UTICouponReport = response.d;
        })
    };
    $scope.fillUTICouponreport();
    $("#loader").hide();
    //

    $scope.fillUTICouponreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListPurchaseCoupon.aspx/fillUTIcouponreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.UTICouponReport = response.d;
        })
    };
    $("#loader").hide();

    //UTICouponReport




    //PreaidcardtransactonReport
    $scope.fillPrepaidTransreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Insprepaidcardtranscation.aspx/fillPrepaidTransreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.PreapicardtransReport = response.d;
        })
    };
    $scope.fillPrepaidTransreport();
    $("#loader").hide();
    //

    $scope.fillPrepaidTransreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Insprepaidcardtranscation.aspx/fillPrepaidTransreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.PreapicardtransReport = response.d;
        })
    };
    $("#loader").hide();

    //PreaidcardtransactonReport




    //NewDMRtransactonReport
    $scope.filldmrnewreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/DmrNewReport.aspx/filldmrnewreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.dmrnewreport = response.d;
        })
    };
    $scope.filldmrnewreport();
    $("#loader").hide();
    //

    $scope.filldmrnewreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/DmrNewReport.aspx/filldmrnewreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.dmrnewreport = response.d;
        })
    };
    $("#loader").hide();

    $scope.filldmrreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/DmrReport.aspx/filldmrreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.dmrreport = response.d;
        })
    };
    $scope.filldmrreport();
    $("#loader").hide();
    //

    $scope.filldmrreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/DmrReport.aspx/filldmrreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.dmrreport = response.d;
        })
    };
    $("#loader").hide();
    //NewDMRtransactonReport

   



    //mymasterdashboard
    $scope.fillrtmasterdashboard = function () {
        alert("ff")
        var httpreq = {
            method: 'POST',
            url: '../Distributor/Dashboard.aspx/Bindmemberdata',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.rtmasterdashboard = response.d;
        })

        //$timeout(function () {
        //    $scope.fillrtmasterdashboard();
        //}, 10000)
    };
    $scope.fillrtmasterdashboard();
    $("#loader").hide();
    //mydashboard

    $scope.fillEwalletbalancesummary = function () {
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListEwalletTransaction.aspx/fillEWalletTransaction',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.EBalanceSummary = response.d;
        })
    };
    $scope.fillEwalletbalancesummary();
    $("#loader").hide();


    //

    $scope.fillEwalletbalancesummarybydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Distributor/ListEwalletTransaction.aspx/fillEWalletTransactionbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.EBalanceSummary = response.d;
        })
    };
    $("#loader").hide();


    $scope.FillLogin = function () {
        alert('test')
        alert($scope.inputValue);
        var httpreq =
            {
                method: 'POST',
                url: '../Distributor/dmr.aspx/RemitLogin',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: { mobile: $scope.inputValue }
            }
        $http(httpreq).success(function (response) {
            alert(response.remitter.name)
            $scope.Employees = response.d;
        })
    };
});


