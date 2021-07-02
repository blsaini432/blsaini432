<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="Viewallmember_old1.aspx.cs" Inherits="Root_Administrator_Viewallmember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <script>
        var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
        app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {
          
            $scope.fillList = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Administrator/Viewallmember.aspx/BindCustomers',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}

                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.Employees = response.d;
                }, function (response) {
                    $("#loader").hide();
                });
            };
            $scope.fillList();
            $("#loader").hide();

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

            //read data by id start

            $scope.Read = function (id) {
                //alert("test");
                ReadRecord(id);
            }
            function ReadRecord(id) {
                //alert("n");
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/BindCustomersByMsrno',
                    method: "POST",
                    data: { msrno: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.fulldetails = response.d;
                })
            };

            //read data by id end


            //read kyc 

            $scope.ReadKyc = function (id) {
                //alert("test");
                ReadKycRecord(id);
            }
            function ReadKycRecord(id) {
                //alert("n");
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/BindKYCByMsrno',
                    method: "POST",
                    data: { msrno: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.kycdetails = response.d;
                })
            };
            //read kyc
            //services start

            //Active DMR Start
            $scope.ServiceDMR = function (id, actions) {

                ReadService(id, actions);
            }
            function ReadService(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updatedmr',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };

            $scope.ServiceCKYC = function (id, actions) {

                ReadServicekyc(id, actions);
            }
            function ReadServicekyc(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updatecentralkyc',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };


            $scope.InstantServiceDMR = function (id, actions) {

                ReadInstantService(id, actions);
            }
            function ReadInstantService(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updateinstantdmr',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };






            $scope.Servicepayout = function (id, actions) {

                ReadInstantServicexp(id, actions);
            }
            function ReadInstantServicexp(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updatexpresspayout',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };

            //Active DMR End

            //Active DMR Start
            $scope.ServiceRecharge = function (id, actions) {

                ReadrechargeService(id, actions);
            }
            function ReadrechargeService(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updaterecharge',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };
            //Active DMR End

            //Active DMR Start
            $scope.Serviceaccount = function (id, actions) {

                Readaccountservice(id, actions);
            }
            function Readaccountservice(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updateaccount',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };
            //Active DMR End

            ///Active AEPS Start

            $scope.ServiceAEPS = function (id, actions) {
                ReadServiceaeps(id, actions);
            }
            function ReadServiceaeps(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updateaeps',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };



            $scope.ServiceAEPSPayout = function (id, actions) {
                ReadServiceaepspayout(id, actions);
            }
            function ReadServiceaepspayout(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updateaepspayout',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };

            $scope.ServiceUTI = function (id, actions) {
                ReadServiceuti(id, actions);
            }
            function ReadServiceuti(id, actions) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updateuti',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };
            //Active AEPS End


            //services end










            //update bank

            $scope.ServiceBank = function (id) {

                ReadServiceBank(id);
            }
            function ReadServiceBank(id) {
                var ID = id;
                $http({
                    url: '../Administrator/Viewallmember.aspx/updatebankdetails',
                    method: "POST",
                    data: { msrno: id, bankac: $scope.bankac, bankname: $scope.bankname, bankifsc: $scope.bankifsc }
                }).success(function (response) {
                    showSwal('success-message');
                })
            };



            //update bank
        

        });

    </script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Member List
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
                            <div class="info-box">
                                              <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                             <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Export" CssClass="btn btn-dribbble" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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
                                    <table id="example1" class="table table-bordered table-hover" class="jsgrid-table">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID
                                                </th>
                                                <th>MemberName
                                                </th>
                                                <th>State Name</th>
                                                <th>Mobile
                                                </th>
                                                <th>Package
                                                </th>
                                                <th>MemberType
                                                </th>
                                                <th>Owner
                                                </th>
                                                <th>Detail
                                                </th>
                                                <th>Account Status</th>
                                                <th>Recharge
                                                </th>
                                                <th>Xpress DMR
                                                </th>
                                                <th>DMR</th>
                                                <th>AEPS
                                                </th>
                                                <th>Payout</th>
                                                <th>UTI</th>
                                               <th>Xpress Payout</th>
                                                <th>Video KYC</th>
                                                <th>Bank Detail</th>
                                            </tr>
                                            <tr dir-paginate="x in Employees|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                 <td>{{x.StateName}}</td>
                                                <td>{{x.Mobile}}</td>
                                                <td>{{x.Package}}</td>
                                                <td>{{x.MemberType}}</td>
                                                <td>{{x.Owner}}</td>                    
                                                <td><a href="#openModal" ng-click="Read(x.MsrNo)" class="badge badge-success badge-pill" title="click to view member detail">View</a></td>
                                                <td><a href="#" ng-click="Serviceaccount(x.MsrNo,x.status)" class="badge badge-danger badge-pill">{{x.status}}</a></td>
                                               <%-- <td><a href="#" ng-click="ServiceRecharge(x.MsrNo,x.isrecharge)" class="badge badge-danger badge-pill">{{x.isrecharge}}</a></td>
                                                  <td><a href="#" ng-click="ServiceUTI(x.MsrNo,x.isuti)" class="badge badge-danger badge-pill">{{x.isuti}}</a></td>--%>
                                                <td><a href="#" ng-click="ServiceRecharge(x.MsrNo,x.isrecharge)" class="badge badge-danger badge-pill">{{x.isrecharge}}</a></td>
                                                <td><a href="#" ng-click="ServiceDMR(x.MsrNo,x.isdmr)" class="badge badge-danger badge-pill">{{x.isdmr}}</a></td>
                                                <td><a href="#" ng-click="InstantServiceDMR(x.MsrNo,x.isemailverify)" class="badge badge-danger badge-pill">{{x.isemailverify}}</a></td>
                                                <td><a href="#" ng-click="ServiceAEPS(x.MsrNo,x.isaeps)" class="badge badge-danger badge-pill">{{x.isaeps}}</a></td>
                                                <td><a href="#" ng-click="ServiceAEPSPayout(x.MsrNo,x.isaepspayout)" class="badge badge-danger badge-pill">{{x.isaepspayout}}</a></td>
                                                <td><a href="#" ng-click="ServiceUTI(x.MsrNo,x.isuti)" class="badge badge-danger badge-pill">{{x.isuti}}</a></td>
                                               <td><a href="#" ng-click="Servicepayout(x.MsrNo,x.isxpresspayout)" class="badge badge-danger badge-pill">{{x.isxpresspayout}}</a></td>
                                                 <td><a href="#" ng-click="ServiceCKYC(x.MsrNo,x.CentralKYC1)" class="badge badge-danger badge-pill">{{x.CentralKYC1}}</a></td>
                                                
                                                 <td>
                                                    <div ng-if="x.CentralKYC!=='ViewKYC'">
                                                       <a href="#"  class="badge badge-danger badge-pill">{{x.CentralKYC}}</a>
                                                    </div>
                                                    <div ng-if="x.CentralKYC=='ViewKYC'">
                                                      <a href="#openModalvideokyc" ng-click="ReadKyc(x.MsrNo)" class="badge badge-success badge-pill">{{x.CentralKYC}}</a>
                                                    
                                                    </div>
                                                </td>

                                                <td><a href="#openModalbank" ng-click="Read(x.MsrNo)" class="badge badge-success badge-pill" title="click to view member detail">View BankDetail</a></td>
                                                <td><a href="ManageMemberMaster.aspx?Id={{x.MsrNo}}" class="badge badge-danger badge-pill" target="_blank">Edit</a></td>
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

                               <div id="openModalvideokyc" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row" ng-repeat="y in kycdetails" style="overflow-y:scroll;overflow-x:scroll;height:500px;"> 
                                            <div class="col-md-6">
                                                <div class="info-box">
                                                    <h5 style="text-align: center;">KYC Info</h5>
                                                    <hr>
                                                    <div class="box-body">
                                                     <table style="width:500px;height:500px;">
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr >
                <td align="center">
                    <asp:Image ID="Image1" runat="server" Height="149px" Width="151px" ImageUrl="{{y.orignalpic}}"   />
                </td>
                <td>=</td>
                <td align="center">
                    <asp:Image ID="Image2" runat="server" Height="149px" Width="151px" ImageUrl="{{y.livepic}}" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbldocumentnumber2" runat="server" Font-Bold="True">Document Image </asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align="center">
                    <asp:Label ID="lbldocumentnumber3" runat="server" Font-Bold="True">Live Image </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lbldocumentnumber4" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#009933">Success</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" Height="250px" Width="360px" ImageUrl="{{y.kycimage1}}"  />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Image ID="Image4" runat="server" Height="250px" Width="360px" ImageUrl="{{y.kycimage2}}" />
                </td>
            </tr>
                                                         <tr>
                                                             <td colspan="3">
                                                             <table class="table table-bordered" style="width:100%;">
                                                               <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbldocumentnumber19" runat="server" Font-Bold="True" ForeColor="#009933">Orignal KYC Info</asp:Label>
                </td>
                <td class="auto-style1" colspan="2">
                    <asp:Label ID="lbldocumentnumber20" runat="server" Font-Bold="True" ForeColor="#009933">Declared KYC Info</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldocumentnumber5" runat="server" Font-Bold="True">Document Number </asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbldocumentnumber6" runat="server" Font-Bold="True">Document Number </asp:Label>
                &nbsp;</td>
            </tr>
            <tr>
                <td>
                   {{y.O_documentnumber}}
                </td>
                <td colspan="2">
                  {{y.D_documentnumber}}
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbldocumentnumber7" runat="server" Font-Bold="True">Name </asp:Label>
                </td>
                <td class="auto-style1" colspan="2">
                    <asp:Label ID="lbldocumentnumber8" runat="server" Font-Bold="True"> Name</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                      {{y.O_Name}}
                </td>
                <td class="auto-style1" colspan="2">
                     {{y.D_Name}}
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbldocumentnumber9" runat="server" Font-Bold="True">Father Name </asp:Label>
                </td>
                <td class="auto-style1" colspan="2">
                    <asp:Label ID="lbldocumentnumber10" runat="server" Font-Bold="True">Father Name </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                     {{y.O_FatherName}}
                </td>
                <td class="auto-style1" colspan="2">
                    {{y.D_FatherName}}
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldocumentnumber11" runat="server" Font-Bold="True">DOB</asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbldocumentnumber12" runat="server" Font-Bold="True">DOB</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    {{y.O_DOB}}
                </td>
                <td colspan="2">
                     {{y.D_DOB}}
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldocumentnumber13" runat="server" Font-Bold="True">Address</asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbldocumentnumber14" runat="server" Font-Bold="True">Address</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    {{y.Address}}
                </td>
                <td colspan="2" class="auto-style1">
                   {{y.address1}}
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldocumentnumber15" runat="server" Font-Bold="True">Mobile</asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbldocumentnumber16" runat="server" Font-Bold="True">Mobile</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                      {{y.Mobile}}
                </td>
                <td colspan="2">
                     {{y.mobile1}}
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldocumentnumber17" runat="server" Font-Bold="True">Gender</asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbldocumentnumber18" runat="server" Font-Bold="True">Email</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                   {{y.Gender}}
                </td>
                <td colspan="2">
                    {{y.Email}}
                </td>
            </tr>
        </table>
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
                            

















                            <div id="openModal" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row" ng-repeat="y in fulldetails">
                                            <div class="col-md-6">
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
                            <%--Modal Popup Code end--%>
                            <div id="openModalbank" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="card">
                                            <div class="card-body" ng-repeat="y in fulldetails">
                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label class="col-form-label">Bank Name<code>*</code></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <label class="col-form-label">{{y.bankname}}</label>


                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label class="col-form-label">IFSC Code<code>*</code></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <label class="col-form-label">{{y.bankifsc}}</label>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label class="col-form-label">Account Number<code>*</code></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <label class="col-form-label">{{y.bankac}}</label>
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

