<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Recharge_Balance.aspx.cs" Inherits="Root_Admin_Balance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title"> API Balance Report
            </h3>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <asp:GridView ID="gvAPI" runat="server" CssClass="table" 
                    AutoGenerateColumns="false"  DataKeyNames="APIID" ShowHeaderWhenEmpty="true"  OnRowDataBound="gvAPI_RowDataBound" onrowcreated="gvAPI_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="API ID" SortExpression="APIID">
                                <ItemTemplate>
                                    <asp:Literal ID="litAPIID" runat="server" Text='<%#Eval("APIID") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="API Name" DataField="APIName" SortExpression="APIName" />
                            <asp:TemplateField HeaderText="Balance URL" SortExpression="BalanceURL">
                                <ItemTemplate>
                                    <asp:Literal ID="litBalanceURL" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance" SortExpression="Balance">
                                <ItemTemplate>
                                <span class="WebRupee">Rs.</span>
                                    <asp:Literal ID="litBalance" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="EmptyDataTemplate">
                                No Record Found !</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                        </div>
                    </div></div></div></ContentTemplate></asp:UpdatePanel></div>



</asp:Content>
