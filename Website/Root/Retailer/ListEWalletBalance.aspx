<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true"
    CodeFile="ListEWalletBalance.aspx.cs" Inherits="Root_Admin_ListEWalletBalance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">E-Wallet Balance Ledger
            </h3>
        </div>
            <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                            CssClass="class24" OnClick="btnexportExcel_Click" />
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                         <div class="table-responsive">
                        <asp:GridView ID="gvEWalletBalance" runat="server" 
                                    CssClass="table" AutoGenerateColumns="false"
                                AllowPaging="false" DataKeyNames="EWalletBalanceID" 
                                 OnRowCommand="gvEWalletBalance_RowCommand" OnSorting="gvEWalletBalance_Sorting"
                                AllowSorting="false" ShowHeaderWhenEmpty="true" 
                                    onrowcreated="gvEWalletBalance_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="MemberID" DataField="MemberID" SortExpression="MemberID" />
                                    <asp:BoundField HeaderText="Member Name" DataField="MemberName" SortExpression="MemberName" />
                                    <asp:BoundField HeaderText="Debit" DataField="Debit" SortExpression="Debit" />
                                    <asp:BoundField HeaderText="Credit" DataField="Credit" SortExpression="Credit" />
                                    <asp:BoundField HeaderText="Balance" DataField="Balance" SortExpression="Balance" />
                                    <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkViewDetail" runat="server" ImageUrl="~/Root/images/icon/view_16x16.png" ToolTip="View Details" CommandName="ViewDetail" CommandArgument='<%#Eval("MsrNo") %>'></asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                             </div>
                        </div></div></div></div></div>
</asp:Content>
