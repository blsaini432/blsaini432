<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="Recharge_ManageCommission.aspx.cs" Inherits="Root_Admin_ManageCommission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                Manage Recharge Commission
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
                                        <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                            Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                <asp:GridView ID="gvOperator" runat="server" CssClass="table" AutoGenerateColumns="false"
                                    DataKeyNames="OperatorID" Width="100%" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Operator Name" DataField="OperatorName" SortExpression="OperatorName" />
                                        <asp:BoundField HeaderText="Service Type" DataField="ServiceType" SortExpression="ServiceType" />
                                        <asp:BoundField HeaderText="API ID" DataField="ActiveAPI" SortExpression="ActiveAPI" />
                                        <asp:BoundField HeaderText="Active API" DataField="APIName" SortExpression="APIName" />
                                        <asp:TemplateField HeaderText="Commission / Surcharge">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCommission" runat="server" Text='<%# Eval("Commission") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Surcharge">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurcharge" runat="server" Checked='<%# Eval("IsSurcharge") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Flat">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFlat" runat="server" Checked='<%# Eval("IsFlat") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                      No Record Found!!
                                    </EmptyDataTemplate>
                                </asp:GridView></div>
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
