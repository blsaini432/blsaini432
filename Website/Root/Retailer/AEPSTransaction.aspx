<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master"
    AutoEventWireup="true" CodeFile="AEPSTransaction.aspx.cs" Inherits="Root_Retailer_AEPSTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../ravi/lytebox.css" rel="stylesheet" type="text/css" />
    <script src="../ravi/lytebox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-header">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>AEPS</a></li>
            <li class="active">Transactions</li>
        </ol>
    </div>
    <div>
        <table class="table table-bordered table-hover ">
            <tr>
                <td>From Date
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </td>
                <td>To Date
                </td>
                <td>
                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
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
                <div style="font-size: 12px; margin: 0px; padding: 0;">
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
                                        AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="id" Width="100%"
                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gv_Transaction_PageIndexChanging" PageSize="10" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="MemberID" DataField="memberid" SortExpression="memberid" />
                                            <asp:BoundField HeaderText="Custmer Mobile" DataField="cn" SortExpression="cn" />
                                            <asp:BoundField HeaderText="Amount" DataField="txn_amount_tra" SortExpression="txn_amount_tra" />
                                                              <asp:BoundField HeaderText="Transcation Status" DataField="txnstatus" SortExpression="txnstatus" />
                                        <asp:BoundField HeaderText="Message" DataField="msg" SortExpression="msg" />
                                            <asp:BoundField HeaderText="Commission" DataField="commission" SortExpression="commission" />
                                            <asp:BoundField HeaderText="Txn. Number" DataField="order_id" SortExpression="order_id" />
                                            <asp:TemplateField HeaderText="Date" SortExpression="creted">
                                                <ItemTemplate>
                                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("creted")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
