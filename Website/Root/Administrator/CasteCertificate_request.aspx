<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="CasteCertificate_request.aspx.cs" Inherits="Root_Admin_CasteCertificate_request" %>

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
            //BBPStransactonReport
            $scope.fillbbpsnewreport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/CasteCertificate_request.aspx/pancardreport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.bbpsnewreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.fillbbpsnewreport();
            $("#loader").hide();
            //

            $scope.fillbbpsnewreportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/CasteCertificate_request.aspx/pancardreporttbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.bbpsnewreport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();


            //ApproveBankRequest

            $scope.ApproveRequest = function (id) {

                ApproveBRequest(id);
            }
            function ApproveBRequest(id) {
                debugger;
                var ID = id;
                var inputVal = $scope.Name;
                var inputVal = $(event.target).closest('tr').find('#myInpust').val();
                $http({
                    url: '../Administrator/CasteCertificate_request.aspx/ApproveRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillbbpsnewreport();

                })
            };

            //End

            //RejectBankRequest

            $scope.RejectRequest = function (id) {

                RejectBRequest(id);
            }
            function RejectBRequest(id) {
                var ID = id;
                $http({
                    url: '../Administrator/CasteCertificate_request.aspx/RejectRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillbbpsnewreport();

                }
            )
            };

            //End

        });
        function getdata() {
            debugger;
            var fromdate = document.getElementById('<%=txt_fromdate.ClientID %>').value;
            var todate = document.getElementById('<%=txttodate.ClientID %>').value;
              document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
              document.getElementById('<%=hdntodate.ClientID %>').value = todate
          }

          //BBPStransactonReport
    </script>
    <style>
        .input {
            float: left;
            width: 87%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Caste Certificate Transactions
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
                                            <a href="#" ng-click="fillbbpsnewreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
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
                                                <th>CustomerName
                                                </th>
                                                <th>Father Name
                                                </th>
                                                <th>Mobile</th>
                                                <th>Email</th>
                                                <th>Status</th>
                                                <th>aadhar</th>
                                                  <th>Photo</th>
                                                 <th>paydate</th>
                                                 <th>Status Update</th>
                                            </tr>

                                            <tr dir-paginate="x in bbpsnewreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.customername}}</td>
                                                <td>{{x.fathername}}</td>
                                                <td>{{x.customermobile}}</td>
                                                <td>{{x.email}}</td>
                                                <td>{{x.Status}}</td>
                                                 <td>
                                                 <a href="../../Uploads/Servicesimage/pan/{{x.aadhar}}"  target="_blank"><img alt="{{x.aadhar}}" ng-src="../../Uploads/Servicesimage/pan/{{x.aadhar}}"  style="height:100px;width:100px" /></a> 
                                                
                                              </td>
                                                 <td>
                                                  <a href="../../Uploads/Servicesimage/pan/{{x.photo}}"  target="_blank"><img alt="{{x.aadhar}}" ng-src="../../Uploads/Servicesimage/pan/{{x.photo}}"  style="height:100px;width:100px" /></a> 
                                                  
                                              </td>
                                                <td>{{x.paydate}}</td>
                                                  <td>
                                                     <div ng-if="x.Status==='Pending'">
                                                        <a href="#" ng-click="ApproveRequest(x.txnid)" class="badge badge-success badge-pill">Approve</a>
                                                    </div>
                                                    <div ng-if="x.Status==='Pending'">
                                                        <a href="#" ng-click="RejectRequest(x.txnid)" class="badge badge-danger badge-pill">Reject</a>
                                                    </div>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td ng-show="bbpsnewreport.length==0" colspan="12">
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

