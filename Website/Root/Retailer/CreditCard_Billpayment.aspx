<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="CreditCard_Billpayment.aspx.cs" Inherits="Root_Retailer_CreditCard_Billpayment" %>

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
        function confirmation() {
            if (confirm('are you sure you want to Submit Data?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
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
            <h3 class="page-title">Credit Card Bill Payment
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
                                        <label class="col-form-label">Credit card number   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_userid" CssClass="form-control" onkeypress="return isNumeric(event)" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_userid"
                                            SetFocusOnError="true" ErrorMessage="Enter Policy number " ValidationGroup="v"></asp:RequiredFieldValidator>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_userid"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                         </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Card holder name    <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_cardname" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cardname"
                                            SetFocusOnError="true" ErrorMessage="Enter name" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                    </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Card Type  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Card Type</asp:ListItem>
                                        <asp:ListItem>American express  </asp:ListItem>
                                        <asp:ListItem>Master card  </asp:ListItem>
                                        <asp:ListItem>Rupay card </asp:ListItem>
                                        <asp:ListItem>Visa card  </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Bill amount   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_amounts" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_amounts"
                                            SetFocusOnError="true" ErrorMessage="Enter amount" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_amounts"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                
                                
                                 
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Card holder mobile number   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="10" onkeypress="return isNumeric(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_mobile"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_Mobile"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_Mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:button id="btnSubmit" runat="server" text="Submit "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                             ValidationGroup="v" onclick="btnSubmit_Click"></asp:button>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
</asp:Content>

