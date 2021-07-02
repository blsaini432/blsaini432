﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="PaytmGateway_Report.aspx.cs" Inherits="Root_Administrator_PaytmGateway_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
    <style>
        .input {
            float: left;
            width: 87%;
        }
    </style>
    <script>
        var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
        app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {

            //Pagination start
            $scope.currentPage = 1;
            $scope.pageSize = 10;
            $scope.pageChangeHandler = function (num) {
                console.log('meals page changed to ' + num);
            };
            $(document).ready(function () {
                $scope.pageChangeHandler = function (num) {
                    console.log('going to page ' + num);
                };
            });
            //pagination end
            //NewDMRtransactonReport
            $scope.filldmrreport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/PaytmGateway_Report.aspx/fillreport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.dmrnewreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.filldmrreport();
            $("#loader").hide();
            //

            $scope.filldmrreportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/PaytmGateway_Report.aspx/fillreportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.dmrnewreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            //Report
            $scope.CheckStatus = function (id) {

                CheckStatus(id);
            }
            function CheckStatus(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Administrator/PaytmGateway_Report.aspx/CheckStatus',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { txnid: id }
                }).success(function (response) {
                    showSwal('success-message');
                 //   $("#loader").hide();
                    $scope.filldmrreport();


                })
            };

            $scope.RejectRequest = function (id, mobile, date, amount) {

                RejectBRequest(id, mobile, date, amount);
            }

            $scope.ApproveRequest = function (id) {
                approvedforce(id);

            }
            function approvedforce(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Administrator/PaytmGateway_Report.aspx/approved',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.filldmrreport();
                });
            };

        });


        function getdata() {
            debugger;
            var fromdate = document.getElementById('<%=txt_fromdate.ClientID %>').value;
            var todate = document.getElementById('<%=txttodate.ClientID %>').value;
            document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
            document.getElementById('<%=hdntodate.ClientID %>').value = todate
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Payment Gateway Transactions
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_fromdate" runat="server" ng-model="from_date" MaxLength="50" Enabled="false" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                            <asp:HiddenField ID="hdnfromdate" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../css/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fromdate"
                                                Display="Dynamic" ErrorMessage="Please Enter From Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" PopupButtonID="Image1"
                                                TargetControlID="txt_fromdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="50" ng-model="to_date" CssClass="form-control" autcomplete="off" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdntodate" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="imgbt" runat="server" ImageUrl="../css/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txttodate"
                                                Display="Dynamic" ErrorMessage="Please Enter To Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" Animated="False"
                                                PopupButtonID="imgbt" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <a href="#" ng-click="filldmrreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Export" CssClass="btn btn-dribbble" OnClientClick="getdata()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <div class="info-box">
                                <div class="form-group">
                                    <label>Search</label>
                                    <input type="text" ng-model="search" class="form-control" placeholder="Search">
                                </div>
                                <div>
                                    <label for="search">items per page:</label>
                                    <input type="number" min="1" max="100" class="form-control" ng-model="pageSize">
                                </div>
                                <div class="table-responsive">
                                    <table id="tblEwalletsummary" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID
                                                </th>
                                                <th>order ID</th>
                                                <th>Method
                                                </th>

                                                <th>Amount
                                                </th>
                                                <th>Wallet Add Amount
                                                </th>
                                                <th>Fee Rate
                                                </th>
                                                <th>Status</th>
                                                <th>Paytm Status</th>
                                                <th>Response Msg</th>
                                                <th>date</th>

                                            </tr>
                                            <tr ng-show="dmrnewreport.length!=0" dir-paginate="x in dmrnewreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.TransactionId}}</td>
                                                <td>{{x.GATEWAYNAME}}</td>
                                                <td>{{x.Amount}}</td>
                                                <td>{{x.totalamount}}</td>
                                                <td>{{x.feerate}}</td>
                                                <td>{{x.Status}}</td>
                                                <td>{{x.callbackstatus}}</td>
                                                <td>{{x.RESPMSG}}</td>
                                                <td>{{x.Createdate}}</td>
                                                <td>
                                                    <div ng-if="x.Status =='Pending'">
                                                        <a href="#" ng-click="CheckStatus(x.TransactionId)" class="badge badge-success badge-pill">Check Status</a>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td ng-show="dmrreport.length==0" colspan="14">
                                                    <span>No data Available</span>
                                                </td>
                                            </tr>
                                        </thead>
                                    </table>
                                    <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
                                        <div class="dataTables_scrollFootInner" style="width: 1224px; padding-right: 17px;">
                                            <table class="display dataTable" role="grid" style="margin-left: 0px; width: 1224px;"></table>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <dir-pagination-controls boundary-links="true" on-page-change="pageChangeHandler(newPageNumber)" template-url="../Angularjsapp/dirPagination.tpl.html"></dir-pagination-controls>
                                    </div>
                                </div>
                            </div>
                            <%--Modal Popup Code--%>

                            <div id="openModal" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row" ng-repeat="y in instantdmrreceipt">
                                            <table class="table table-responsive ps--scrolling-y">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="memberid"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <td>{{y.MemberID}}</td>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label for="cars">Payment Methods:</label>
                                                    </td>
                                                    <td>

                                                        <asp:DropDownList ID="droplist" runat="server">
                                                            <asp:ListItem>Please select</asp:ListItem>
                                                            <asp:ListItem>UPI / BHARAT QR</asp:ListItem>
                                                            <asp:ListItem>Wallets</asp:ListItem>
                                                            <asp:ListItem>netbanking</asp:ListItem>
                                                            <asp:ListItem>Credit Cards</asp:ListItem>
                                                            <asp:ListItem>Debit Card ( Rupay ) </asp:ListItem>
                                                            <asp:ListItem>Debit Card ( Visa )</asp:ListItem>
                                                            <asp:ListItem>Credit Cards</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Amount</td>
                                                    <td>

                                                        <asp:TextBox ID="txt_amount" runat="server" TextMode="MultiLine"></asp:TextBox>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">

                                                        <%--  <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Submit"  OnClick="btnSubmit_Click" />--%>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--Modal Popup Code end--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
