<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Institute.aspx.cs" Inherits="Root_Retailer_Institute" %>

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
            <h3 class="page-title">3D INSTITUTE
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
                                        <label class="col-form-label">DOB   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_date" runat="server" MaxLength="50" onkeypress="return false;"
                                        CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_date" TargetControlID="txt_date">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_date"
                                        Display="Dynamic" ErrorMessage="Please Enter Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Full Address<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_address"
                                            SetFocusOnError="true" ErrorMessage="Enter FullAddress" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Father Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_father" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_father"
                                            SetFocusOnError="true" ErrorMessage="Enter father name" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Father Mobile Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_fatnumber" CssClass="form-control" runat="server" 
                                            autocomplete="off" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_fatnumber"
                                            SetFocusOnError="true" ErrorMessage="Enter Mobile number  " ValidationGroup="v"></asp:RequiredFieldValidator>                            
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_fatnumber"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Qualification<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_quali" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_quali"
                                            SetFocusOnError="true" ErrorMessage="Enter Qualification" ValidationGroup="v"></asp:RequiredFieldValidator>
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

