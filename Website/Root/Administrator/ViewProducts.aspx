<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/adminmaster.master" AutoEventWireup="true" CodeFile="ViewProducts.aspx.cs" Inherits="Root_Administrator_ViewProducts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/script_pop.js" type="text/javascript"></script>
    <link href="../css/style2.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 85px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <div class="content-wrapper">
                <div class="page-header">
                    <h3 class="page-title">List Product Details 
                    </h3>
                </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MMM-yyyy" Animated="False"
                                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:Button ID="Button1" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                                        class="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="info-box">

                                        <div class="table-responsive">

                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                        <div class="box-inner">
                                                            <div class="box-content">
                                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%">
                                                                    <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText table-responsive"
                                                                        AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                                                        PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                                                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="ProductName" DataField="ProductName" SortExpression="ProductName" />
                                                                            <asp:BoundField HeaderText="Per Unit Price" DataField="Priceperunit" SortExpression="Priceperunit" />

                                                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" SortExpression="Quantity" />
                                                                            <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description" />
                                                                            <asp:BoundField HeaderText="Minimumorder" DataField="Miniumorder" SortExpression="Miniumorder" />
                                                                            <asp:TemplateField HeaderText="Image">
                                                                                <ItemTemplate>
                                                                                    <a href="../../Uploads/ProductImage/Actual/<%# Eval("Img") %>" target="_blank">Download
                                                              
                                                                                    </a>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date" SortExpression="AddDate">
                                                                                <ItemTemplate>
                                                                                    <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product update">
                                                                                <ItemTemplate>
                                                                                    <a href="<%# String.Format("Shooping_Cart.aspx?itemid={0}", Eval("itemid")) %>">Update</a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product Delete">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btndelete" runat="server" Text="Delete" CommandArgument='<%#Eval("itemid") %>'
                                                                                        CommandName="deletesuser" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
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
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
