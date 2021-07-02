<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="AepsWallet.aspx.cs" Inherits="Root_Retailer_AepsWallet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        var isSubmitted = false;
        var isSubmittedd = false;

        function preventMultipleSubmissions() {

            if (!isSubmitted) {

                $('#<%=btn_transferbank.ClientID %>').val('Submitting.. Plz Wait..');
      isSubmitted = true;
      return true;

  }

  else {

      return false;

  }

}

function preventMultipleSubmissions1() {

    if (!isSubmittedd) {

        $('#<%=btn_transferwallet.ClientID %>').val('Submitting.. Plz Wait..');
                 isSubmittedd = true;
                 return true;

             }

             else {

                 return false;

             }

         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">AEPS Wallet</h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Total AEPS Limit<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label ID="lbl_walletbal" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Fund Request Balance<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label ID="lbl_fzbal" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">

                                <label class="col-form-label">Transfer Type<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <label class="form-check-label">
                                    <asp:RadioButtonList ID="rbttransfertype" runat="server" CssClass="form-check-input" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbttransfertype_SelectedIndexChanged" AutoPostBack="true">
                                        
                                        <asp:ListItem Value="TM" Selected="True">Transfer MainWallet</asp:ListItem>
                                        <asp:ListItem Value="TB">Transfer Bank</asp:ListItem>
                                    </asp:RadioButtonList></label>
                            </div>
                        </div>

                        <div id="divbb" runat="server" visible="false">  
                                                  <div class="form-group row">
                            <div class="col-lg-3">

                                <label class="col-form-label">Bank Name<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <label class="form-check-label">
                                    <asp:Label ID="lblbankname" runat="server" Text="Not updated Yet"></asp:Label>
                                </label>
                            </div>
                        </div>
                                         <div class="form-group row">
                            <div class="col-lg-3">

                                <label class="col-form-label">Bank Account<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <label class="form-check-label">
                                    <asp:Label ID="lblbankaccount" runat="server" Text="Not updated Yet"></asp:Label>
                                </label>
                            </div>
                        </div>

                                         <div class="form-group row">
                            <div class="col-lg-3">

                                <label class="col-form-label">IFSC<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <label class="form-check-label">
                                    <asp:Label ID="lblbankifsc" runat="server" Text="Not updated Yet"></asp:Label>
                                </label>
                            </div>
                        </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Withdrwal AEPS Limit<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label ID="lbl_usebal" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Enter Withdrwal Amount<code></code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amount"
                                    ErrorMessage="Please Enter Amount"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amount"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-8">
                                <asp:Button ID="btn_transferbank" runat="server" Text="Transfer Bank" CssClass="btn btn-primary btn-fw" OnClick="btn_transferbank_Click" OnClientClick="preventMultipleSubmissions()" Visible="false" />
                                <asp:Button ID="btn_transferwallet" runat="server" Text="Transfer Main Wallet" CssClass="btn btn-success" OnClick="btn_transferwallet_Click" OnClientClick="preventMultipleSubmissions1()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

