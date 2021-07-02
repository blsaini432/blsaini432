<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageMemberBanker.aspx.cs" Inherits="Root_Admin_ManageMemberBanker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
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
                                        <label class="col-form-label">Bank Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="ddlBankName"
                                            Display="Dynamic" ErrorMessage="Please Select Bank Name !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Branch Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtBankBranch" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBankBranch" runat="server" ControlToValidate="txtBankBranch"
                                            Display="Dynamic" ErrorMessage="Please Enter Branch Name !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Account Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select Account Type</asp:ListItem>
                                            <asp:ListItem Value="Saving Account">Saving Account</asp:ListItem>
                                            <asp:ListItem Value="Current Account">Current Account</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAccountType" runat="server" ControlToValidate="ddlAccountType"
                                            Display="Dynamic" ErrorMessage="Please Select Account Type !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Account Holder Name</label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtBankDesc" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Account Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtAccountNumber" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server" ControlToValidate="txtAccountNumber"
                                            Display="Dynamic" ErrorMessage="Please Enter Account Number !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Acno" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtAccountNumber">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAccountNumber" ID="regExp_Acno"
                                                    ValidationExpression="^[\s\S]{9,18}$" runat="server" ErrorMessage="Enter correct bank Account Number."
                                                    ValidationGroup="v"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">IFSC Code</label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtIFSCCode" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" CssClass="btn btn-danger" />
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
            </Triggers>
        </asp:UpdatePanel>
    </div>




</asp:Content>







