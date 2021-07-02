<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Jobus_report.aspx.cs" Inherits="Root_Distributor_Jobus_report" %>

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
            $scope.filldmrreport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/Jobus_report.aspx/Jobus_report',
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

            $scope.filldmrreportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/Jobus_report.aspx/Jobus_reportbydate',
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
                    url: '../Distributor/EMIPayment_report.aspx/loaddmrreceipt',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
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
        function getdata() {
            debugger;
            var fromdate = document.getElementById('<%=Txt_FromDate.ClientID %>').value;
            var todate = document.getElementById('<%=txttodate.ClientID %>').value;
                 document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
                 document.getElementById('<%=hdntodate.ClientID %>').value = todate;
             }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Job us Report
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
                                        <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
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
                                            <a href="#" ng-click="filldmrreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>

                                        </div>
                                    </div>
                                </div>
                            </div>
                           <%-- <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Export" CssClass="btn btn-dribbble" OnClientClick="getdata()" />
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
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
                                                 <th>Name
                                                </th>
                                                  <th>DOB
                                                </th>
                                                  <th>Mobile
                                                </th>
                                                <th>Working Field
                                                </th>
                                                <th>Experinces
                                                </th>
                                                 <th>Last Salary
                                                </th>
                                                <th>Want Salary Now</th>
                                                <th>Doing currently</th>
                                                <th>
                                                    Address
                                                </th>
                                                    <th>Date</th>
                                              
                                               
                                            </tr>
                                            <tr dir-paginate="x in dmrreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                <td>{{x.DOB}}</td>
                                                 <td>{{x.mobile}}</td>
                                                 <td>{{x.Working}}</td>
                                                <td>{{x.Experinces}}</td>
                                                <td>{{x.Last_Salary}}</td>
                                                 <td>{{x.Want_Salary}}</td>
                                                <td>{{x.doing_currently}}</td>
                                                <td>{{x.full_address}}</td>
                                                 <td>{{x.Createdate}}</td>
                                                    
                                            </tr>
                                             <tr>
                                                <td ng-show="dmrnewreport.length==0" colspan="13">
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
                                            <div class="">
                                                <table class="">
                                                    <tr>
                                                        <td>
                                                            
                                                                 <img src="../../Uploads/Company/Logo/actual/logo.png"" class="navbar-brand brand-logo" style="height:100px; width:100px;border-radius:0%"/>
                                                                
                                                               <%-- <asp:Image ID="imgbbps" runat="server" ImageUrl="{{y.logo}}" CssClass="forimg" /></a>--%>
                                                        </td>
                                                        <td>
                                                          
                                                            <img src="../../Uploads/Company/Logo/actual/index.png"" class="navbar-brand brand-logo" style="height:100px; width:100px;border-radius:0%"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="PAYMENT RECEIPT for RENEWAL PREMIUM"></asp:Label>
                                                        </td>
                                                      
                                                    </tr>
                                                    <tr>
                                                        <td>Instalment Premium </td>
                                                        <td>{{y.AgentOrderId}}</td>
                                                    </tr>
                                                    <tr> 
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Mode"></asp:Label>
                                                        </td>
                                                        <td>{{y.mode}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>{{y.AgentOrderId}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="DateTime"></asp:Label>
                                                        </td>
                                                        <td>{{y.TxnDate}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Service Name"></asp:Label>
                                                        </td>
                                                        <td>DMR( Money Transfer)</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Account Number"></asp:Label>
                                                        </td>
                                                        <td>{{y.beneficiaryAccount}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Transaction Amount"></asp:Label>
                                                        </td>
                                                        <td>Rs. {{y.amount}}</td>
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

