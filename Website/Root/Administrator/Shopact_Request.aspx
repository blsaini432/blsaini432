<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/Adminmaster.master" AutoEventWireup="true"
    CodeFile="Shopact_Request.aspx.cs" Inherits="Portal_Admin_Voteridrequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script src="../../js/script_pop.js" type="text/javascript"></script>
    <link href="../../css/style2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 420px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h2 style="text-align: center; text-decoration: underline">
                        Update  Request</h2>
                    <table width="100%">
                        <tr>
                            <td>
                                Member Info
                            </td>
                            <td>
                                <asp:Literal ID="litMember" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 Details
                            </td>
                            <td>
                                <asp:Literal ID="LitTransaction" runat="server"></asp:Literal>
                                <asp:Label ID="litOpname" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnid" runat="server" />
                                <asp:HiddenField ID="hdn_memberID" runat="server" />
                                <asp:HiddenField ID="hdn_amount" runat="server" />
                                <asp:HiddenField ID="hdn_txnid" runat="server" />
                            </td>
                        </tr>
                        <tr runat="server" id="ressf" visible="false">
                            <td>
                                Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="recp" visible="false">
                            <td>
                                Add Receipt File
                            </td>
                            <td>
                                <asp:FileUpload ID="fupRcpt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Admin Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtadminRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Admin Remarks!"
                                    ControlToValidate="txtadminRemark" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSuccess" runat="server" Visible="false" Text="Success" ValidationGroup="1v"
                                    OnClick="btnSubmit_Click" />
                               
                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" OnClientClick="this.disabled='true';" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSuccess" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
      
    </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>
    <div class="content-header">
        <h1>
            List Shop Act   Request <small>Admin Panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Admin</a></li>
            <li class="active">List  Request</li>
        </ol>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-content">
                <div class="content mydash">
                    <table class="table table-bordered table-hover ">
                        <tr>
                            <td>
                                From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                    TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                By Request Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                    <asp:ListItem Value="success">success</asp:ListItem>
                                    <asp:ListItem Value="failed">failed</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Txn ID
                            </td>
                            <td>
                                <asp:TextBox ID="txt_orderID" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                    class="btn btn-primary" />
                            </td>
                        </tr>
                    </table>
                    <h4>
                     
                    <table class="table table-bordered table-hover">
                        <tr>
                           
                                 <div style="font-size: 12px;margin:0px;padding: 0;">
                                    <table class="aleft">
                                        <tr>
                                            <td>
                                                Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                                    CssClass="class24" OnClick="btnexportExcel_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                                    CssClass="class24" OnClick="btnexportWord_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                                    CssClass="class24" OnClick="btnexportPdf_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="box-inner">
                                    <div class="box-content">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%">
                                            <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText table-responsive"
                                                AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                                PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                                                AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="MemberID" DataField="MemberID" SortExpression="RequestStatus" />
                                                    <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                                 
                                                    <asp:BoundField HeaderText="Name " DataField="Name_of_ect" SortExpression="Name_of_ect" />
                                                    <asp:BoundField HeaderText="Aadhar Number" DataField="Aadharnumber" SortExpression="Aadharnumber" />
                                                      <asp:BoundField HeaderText="Pan Card " DataField="Name_of_emp" SortExpression="RequestType" />
                                                         <asp:BoundField HeaderText="addDate" DataField="addDate" SortExpression="addDate" />
                                                  
                                                 
                                                    <asp:BoundField HeaderText="RequestStatus." DataField="RequestStatus" SortExpression="RequestStatus" />
                                                     <asp:TemplateField HeaderText="Client Data">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnWord" runat="server" Text="Download" CommandArgument='<%#Eval("shopact_id") %>'
                                                                    CommandName="WordDownload" ToolTip="Download Word File" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Photo">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("photo") %>" target="_blank">
                                                               
                                                             DOWNLOAD
                                                               </ItemTemplate>
                                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText=" aadhar ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("aadhar") %>" target="_blank">
                                                               
                                                             DOWNLOAD
                                                               </ItemTemplate>
                                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" shop ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("Oldshop") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>
                                                                </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="file1 ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("Actualphoto") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>
                                                                </asp:TemplateField> 
                                                       <asp:TemplateField HeaderText=" file2 ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("file2") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>

                                                                </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText=" file3 ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("file3") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>

                                                                </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText=" file4 ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("file4") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>

                                                                </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="SelfDeclaration_Shop  ">
                                                        <ItemTemplate>
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("file5") %>" target="_blank">
                                                               
                                                              DOWNLOAD
                                                               </ItemTemplate>

                                                                </asp:TemplateField>     
                                                    <asp:TemplateField HeaderText="Approve">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("shopact_id") %>'
                                                                CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("shopact_id") %>'
                                                                CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />
            <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:PostBackTrigger ControlID="gvBookedBusList" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>--%>





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
                    url: '../Administrator/Shopact_Request.aspx/fillposrequest',
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
                    url: '../Administrator/Shopact_Request.aspx/fillposrequestbydate',
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
                    url: '../Administrator/Shopact_Request.aspx/ApproveRequest',
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
                    url: '../Administrator/Shopact_Request.aspx/clientdata',
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
                    url: '../Administrator/Shopact_Request.aspx/RejectRequest',
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
                    url: '../Administrator/Shopact_Request.aspx/ShowFundImage',
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
            <h3 class="page-title">Shop Act Request List
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
                                                        <a  class="badge badge-danger badge-pill">REJECTED</a>
                                                    </div>
                                                    <div ng-if="x.status==='pending'">
                                                        <a href="#openModalstatus" ng-click="ApproveFundRequest(x.txnid)" class="badge badge-primary badge-pill">Status Update</a>
                                                    </div>
                                                    <div ng-if="x.status==='Success'">
                                                        <a class="badge badge-success badge-pill">APPROVED</a>
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
                                                        <label style="color:green">photo </label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.photo}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.photo}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                 
                                                    <div class="col-lg-3">
                                                         <label style="color:green">Oldshop </label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.pancard}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.pancard}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">aadhar</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.aadhar}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.aadhar}}" alt="No Image Available" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">SelfDeclaration</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.ageProof}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.ageProof}}" alt="click" height="200" width="200" />
                                                    </div>
                                                    
                                                     
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                         <label style="color:green">sign</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.sign}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.sign}}" alt="click" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">file2</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file2}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file2}}" alt="click" height="200" width="200" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                         <label style="color:green">file3</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file3}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file3}}" alt="click" height="200" width="200" />
                                                    </div>
                                                     <div class="col-lg-3">
                                                          <label style="color:green">file4</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file4}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file4}}" alt="click" height="200" width="200" />
                                                    </div>
                                                   <%-- <div class="col-lg-4">
                                                         <label style="color:green">fil5</label>
                                                        <a href="../Upload/PanCardRequest/Actual/{{y.file5}}" target="_blank" />
                                                        <img src="../Upload/PanCardRequest/Actual/{{y.file5}}" alt="click" height="200" width="200" />
                                                    </div>--%>
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
