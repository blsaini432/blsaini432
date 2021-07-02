<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Root_Administrator_View" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Design/Files/responsive.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" ng-app="myApp" ng-controller="myCntrl">
            <div class="info-box">
                <div class="form-group">
                    <label>Search</label>
                    <input type="text" ng-model="search" class="form-control" placeholder="Search">
                </div>
                <div class="col-xs-4">
                    <label for="search">items per page:</label>
                    <input type="number" min="1" max="100" class="form-control" ng-model="pageSize">
                </div>
                <div class="table-responsive">
                    <table id="example1" class="table table-bordered table-hover" cellspacing="0">
                        <thead>
                            <tr>
                                <th>
                                    S.N.
                                </th>
                                <th>MemberID
                                </th>
                                <th>MemberName
                                </th>
                                <th>Email
                                </th>
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
                            </tr>
                            <tr dir-paginate="x in Employees|filter:search|itemsPerPage:pageSize">
                                <td>{{$index + 1}}</td>
                                <td>{{x.MemberID}}</td>
                                <td>{{x.MemberName}}</td>
                                <td>{{x.Email}}</td>
                                <td>{{x.Mobile}}</td>
                                <td>{{x.Package}}</td>
                                <td>{{x.MemberType}}</td>
                                <td>{{x.Owner}}</td>
                                <td>{{x.Detail}}</td>
                            </tr>

                        </thead>
                    </table>               <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
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
           <script src="../../Design/js/angular.min.js"></script>
                <script src="../Angularjsapp/dirPagination.js"></script>
        <script src="../../Design/Files/jquery-1.10.2.min.js"></script>
        <script src="../Angularjsapp/bootstrap.js"></script>
        <script src="../../Design/Files/jquery.dataTables.js"></script>
        <script src="../Angularjsapp/myapp.js"></script>


    </form>
</body>
</html>
