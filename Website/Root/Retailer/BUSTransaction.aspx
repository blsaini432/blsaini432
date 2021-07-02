<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true"
    CodeFile="BUSTransaction.aspx.cs" Inherits="Root_Admin_BUSTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />

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
       
        //Recharge
            $scope.fillrechargereport = function () {
                $("#loader").show();
            var httpreq = {
                method: 'POST',
                url: '../Retailer/BUSTransaction.aspx/fillbusreport',
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

        $scope.Readinstantdmrreceipt = function (id){
            $("#loader").show();
            var httpreq = {
                method: 'POST',
                url: '../Retailer/BUSTransaction.aspx/loadreceipt',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: { txnid: id }
            }
            $http(httpreq).success(function (response) {
                $("#loader").hide();
                $scope.instantdmrreceipt = response.d;
            }, function (response) {
                $("#loader").hide();
            });
        };

        $scope.fillrechargereportbydate = function (fdate, tdate) {
            $("#loader").show();
            var httpreq = {
                method: 'POST',
                url: '../Retailer/BUSTransaction.aspx/fillbusreportbydate',
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
        });
        //Recharge
      
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Bus History
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
                                    <table id="tblEwalletsummary" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID</th>
                                                <th>MobileNo</th>
                                                <th>travelername</th>
                                                <th>pnrno</th>
                                                <th>transactionid</th>
                                                <th>pickuplocationaddress</th>
                                                <th>seatname</th>
                                                <th>Amount</th>
                                                <th>Updatedatetime</th>
                                                <th>bookingstatus</th>
                                                <th>droplocation</th>
                                                <th>email</th>
                                                <th>Receipte</th>
                                           <%--     <th>Receipt</th>
                                                <th>View</th>--%>

                                            </tr>
                                            <tr ng-show="RechargeReport.length!=0" dir-paginate="x in RechargeReport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MobileNo}}</td>
                                                <td>{{x.travelername}}</td>
                                                <td>{{x.pnrno}}</td>
                                                <td>{{x.transactionid}}</td>
                                                <td>{{x.pickuplocationaddress}}</td>
                                                <td>{{x.seatname}}</td>
                                                <td>{{x.Amount}}</td>
                                                <td>{{x.Updatedatetime}}</td>
                                                <td>{{x.bookingstatus}}</td>
                                                <td>{{x.droplocation}}</td>
                                                <td>{{x.pemail}}</td>
                                          <td><a href="#openModal" ng-click="Readinstantdmrreceipt(x.transactionid)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                            </tr>
                                            <tr>
                                                <td ng-show="RechargeReport.length==0" colspan="15">
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
                                                                 <img src="../../Uploads/Company/Logo/actual/logo.png"" class="navbar-brand brand-logo" style="height:100px; width:100px;border-radius:0%"/>

                                                               <%-- <asp:Image ID="imgbbps" runat="server" ImageUrl="{{y.logo}}" CssClass="forimg" /></a>--%>
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr></tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="BUS Receipt"></asp:Label>
                                                        </td>
                                                      <td></td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                                        </td>
                                                        <td>{{y.Updatedatetime}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="PNR NO"></asp:Label>
                                                        </td>
                                                        <td>{{y.pnrno}}</td>
                                                    </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>RS {{y.Amount}}</td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Pick Up Location"></asp:Label>
                                                        </td>
                                                        <td>{{y.pickuplocationaddress}}</td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Drop Location"></asp:Label>
                                                        </td>
                                                        <td>{{y.droplocation}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Text="Seat Name"></asp:Label>
                                                        </td>
                                                        <td>{{y.seatname}}</td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="travelername"></asp:Label>
                                                        </td>
                                                        <td>Rs. {{y.travelername}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="status"></asp:Label>
                                                        </td>
                                                        <td>Rs. {{y.bookingstatus}}</td>
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
