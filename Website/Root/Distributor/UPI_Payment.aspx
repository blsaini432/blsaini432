<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="UPI_Payment.aspx.cs" Inherits="Root_Distributor_UPI_Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">UPI Payment Gateway
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
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Amount" runat="server" autocomplete="off"
                                            MaxLength="6" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amount"
                                            ErrorMessage="Enter Valid Amount" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amount"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                   <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">UPI ID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_upi" runat="server" autocomplete="off"
                                            MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_upi"
                                            ErrorMessage="Enter Valid UPI ID" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                      
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" type="button" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset"
                                            CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

</asp:Content>

