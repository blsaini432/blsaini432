<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="Managewaterbillcommission.aspx.cs" Inherits="Root_Admin_Managewaterbillcommission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage Water Bill  Commission
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
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOperator" runat="server" CssClass="table" AutoGenerateColumns="false"
                                        Width="100%" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Operator Name" DataField="Serviceboard" SortExpression="Serviceboard" />
                                          
                                            <asp:TemplateField HeaderText="Commission">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCommission" runat="server" Text='<%# Eval("Commission") %>'></asp:TextBox>
                                                     
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Is Flat">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFlat" runat="server" Checked='<%# Eval("IsFlat") %>' />
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

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPackage" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

