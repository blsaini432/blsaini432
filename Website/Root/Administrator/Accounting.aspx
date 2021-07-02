<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Accounting.aspx.cs" Inherits="Root_Admin_Accounting" %>

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


            //NewDMRtransactonReport
            $scope.filldmrreport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/Accounting.aspx/filldmrreport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.dmrreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.filldmrreport();
            $("#loader").hide();
            //

            $scope.filldmrreportbydate = function (service, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/Accounting.aspx/filldmrreportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { service: service, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.dmrreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            //NewDMRtransactonReport
        });


        function getdata() {
            debugger;
            var fromdate = document.getElementById('<%=Txt_FromDate.ClientID %>').value;
            var todate = document.getElementById('<%=Txt_FromDate.ClientID %>').value;
            document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
            document.getElementById('<%=Txt_FromDate.ClientID %>').value = todate
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Accounting Report
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
                            <div class="row">
                                <div class="col-md-4">
                                    <h6>Serevice</h6>
                                    <select id="cars"  runat="server"  CssClass="form-control" autcomplete="off" ng-model="service">
                                       
                                        <option value="Recharge">Recharge</option>
                                        <option value="AEPS">AEPS</option>
                                        <option value="XPRESSDMR">Xpress DMR</option>
                                        <option value="BBPS">BBPS</option>
                                        <option value="ICICAEPS">ICIC AEPS</option>
                                        <option value="DMR">DMR</option>
                                    </select>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Date<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="Txt_FromDate" runat="server" ng-model="from_date" MaxLength="50" Enabled="false" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                            <asp:HiddenField ID="hdnfromdate" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="Txt_FromDate"
                                                Display="Dynamic" ErrorMessage="Please Enter From Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" PopupButtonID="Image1"
                                                TargetControlID="Txt_FromDate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <a href="#" ng-click="filldmrreportbydate(service,from_date)" class="btn btn-primary">Search &gt;&gt;</a>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="info-box">
                               
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
                                                <th>Total
                                                </th>

                                            </tr>
                                            <tr ng-show="dmrreport.length!=0" dir-paginate="x in dmrreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.amount}}</td>
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

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

