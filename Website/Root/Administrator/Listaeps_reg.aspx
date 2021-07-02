<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Listaeps_reg.aspx.cs" Inherits="Root_Admin_Listaeps_reg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../ravi/lytebox.css" rel="stylesheet" type="text/css" />
    <script src="../ravi/lytebox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-header">
        <h1>AEPS Registrantion Report <small>Admin Panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>AEPS</a></li>
            <li class="active">Registration Report</li>
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
            <div class="content mydash">
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
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                class="btn btn-primary" />
                        </td>
                    </tr>
                </table>
                <table class="aleft">
                    <tr>
                        <td>Total Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                        </td>
                        <td>|
                        </td>
                        <td>
                            <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                CssClass="class24" OnClick="btnexportExcel_Click" />
                        </td>
                        <td>|
                        </td>
                        <td>
                            <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                CssClass="class24" OnClick="btnexportWord_Click" />
                        </td>
                        <td>|
                        </td>
                        <td>
                            <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                CssClass="class24" OnClick="btnexportPdf_Click" />
                        </td>
                    </tr>
                </table>
                <div style="min-height: 250px; margin: 0px; padding: 0px; float: left; width: 100%">
                    <table class="table table-bordered table-hover ">
                        <tr>
                            <td>
                                <asp:GridView ID="gvEWalletTransaction" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                    AutoGenerateColumns="false" AllowPaging="false" DataKeyNames="id" Width="100%"
                                    OnRowCommand="gvEWalletTransaction_RowCommand" AllowSorting="false" ShowHeaderWhenEmpty="true"
                                    OnRowCreated="gvEWalletTransaction_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Member ID" DataField="MemberID" SortExpression="MemberID" />
                                        <asp:BoundField HeaderText="First Name" DataField="F_Name" SortExpression="F_Name" />
                                        <asp:BoundField HeaderText="Last Name" DataField="L_Name" SortExpression="L_Name" />
                                        <asp:BoundField HeaderText="Shop Name" DataField="Shop_Name" SortExpression="Shop_Name" />
                                        <asp:BoundField HeaderText="Pan Number" DataField="Pan_Number" SortExpression="Pan_Number" />
                                        <asp:BoundField HeaderText="Mobile Number" DataField="Contact_Number" SortExpression="Contact_Number" />
                                        <%--                                        <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />--%>
                                        <asp:TemplateField HeaderText="Primary Address">
                                            <ItemTemplate>
                                                <%#Eval("P_Address")+ ","+Eval("P_City")+","+Eval("P_State")+","+Eval("P_Pin") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField HeaderText="Shop Address">
                                            <ItemTemplate>
                                                <%#Eval("S_Address")+ ","+Eval("S_City")+","+Eval("S_State")+","+Eval("S_Pin") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--                              <asp:TemplateField HeaderText="Iden_Proof_Num">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyp_IdenProofnum" runat="server" Text='<%#Eval("Iden_Proof_Num") %>'
                                                    NavigateUrl='<%#Eval("Iden_Proof_Filename") %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Add. Proof Number">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyp_AddProofnum" runat="server" Text='<%#Eval("Addr_Proof_Num") %>'
                                                    NavigateUrl='<%#Eval("Addr_Proof_Filename") %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Self Declaration Number">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyp_SelfProofnum" runat="server" Text='<%#Eval("Self_Decl_Num") %>'
                                                    NavigateUrl='<%#Eval("Self_Decl_Filename") %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Current Status" DataField="Statu" SortExpression="Statu" />
                                        <%--                                      <asp:TemplateField HeaderText="Update Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStatus" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Pending</asp:ListItem>
                                                    <asp:ListItem Value="2">Under Processing</asp:ListItem>
                                                    <asp:ListItem Value="3">Send to Ezulix Software</asp:ListItem>
                                                    <asp:ListItem Value="4">Send to YES Bank</asp:ListItem>
                                                    <asp:ListItem Value="5">Approved</asp:ListItem>
                                                    <asp:ListItem Value="6">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Reject Reson">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Rejection" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btn_Submit" runat="server" Text="Approved" CommandName="approve" CommandArgument='<%#Eval("MsrNo") %>' Visible='<%#Eval("Statu").ToString()=="Pending"? true:false%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btn_Reject" runat="server" Text="Reject" CommandName="reject" CommandArgument='<%#Eval("MsrNo") %>' Visible='<%#Eval("Statu").ToString()=="Pending"? true:false%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btn_ForceAepsAcitve" runat="server" Text="Force apps start" CommandName="force" CommandArgument='<%#Eval("MemberID") %>' Visible='<%#Eval("Statu").ToString()=="Rejected"? true:false%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="gvEWalletTransaction" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<script language="javascript" type="text/javascript">
        function pageLoad() {

        }
    </script>--%>
</asp:Content>
