<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListPackage.aspx.cs" Inherits="Root_Admin_ListPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                List Package
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                 <asp:GridView ID="gvPackage" runat="server" CssClass="table table-bordered table-responsive" 
                            AutoGenerateColumns="false" AllowPaging="false" DataKeyNames="PackageID" OnPageIndexChanging="gvPackage_PageIndexChanging"
                                PageSize="10"  OnRowCommand="gvPackage_RowCommand" OnSorting="gvPackage_Sorting"
                                AllowSorting="false" ShowHeaderWhenEmpty="true" onrowcreated="gvPackage_RowCreated"> 
                                    
                                <Columns>
                                    <asp:BoundField HeaderText="PackageName" DataField="PackageName" SortExpression="PackageName" />
                                    <asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" />
                                    <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" />
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <a href="ManagePackage.aspx?id=<%#Eval("PackageID") %>" title="Edit this record" class="btn btn-dark btn-icon-text">
                                               Edit
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnIsActive" runat="server" CssClass="btn btn-outline-warning btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "DeActivate": "Activate" %>'
                                                AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>' CommandName="IsActive"
                                                CommandArgument='<%#Eval("PackageID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                </div></div></div></div></ContentTemplate></asp:UpdatePanel></div>

</asp:Content>
