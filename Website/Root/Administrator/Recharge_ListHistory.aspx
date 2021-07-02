<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Recharge_ListHistory.aspx.cs" Inherits="Root_Admin_ListHistory" %>

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
       
        //Recharge
            $scope.fillrechargereport = function () {
                $("#loader").show();
            var httpreq = {
                method: 'POST',
                url: '../Administrator/Recharge_ListHistory.aspx/fillrechargereport',
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
                url: '../Administrator/Recharge_ListHistory.aspx/fillrechargereportbydate',
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
            <h3 class="page-title">Recharge History
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
                                                <th>Source</th>
                                                <th>MemberID</th>
                                                <th>Member Name</th>
                                                <th>MobileNo</th>
                                                <th>caNumber</th>
                                                <th>RechargeAmount</th>
                                                <th>OperatorName</th>
                                                <th>ServiceType</th>
                                                <th>TransID</th>
                                                <th>Status</th>
                                                <th>APIID</th>
                                                <th>APIMessage</th>
                                                <th>ErrorMsg</th>
                                                <th>AddDate</th>
                                           <%--     <th>Receipt</th>
                                                <th>View</th>--%>

                                            </tr>
                                            <tr ng-show="RechargeReport.length!=0" dir-paginate="x in RechargeReport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.Source}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                <td>{{x.MobileNo}}</td>
                                                <td>{{x.caNumber}}</td>
                                                <td>{{x.RechargeAmount}}</td>
                                                <td>{{x.OperatorName}}</td>
                                                <td>{{x.ServiceType}}</td>
                                                <td>{{x.TransID}}</td>
                                                <td>{{x.Status}}</td>
                                                <td>{{x.APIID}}</td>
                                                <td>{{x.APIMessage}}</td>
                                                <td>{{x.ErrorMsg}}</td>
                                                <td>{{x.AddDate}}</td>
                                         <%--       <td><a href="{{x.ReceiptLink}}">Receipt</a></td>--%>
                                               <%-- <td><a href="#openModal" ng-click="Read(x.agent_id)" class="badge badge-success badge-pill" title="click to view member detail">Genrate Refund </a></td>--%>
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

                            <div id="openModal" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row" ng-repeat="y in rceiptdetails">
                                            <div class="col-md-6">
                                                <table style="border: 1px solid Black; margin: 10px auto;" width="700px" cellspacing="10"
                                                    class="inv_1">
                                                    <tr>
                                                        <td colspan="2" align="center" style="font-size: 18px; font-weight: bold">RECEIPT
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>RECEIPT # :</b> R -
                                <%#Eval("HistoryID")%>
                                                        </td>
                                                        <td align="right">
                                                            <b>Date :</b>
                                                            <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <hr />
                                                            <b>TRANSACTION DETAILS</b>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Name</b>
                                                        </td>
                                                        <td style="text-transform: capitalize">
                                                          {{y.MemberName}}
                                                          
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Service Provider</b>
                                                        </td>
                                                        <td>
                                                             {{y.OperatorName}}
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Service Number</b>
                                                        </td>
                                                        <td>
                                                             {{y.MobileNo}}
                                                          [Account Number:{{y.caNumber}}]
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Transaction ID</b>
                                                        </td>
                                                        <td>
                                                            {{y.TransID}}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Operator Reference Number</b>
                                                        </td>
                                                        <td>
                                                             {{y.APITransID}}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Time</b>
                                                        </td>
                                                        <td>
                                                            {{y.AddDate}}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Recharge Amount</b>
                                                        </td>
                                                        <td>Rs.
                                                                {{y.RechargeAmount}}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Surcharge</b>
                                                        </td>
                                                        <td>Rs.
                                                             <%#Eval("CoupanAmount")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 16px">
                                                        <td>
                                                            <b>Net Amount</b>
                                                        </td>
                                                        <td>Rs.
                                                              <%#Eval("RechargeAmount")%>
                                                        </td>
                                                    </tr>
                                                </table>







                                                <div class="info-box">
                                                    <h5 style="text-align: center;">Profile Info</h5>
                                                    <hr>
                                                    <div class="box-body">
                                                        <h6 style="color: black;"><i class="fa fa-map-marker margin-r-5"></i>State , City</h6>
                                                        <p class="text-muted">{{y.StateName}},{{y.CityName}}</p>
                                                        <hr>
                                                        <h6 style="color: black;"><i class="fa fa-envelope margin-r-5"></i>Email address </h6>
                                                        <p class="text-muted">{{y.Email}}</p>
                                                        <hr>
                                                        <h6 style="color: black;"><i class="fa fa-map-marker margin-r-5"></i>Address</h6>
                                                        <p>{{y.Address}}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="info-box">
                                                    <h5 style="text-align: center;">Login Info</h5>
                                                    <hr>
                                                    <div class="box-body">
                                                        <h6 style="color: black;"><i class="fa fa-book margin-r-5"></i>Member ID</h6>
                                                        <p class="text-muted">{{y.MemberID}} </p>
                                                        <hr>
                                                        <h6 style="color: black;"><i class="fa fa-key margin-r-5"></i>Password</h6>
                                                        <p class="text-muted">{{y.Password}}</p>
                                                        <hr>
                                                        <h6 style="color: black;"><i class="fa fa-key margin-r-5"></i>Transaction Password</h6>
                                                        <p class="text-muted">{{y.TransactionPassword}}</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
