<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/adminmaster.master" AutoEventWireup="true" CodeFile="ViewProductsdetails.aspx.cs" Inherits="Root_Administrator_ViewProductsdetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/script_pop.js" type="text/javascript"></script>
    <link href="../../css/style2.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 85px;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 420px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h2 style="text-align:center; text-decoration:underline">Update Product Request</h2>
                    <table width="100%">
                        <tr>
                            <td>Member Info
                            </td>
                            <td>
                                <asp:Literal ID="litMember" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                            <td>Courior No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="recp" visible="false">
                            <td>Courier Receipt
                            </td>
                            <td>
                                <asp:FileUpload ID="fupRcpt" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td>Admin Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtadminRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Admin Remarks!"
                                    ControlToValidate="txtadminRemark" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSuccess" runat="server" Visible="false" Text="Success" ValidationGroup="1v"  OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" OnClientClick="this.disabled='true';" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSuccess"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--your content end-->
    </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>
    <div class="content-header">
        <h1>List Product Request <small>Admin Panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Admin</a></li>
            <li class="active">List Product Request</li>
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
                            <td>From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                    TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>By Request Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                    <asp:ListItem Value="success">success</asp:ListItem>
                                    <asp:ListItem Value="failed">failed</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Txn ID
                            </td>
                            <td>
                                <asp:TextBox ID="txt_orderID" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                    class="btn btn-primary" />
                            </td>
                        </tr>
                    </table>
                    <h4>Total Product Requests(s)<strong><asp:Label ID="lblTotPan" runat="server" Style="float: right;"></asp:Label></strong></h4>
                    <table class="table table-bordered table-hover">
                        <tr>
                            <td valign="top" colspan="2">
                                <div style="font-size: 0px; left: 88%; margin: 0; padding: 0; position: absolute;">
                                    <table class="aleft">
                                        <tr>
                                            <td>Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
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
                                            <asp:BoundField HeaderText="ProductName" DataField="ProductName" SortExpression="ProductName" />
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" SortExpression="Quantity" />
                                            <asp:BoundField HeaderText="Totalamount" DataField="Totalamount" SortExpression="Totalamount" />
                                            <asp:BoundField HeaderText="TxnId" DataField="TxnId" SortExpression="TxnId" />
                                            <asp:BoundField HeaderText="RequestStatus" DataField="RequestStatus" SortExpression="RequestStatus" />
                                            <asp:BoundField HeaderText="RequestBy" DataField="RequestBy" SortExpression="RequestBy" />
                                            <asp:TemplateField HeaderText="PurchaseDate" SortExpression="AddDate">
                                                <ItemTemplate>
                                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                   <asp:TemplateField HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("purchaseid") %>'
                                                                CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                            <asp:Label ID="lblssdsd" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                Text='<%#Eval("Staussss") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("purchaseid") %>'
                                                                CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                         <asp:Label ID="lbfsdfewrewl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>'
                                                                runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </td>
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
</asp:Content>
--%>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 250px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h4 style="text-align: center; color: green; text-decoration: underline">Update Product Request</h4>
                    <table width="100%">
                        <asp:HiddenField ID="hdnid" runat="server" Visible="false" />
                        <tr runat="server" id="ressf" visible="false">
                            <td>Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="recp" visible="false">
                            <td>Details
                            </td>
                            <td>
                                <asp:Literal ID="LitTransaction" runat="server"></asp:Literal>
                                <asp:Label ID="litOpname" runat="server"></asp:Label>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="hdn_memberID" runat="server" />
                                <asp:HiddenField ID="hdn_amount" runat="server" />
                                <asp:HiddenField ID="hdn_txnid" runat="server" />
                            </td>
                        </tr>
                         <tr runat="server" id="Tr1" visible="false">
                            <td>Add Receipt File -2
                            </td>
                            <td>
                                <asp:FileUpload ID="fupRcpt" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fupRcpt"
                                    Display="Dynamic" ErrorMessage="Please Select Address receipt2 " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--<tr runat="server" id="Tr2" visible="false">
                            <td>Add Receipt File -3
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload2"
                                    Display="Dynamic" ErrorMessage="Please Select Address receipt3 " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>Admin Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtadminRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Admin Remarks!"
                                    ControlToValidate="txtadminRemark" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSuccess" runat="server" Visible="false" Text="Success" ValidationGroup="1v"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" />
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
            <div class="content-wrapper">
                <div class="page-header">
                    <h3 class="page-title">List Product Request 
                    </h3>
                </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                                        class="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <table class="aleft">
                                                        <tr>
                                                            <td>Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnexportExcel" runat="server" text="Export" value="Export"
                                                                    class="btn btn-dribbble" OnClick="btnexportExcel_Click" />

                                                            </td>

                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">Status<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                                        <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                        <asp:ListItem Value="success">success</asp:ListItem>
                                                        <asp:ListItem Value="failed">failed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="info-box">
                                        <%--<div class="form-group">
                                    <label>Search</label>
                                    <input type="text" ng-model="search" class="form-control" placeholder="Search">
                                </div>
                                <div>
                                    <label for="search">items per page:</label>
                                    <input type="number" min="1" max="100" class="form-control" ng-model="pageSize">
                                </div>--%>
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                       <%-- <div style="font-size: 0px; left: 88%; margin: 0; padding: 0; position: absolute;">
                                                            <table class="aleft">
                                                                <tr>
                                                                    <td>Record(s) :<asp:Literal ID="Literal1" runat="server" Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/icon/excel_32X32.png"
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
                                                        </div>--%>
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
                                                                            <asp:BoundField HeaderText="ProductName" DataField="ProductName" SortExpression="ProductName" />
                                                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" SortExpression="Quantity" />
                                                                            <asp:BoundField HeaderText="Totalamount" DataField="Totalamount" SortExpression="Totalamount" />
                                                                            <asp:BoundField HeaderText="TxnId" DataField="TxnId" SortExpression="TxnId" />
                                                                            <asp:BoundField HeaderText="RequestStatus" DataField="RequestStatus" SortExpression="RequestStatus" />
                                                                            <asp:BoundField HeaderText="RequestBy" DataField="RequestBy" SortExpression="RequestBy" />
                                                                            <asp:TemplateField HeaderText="PurchaseDate" SortExpression="AddDate">
                                                                                <ItemTemplate>
                                                                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("purchaseid") %>'
                                                                                        CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                                        Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                                    <asp:Label ID="lblssdsd" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                                        Text='<%#Eval("Staussss") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("purchaseid") %>'
                                                                                        CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                                        Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                                    <asp:Label ID="lbfsdfewrewl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>'
                                                                                        runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
                                                <div class="dataTables_scrollFootInner" style="width: 1224px; padding-right: 17px;">
                                                    <table class="display dataTable" role="grid" style="margin-left: 0px; width: 1224px;"></table>
                                                </div>
                                            </div>
                                            <%-- <div class="text-center">
                                        <dir-pagination-controls boundary-links="true" on-page-change="pageChangeHandler(newPageNumber)" template-url="../Angularjsapp/dirPagination.tpl.html"></dir-pagination-controls>
                                    </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />
            <%-- <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:PostBackTrigger ControlID="gvGST" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
