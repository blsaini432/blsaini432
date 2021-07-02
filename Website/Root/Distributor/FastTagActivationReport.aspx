<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="FastTagActivationReport.aspx.cs" Inherits="Root_Distributor_FastTagActivationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/script_pop.js" type="text/javascript"></script>
    <link href="../css/style2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="toPopup">
        <div class="close">
        </div>
        
       
    </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>
    <div class="content-header">
        <h1>
            List Fasttagactivation Request <small>Admin Panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Admin</a></li>
            <li class="active">List Fasttagactivation Request</li>
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
                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
                                    TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MM-yyyy" Animated="False"
                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
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
                    <table class="table table-bordered table-hover">
                        <tr>
                            <td valign="top" colspan="2">
                                <div style="font-size: 0px; left: 88%; margin: 0; padding: 0; position: absolute;">
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
                                        <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                            AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                            PageSize="10" Width="100%"
                                            AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
                                                <asp:BoundField HeaderText="Request By" DataField="MemberId" SortExpression="MemberId" />
                                                <asp:BoundField HeaderText="TID" DataField="TID" SortExpression="TID" />
                                                 <asp:BoundField HeaderText="FirstName" DataField="FirstName" SortExpression="FirstName" />
                                                  <asp:BoundField HeaderText="LastName" DataField="LastName" SortExpression="LastName" />
                                                <asp:BoundField HeaderText="Mobile" DataField="Mobile" SortExpression="Mobile" />
                                                  <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />
                                                             <asp:BoundField HeaderText="Vehicleclass" DataField="Vehicleclass" SortExpression="Vehicleclass" />
                                                             <asp:BoundField HeaderText="VehicleType" DataField="VehicleType" SortExpression="VehicleType" />
                                                             <asp:BoundField HeaderText="VehicleReg" DataField="VehicleReg" SortExpression="VehicleReg" />
                                                             <asp:BoundField HeaderText="ChaisisNumber" DataField="ChaisisNumber" SortExpression="ChaisisNumber" />

                                                          <asp:TemplateField HeaderText="RC Front" Visible="true">
                                                        <ItemTemplate>
                                                            <a href="../Upload/FastTagRequest/Actual/<%# Eval("Rcfront") %>" target="_blank">
                                                                Download
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="RC Front" Visible="true">
                                                        <ItemTemplate>
                                                            <a href="../Upload/FastTagRequest/Actual/<%# Eval("Rcback") %>" target="_blank">
                                                                Download
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request On" SortExpression="RequestDate">
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                            </Columns>
                                        </asp:GridView>
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
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>--%>







<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">List Fasttagactivation Request
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
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
                                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MM-yyyy" Animated="False"
                                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" class="btn btn-primary" />
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
                                                            <%--<td>
                                                            <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                                                CssClass="class24" OnClick="btnexportWord_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                                                CssClass="class24" OnClick="btnexportPdf_Click" />
                                                        </td>--%>
                                                        </tr>
                                                    </table>
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
                                            <div id="loader" style='display: none;'>
                                                <img src='../../Design/images/pageloader.gif' width='32px' height='32px'>
                                            </div>
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                        <div class="box-inner">
                                                            <div class="box-content">
                                                                <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                                    AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                                                    PageSize="10" Width="100%"
                                                                    AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
                                                                        <asp:BoundField HeaderText="Request By" DataField="MemberId" SortExpression="MemberId" />
                                                                        <asp:BoundField HeaderText="TID" DataField="TID" SortExpression="TID" />
                                                                        <asp:BoundField HeaderText="FirstName" DataField="FirstName" SortExpression="FirstName" />
                                                                        <asp:BoundField HeaderText="LastName" DataField="LastName" SortExpression="LastName" />
                                                                        <asp:BoundField HeaderText="Mobile" DataField="Mobile" SortExpression="Mobile" />
                                                                        <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />
                                                                        <asp:BoundField HeaderText="Vehicleclass" DataField="Vehicleclass" SortExpression="Vehicleclass" />
                                                                        <asp:BoundField HeaderText="VehicleType" DataField="VehicleType" SortExpression="VehicleType" />
                                                                        <asp:BoundField HeaderText="VehicleReg" DataField="VehicleReg" SortExpression="VehicleReg" />
                                                                        <asp:BoundField HeaderText="ChaisisNumber" DataField="ChaisisNumber" SortExpression="ChaisisNumber" />

                                                                        <asp:TemplateField HeaderText="RC Front" Visible="true">
                                                                            <ItemTemplate>
                                                                                <a href="../../Uploads/FastTagRequest/Actual/<%# Eval("Rcfront") %>" target="_blank">Download
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="RC Front" Visible="true">
                                                                            <ItemTemplate>
                                                                                <a href="../../Uploads/FastTagRequest/Actual/<%# Eval("Rcback") %>" target="_blank">Download
                                                                                </a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Request On" SortExpression="RequestDate">
                                                                            <ItemTemplate>
                                                                                <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
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
                <asp:PostBackTrigger ControlID="btnexportpdf" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>


