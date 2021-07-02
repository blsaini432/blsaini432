<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="UPIGateway_surchage.aspx.cs" Inherits="Root_Admin_UPIGateway_surchage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage UPI Gateway Surcharge
            </h3>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
       <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Package Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                      <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPackage"
                                Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="table-responsive" id="dvadd" runat="server">
                                                  <table class="table">
                                        <tr>
                                            <td>Start Range
                                       <asp:TextBox ID="txtfromd" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                            </td>
                                            <td>End Range
                                        <asp:TextBox ID="txttod" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>Surcharge
                                        <asp:TextBox ID="txtsurcharged" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                          <%--  <td><asp:DropDownList ID="Transactionlist" runat="server" CssClass="btn btn-outline-dark  dropdown-toggle" AutoPostBack="True">
                                            <asp:ListItem Value="0">Select Type</asp:ListItem>
                                            <asp:ListItem Value="2">NEFT</asp:ListItem>
                                            <asp:ListItem Value="4">IMPS</asp:ListItem>
                                        </asp:DropDownList></td>--%>
                                            <td>Flat
                                        <asp:CheckBox ID="chkflatd" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary" 
                                            onclick="btnadd_Click" Text="Add / Update" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOperator" runat="server"
                                CssClass="table table-bordered table-hover tablesorter" AutoGenerateColumns="false"
                                DataKeyNames="id" Width="100%" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvOperator_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="StartRange">
                                        <ItemTemplate>
                                            <%# Eval("startval") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EndRange">
                                        <ItemTemplate>
                                            <%# Eval("endval") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surcharge">
                                        <ItemTemplate>
                                            <%# Eval("surcharge") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Flat">
                                        <ItemTemplate>
                                            <%# Convert.ToString(Eval("IsFlat")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="mode">
                                        <ItemTemplate>
                                            <%# Convert.ToString(Eval("modetype")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="medit" CommandArgument='<%#Container.DataItemIndex+1 %>' />
                                            <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="mdelete" CommandArgument='<%#Container.DataItemIndex+1 %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">
                                        No Record Found !
                                    </div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>
                                </div>
                                <div class="form-group row" id="dvbutton" runat="server">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
