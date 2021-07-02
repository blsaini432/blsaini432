<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Vehicleowner_report.aspx.cs" Inherits="Root_Retailer_Vehicleowner_report" %>
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
                       url: '../Distributor/Drivinglicense_report.aspx/loadreceipt',
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
                       url: '../Retailer/Drivinglicense_report.aspx/fillaepstransactions',
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
                       url: '../Retailer/Drivinglicense_report.aspx/fillaepstransactionsbydate',
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
            <h3 class="page-title">Vehicle Ownership Report 
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
                                                <th>FRANCHISEE CODE
                                                </th>
                                         
                                                  <th>Customer name
                                                </th>
                                                  <th>father name
                                                </th>
                                                  <th>mobile
                                                </th>
                                              
                                                <th>state
                                                </th>
                                                <th>
                                                  city
                                                </th>
                                                <th>Status</th>
                                                
                                            </tr>
                                            <tr dir-paginate="x in aepstransactionsreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.memberid}}</td>
                                                <td>{{x.Custmer_Number}}</td>
                                                 <td>{{x.father_name}}</td>
                                                 <td>{{x.mobile}}</td>
                                                  <td>{{x.state}}</td>
                                                <td>{{x.city}}</td>
                                                 <td>{{x.status}}</td>
                                                
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

                          
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>






















