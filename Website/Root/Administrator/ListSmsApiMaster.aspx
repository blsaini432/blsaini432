<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master"
    AutoEventWireup="true" CodeFile="ListSmsApiMaster.aspx.cs" Inherits="Root_Admin_ListSmsApiMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title"> List SMS API Master
            </h3>
        </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                         <asp:GridView ID="gvAPI" runat="server" 
                                    
                                    CssClass="table table-responsive" AutoGenerateColumns="false"
                                AllowPaging="false" DataKeyNames="SAPIid" OnPageIndexChanging="gvAPI_PageIndexChanging"
                                PageSize="10" Width="100%" OnRowCommand="gvAPI_RowCommand" OnSorting="gvAPI_Sorting"
                                AllowSorting="false" ShowHeaderWhenEmpty="true" 
                                    onrowcreated="gvAPI_RowCreated" onrowdatabound="gvAPI_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="API ID" DataField="SAPIid" SortExpression="SAPIid" />
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
                                    <asp:TemplateField HeaderText="Prm-3" SortExpression="prm3">
                                        <ItemTemplate>
                                            <%#Eval("prm3")%>
                                            -
                                            <%#Eval("prm3val")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" />
                                    <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" />
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <a href="SMSApiMaster.aspx?SMSID=<%#Eval("SAPIid") %>" title="Edit this record" class="btn btn-dark btn-icon-text">
                                               Edit
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-danger btn-icon-text" Text="Delete"
                                                AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("SAPIid") %>'
                                                OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnIsActive" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Active" : "Deactive" %>' CssClass="btn btn-outline-warning btn-icon-text"
                                                AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>'
                                                CommandName="IsActive" CommandArgument='<%#Eval("SAPIid") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")' />
                                                
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSwitch" runat="server" Text="Activate Now" CommandName="activate" class="btn btn-primary"
                                                    CommandArgument='<%# Eval("sapiid") %>' />
                                                    <asp:HiddenField ID="hdnAPIid" runat="server" Value='<%# Eval("sapiid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div></div></div></div></ContentTemplate></asp:UpdatePanel></div>
</asp:Content>
