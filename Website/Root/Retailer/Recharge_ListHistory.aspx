<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true"
    CodeFile="Recharge_ListHistory.aspx.cs" Inherits="Root_Retailer_ListHistory" %>

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
        var app = angular.module("retailerApp", ['angularUtils.directives.dirPagination']);
        app.controller("retailerCntrl", function ($scope, $http, $timeout) {

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


            //Recharge
            $scope.fillrechargereport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Retailer/Recharge_ListHistory.aspx/fillrechargereport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.RechargeReport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.fillrechargereport();
            $("#loader").hide();
            //

            $scope.fillrechargereportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Retailer/Recharge_ListHistory.aspx/fillrechargereportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.RechargeReport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            //Recharge


            //Show RechargeReceipt

            //read data by id start

            $scope.Readrechargereceipt = function (id) {

                rechargereceipt(id);
            }
            function rechargereceipt(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Retailer/Recharge_ListHistory.aspx/loadrechargereceipt',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.rechargereceipt = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            //read data by id end
            //Show RechargeReceipt
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

             function getdata()
        {
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
            <h3 class="page-title">Recharge History
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="retailerApp" ng-controller="retailerCntrl">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_fromdate" runat="server" ng-model="from_date" MaxLength="50" Enabled="false" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                             <asp:HiddenField ID="hdnfromdate" runat="server"  ClientIDMode="Static" />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
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
                                            <asp:HiddenField ID="hdntodate" runat="server"  ClientIDMode="Static" />
                                            <asp:Image ID="imgbt" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
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
                                            <a href="#" ng-click="fillrechargereportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>

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
                                                <th>S.N.
                                                </th>
                                                <th>memberid
                                                </th>
                                                <th>MobileNo
                                                </th>
                                                <th>RechargeAmount
                                                </th>
                                                <th>OperatorName
                                                </th>
                                                <th>TransID
                                                </th>
                                                <th>Status
                                                </th>
                                                <th>ErrorMsg</th>
                                                <th>APIMessage</th>
                                                <th>AddDate</th>
                                                <th>Receipt</th>

                                            </tr>
                                            <tr ng-show="RechargeReport.length!=0" dir-paginate="x in RechargeReport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MobileNo}}</td>
                                                <td>{{x.RechargeAmount}}</td>
                                                <td>{{x.OperatorName}}</td>
                                                <td>{{x.TransID}}</td>
                                                <td>{{x.Status}}</td>
                                                <td>{{x.ErrorMsg}}</td>
                                                 <td>{{x.APIMessage}}</td>
                                                <td>{{x.AddDate}}</td>
                                                <td><a href="#openModal" ng-click="Readrechargereceipt(x.HistoryID)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                            </tr>
                                            <tr>
                                                <td ng-show="RechargeReport.length==0" colspan="12">
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
                                        <div class="row" ng-repeat="y in rechargereceipt">
                                            <div class="col-md-12">
                                                <table class="table table-responsive">
                                                    <tr>
                                                        <td>
                                                            <a class="navbar-brand brand-logo">
                                                                <asp:Image ID="imgbbps" runat="server" ImageUrl="{{y.logo}}" CssClass="forimg" /></a>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Transaction Receipt"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>TR:{{y.receiptno}} </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="Transaction Details"></asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Service For"></asp:Label>
                                                        </td>
                                                        <td>Recharge</td>
                                                    </tr>
                                                                                                       <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="PayDate"></asp:Label>
                                                        </td>
                                                        <td>{{y.AddDate}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Account"></asp:Label>
                                                        </td>
                                                        <td>{{y.MobileNo}}Account Number({{y.caNumber}})</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Transaction ID"></asp:Label>
                                                        </td>
                                                        <td>{{y.TransID}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Operator Reference Number"></asp:Label>
                                                        </td>
                                                        <td>{{y.APIMessage}}</td>
                                                    </tr>
                                                       <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td>{{y.Status}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Recharge Amount"></asp:Label>
                                                        </td>
                                                        <td>Rs. {{y.RechargeAmount}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Merchant Details"></asp:Label>
                                                        </td>
                                                        <td>{{y.MemberName}}
                                                            {{y.address}}
                                                            <br />
                                                            {{y.mymobile}}<br />
                                                            {{y.Email}}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <input type="button" value="Print" class="btn btn-primary" onclick="printDiv()">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
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

















