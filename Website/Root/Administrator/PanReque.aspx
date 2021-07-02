<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="PanReque.aspx.cs" Inherits="Root_Admin_panre" %>
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
                    url: '../Administrator/PanRequest_new.aspx/PanReport',
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

            $scope.fillreportbydate = function (fdate, tdate, ddl_stauts) {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/PanRequest_new.aspx/fillreportbydate',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { fromdate: fdate, todate: tdate, ddl_stauts: ddl_stauts }
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
                    url: '../Administrator/PanRequest_new.aspx/ApproveRequest',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { msrno: id }
                }).success(function (response) {
                    //  showSwal('success-message');
                    //  $scope.fillbbpsnewreport();
                    $scope.fundimage = response.d;

                })
            };
            //End
            //RejectBankRequest

            $scope.FinalPdf = function (ReciptImg, AddProof, RefNo, Pankid) {

                ApproveFinalPdf(ReciptImg, AddProof, RefNo, Pankid);
            }
            function ApproveFinalPdf(ReciptImg, AddProof, RefNo, Pankid) {
                debugger;
                $http({
                    url: '../Administrator/PanRequest_new.aspx/FinalPdf',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { ReciptImg: ReciptImg, AddProof: AddProof, RefNo: RefNo, Pankid: Pankid }

                }).success(function (response) {
                    //  showSwal('success-message');
                    //  $scope.fillbbpsnewreport();
                    $scope.fundimage = response.d;


                })
            };

            //End
        });
        function getdata() {
           
            var fromdate = document.getElementById('<%=txt_fromdatea.ClientID %>').value;
            var todate = document.getElementById('<%=txttodatea.ClientID %>').value;
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
            <h3 class="page-title">Pan Request
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
                                            <asp:TextBox ID="txt_fromdatea"  runat="server" ng-model="from_date" MaxLength="50"  autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnfromdate" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fromdatea"
                                                Display="Dynamic" ErrorMessage="Please Enter From Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" PopupButtonID="Image1"
                                                TargetControlID="txt_fromdatea">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txttodatea" runat="server" MaxLength="50" ng-model="to_date" autocomplete="off" CssClass="form-control" ></asp:TextBox>
                                            <asp:HiddenField ID="hdntodate" runat="server" ClientIDMode="Static" />
                                            <asp:Image ID="imgbt" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txttodatea"
                                                Display="Dynamic" ErrorMessage="Please Enter To Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" Animated="False"
                                                PopupButtonID="imgbt" TargetControlID="txttodatea">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Status<code>*</code></label>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddl_stauts" runat="server" ng-model="ddl_stauts" CssClass="form-control">
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                                <asp:ListItem Text="failed" Value="failed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <a href="#" ng-click="fillreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
                                        </div>
                                    </div>
                                </div>
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
                                                <th>Member Name
                                                </th>
                                                <th>Amount</th>
                                                <th>Txn ID
                                                </th>
                                                <th>Request On
                                                </th>
                                                <th>statuS
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>PDF</th>
                                                <td>Final PDF</td>
                                            </tr>
                                            <tr dir-paginate="x in bbpsnewreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.Member_Name}}</td>
                                                <td>{{x.trans_amt}}</td>
                                                <td>{{x.agent_id}}</td>
                                                <td>{{x.paydate}}</td>
                                                <td>{{x.statu}}</td>
                                                <td>{{x.customername}}</td>
                                                <td>
                                                    <a href="../../Uploads/Servicesimage/Actual/{{x.AddProof}}" target="_blank" class="badge badge-primary badge-pill">download</a>
                                                </td>
                                                <td>
                                                    <div ng-if="x.statu =='Success'">
                                                        <a href="#openModalpdf" ng-click="FinalPdf(x.ReciptImg,x.AddProof,x.RefNo,x.Pankid)" class="badge badge-success badge-pill">Final PDF</a>
                                                    </div>
                                                </td>
                                                <td>
                                                    <a href="#openModaldownload" ng-click="ApproveRequest(x.Pankid)"  class="badge badge-success badge-pill">Client Data</a>
                                                </td>
                                                <td>
                                                    <a href="#openModalpdf" ng-click="ApproveRequest(x.Pankid)"  class="badge badge-success badge-pill">Client Data</a>
                                                </td>
                                                <td>
                                                    <div ng-if="x.statu!=='Pending'">
                                                        {{x.statu}}
                                                    </div>
                                                    <div ng-if="x.statu==='Pending'">
                                                        <a href="#openModalstatus" ng-click="ApproveRequest(x.Pankid)" class="badge badge-success badge-pill">Update Status</a>
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
                            <div id="openModalstatus" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <table class="table table-responsive ps--scrolling-y">

                                                    <tr>
                                                        <td>Acknowledgement_No</td>
                                                        <td>

                                                            <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Remarks</td>
                                                        <td>

                                                            <asp:TextBox ID="txt_status" runat="server" TextMode="MultiLine"></asp:TextBox>

                                                        </td>

                                                    </tr>


                                                    <tr>
                                                        <td>Receipt</td>
                                                        <td>
                                                            <asp:FileUpload ID="fupRcpt" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>


                                                            <asp:Button ID="btnSuccess" runat="server" Visible="true" Text="Approve" OnClick="btnSubmit_Success" />

                                                            <asp:Button ID="btnreject" runat="server" Visible="true" Text="Reject" OnClick="btnSubmit_Reject" />
                                                        </td>
                                                    </tr>



                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div id="openModaldownload" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <table class="table table-responsive ps--scrolling-y">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Button ID="submitButton" runat="server" Visible="true" Text="ClientDataDownload" OnClick="btnSubmit_data" />--%>
                                                            <asp:Button ID="submitButton" runat="server" EnableEventValidation="false" Text="Client Data Download"  OnClick="btnSubmit_dataa" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                              <div id="openModald" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <table class="table table-responsive ps--scrolling-y">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Button ID="submitButton" runat="server" Visible="true" Text="ClientDataDownload" OnClick="btnSubmit_data" />--%>
                                                            <asp:Button ID="btnww" runat="server" EnableEventValidation="true" Text="Client Data Download"  OnClick="btnSubmit_dg" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="openModalpdf" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <t.able class="table table-responsive ps--scrolling-y">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Button ID="submitButton" runat="server" Visible="true" Text="ClientDataDownload" OnClick="btnSubmit_data" />--%>
                                                            <asp:Button ID="btn_pdf" runat="server" Text="Final Pdf "  OnClick="btnSubmit_finalpdf" />
                                                        </td>
                                                    </tr>
                                                </t.able>
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