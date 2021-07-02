<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Business.aspx.cs" Inherits="Root_Distributor_Business" %>

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
            <h3 class="page-title">Business With US
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
                                        <label class="col-form-label">Business Partner <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                            <asp:ListItem>3D GPS Traking System </asp:ListItem>
                                            <asp:ListItem>3D 4k Full HD LED</asp:ListItem>
                                            <asp:ListItem>3D Anti Radiation chip </asp:ListItem>
                                            <asp:ListItem>3D Health </asp:ListItem>
                                            <asp:ListItem>3D Store -99  </asp:ListItem>
                                            <asp:ListItem>3D Mart </asp:ListItem>
                                            <asp:ListItem>3D E-cycle</asp:ListItem>
                                            <asp:ListItem>3D E-bike  </asp:ListItem>
                                            <asp:ListItem>3D E-Rikshya </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Name<code>*</code></label>
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
                                        <label class="col-form-label">Mobile Number   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_mobile"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                            ControlToValidate="txt_mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                            SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_mobile"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Full Address<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_address"
                                            SetFocusOnError="true" ErrorMessage="Enter FullAddress" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Present Business <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_business" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_business"
                                            SetFocusOnError="true" ErrorMessage="Enter Present Busines " ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Experience<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_experance" CssClass="form-control" runat="server" placeholder="1 year"
                                            autocomplete="off" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_experance"
                                            SetFocusOnError="true" ErrorMessage="Enter Experience  " ValidationGroup="v"></asp:RequiredFieldValidator>
                                      
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_experance"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Present Income <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_income" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_income"
                                            SetFocusOnError="true" ErrorMessage="Enter Present Income " ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Invest   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_rent" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_rent"
                                            SetFocusOnError="true" ErrorMessage="Enter Invest Rent" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">What Kind of Business <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:RadioButtonList ID="rdoBtnLstGender" runat="server" CssClass="form-control" Width="193px" RepeatDirection="Horizontal">
                                            <asp:ListItem>OWN</asp:ListItem>
                                            <asp:ListItem>Partnship</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:button id="btnSubmit" runat="server" text="Submit "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                            validationgroup="v" onclick="btnSubmit_Click"></asp:button>
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

