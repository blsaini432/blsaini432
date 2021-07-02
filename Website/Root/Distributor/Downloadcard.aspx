<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Downloadcard.aspx.cs" Inherits="Root_Distributor_Downloadcard" %>

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
            <h3 class="page-title">Download Card
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <label>Per Card Download Charge Rs 15 </label>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">HHID NO <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_userid" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_userid"
                                            SetFocusOnError="true" ErrorMessage="Enter HHID NO " ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Card Holder Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_cardname" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_userid"
                                            SetFocusOnError="true" ErrorMessage="Enter name" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:button id="btnSubmit" runat="server" text="Submit "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                            onclientclick="return confirmation();" onclick="btnSubmit_Click"></asp:button>
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

