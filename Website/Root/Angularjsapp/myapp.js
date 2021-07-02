var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {
    $scope.loadDefaultData = function (formType)
    {
        if (formType == "viewstatemnt")
        {
          //  $scope.fillList();
        }
    }

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

    //AEPSWallet

    //AEPSWallet Balance ledger
    $scope.fillaepsWalletBalance = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ListaepsWalletBalance.aspx/fillaepsWalletBalance',
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
            url: '../Administrator/ListRwalletTransaction.aspx/fillRWalletTransaction',
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
            url: '../Administrator/ListRwalletTransaction.aspx/fillRWalletTransactionbydate',
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


    //EwalletBalance ledger
    $scope.fillEwalletbalance = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ListEwalletBalance.aspx/fillEWalletBalance',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.EBalance = response.d;
        })
    };
    $scope.fillEwalletbalance();
    $("#loader").hide();


    $scope.fillEwalletbalancesummary = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ListEwalletTransaction.aspx/fillEWalletTransaction',
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
            url: '../Administrator/ListEwalletTransaction.aspx/fillEWalletTransactionbydate',
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


    //
    $scope.Export = function () {
        $("#tblEwalletsummary").table2excel({
            filename: "EwalletSummary.xls"
        });
    }
    $scope.Exortexcel = function () {
        var csvInput = EBalanceSummary();

        // File is an angular resource. We call its save method here which
        // accesses the api above which should return the content of csv
        File.save(csvInput, function (content) {
            var dataUrl = 'data:text/csv;utf-8,' + encodeURI(content);
            var hiddenElement = document.createElement('a');
            hiddenElement.setAttribute('href', dataUrl);
            hiddenElement.click();
        });
    }
    //EwalletBalance Ledger


    //NewDMRtransactonReport
    $scope.filldmrnewreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/DmrNewTranscation.aspx/fillnewdmrreport',
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
            url: '../Administrator/DmrNewTranscation.aspx/fillnewdmrreportbydate',
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

    //NewDMRtransactonReport


    //BBPStransactonReport
    $scope.fillbbpsnewreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ElectricityTransactions.aspx/fillelectricityreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.bbpsnewreport = response.d;
        })
    };
    $scope.fillbbpsnewreport();
    $("#loader").hide();
    //

    $scope.fillbbpsnewreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ElectricityTransactions.aspx/fillelectricityreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.bbpsnewreport = response.d;
        })
    };
    $("#loader").hide();

    //BBPStransactonReport









    //AEPSBankRequest
    $scope.fillaepsbankreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Aepsbank.aspx/fillaepsbankreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.aepsbankreport = response.d;
        })
    };
    $scope.fillaepsbankreport();
    $("#loader").hide();
    //

    $scope.fillaepsbankreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/AepsBank.aspx/fillaepsbankreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.aepsbankreport = response.d;
        })
    };
    $("#loader").hide();

    //AEPSBankRequest




    //AEPSBankTranscation
    $scope.fillaepsbanktransaction = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/AepsTranscationNew.aspx/fillaepsbanktransaction',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.aepstransactionreport = response.d;
        })
    };
    $scope.fillaepsbanktransaction();
    $("#loader").hide();
    //

    $scope.fillaepsbanktransactionbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/AepsTranscationNew.aspx/fillaepsbanktransactionbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.aepstransactionreport = response.d;
        })
    };
    $("#loader").hide();

    //AEPSBankTranscation


    //AEPSTransactionReport

    $scope.fillaepstransaction = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/AEPSTransactions.aspx/fillaepstransactions',
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
            url: '../Administrator/AEPSTransactions.aspx/fillaepstransactionsbydate',
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

    //DMRTransaction
    $scope.filldmrinreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/MM_Transactions.aspx/filldmrinreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.dmrinreport = response.d;
        })
    };
    $scope.filldmrinreport();
    $("#loader").hide();
    //

    $scope.filldmrinreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/MM_Transactions.aspx/filldmrinreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.dmrinreport = response.d;
        })
    };
    $("#loader").hide();

    //DMRTransaction






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


    //UTIReport

    $scope.filluticouponreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ViewCouponRequest.aspx/filluticouponreport',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.UtiCouponReport = response.d;
        })
    };
    $scope.filluticouponreport();
    $("#loader").hide();
    //

    $scope.filluticouponreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ViewCouponRequest.aspx/filluticouponreportbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.UtiCouponReport = response.d;
        })
    };
    $("#loader").hide();
    //UTIReport End





    //UTIReport

    $scope.fillutiregistrationreport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ViewPSAregall.aspx/fillutiregistration',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.UtiRegistrationReport = response.d;
        })
    };
    $scope.fillutiregistrationreport();
    $("#loader").hide();
    //

    $scope.fillutiregistrationreportbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ViewPSAregall.aspx/fillutiregistrationbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.UtiRegistrationReport = response.d;
        })
    };
    $("#loader").hide();
    //UTIReport End




    //Preapidcard Report

    $scope.fillprepaidcardtxn = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Insprepaidcardtranscation.aspx/fillprepaidcardtxn',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.PrepaidcardtxnReport = response.d;
        })
    };
    $scope.fillprepaidcardtxn();
    $("#loader").hide();
    //

    $scope.fillprepaidcardtxnbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Insprepaidcardtranscation.aspx/fillprepaidcardtxnbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.PrepaidcardtxnReport = response.d;
        })
    };
    $("#loader").hide();
    //Preapidcard Report End

    //Recharge
    $scope.fillrechargereport = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Recharge_ListHistory.aspx/fillrechargereport',
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
            url: '../Administrator/Recharge_ListHistory.aspx/fillrechargereportbydate',
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



    //AEPSKYC
    $scope.fillaepskyc = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Listaeps_reg.aspx/fillaepskyc',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
        $http(httpreq).success(function (response) {
            $scope.aepskyc = response.d;
        })
    };
    $scope.fillaepskyc();
    $("#loader").hide();
    //

    $scope.fillaepskycbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Listaeps_reg.aspx/fillaepskycbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.aepskyc = response.d;
        })
    };
    $("#loader").hide();
    //AEPSKYC



    //FundRequest
    $scope.fillfundrequest = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/ListFundRequest.aspx/fillfundrequest',
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
            url: '../Administrator/ListFundRequest.aspx/fillfundrequestbydate',
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
    //FundRequest

    //ApproveFundRequest

    $scope.ApproveFundRequest = function (id) {

        ApproveRequest(id);
    }
    function ApproveRequest(id) {
        var ID = id;
        $http({
            url: '../Administrator/ListFundRequest.aspx/ApproveRequest',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fundid: id }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillfundrequest();

        })
    };

    //End

    //RejectFundRequest

    $scope.RejectFundRequest = function (id) {

        RejectRequest(id);
    }
    function RejectRequest(id) {
        var ID = id;
        $http({
            url: '../Administrator/ListFundRequest.aspx/RejectRequest',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fundid: id }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillfundrequest();

        })
    };

    //End


    //RejectFundRequest

    $scope.ShowFundImage = function (id) {

        FundImage(id);
    }
    function FundImage(id) {
        var ID = id;
        $http({
            url: '../Administrator/ListFundRequest.aspx/ShowFundImage',
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

    //End



    //Credit Report
    $scope.fillcredittransaction = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Credit_Transaction_Report.aspx/fillcredittransaction',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.CreditTransaction = response.d;
        })
    };
    $scope.fillcredittransaction();
    $("#loader").hide();


    //

    $scope.fillcredittransactionbydate = function (fdate, tdate) {
        //alert("pooja");
        debugger;
        var httpreq = {
            method: 'POST',
            url: '../Administrator/Credit_Transaction_Report.aspx/fillcredittransactionbydate',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {
            $scope.CreditTransaction = response.d;
        })
    };
    $("#loader").hide();





    $scope.MarkStatus = function (EwalletTransactionID) {

        MarkStatus(EwalletTransactionID);
    }
    function MarkStatus(EwalletTransactionID) {
        debugger
        $http({
            
            url: '../Administrator/Credit_Transaction_Report.aspx/MarkStatus',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { EwalletTransactionID: EwalletTransactionID }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillcredittransaction();
        })
    };
    //Credit Report

    //ApproveBankRequest

    $scope.ApproveBankRequest = function (id, banksid) {

        ApproveBRequest(id, banksid);
    }
    function ApproveBRequest(id, banksid) {
        debugger;
        var ID = id;
        var bid = $scope.inputValue;
        $http({
            url: '../Administrator/Aepsbank.aspx/ApproveBankRequest',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fundid: id, bankid:bid }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillaepsbankreport();

        })
    };

    //End

    //RejectBankRequest

    $scope.RejectBankRequest = function (id) {

        RejectBRequest(id);
    }
    function RejectBRequest(id) {
        var ID = id;
        $http({
            url: '../Administrator/Aepsbank.aspx/RejectBankRequest',
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fundid: id }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillaepsbankreport();

        })
    };

    //End


    //RejectFundRequest















    $scope.ExportDMR = function (fdate, tdate) {

        //alert("pooja");
        debugger;
        if (fdate == null || fdate === "" || tdate == null || tdate === "")
        {
            $scope.date = new Date();
            fdate = $filter('date')($scope.date, 'dd-MM-yyyy');
            tdate = $filter('date')(new Date(), 'dd-MM-yyyy');
        }
        var httpreq = {
            method: 'POST',
            url: '../Administrator/MM_Transactions.aspx/ExportDMR',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { fromdate: fdate, todate: tdate }
        }
        $http(httpreq).success(function (response) {

        })
    };
});




