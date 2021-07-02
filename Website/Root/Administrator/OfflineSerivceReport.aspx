<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="OfflineSerivceReport.aspx.cs" Inherits="Root_Administrator_OfflineSerivceReport" %>

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
        var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
        app.controller("myCntrl", function ($scope, $http, $timeout) {

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
            $scope.fillofflinereport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/OfflineSerivceReport.aspx/fillofflinereport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();

                    $scope.OfflineReport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });

            };
            $scope.fillofflinereport();
            $("#loader").hide();
            //

            $scope.fillofflinereportbydate = function (fdate, tdate) {
                $("#loader").show();

                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/OfflineSerivceReport.aspx/fillofflinereportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.OfflineReport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            //Recharge


            //Show RechargeReceipt

            //read data by id start

            $scope.Readofflinereceipt = function (id) {

                offlinereceipt(id);
            }
            function offlinereceipt(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Administrator/OfflineSerivceReport.aspx/loadofflinereceipt',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.offlinereceipt = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.ShowDocs = function (id) {

                ShowDocsa(id);
            }
            function ShowDocsa(id) {
                $("#loader").show();
                var ID = id;
                $http({
                    url: '../Administrator/OfflineSerivceReport.aspx/ShowDocs',
                    method: "POST",
                    data: { txnid: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.offlinereceipts = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };


            //PSARegRequest

            //ApprovePSARequest
            $scope.ApproveRequest = function (id) {

                ApproveBRequest(id);
            }
            function ApproveBRequest(id) {
                debugger;
                var ID = id;
                var inputVal = $scope.Name;
                var inputVal = $(event.target).closest('tr').find('#myInput').val();
                $http({
                    url: '../Administrator/OfflineSerivceReport.aspx/ApproveRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id, psaid: inputVal }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillofflinereport();

                })
            };
            //End
            //RejectPSARequest
            $scope.RejectRequest = function (id) {

                RejectBRequest(id);
            }
            function RejectBRequest(id) {
                var ID = id;
                var inputValmyInputrejection = $(event.target).closest('tr').find('#myInput').val();
                $http({
                    url: '../Administrator/OfflineSerivceReport.aspx/RejectRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id, psaid: inputValmyInputrejection }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillofflinereport();
                }
            )
            };
            //End
            //read data by id end
            //Show RechargeReceipt

            $scope.upload = function (id) {

                uploads(id);
            }
            function uploads(id) {
                var ID = id;
                var inputValmyInputrejection = $(event.target).closest('tr').find('#myInput').val();
                $http({
                    url: '../Administrator/OfflineSerivceReport.aspx/uploads',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { id: id }
                }).success(function (response) {
                    //showSwal('success-message');
                    $scope.offlinereceipt = response.d;
                }
            )
            };
            //End
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
            <h3 class="page-title">History
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
                                            <a href="#" ng-click="fillofflinereportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>

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
                                                <th>For Service</th>
                                                <th>MemberId
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Phone
                                                </th>
                                                <th>Email
                                                </th>
                                                <th>Address
                                                </th>
                                                <th>Status
                                                </th>
                                                <th>CompanyName</th>
                                                <th>CompanyAddress</th>
                                                <th>TransactionID</th>
                                                <th>AddDate</th>


                                            </tr>
                                            <tr dir-paginate="x in OfflineReport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.ServiceName}}</td>
                                                <td>{{x.MemberId}}</td>
                                                <td>{{x.Name}}</td>
                                                <td>{{x.Phone}}</td>
                                                <td>{{x.Email}}</td>
                                                <td>{{x.Address}}</td>
                                                <td>{{x.Status}}</td>
                                                <td>{{x.CompanyName}}</td>
                                                <td>{{x.CompanyAddress}}</td>
                                                <td>{{x.TransID}}</td>
                                                <td>{{x.AddDate}}</td>
                                                <%--<td>
                                                    
                                                    <div ng-if="x.Status=='Pending'">
                                                         <a href="#openModalstatus" ng-click="upload(x.TransID)" class="badge badge-danger badge-pill">Upload</a>
                                                    </div>
                                                </td>--%>
                                                <td>
                                                    <div ng-if="x.Status!=='Pending'">
                                                        {{x.RefrenceNumber}}
                                                    </div>
                                                   <%-- <div ng-if="x.Status=='Pending'">
                                                        <input type="text" placeholder="Type Here ..." ng-model="Name" id="myInput">
                                                    </div>--%>
                                                </td>
                                                <td>
                                                    <div ng-if="x.Status!=='Pending'">
                                                        {{x.Status}}
                                                    </div>
                                                    <div ng-if="x.Status==='Pending'">
                                                        <a href="#openModalstatus" ng-click="upload(x.TransID)" class="badge badge-danger badge-pill">Approve</a>
                                                    </div>
                                                </td>

                                                <td>
                                                    <div ng-if="x.Status!=='Pending'">
                                                        {{x.Status}}
                                                    </div>
                                                    <div ng-if="x.Status==='Pending'">
                                                        <a href="#" ng-click="RejectRequest(x.TransID)" class="badge badge-danger badge-pill">Reject</a>
                                                    </div>
                                                </td>
                                                <td><a target="_blank" href="TestGrid.aspx?Ezulixtranid={{x.TransID}}" class="badge badge-success badge-pill" title="">Download Docs</a></td>

                                            </tr>
                                            <tr>
                                                <td ng-show="OfflineReport.length==0" colspan="12">
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

                         

                             <div id="openModalstatus" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in offlinereceipt">
                                                <table class="table table-responsive ps--scrolling-y">
                                                    
                                                      <tr>
                                                        <td>Type Here</td>
                                                        <td>
                                                          
                                                           <asp:TextBox ID="txt_status" runat="server" TextMode="MultiLine"></asp:TextBox>
                               
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:FileUpload ID="RECEPT" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Success" OnClick="btnSubmit_Click" />
                                                            
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

















