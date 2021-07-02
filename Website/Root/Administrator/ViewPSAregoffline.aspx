<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewPSAregoffline.aspx.cs" Inherits="Portals_Admin_ViewPSAregoffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
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
            //PSARegRequest
            $scope.fillpsareport = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/ViewPSAregoffline.aspx/fillpsareport',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.psareport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.fillpsareport();
            $("#loader").hide();
            //
            $scope.fillpsareportbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/ViewPSAregoffline.aspx/fillpsareportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.psareport = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();

            //PSARegRequest

            //ApprovePSARequest
            $scope.ApprovePSARequest = function (id) {

                ApproveBRequest(id);
            }
            function ApproveBRequest(id) {
                //debugger;
                //var ID = id;
                //var inputVal = $scope.Name;
                //var inputVal = $(event.target).closest('tr').find('#myInput').val();
                $http({
                    url: '../Administrator/ViewPSAregoffline.aspx/ApprovePSARequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { memberid: id }
                }).success(function (response) {
                    $scope.fundimage = response.d;

                })
            };
            //End
            //RejectPSARequest
            $scope.RejectPSARequest = function (id) {

                RejectBRequest(id);
            }
            function RejectBRequest(id) {
                var ID = id;
                var inputValmyInputrejection = $(event.target).closest('tr').find('#myInputrejection').val();
                $http({
                    url: '../Administrator/ViewPSAregoffline.aspx/RejectPSARequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id, rejection: inputValmyInputrejection }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillpsareport();
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
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">UTI Registrtion Process
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
                                            <a href="#" ng-click="fillpsareportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
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
                                                <th>Name
                                                </th>
                                                <th>Email
                                                </th>
                                                <th>Mobile
                                                </th>
                                                <th>Status
                                                </th>
                                               
                                                <th>PSA LoginID</th>
                                                <th>PAN
                                                </th>
                                                <th>Aadhar
                                                </th>
                                            </tr>
                                            <tr dir-paginate="x in psareport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.Name}}</td>
                                                <td>{{x.Email}}</td>
                                                <td>{{x.Contact_Number}}</td>
                                                <td>{{x.Statu}}</td>
                                                
                                                <td>{{x.PsaLoginId}}
                                                </td>
                                                <td>
                                                    <img alt="{{m.Iden_Proof_Filename}}" ng-src="../../Uploads/User/UTI/{{x.Iden_Proof_Filename}}" style="height: 100px; width: 100px" />
                                                    <a href="../../Uploads/User/UTI/{{x.Iden_Proof_Filename}}" target="_blank">Download</a>
                                                </td>
                                                <td>
                                                    <img alt="{{m.Addr_Proof_Filename}}" ng-src="../../Uploads/User/UTI/{{x.Addr_Proof_Filename}}" style="height: 100px; width: 100px" />
                                                    <a href="../../Uploads/User/UTI/{{x.Addr_Proof_Filename}}" target="_blank">Download</a>
                                                </td>
                                                 <td>
                                                    <div ng-if="x.Statu!=='Pending'">
                                                        {{x.Status}}
                                                    </div>
                                                    <div ng-if="x.Statu==='Pending'">
                                                        <a href="#openModalstatus" ng-click="ApprovePSARequest(x.MemberID)" class="badge badge-danger badge-pill">Update</a>
                                                    </div>
                                                </td>
                                                <%--  <td><a href="{{x.Iden_Proof_Filename}}" target="_blank">Download</a></td>
                                                 <td><a href="{{x.Addr_Proof_Filename}}" target="_blank">Download</a></td>
                                                --%>
                                            </tr>
                                            <tr>

                                                <td ng-show="psareport.length==0" colspan="12">
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


                             <div id="openModalstatus" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <table class="table table-responsive ps--scrolling-y">
                                                    
                                                      <tr>
                                                        <td>Psaid</td>
                                                        <td>
                                                          
                                                           <asp:TextBox ID="txt_psaid" runat="server" TextMode="MultiLine"></asp:TextBox>
                               
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                         <td> Remark</td>
                                                        <td>
                                                             <asp:TextBox ID="txt_Reject" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Success" OnClick="btnSubmit_Click" />
                                                             <asp:Button ID="btnreject" runat="server" Visible="true" Text="Failed" OnClick="btnSubmit_Reject" />
                                                        </td>
                                                    </tr>



                                                </table>
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









