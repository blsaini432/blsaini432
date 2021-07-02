<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master"
    AutoEventWireup="true" CodeFile="BBPS_Transactions_New.aspx.cs" Inherits="Root_Retailer_BBPS_Transactions_New" %>

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

        .forimg{
            border-radius:0px !important;
            height:70px !important;
            width:150px !important;
        }
    </style>
     <script> 
         var app = angular.module("DistributorApp", ['angularUtils.directives.dirPagination']);
         app.controller("DistributorCntrl", function ($scope, $http, $timeout) {

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

             //electrictyReport

             $scope.fillelectricityreport = function () {
                 var httpreq = {
                     method: 'POST',
                     url: '../Retailer/BBPS_Transactions_New.aspx/fillelectricityreport',
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
                     url: '../Retailer/BBPS_Transactions_New.aspx/fillelectricityreportbydate',
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
             $scope.Read = function (id) {
                 //alert("test");
                 ReadRecord(id);
             }
             function ReadRecord(id) {
                 //alert("n");
                 var ID = id;
                 $http({
                     url: '../Retailer/BBPS_Transactions_New.aspx/loadreceipt',
                     method: "POST",
                     data: { txnid: id }
                 }).success(function (response) {
                     $("#loader").hide();
                     $scope.fulldetails = response.d;
                 })
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
            <h3 class="page-title">Electricity Transactions
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="DistributorApp" ng-controller="DistributorCntrl">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_fromdate" runat="server" ng-model="from_date" MaxLength="50" Enabled="false" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                               <asp:HiddenField ID="hdnfromdate" runat="server" ClientIDMode="Static" />
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
                                            <asp:HiddenField ID="hdntodate" runat="server" ClientIDMode="Static" />
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
                                            <a href="#" ng-click="fillelectricityreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>

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
                                    <table id="tblEwalletsummary" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID
                                                </th>
                                                <th>ServiceName
                                                </th>
                                                <th>TransactionID
                                                </th>
                                                <th>OperatorID
                                                </th>
                                                <th>AccountNumber</th>
                                                <th>Amount</th>
                                                <th>Status</th>
                                                <th>Message</th>
                                                <th>PayDate</th>
                                                <th>Receipt</th>
                                            </tr>
                                            <tr ng-show="ElectrictyReport.length!=0" dir-paginate="x in ElectrictyReport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.servicename}}</td>
                                                <td>{{x.agent_id}}</td>
                                                <td>{{x.opr_id}}</td>
                                                <td>{{x.account_no}}</td>
                                                <td>{{x.trans_amt}}</td>
                                                <td>{{x.Status}}</td>
                                                <td>{{x.msg}}</td>
                                                <td>{{x.paydate}}</td>
                                                <td><a target="_blank" href="BBPS_Receipt.aspx?refidforreceipt={{x.agent_id}}"  class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                            </tr>
                                            <tr>
                                                <td ng-show="ElectrictyReport.length==0" colspan="7">
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


                                                     <%--Modal Popup Code--%>

                            <div id="openModal" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row" ng-repeat="y in fulldetails">
                                            <div class="col-md-12">
                                 <table class="table table-responsive">
            <tr>
                <td>
                    <a class="navbar-brand brand-logo">
                    <asp:Image ID="imgbbps" runat="server" ImageUrl={{y.logo}}  CssClass="forimg"/></a>
                    </td>
                <td >
                    <img src="../../Uploads/BBPS-LOGO.png" class="forimg" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Transaction Receipt"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>TR:{{y.agent_id}} </td>
                <td ></td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="Transaction Details"></asp:Label>
                </td>
                <td ></td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="Label3" runat="server" Text="Service For"></asp:Label>
                </td>
                <td >Electricity </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Acknowledgement"></asp:Label>
                </td>
                <td>{{y.agent_id}}</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="PayDate"></asp:Label>
                </td>
                <td>{{y.paydate}}</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Service Name"></asp:Label>
                </td>
                <td>{{y.servicename}}</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Account Number"></asp:Label>
                </td>
                <td>{{y.account_no}}</td>
            </tr>
                                      <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td>{{y.customername}}</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Bill Amount"></asp:Label>
                </td>
                <td>Rs. {{y.trans_amt}}</td>
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
        </div>
    </div>

</asp:Content>
