<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="VirtualAccount.aspx.cs" Inherits="Root_Distributor_VirtualAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
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
            height: 250px;
        }

        .modalPopupRecept {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-left: 10px;
            width: 500px;
            height: 500px;
        }

        .pull-right {
            float: right !important;
            right: 0;
            position: relative;
        }

        .row .form-group.row {
            width: 100%;
        }

        #loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../../Design/images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <link href="../../Design/css/modelpopupdmr.css" rel="stylesheet" />

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                            autocomplete="off" CssClass="form-control" MaxLength="19"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Acno" runat="server" ControlToValidate="txt_Acno"
                                            SetFocusOnError="true" ErrorMessage="Enter Account Number"
                                            ValidationGroup="vgBeni"></asp:RequiredFieldValidator>                                                                      
                                    </div>
                                </div>
                               
                               
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
               <%-- <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>
