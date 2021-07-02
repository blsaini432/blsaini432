<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="membercommission.aspx.cs" Inherits="Root_Retailer_membercommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title"> AEPS Commission
            </h3>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">

                              
                               <%-- <div class="table-responsive" id="dvadd" runat="server">
                                    <table class="table">
                                        <tr>
                                            <td>Start Range
                                        <asp:TextBox ID="txtfromd" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                            </td>
                                            <td>End Range
                                        <asp:TextBox ID="txttod" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>Commission
                                        <asp:TextBox ID="txtCommission" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>Flat
                                        <asp:CheckBox ID="chkflatd" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary"
                                                    OnClick="btnadd_Click" Text="Add / Update" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOperator" runat="server"
                                        CssClass="table" AutoGenerateColumns="false"
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
                                            <asp:TemplateField HeaderText="Commission">
                                                <ItemTemplate>
                                                    <%# Eval("commision") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Flat">
                                                <ItemTemplate>
                                                    <%# Convert.ToString(Eval("IsFlat")) %>
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
                                
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
               
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>






















