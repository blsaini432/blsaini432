<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Ezulix_Recharge_ListAPI.aspx.cs" Inherits="Root_Admin_ListAPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/PopupScript.js" type="text/javascript"></script>
    <link href="../css/Popup.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div#popup_content
        {
            margin: 4px 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<section class="content-header">
                <h1>
                    API Listing
                    <small>Admin Panel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Recharge</a></li>
                    <li class="active">API Listing</li>
                </ol>
            </section>
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
            <section class="content mydash">
             <table class="table table-bordered table-hover ">
                    <tr>
                        <td align="left">
                            <table class="aleft" width="30%">
                                <tr>
                                    <td width="50%">
                                        Total Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                                    </td>
                                    <td width="1%">
                                        |
                                    </td>
                                    <td width="16%">
                                        <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                            CssClass="class24" OnClick="btnexportExcel_Click" />
                                    </td>
                                    <td width="1%">
                                        |
                                    </td>
                                    <td width="16%">
                                        <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                            CssClass="class24" OnClick="btnexportWord_Click" />
                                    </td>
                                    <td width="1%">
                                        |
                                    </td>
                                    <td width="17%">
                                        <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                            CssClass="class24" OnClick="btnexportPdf_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="10">10 Record(s)</asp:ListItem>
                                <asp:ListItem Value="25">25 Record(s)</asp:ListItem>
                                <asp:ListItem Value="50">50 Record(s)</asp:ListItem>
                                <asp:ListItem Value="100">100 Record(s)</asp:ListItem>
                                <asp:ListItem Value="200">200 Record(s)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            <asp:GridView ID="gvAPI" runat="server" CssClass="table table-bordered table-hover tablesorter" AutoGenerateColumns="false"
                                AllowPaging="True" DataKeyNames="APIID" OnPageIndexChanging="gvAPI_PageIndexChanging"
                                PageSize="10" Width="100%" OnRowCommand="gvAPI_RowCommand" OnSorting="gvAPI_Sorting"
                                AllowSorting="true" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="API ID" DataField="APIID" SortExpression="APIID" />
                                    <asp:BoundField HeaderText="API Name" DataField="APIName" SortExpression="APIName" />
                                    <asp:BoundField HeaderText="Recharge URL" DataField="URL" SortExpression="URL" />
                                    <asp:TemplateField HeaderText="Prm-1" SortExpression="prm1">
                                        <ItemTemplate>
                                            <%#Eval("prm1")%>
                                            -
                                            <%#Eval("prm1val")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prm-2" SortExpression="prm2">
                                        <ItemTemplate>
                                            <%#Eval("prm2")%>
                                            -
                                            <%#Eval("prm2val")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Prm-3" DataField="prm3" SortExpression="prm3" />
                                    <asp:BoundField HeaderText="Prm-4" DataField="prm4" SortExpression="prm4" />
                                    <asp:BoundField HeaderText="Prm-5" DataField="prm5" SortExpression="prm5" />
                                    <asp:BoundField HeaderText="Prm-6" DataField="prm6" SortExpression="prm6" />
                                    <asp:BoundField HeaderText="Prm-7" DataField="prm7" SortExpression="prm7" />
                                    <asp:BoundField HeaderText="Prm-8" DataField="prm8" SortExpression="prm8" />--%>
                                    <asp:TemplateField HeaderText="Prm-9" SortExpression="prm9">
                                        <ItemTemplate>
                                            <%#Eval("prm9") %>
                                            -
                                            <%#Eval("prm9val")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prm-10" SortExpression="prm10">
                                        <ItemTemplate>
                                            <%#Eval("prm10")%>
                                            -
                                            <%#Eval("prm10val")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" />
                                    <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" />--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" ImageUrl="../images/icon/view_16x16.png"
                                                AlternateText="View" ToolTip="View this API" CommandName="View" CommandArgument='<%#Eval("APIID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="Recharge_ManageAPI.aspx?id=<%#Eval("APIID") %>" title="Edit this record">
                                                <img src="../images/icon/edit_16x16.png" alt="Edit" />
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../images/icon/icn_trash.png"
                                                AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("APIID") %>'
                                                OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "../images/icon/IsActive.png" : "../images/icon/IsDeactive.png" %>'
                                                AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>'
                                                CommandName="IsActive" CommandArgument='<%#Eval("APIID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">
                                        No Record Found !</div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
           </section>
            <!-- Start Popup Box -->
            <div id="toPopup">
                <div class="close" onclick="disablePopup();">
                </div>
                <div id="popup_content">
                    <h2><b><asp:Literal ID="litAPIName" runat="server"></asp:Literal></b></h2>
                    <hr />
                    <h3>Recharge API</h3>
                    <asp:Literal ID="litAPI" runat="server"></asp:Literal>
                    <hr />
                    <h3>Balance API</h3>
                    <asp:Literal ID="litBalanceAPI" runat="server"></asp:Literal>
                    <hr />
                    <h3>Status API</h3>
                    <asp:Literal ID="litStatusAPI" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="loader">
            </div>
            <div id="backgroundPopup" onclick="disablePopup();">
            </div>
            <!-- End Popup Box -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />
            <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvAPI" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="gvAPI" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvAPI" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
