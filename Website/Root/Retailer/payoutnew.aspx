<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="payoutNew.aspx.cs" Inherits="Root_Retailer_payoutNew" %>

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
    <script type="text/javascript">
        function convertTimestamptoTime() {
            var timestamp = Math.round((new Date()).getTime() / 1000);
            alert(timestamp);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Money Transfer
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
                                        <label class="col-form-label">Beneficiary Account<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Acno" runat="server"
                                            autocomplete="off" CssClass="form-control" MaxLength="19" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Acno" runat="server" ControlToValidate="txt_Acno"
                                            SetFocusOnError="true" ErrorMessage="Enter Account Number"
                                            ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Acno" runat="server" ValidChars="0123456789"
                                            TargetControlID="txt_Acno">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_Acno" ID="regExp_Acno"
                                            ValidationExpression="^[\s\S]{9,18}$" runat="server" ErrorMessage="Enter Valid Account Number"
                                            ValidationGroup="vgBeni"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">IFSC Code<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Ifsccode" CssClass="form-control" runat="server" Enabled="false" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_Ifsccode"
                                            SetFocusOnError="true" ErrorMessage="Enter IFSC Code" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Customer Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_name" CssClass="form-control" placeholder="please enter name without space" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="txt_name"
                                            SetFocusOnError="true" Display="Dynamic" ErrorMessage="Enter Customer Name" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator Display="Dynamic" runat="server" ID="Exp_Acno" ValidationGroup="vgBeni" ControlToValidate="txt_name"
                                          ValidationExpression="[^\s]+" ErrorMessage="*Valid characters:space." />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Email Id<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_email" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_email"
                                            SetFocusOnError="true" ErrorMessage="Enter Email Id" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Mobile Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_Ifsccode"
                                            SetFocusOnError="true" ErrorMessage="Enter Mobile Number" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Transaction types<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="Transactionlist" runat="server" CssClass="btn btn-outline-dark  dropdown-toggle" AutoPostBack="True">
                                            <asp:ListItem Value="0">Select Type</asp:ListItem>
                                            <asp:ListItem Value="2">NEFT</asp:ListItem>
                                           
                                            <asp:ListItem Value="4">IMPS</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_Ifsccode"
                                            SetFocusOnError="true" ErrorMessage="Select Transaction types id" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <%--<div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">merchant ref id<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mer" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_Ifsccode"
                                            SetFocusOnError="true" ErrorMessage="Enter merchant ref id" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>--%>
                                <%--<div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">OTP<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_otp" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_Ifsccode"
                                            SetFocusOnError="true" ErrorMessage="Enter OTP Code" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>--%>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Amount" runat="server" autocomplete="off"
                                            MaxLength="10" step="0.00" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amount"
                                            ErrorMessage="Enter Valid Amount" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amount"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>


                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                     <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btn_dmrotp_Click" />
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
        <input type="button" value="OpenModalPopup" id="btn_opendmr" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btn_closedmrc" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_dmrotp" runat="server" PopupControlID="pnltransotp"
            TargetControlID="btn_opendmr" CancelControlID="btn_closedmrc" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnltransotp" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 50%;">
            <div class="page-header">
                <h3 class="page-title">PIN For Transaction
                </h3>
            </div>
            Enter PIN:<asp:TextBox ID="txt_dmrotp" runat="server" Height="25px" Width="152px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dmrotp"
                ErrorMessage="*" ValidationGroup="aas"></asp:RequiredFieldValidator>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btn_dmrotp" runat="server" Text="Submit" CssClass="btn btn-primary"
                            ValidationGroup="aas" Width="104px" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Wait...'" />
                        <asp:Button ID="btn_Closedmr" runat="server" ValidationGroup="daas" CssClass="btn btn-danger"
                            Text="Close" OnClick="btn_Closedmr_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

</asp:Content>

