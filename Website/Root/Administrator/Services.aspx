<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="Root_Administrator_Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
           <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
<script>
    var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);
    app.controller("myCntrl", function ($scope, $http, $timeout, $filter) {

        //loadmemberdata
        $scope.filladmindashboard = function () {
            var httpreq = {
                method: 'POST',
                url: '../Administrator/Dashboard.aspx/Bindmemberdata',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: {}
            }
            $http(httpreq).success(function (response) {
                $scope.admindashboard = response.d;
            })
            $timeout(function () {
                $scope.filladmindashboard();
            }, 5000)
        };
        $scope.filladmindashboard();
        $("#loader").hide();
        //loadmemberdata


    //EwalletBalance ledger
    $scope.fillservices = function () {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/services.aspx/fillservices',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}

        }
        $http(httpreq).success(function (response) {
            $scope.Eservices = response.d;
        })
    };
    $scope.fillservices();
    $("#loader").hide();



        //Active DMR Start
    $scope.Servicec = function (id, actions) {

        ReadServices(id, actions);
    }
    function ReadServices(id, actions) {
        var ID = id;
        $http({
            url: '../Administrator/Services.aspx/updateservices',
            method: "POST",
            data: { msrno: id, action: actions }
        }).success(function (response) {
            showSwal('success-message');
            $scope.fillservices();

        })
    };
        //Active DMR End
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Services
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
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
                                                <th>Serivce Name
                                                </th>
                                                <th>
                                                    Change Status
                                                </th>
                                            </tr>
                                            <tr dir-paginate="x in Eservices|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.ServiceTypeName}}</td>
                                                <td><a href="#" ng-click="Servicec(x.ServiceTypeName,x.IsActive)" class="badge badge-danger badge-pill">{{x.IsActive}}</a></td>
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
                            </div></div></div></div></div></div>
</asp:Content>

