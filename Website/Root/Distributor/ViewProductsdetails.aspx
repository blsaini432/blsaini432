<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="ViewProductsdetails.aspx.cs" Inherits="Root_Distributor_ViewProductsdetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../ravi/lytebox.css" rel="stylesheet" type="text/css" />
    <script src="../ravi/lytebox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-header">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Product</a></li>
            <li class="active">Purchase Informations</li>
        </ol>
    </div>
    <div>
        <table class="table table-bordered table-hover ">
            <tr>
                <td>From Date
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </td>
                <td>To Date
                </td>
                <td>
                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MM-yyyy" Animated="False"
                        PopupButtonID="txttodate" TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                </td>
                <asp:TextBox ID="txtMemberid" runat="server" CssClass="form-control" MaxLength="8"
                    Visible="false"></asp:TextBox>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                        class="btn btn-primary" />
                </td>
            </tr>
        </table>
        <asp:MultiView ID="MV" runat="server" ActiveViewIndex="0">
            <asp:View ID="v1" runat="server">
                <div style="font-size: 12px; margin: 0px; padding: 0; display: none;">
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
                <table class="aleft" style="width: 100%;">
                    <tr>
                        <td>
                            <div class="box-inner">
                                <div class="box-content">
                                    <asp:GridView ID="gv_Transaction" runat="server" CssClass="table table-bordered table-striped dtable"
                                        AutoGenerateColumns="false" AllowPaging="true" Width="100%"
                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gv_Transaction_PageIndexChanging"
                                        PageSize="10" ShowFooter="true">
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
                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />
                                            <asp:BoundField HeaderText="CourierNo" DataField="RefNo" SortExpression="RefNo" />
                                          <%--  <asp:TemplateField HeaderText="Receipt">
                                                <ItemTemplate>
                                                    <div id="dd" runat="Server" visible='<%#Convert.ToString(Eval("RequestStatus"))=="Success"?true:false %>'>
                                                        <a href="../../Uploads/ProductImage/Actual/<%# Eval("ReciptImg") %>" target="_blank">Download
                                                           
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="PurchaseDate" SortExpression="AddDate">
                                                <ItemTemplate>
                                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="StatusDate" SortExpression="ModifiedDate">
                                                <ItemTemplate>
                                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("ModifiedDate")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
