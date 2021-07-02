<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="ListMemberMaster.aspx.cs" Inherits="Root_Distributor_ListMemberMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>

    <script src="../Angularjsapp/dirPagination.js"></script>
    <script>
        var app = angular.module("distributorApp", ['angularUtils.directives.dirPagination']);
        app.controller("distributorCntrl", function ($scope, $http, $timeout, $filter) {

            $scope.fillList = function () {
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/ListMemberMaster.aspx/BindCustomers',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}

                }
                $http(httpreq).success(function (response) {
                    $scope.Employees = response.d;
                })
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
            //mydashboard
            $scope.fillrtdashboard = function () {
                $("#loader").show();
                var httpreq = {
                    method: 'POST',
                    url: '../Distributor/Dashboard.aspx/Bindmemberdata',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: {}
                }
                $http(httpreq).success(function (response) {
                    $("#loader").hide();
                    $scope.rtdashboard = response.d;
                }, function (response) {
                    $("#loader").hide();
                });

                $timeout(function () {
                    $scope.fillrtdashboard();
                }, 10000)
            };
            $scope.fillrtdashboard();
            $("#loader").hide();
            //mydashboard

            //active
            $scope.ServiceAEPSPayout = function (id, actions) {
                ReadServiceaepspayout(id, actions);
            }
            function ReadServiceaepspayout(id, actions) {
                var ID = id;
                $http({
                    url: '../Distributor/ListMemberMaster.aspx/updateservice',
                    method: "POST",
                    data: { msrno: id, action: actions }
                }).success(function (response) {
                    showSwal('success-message');
                    $scope.fillList();

                })
            };
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Downline Member List
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="distributorApp" ng-controller="distributorCntrl">
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
                                    <table id="example1" class="table table-bordered table-hover" class="jsgrid-table">
                                        <thead>
                                            <tr>
                                                <th>S.N.
                                                </th>
                                                <th>MemberID
                                                </th>
                                                <th>MemberName
                                                </th>
                                                <th>Mobile
                                                </th>
                                                <th>Package
                                                </th>
                                                <th>MemberType
                                                </th>
                                                <th>Owner
                                                </th>
                                                <th>Joining Date
                                                </th>
                                                <th>active/Deactive</th>
                                            </tr>
                                            <tr dir-paginate="x in Employees|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                <td>{{x.Mobile}}</td>
                                                <td>{{x.Package}}</td>
                                                <td>{{x.MemberType}}</td>
                                                <td>{{x.Owner}}</td>
                                                <td>{{x.joiningdate}}</td>
                                                 <td>
                                                    <div ng-if="x.parid==='2'">
                                                       <a href="#" ng-click="ServiceAEPSPayout(x.Msrno,x.IsActive)" class="badge badge-success badge-pill">{{x.IsActive}}</a>
                                                    </div>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td ng-show="Employees.length==0" colspan="10">
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

