<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Root_Admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        });
    </script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
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
            <h3 class="page-title">Dashboard
            </h3>
        </div> 
        <div class="row grid-margin" ng-app="myApp" ng-controller="myCntrl">
            <div class="col-12">
                <div class="card card-statistics" ng-repeat="x in admindashboard">
                    <div class="card-body">
                        <div class="d-flex flex-column flex-md-row align-items-center justify-content-between">
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fa fa-user mr-2"></i>
                                    Total Users
                       
                                </p>
                                <h2><a href="viewallmember.aspx" target="_blank" style="color:white;">{{x.TotalMember}}</a></h2>

                            </div>
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-hourglass-half mr-2"></i>
                                   E-Wallet Balance
                       
                                </p>
                                <h2><a href="ListEWalletBalance.aspx" target="_blank" style="color:white;">{{x.EBalance}}</a></h2>
                               
                            </div>
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-cloud-download-alt mr-2"></i>
                                    AEPS Balance
                       
                                </p>
                                <h2><a href="listaepsWalletBalance.aspx" target="_blank" style="color:white;">{{x.RBalance}}</a></h2>
                               
                            </div>
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-check-circle mr-2"></i>
                                    Today Funds Given
                                </p>
                                <h2><a href="listfundRequest.aspx" target="_blank" style="color:white;">{{x.fundGiven}}</a></h2>
                            </div>
                      
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <div class="row">
            <div class="col-md-8 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">
                            <i class="fas fa-table"></i>
                            Last Login IP:
                  </h4>
                        <div class="table-responsive">
                            <table class="table">

                                <thead>
                                    <tr>
                                        <th>Login IP
                                        </th>
                                        <th>Login Date/Time
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repEmployeeLoginDetail" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="font-weight-bold">
                                                    <%# Eval("LoginIP")%>
                                                </td>
                                                <td class="text-muted">
                                                    <%# String.Format("{0:dd MMM yy hh:mm tt}", Eval("LoginDate"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">
                            <i class="fas fa-calendar-alt"></i>
                            Calendar
                  </h4>
                        <div id="inline-datepicker-example" class="datepicker"></div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

