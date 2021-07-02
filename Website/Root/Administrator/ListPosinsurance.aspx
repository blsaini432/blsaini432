<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListPosinsurance.aspx.cs" Inherits="Root_Admin_ListPosinsurance" %>

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


            //FundRequest
            $scope.fillfundrequest = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/ListPosinsurance.aspx/fillposrequest',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.listfundrequest = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.fillfundrequest();
            $("#loader").hide();
            //

            $scope.fillfundrequestbydate = function (fdate, tdate) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/ListPosinsurance.aspx/fillposrequestbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate }
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.listfundrequest = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $("#loader").hide();
            //FundRequest

            //ApproveFundRequest

            $scope.ApproveFundRequest = function (id) {

                ApproveRequest(id);
            }
            function ApproveRequest(id) {
                var ID = id;
                $http({
                    url: '../Administrator/ListPosinsurance.aspx/ApproveRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { msrno: id }
                }).success(function (response) {
                    $scope.fundimage = response.d;

                })
            };

            //End

            //RejectFundRequest

            $scope.RejectFundRequest = function (id) {

                RejectRequest(id);
            }
            function RejectRequest(id) {
                var ID = id;
                $http({
                    url: '../Administrator/ListPosinsurance.aspx/RejectRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillfundrequest();

                })
            };

            //End


            //RejectFundRequest

            $scope.ShowFundImage = function (id) {

                FundImage(id);
            }
            function FundImage(id) {
                var ID = id;
                $http({
                    url: '../Administrator/ListPosinsurance.aspx/ShowFundImage',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fundid: id }
                }).success(function (response) {
                    $scope.fundimage = response.d;

                })
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
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Pos Registration Request List
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
                                            <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" MaxLength="50" ng-model="to_date" CssClass="form-control" autcomplete="off" Enabled="false"></asp:TextBox>
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
                                            <a href="#" ng-click="fillfundrequestbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
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
                                                <th>Membername
                                                </th>
                                                <th>email
                                                </th>
                                                <th>mobile
                                                </th>
                                                <th>Adhar number
                                                </th>
                                                <th>pan number</th>
                                                <th>Add Date</th>
                                                <th>status</th>
                                                <th>Document View</th>
                                                <th>AdminRemark
                                                </th>
                                                <th>Update</th>
                                            </tr>
                                            <tr dir-paginate="x in listfundrequest|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.Membername}}</td>
                                                <td>{{x.email}}</td>
                                                <td>{{x.mobile}}</td>
                                                <td>{{x.adharnumber}}</td>
                                                <td>{{x.pannumber}}</td>
                                                <td>{{x.AddDate}}</td>
                                                <td>{{x.status}}</td>
                                                <td><a href="#openModalImage" ng-click="ShowFundImage(x.Msrno)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                                <td>{{x.Remark}}</td>
                                                <td>
                                                    <div ng-if="x.status==='REJECTED'">
                                                        <a href="#"  class="badge badge-danger badge-pill">REJECTED</a>
                                                    </div>
                                                    <div ng-if="x.status==='Pending'">
                                                        <a href="#openModalstatus" ng-click="ApproveFundRequest(x.Msrno)" class="badge badge-primary badge-pill">Status Update</a>
                                                    </div>
                                                     <div ng-if="x.status==='DOCUMENT OK SENT FOR REGISTRATION'">
                                                        <a href="#openModalstatus" ng-click="ApproveFundRequest(x.Msrno)" class="badge badge-info badge-pill">Status Update</a>
                                                    </div>
                                                    <div ng-if="x.status==='APPROVED'">
                                                        <a href="#"  class="badge badge-success badge-pill">APPROVED</a>
                                                    </div>
                                                </td>


                                            </tr>
                                            <tr>
                                                <td ng-show="listfundrequest.length==0" colspan="13">
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


                            <div id="openModalImage" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label style="color:green">Aadhar font</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.aadharfont}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.aadharfont}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">Aadhar Back</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.adharback}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.adharback}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">Pan Card</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.pancard}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.pancard}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                     <div class="col-lg-3">
                                                          <label style="color:green">Marksheet/ </label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.marksheet}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.marksheet}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                   
                                                    <div class="col-lg-4">
                                                         <label style="color:green">Photo</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.photo}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.photo}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-4">
                                                         <label style="color:green">Passbook/Cancel chq /</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.passbook}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.passbook}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                     <div class="col-lg-4">
                                                          <label style="color:green">Noc</label>
                                                        <a href="../../Uploads/servicesimage/Actual/{{y.noc}}" target="_blank" />
                                                        <img src="../../Uploads/servicesimage/Actual/{{y.noc}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
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
                                                        <td>
                                                            <label>select</label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="droplist" runat="server">
                                                                <asp:ListItem>Please select</asp:ListItem>
                                                                <asp:ListItem>REJECTED</asp:ListItem>
                                                                <asp:ListItem>DOCUMENT OK SENT FOR REGISTRATION</asp:ListItem>
                                                                <asp:ListItem>APPROVED</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td>Remark</td>
                                                        <td>
                                                          
                                                           <asp:TextBox ID="txt_status" runat="server" TextMode="MultiLine"></asp:TextBox>
                               
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Submit" OnClick="btnSubmit_Click" />
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
