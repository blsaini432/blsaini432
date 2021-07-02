<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="AEPSTransactions.aspx.cs" Inherits="Root_Retailer_AEPSTransactionss" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
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
               $scope.Readinstantdmrreceipt = function (id) {
                   instantdmrreceipt(id);
               }
               function instantdmrreceipt(id) {
                   $("#loader").show();
                   var ID = id;
                   $http({
                       url: '../Distributor/AEPSTransactions.aspx/loadreceipt',
                       method: "POST",
                       data: { txnid: id }
                   }).success(function (response) {
                       $("#loader").hide();
                       $scope.instantdmrreceipt = response.d;
                   }, function (response) {
                       $("#loader").hide();
                   });
               };
               //AEPSTransactionReport

               $scope.fillaepstransaction = function () {
                   $("#loader").show();
                   var httpreq = {
                       method: 'POST',
                       url: '../Retailer/AEPSTransactions.aspx/fillaepstransactions',
                       headers: {
                           'Content-Type': 'application/json; charset=utf-8',
                           'dataType': 'json'
                       },
                       data: {}
                   }
                   $http(httpreq).success(function (response) {
                       $("#loader").hide();
                       $scope.aepstransactionsreport = response.d;

                   }, function (response) {
                       $("#loader").hide();
                   });
               };
               $scope.fillaepstransaction();
               $("#loader").hide();
               //

               $scope.fillaepstransactionbydate = function (fdate, tdate) {
                   $("#loader").show();
                   var httpreq = {
                       method: 'POST',
                       url: '../Retailer/AEPSTransactions.aspx/fillaepstransactionsbydate',
                       headers: {
                           'Content-Type': 'application/json; charset=utf-8',
                           'dataType': 'json'
                       },
                       data: { fromdate: fdate, todate: tdate }
                   }
                   $http(httpreq).success(function (response) {
                       $("#loader").hide();
                       $scope.aepstransactionsreport = response.d;
                   }, function (response) {
                       $("#loader").hide();
                   });
               };
               $("#loader").hide();

               //AEPSTransactionReport

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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">AEPS Transactions
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
                                            <a href="#" ng-click="fillaepstransactionbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
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
                                                <th>MemberID
                                                </th>
                                         
                                                  <th>Customer Mobile
                                                </th>
                                                  <th>TransactionId
                                                </th>
                                                  <th>Amount
                                                </th>
                                              
                                                <th>Date
                                                </th>
                                                <th>
                                                  Status
                                                </th>
                                                 <th>Message</th>
                                                   <th>Receipt</th>
                                            </tr>
                                            <tr dir-paginate="x in aepstransactionsreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.memberid}}</td>
                                                <td>{{x.Custmer_Number}}</td>
                                                 <td>{{x.order_id}}</td>
                                                 <td>{{x.Amount}}</td>
                                                  <td>{{x.creted}}</td>
                                                <td>{{x.txnstatus}}</td>
                                                 <td>{{x.msg}}</td>
                                                <td><a href="#openModal" ng-click="Readinstantdmrreceipt(x.order_id)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                            </tr>
                                             <tr>
                                                <td ng-show="aepstransactionsreport.length==0" colspan="13">
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
                                                                 <img src="../../Uploads/Company/Logo/actual/logo.png"" class="navbar-brand brand-logo" style="height:50px; width:100px;border-radius:0%"/>

                                                               <%-- <asp:Image ID="imgbbps" runat="server" ImageUrl="{{y.logo}}" CssClass="forimg" /></a>--%>
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr></tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="AEPS Transction"></asp:Label>
                                                        </td>
                                                      <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Member ID"></asp:Label>
                                                        </td>
                                                        <td>{{y.memberid}})</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                                        </td>
                                                        <td>{{y.creted}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="order id"></asp:Label>
                                                        </td>
                                                        <td>{{y.order_id}}</td>
                                                    </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>{{y.Amount}}</td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Aadhar Number"></asp:Label>
                                                        </td>
                                                        <td>{{y.AadharNumber}}</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Text="BANK NAME"></asp:Label>
                                                        </td>
                                                        <td>{{y.BANK_NAME}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="txn status"></asp:Label>
                                                        </td>
                                                        <td>Rs. {{y.txnstatus}}</td>
                                                    </tr>
                                                    <tr><td></td>
                                                        <td></td>
                                                    </tr>
                                                     <tr><td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>Signature</td>
                                                    </tr>
                                                    <tr>
                                                       
                                                    </tr>
                                                     <tr>
                                                         
                                                         <td></td>
                                                    </tr>
                                                     <tr>
                                                         
                                                         <td></td>
                                                    </tr>
                                                     <tr>
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td></td>
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
                            <%--Modal Popup Code end--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>






















