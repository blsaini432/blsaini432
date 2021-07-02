<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListaepsWalletBalance.aspx.cs" Inherits="Root_Admin_ListRWalletBalance" %>
      <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
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

        $scope.fillaepsWalletBalance = function () {
            $("#loader").show();
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
                $("#loader").hide();
                $scope.AepsBalance = response.d;
            }, function (response) {
                $("#loader").hide();
            });
        };
        $scope.fillaepsWalletBalance();
        $("#loader").hide();
    });
</script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">AEPS Wallet Balance Ledger
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
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
                                       <div id="loader" style='display: none;'>
                                        <img src='../../Design/images/pageloader.gif' width='32px' height='32px'>
                                    </div>
                                    <table id="example1" class="table table-bordered table-hover" class="jsgrid-table">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID
                                                </th>
                                                <th>MemberName
                                                </th>
                                                <th>Debit
                                                </th>
                                                <th>Credit
                                                </th>
                                                <th>Balance
                                                </th>
                                                <th>Detail</th>
                                            </tr>
                                            <tr dir-paginate="x in AepsBalance|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                <td style="color:red">{{x.Debit}}</td>
                                                <td style="color:green">{{x.Credit}}</td>
                                                <td>{{x.Balance}}</td>
                                                <td><a href="ListRWalletTransaction.aspx?id={{x.MsrNo}}" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                            </tr>
                                              <tr>
                                                <td ng-show="AepsBalance.length==0" colspan="12">
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
                          


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
