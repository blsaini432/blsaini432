<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Licpremium.aspx.cs" Inherits="Root_Distributor_Licpremium" %>

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
            <h3 class="page-title">Lic Premium 
            </h3>

            <div class="logo">
                <a href="#">
                    <img src="../../Uploads/Company/Logo/actual/index.png" alt="" style="height: 94px; width: 374px"></a>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy Type   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem>Select Type</asp:ListItem>
                                            <asp:ListItem Value="LICOF">Life Insurance Corporation of India</asp:ListItem>
                                            <asp:ListItem Value="AVLI">Aviva Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="IFLI">India First Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="EXLI">Exide Life Insurance </asp:ListItem>
                                            <asp:ListItem Value="BALI">Bajaj Allianz Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="SLIC">Shriram Life Insurance </asp:ListItem>
                                            <asp:ListItem Value="ICIC">ICICI Prudential Life Insurance </asp:ListItem>
                                            <asp:ListItem Value="FGIL">Future General India Life Insurance </asp:ListItem>
                                            <asp:ListItem Value="SBIL">SBI  Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="HDFC">HDFC Standard  Life Insurance </asp:ListItem>
                                            <asp:ListItem Value="AXLI">Bharti AXA Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="TALI">TATA AIA  Life Insurance</asp:ListItem>
                                            <asp:ListItem Value="FGIL">Future Generali India Life Insurance Company Limited</asp:ListItem>
                                            <asp:ListItem Value="ABSL">Aditya Birla Sun Life Insurance</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="ev"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy number  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_userid" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_userid"
                                            SetFocusOnError="true" ErrorMessage="Enter Policy number " ValidationGroup="ev"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_userid"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:button id="Button1" runat="server" text="Search "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                            validationgroup="ev" onclick="btnsearch_click"></asp:button>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy holder name   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_cardname" CssClass="form-control" runat="server" Enabled="false"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cardname"
                                            SetFocusOnError="true" ErrorMessage="Enter name" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy amount   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_amount" CssClass="form-control" runat="server" Enabled="false"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_amount"
                                            SetFocusOnError="true" ErrorMessage="Enter amount" ValidationGroup="v"></asp:RequiredFieldValidator>
                                      
                                    </div>
                                </div>

                                <%-- <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Due Date <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                                            CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                            PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_dob"
                                            Display="Dynamic" ErrorMessage="Please Enter Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>--%>


                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy payment last date    <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_date" runat="server" MaxLength="50" onkeypress="return false;" Enabled="false"
                                            CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                            PopupButtonID="txt_date" TargetControlID="txt_date">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_date"
                                            Display="Dynamic" ErrorMessage="Please Enter Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <%--    <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy holder Email ID  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_email" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_email"
                                            SetFocusOnError="true" ErrorMessage="Enter email id" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Policy holder mobile number   <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_mobile"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_mobile"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                         <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                         </div>
                                </div>--%>

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
