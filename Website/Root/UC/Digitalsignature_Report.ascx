<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Digitalsignature_Report.ascx.cs" Inherits="Root_UC_Digitalsignature_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<div class="content mydash" style="padding-top: 53px;">
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
            <td style="display:none;">
                By Request Status
            </td>
            <td style="display:none;">
                <asp:DropDownList ID="ddl_status" runat="server" Visible="false">
                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    <asp:ListItem Value="success">success</asp:ListItem>
                    <asp:ListItem Value="failed">failed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Acknowledgement No
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
    <div class="user-section" style="display:none;">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="card card-hight">
                    <div class="card-action">
                        <h3>
                            Total Applied Pan Card<asp:Label ID="lblTotPan" runat="server" Style="float: right;"></asp:Label></h3>
                    </div>
                    <div class="card-content">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>
                                            Total Successfull
                                        </th>
                                        <th>
                                            Total Fail
                                        </th>
                                        <th>
                                            Total Temp Rejected
                                        </th>
                                        <th>
                                            Total Pending
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="odd gradeX">
                                        <td>
                                            <asp:Label ID="lblSuccPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFailPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTempPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPendPan" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table class="table table-bordered table-hover">
        <tr>
            <td valign="top" colspan="2">
                <div style="font-size: 0px; left: 88%; margin-top: -32px; padding: 0; position: absolute;">
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
                            PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                            AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />
                                <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                 <asp:BoundField HeaderText="Trans_No" DataField="Acknowledgement_No" SortExpression="Acknowledgement_No" />
                                
                                <asp:BoundField HeaderText="Name" DataField="applicantname" SortExpression="applicantname" />
                                 <asp:BoundField HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />
                              
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="loader">
</div>
<div id="backgroundPopup" onclick="disablePopup();">
</div>--%>





<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Digital signature Report
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
                                                    <%--   <asp:Button ID="button" runat="server" Text="Export"  OnClick="btnexportExcel_Click" class="btn btn-dribbble" />--%>
                                                    <%--  <input type="button" value="Export" runat="server"  CssClass="class24"  OnClick="btnexportExcel_Click" />--%>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" text="Export" value="Export"
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
                                                        PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                <asp:BoundField HeaderText="DSC Type" DataField="state" SortExpression="RequestStatus" />
                                                            <asp:BoundField HeaderText="Name" DataField="applicantname" SortExpression="applicantname" />

                                                            <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                                            <asp:BoundField HeaderText="Trans_No" DataField="Acknowledgement_No" SortExpression="Acknowledgement_No" />
                                                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />

                                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />

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
</div>
