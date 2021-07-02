<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListMemberBanker.aspx.cs" Inherits="Root_Admin_ListMemberBanker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">List Member Bank
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container">
                            <div class="row">
                                 <asp:GridView ID="gvMemberBanker" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText" 
                            AutoGenerateColumns="false"
                                AllowPaging="false" DataKeyNames="MemberBankerID" OnPageIndexChanging="gvMemberBanker_PageIndexChanging"
                                PageSize="10" Width="100%" OnRowCommand="gvMemberBanker_RowCommand" OnSorting="gvMemberBanker_Sorting"
                                AllowSorting="false" ShowHeaderWhenEmpty="true" 
                                    onrowcreated="gvMemberBanker_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="MemberID" DataField="MemberID" SortExpression="MemberID" />--%>
                                    <asp:TemplateField HeaderText="Account holder Name" SortExpression="MemberName">
                                        <ItemTemplate>
                                            <%#Eval("BankDesc") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Bank Name" DataField="BankerMasterName" SortExpression="BankerMasterName" />
                                    <asp:BoundField HeaderText="Branch Name" DataField="BankBranch" SortExpression="BankBranch" />
                                    <asp:BoundField HeaderText="Account Type" DataField="AccountType" SortExpression="AccountType" />
                                    <asp:BoundField HeaderText="Account Number" DataField="AccountNumber" SortExpression="AccountNumber" />
                                    <asp:BoundField HeaderText="IFSC Code" DataField="IFSCCode" SortExpression="IFSCCode" />
                                    <asp:TemplateField HeaderStyle-Width="16px" HeaderText="IsApprove" SortExpression="IsApprove">
                                        <ItemTemplate>




                                            <asp:ImageButton ID="btnIsApprove" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsApprove")) == true ? "../images/icon/IsActive.png" : "../images/icon/IsDeactive.png" %>'
                                                AlternateText="Approve this record" ToolTip='<%# Convert.ToBoolean(Eval("IsApprove")) == true ? "Deactive this record" : "Approve this record" %>'
                                                CommandName="IsApprove" CommandArgument='<%#Eval("MemberBankerID") %>' OnClientClick='return confirm("Are You Sure To IsApprove This Record?")'
                                                Enabled='<%# Convert.ToBoolean(Eval("IsApprove")) == true ? false : true  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-----------------------------------------------------------------------------------------------------------------%>
                                    <asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" />
                                    <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" />
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <a href="ManageMemberBanker.aspx?id=<%#Eval("MemberBankerID") %>" title="Edit this record" class="btn btn-dark btn-icon-text">
                                              <i class="fas fa-pencil-alt btn-icon-append"></i>Edit</a>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-icon-text"
                                                AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("MemberBankerID") %>'
                                                OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px" SortExpression="IsActive">
                                        <ItemTemplate>
                                            <asp:Button ID="btnIsActive" runat="server" CssClass="btn btn-primary btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive" : "Active" %>'
                                                AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>'
                                                CommandName="IsActive" CommandArgument='<%#Eval("MemberBankerID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                                </div></div></div></div></div></div></div>






</asp:Content>
