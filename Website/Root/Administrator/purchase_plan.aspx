<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="purchase_plan.aspx.cs" Inherits="root_admin_purchase_plan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/chosen/jquery.min.js"></script>
    <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
    <link href="../../Scripts/chosen/chosen.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Free Plan
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
                                        <label class="col-form-label">MemberId<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlmemberid" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlmemberid_SelectedIndexChanged" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="ddlmemberid" ErrorMessage="*" InitialValue="Select MemberId"
                                            ValidationGroup="v0"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblMemberName" runat="server" CssClass="green" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Request For Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlmymembertype" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlMemberType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="ddlmymembertype" Display="Dynamic"
                                            ErrorMessage="Please Select member type !" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Total Remaining ID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Label ID="lblremainingid" runat="server" Font-Bold="True"
                                            ForeColor="#336600" Text="Label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Method<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:RadioButtonList ID="radiobuttonmethod" runat="server" AutoPostBack="True"
                                            Height="31px" OnSelectedIndexChanged="radiobuttonmethod_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" CssClass="form-control">
                                            <asp:ListItem Selected="True">Credit</asp:ListItem>
                                            <asp:ListItem>Debit</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblmaxid" runat="server" CssClass="red" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">No Of ID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="DSO1_totid" runat="server" AutoPostBack="True"
                                            ClientIDMode="Static" OnTextChanged="DSO1_totid_TextChanged" CssClass="form-control"
                                            Text="0"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="DSO1_totid_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="DSO1_totid">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="DSO1_totid" ErrorMessage="*" ValidationGroup="v0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSuccess" runat="server" class="btn btn-primary"
                                            OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="v0" Width="103px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <script>
                    $('#<%=ddlmemberid.ClientID%>').chosen();
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


