<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Franchise.aspx.cs" Inherits="Root_Distributor_Franchise" %>

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
            <h3 class="page-title">Franchise Apply
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
                                        <label class="col-form-label">Name <code>*</code></label>
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
                                        <asp:TextBox ID="txt_mobiles" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="10" onkeypress="return isNumeric(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_mobiles"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_Mobiles"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="rfv"
                                            ControlToValidate="txt_Mobiles" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                            SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Email <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_email" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_email"
                                            SetFocusOnError="true" ErrorMessage="Enter Email " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Pin Code <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_pin" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_pin"
                                            SetFocusOnError="true" ErrorMessage="Enter Pin Code " ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_pin"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Area <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_area" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_area"
                                            SetFocusOnError="true" ErrorMessage="Enter Area " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Age <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_age" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_age"
                                            SetFocusOnError="true" ErrorMessage="Enter Age " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Educational  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Educational</asp:ListItem>
                                        <asp:ListItem>Below 12th Standard</asp:ListItem>
                                        <asp:ListItem>Passed 12th Standard</asp:ListItem>
                                        <asp:ListItem>Graduate Degree Holder</asp:ListItem>
                                        <asp:ListItem>Post Graduate Degree Holder</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Work Experience <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_exp" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_exp"
                                            SetFocusOnError="true" ErrorMessage="Enter Work Experience " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_exp"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">How did you get to know about us?  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="drop_abouts" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Abouts Us</asp:ListItem>
                                        <asp:ListItem>Newspapers Ad</asp:ListItem>
                                        <asp:ListItem>Interanet Ad</asp:ListItem>
                                        <asp:ListItem>Friend  Or Family</asp:ListItem>
                                        <asp:ListItem>Others</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">If you get an Amazon Easy store, who will run it? <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="drop_amazon" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Amazon Easy store </asp:ListItem>
                                        <asp:ListItem>MySelf</asp:ListItem>
                                        <asp:ListItem>Other Family Member</asp:ListItem>
                                        <asp:ListItem>Hired Staff</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Do you have other businesses<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="drop_business" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select other businesse </asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>                                                                         
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label"> Communication Address (with nearest landmark)<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_address"
                                            SetFocusOnError="true" ErrorMessage="Enter address " ValidationGroup="v"></asp:RequiredFieldValidator>
                                     
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Message<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_message" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_message"
                                            SetFocusOnError="true" ErrorMessage="Enter message " ValidationGroup="v"></asp:RequiredFieldValidator>
                                     
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

