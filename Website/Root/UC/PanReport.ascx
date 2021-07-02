<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PanReport.ascx.cs" Inherits="Root_UC_PanReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Pan Card Form Report
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
                                    <label class="col-sm-3 col-form-label">FromDate</label>
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
                                    <label class="col-sm-3 col-form-label">ToDate</label>
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
                                                      <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                    CssClass="class24" OnClick="btnexportExcel_Click" />--%>

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
                                    <label class="col-sm-3 col-form-label">Status</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control">
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
                                <div id="loader" style='display: none;'>
                                    <img src='../../Design/images/pageloader.gif' width='32px' height='32px'>
                                </div>
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <%--  <div style="font-size: 0px; left: 88%; margin: 0; padding: 0; position: absolute;">
                    <table class="aleft">
                        <tr>
                            <td>
                                Record(s) :<asp:Literal ID="Literal1" runat="server" Text="0" />
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
                                                           
                                                            <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                                             <asp:BoundField HeaderText="Name" DataField="NameOnPAN" SortExpression="NameOnPAN" />
                                                            <asp:BoundField HeaderText="PanCard Type" DataField="RequestType" SortExpression="RequestType" />
                                                            <asp:BoundField HeaderText="TxnID" DataField="Acknowledgement_No" SortExpression="TxnID" />
                                                            <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" />
                                                           
                                                            <asp:TemplateField HeaderText="Request On" SortExpression="RequestDate">
                                                                <ItemTemplate>
                                                                    <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("RequestDate")))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />
                                                            <asp:TemplateField HeaderStyle-Width="30px"  HeaderText="pan Status ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                        Text='<%#Eval("Staussss") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="30px"  HeaderText="Admin Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                        Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" Visible='<%#(Eval("StatusSwow")) %>' runat="server">
                                                            <a href="../../Uploads/Servicesimage/Actual/<%# Eval("ReciptImg") %>" target="_blank">
                                                                Download                                                                
                                                            </a>
                                                                    </asp:Label>
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
</div>
