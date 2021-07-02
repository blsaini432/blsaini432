<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Udarkhatha_report.aspx.cs" Inherits="Root_Distributor_Udarkhatha_report" %>
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

        .forimg {
            border-radius: 0px !important;
            height: 70px !important;
            width: 150px !important;
        }
    </style>
    <script>
        var app = angular.module("distributorApp", ['angularUtils.directives.dirPagination']);
        app.controller("distributorCntrl", function ($scope, $http, $timeout) {

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
            $scope.fillreport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/Udarkhatha_report.aspx/fillreport',
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
            $scope.fillreport();
            $("#loader").hide();
            //

            $scope.fillreportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/Udarkhatha_report.aspx/fillreportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.dmrreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            $scope.Readinstantdmrreceipt = function (id) {
                instantdmrreceipt(id);

            }
            function instantdmrreceipt(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Distributor/Udarkhatha_report.aspx/loaddmrreceipt',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.instantdmrreceipt = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };


            $scope.delete = function (id) {
                apdateamount(id);
            }
            function apdateamount(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Distributor/Udarkhatha_report.aspx/deletedata',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
                    window.location.reload();
                    $scope.instantdmrreceipt = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };

        });






        function printDiv() {
            var divContents = document.getElementById("openModal").innerHTML;
            var a = window.open('', '', 'height=500, width=500');
            a.document.write('<html>');
            a.document.write('<body>');
            a.document.write(divContents);
            a.document.write('</body></html>');
            a.document.close();
            a.print();
        }
        <%-- function getdata() {
            debugger;
            var fromdate = document.getElementById('<%=Txt_FromDate.ClientID %>').value;
            var todate = document.getElementById('<%=txttodate.ClientID %>').value;
                 document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
                 document.getElementById('<%=hdntodate.ClientID %>').value = todate;
             }--%>
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title"> Udarkhata Transactions
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="distributorApp" ng-controller="distributorCntrl">
                            <div class="row">
                               
                                     <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                              <a href="udarkhata.aspx"class="btn btn-primary">Create Account &gt;&gt;</a>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="col-md-4">
                                    <div class="form-group row">
                                      <td colspan="2">Total Pending Amount  :  <b><asp:Label ID="lblamount" style="color:red" runat="server"/></b></td>
                                        <div class="col-sm-6">
                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                         
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
                                    <table id="tblEwalletsummary" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.N.</th>
                                                <th>Name </th>
                                                <th>Mobile </th>
                                                <th>Address </th>
                                                <th>Credit </th>
                                                <th>Debit </th>
                                                <th>Total Amount</th>
                                                <th>date</th>
                                                <th>update </th>
                                                <th>Delete </th>

                                            </tr>
                                            <tr ng-show="dmrreport.length!=0" dir-paginate="x in dmrreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.Name}}</td>
                                                <td>{{x.Mobile}}</td>
                                                <td>{{x.address}}</td>
                                                <td>{{x.credit}}</td>
                                                 <td>{{x.debit}}</td>
                                                 <td>{{x.Amount}}</td>
                                                <td>{{x.Createdate}}</td>
                                                <td><a href="#openModal" ng-click="Readinstantdmrreceipt(x.TxnID)" class="badge badge-success badge-pill" title="click to view member detail">Update</a></td>
                                               <td><a href="#" ng-click="delete(x.TxnID)" class="badge badge-danger badge-pill" title="click to view member detail">delete </a></td>
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
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text= "Udarkhata Update"></asp:Label>
                                                        </td>
                                                      <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>name </td>
                                                        <td>{{y.Name}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="mobile"></asp:Label>
                                                        </td>
                                                        <td>{{y.Mobile}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="address"></asp:Label>
                                                        </td>
                                                        <td>{{y.address}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="amount"></asp:Label>
                                                        </td>
                                                        <td>{{y.Amount}}</td>
                                                    </tr>
                                                
                                                    <tr>
                                                        <td>Credit</td>
                                                        <td>
                                                          
                                                           <asp:TextBox ID="txt_credit" runat="server" TextMode="MultiLine"></asp:TextBox>
                              
                                                        </td>

                                                    </tr>
                                                   <tr>
                                                        <td>Debit</td>
                                                        <td>
                                                          
                                                           <asp:TextBox ID="txt_debit" runat="server" TextMode="MultiLine"></asp:TextBox>
                               
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">

                                                            <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Submit"  OnClick="btnSubmit_Click" />
                                                            <%--<input type="button" value="Submit" onclick="updateRequest(document.getElementById('configname').value)" />--%>
                                                            <%-- <input type="button" value="Submit" ng-click="update(configname)" />--%>
                                                           
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

