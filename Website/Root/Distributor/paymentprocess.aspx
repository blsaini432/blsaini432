<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="paymentprocess.aspx.cs" Inherits="Root_Distributor_paymentprocess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-left: 10px;
            width: 270px;
            height: 185px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Payment Gateway
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
                                    
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                                    </div>

                                    <div class="col-lg-3">
                                        <h4 style="color: green">Fee Rate </h4>
                                    </div>
                                    <div class="col-lg-8">
                                    </div>

                                    </h3>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Debit Cards (Master, VISA) < 2000<code></code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label class="col-form-label">1.5%<code></code></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Debit Cards (Master, VISA) > 2000<code></code></label>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">1.5%</label>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">TRANSACTION LIMIT (on one id)</label>
                                        <label></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Debit Cards (Rupay) - Across slabs<code></code></label>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">1.5%<code></code></label>

                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">SINGEL TRANSECTION - MIN  100/-  MAX 5000/- <code></code></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Credit Cards ( Master, VISA, Rupay )<code></code></label>
                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">1.5%<code></code></label>

                                    </div>
                                    <div class="col-lg-4">
                                        <label class="col-form-label">DAY LIMIT - MAX  25000/- PER DAY <code></code></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Net-banking - ANY BANK<code></code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label class="col-form-label">Maxmium Amount 12 and minimum Amount 1.60%<code></code></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Mobile Wallets - ANY<code></code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label class="col-form-label">1.5%<code></code></label>

                                    </div>
                                    <div class="col-lg-3">
                                        <label class="col-form-label">UPI / BHARAT QR<code></code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label class="col-form-label">0.10%<code></code></label>

                                    </div>


                                </div>
                                <div class="form-group row">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>


        <input type="button" value="OpenModalPopup" id="btn_opendmr" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btn_closedmrc" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_dmrotp" runat="server" PopupControlID="pnltransotp"
            TargetControlID="btn_opendmr" CancelControlID="btn_closedmrc" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnltransotp" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 50%;">
            <div class="page-header">
                <h3 class="page-title">OTP For Transaction
                </h3>
            </div>
            Enter OTP:<asp:TextBox ID="txt_dmrotp" runat="server" Height="25px" Width="152px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dmrotp"
                ErrorMessage="*" ValidationGroup="aas"></asp:RequiredFieldValidator>
            <br />
            <br />
            <table>
                <tr>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

