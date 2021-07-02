<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="MM_Transactions.aspx.cs" Inherits="Root_Admin_MM_Transactions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.3.14/angular.min.js" type="text/javascript"></script>
    <script src="../Angularjsapp/dirPagination.js" language="javascript" type="text/javascript"></script>
    <script src="../Angularjsapp/myapp.js" type="text/javascript"></script>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
    <style type="text/css">
        .input {
            float: left;
            width: 87%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">DMR Transactions
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
                                            <asp:TextBox ID="txt_fromdate" runat="server" ng-model="from_date" MaxLength="50" Enabled="false" CssClass="form-control" autcomplete="off" ClientIDMode = "Static" Text=""></asp:TextBox>
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
                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="50" ClientIDMode = "Static" ng-model="to_date" CssClass="form-control" autcomplete="off" Enabled="false" Text=""></asp:TextBox>
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
                                            <a href="#" ng-click="filldmrinreportbydate(from_date,to_date)" class="btn btn-primary">Search &gt;&gt;</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                              <a href="#" ng-click="ExportDMR(from_date,to_date)" class="btn btn-dribbble">Export &gt;&gt;</a>
                                            <asp:Button ID="btnexport" runat="server" Text="Export" CssClass="btn btn-dribbble" OnClick="btnexport_Click" />
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
                                                 <th>MemberName
                                                </th>
                                                  <th>Customer Mobile
                                                </th>
                                                  <th>Name
                                                </th>
                                                  <th>Benificary Account
                                                </th>
                                                <th>Bank Name
                                                </th>
                                                <th>Reference Number
                                                </th>
                                                <th>Wallet TransactionId
                                                </th>
                                                <th>Amount
                                                </th>
                                                <th>SurchargeTaken</th>
                                                <th>CommissionGiven</th>
                                                <th>AdminCost</th>
                                                <th>Status</th>
                                                <th>createdate</th>
                                            </tr>
                                            <tr dir-paginate="x in dmrinreport|filter:search|itemsPerPage:pageSize">
                                                <td>{{$index + 1}}</td>
                                                <td>{{x.MemberID}}</td>
                                                <td>{{x.MemberName}}</td>
                                                <td>{{x.CustMobile}}</td>
                                                 <td>{{x.name}}</td>
                                                 <td>{{x.BeneAC}}</td>
                                               <td>{{x.BankName}}</td>
                                                <td>{{x.RefNO}}</td>
                                                <td>{{x.TxnID}}</td>
                                                <td>{{x.Amount}}</td>
                                                <td>{{x.SurchargeTaken}}</td>
                                                <td>{{x.CommissionGiven}}</td>
                                                <td>{{x.AdminCost}}</td>
                                                 <td>{{x.Status}}</td>
                                                 <td>{{x.createdate}}</td>
                                            </tr>
                                             <tr>
                                                <td ng-show="dmrinreport.length==0" colspan="15">
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
