<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="ViewProducts.aspx.cs" Inherits="Root_Retailer_ViewProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="page-header">
                    <h3 class="page-title">Buy Product 
                    </h3>
                </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">

                                <div class="info-box">

                                    <div class="table-responsive">
                                        <div id="loader" style='display: none;'>
                                            <img src='../../Design/images/pageloader.gif' width='32px' height='32px'>
                                        </div>
                                        <asp:GridView ID="gv_Transaction" runat="server" CssClass="table table-bordered table-striped dtable"
                                            AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="itemid" Width="100%"
                                            AllowSorting="false" ShowHeaderWhenEmpty="true"
                                            PageSize="10" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="lblitemid" runat="server" Value='<%#Eval("itemid")%>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProductName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProductImage">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"../../Uploads/ProductImage/Actual/"+Eval("Img")%>' Height="120px" Width="150px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Per Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpriceperunit" runat="server" Text='<%#Eval("Priceperunit")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" SortExpression="Quantity" />

                                                <asp:TemplateField HeaderText="MinimumOrder">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblminiumorder" runat="server" Text='<%#Eval("Miniumorder")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NoOfProduct">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtnoofproduct" runat="server" OnTextChanged="txtnoofproduct_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount(In INR)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttotalamount" runat="server" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnsubmit" runat="server" Text="Buy" CssClass="btn btn-primary" OnClick="btnsubmit_Click" OnClientClick="return confirm('Are you sure you want to buy this product?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
