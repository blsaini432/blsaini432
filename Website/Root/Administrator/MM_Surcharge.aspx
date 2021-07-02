<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="MM_Surcharge.aspx.cs" Inherits="Root_Admin_MM_Surcharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage DMR Commission
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
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
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
                                onrowcommand="gvOperator_RowCommand">
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
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="medit" CssClass="btn btn-primary btn-icon-text" CommandArgument='<%#Container.DataItemIndex+1 %>' />
                                            <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="mdelete" CssClass="btn btn-warning btn-icon-text" CommandArgument='<%#Container.DataItemIndex+1 %>' />
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
                                </div>
                                <div class="form-group row" id="dvbutton" runat="server">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                           
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
           <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPackage" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
    </div>






</asp:Content>
