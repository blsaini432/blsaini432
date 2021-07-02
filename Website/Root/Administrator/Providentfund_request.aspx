<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/Adminmaster.master" AutoEventWireup="true"
    CodeFile="Providentfund_request.aspx.cs" Inherits="Portal_Admin_Voteridrequest" %>

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
                    url: '../Administrator/Providentfund_request.aspx/fillposrequest',
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
                    url: '../Administrator/Providentfund_request.aspx/fillposrequestbydate',
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
                    url: '../Administrator/Providentfund_request.aspx/ApproveRequest',
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
            $scope.clentdata = function (id) {

                clentdata(id);
            }
            function clentdata(id) {
                var ID = id;
                $http({
                    url: '../Administrator/Providentfund_request.aspx/clientdata',
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




            //RejectFundRequest

            $scope.RejectFundRequest = function (id) {

                RejectRequest(id);
            }
            function RejectRequest(id) {
                var ID = id;
                $http({
                    url: '../Administrator/Providentfund_request.aspx/RejectRequest',
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
                    url: '../Administrator/Providentfund_request.aspx/ShowFundImage',
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
            <h3 class="page-title">Provident fund  Request List
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
                                                <th>member email
                                                </th>
                                                <th>member mobile
                                                </th>
                                                <th> mobile
                                                </th>
                                               <th>Txnid</th>
                                                <th>Adhar number
                                                </th>
                                                <th>pan number</th>
                                                <th>Add Date</th>
                                                <th>status</th>
                                                <th>client data</th>
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
                                                <td>{{x.agentmobile}}</td>
                                                <td>{{x.txnid}}</td>
                                                <td>{{x.adharnumber}}</td>
                                                <td>{{x.pannumber}}</td>
                                                <td>{{x.AddDate}}</td>
                                                <td>{{x.status}}</td>
                                                <td>
                                                    <a href="#openModaldata" ng-click="clentdata(x.txnid)" class="badge badge-primary badge-pill">data</a>
                                                </td>
                                                <td><a href="#openModalImage" ng-click="ShowFundImage(x.txnid)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                                <td>{{x.Remark}}</td>
                                                <td>
                                                    <div ng-if="x.status==='failed'">
                                                        <a href="#"  class="badge badge-danger badge-pill">REJECTED</a>
                                                    </div>
                                                    <div ng-if="x.status==='pending'">
                                                        <a href="#openModalstatus" ng-click="ApproveFundRequest(x.txnid)" class="badge badge-primary badge-pill">Status Update</a>
                                                    </div>
                                                    <div ng-if="x.status==='Success'">
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
                                                        <label style="color:green">Pan </label>
                                                        <a href="../Upload/PanCardRequest/Actuall/{{y.photo}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.photo}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                 
                                                    <div class="col-lg-3">
                                                         <label style="color:green">passbook </label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.pancard}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.pancard}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">aadharcard</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.ageProof}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.ageProof}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">file1</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file}}" alt="click" height="200" width="200" />
                                                    </div>
                                                     
                                                </div>
                                                <div class="form-group row">
                                                   
                                                    <div class="col-lg-4">
                                                         <label style="color:green">file2</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file2}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file2}}" alt="click" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-4">
                                                         <label style="color:green">file3</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file3}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file3}}" alt="click" height="200" width="200" />
                                                    </div>
                                                     <div class="col-lg-4">
                                                          <label style="color:green">file4</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file4}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file4}}" alt="click" height="200" width="200" />
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
                                                        <td>Remark</td>
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
                                                             <asp:Button ID="btnreject" runat="server" Visible="true" Text="Failed" OnClick="btnSubmit_Reject" />
                                                        </td>
                                                    </tr>



                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        
                           <div id="openModaldata" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fundimage">
                                                <table class="table table-responsive ps--scrolling-y">
                                                    
                                                   
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btn" runat="server" Visible="true" Text="Client Data click here " OnClick="btn_export_Clickdata" />
                                                             
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
