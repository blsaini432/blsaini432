<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="ChangeTransactionpassword.aspx.cs" Inherits="Root_Retailer_ChangeTransactionpassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
            Change Transaction Password
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
                                <label class="col-form-label">Current Transaction Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                                          <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="4" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Please Enter Current Pin !"
                                ControlToValidate="txtCurrentPassword" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="txtTransactionPassword_FilteredTextBoxExtender"
                                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCurrentPassword">
                                                    </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>


                                                
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">New Transaction Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                        <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="4" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter New Pin !"
                                ControlToValidate="txtNewPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtNewPassword">
                                                    </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>


                                                
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Confirm Transaction Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                  <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="4" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please Enter Confirm Pin  !"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Confirm Pin does not match"
                                SetFocusOnError="True" ValidationGroup="v"></asp:CompareValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtConfirmPassword">
                                                    </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>

                                      <div class="form-group row">
                            <div class="col-lg-3">
                                
                            </div>
                            <div class="col-lg-8">
   <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" CssClass="btn btn-primary"/>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger" />
                            </div>
                        </div>

                        </div></div></div></div></ContentTemplate>      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers></asp:UpdatePanel></div>

</asp:Content>

