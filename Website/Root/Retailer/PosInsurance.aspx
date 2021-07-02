<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="PosInsurance.aspx.cs" Inherits="Root_Retailer_PosInsurance" %>

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
            <h3 class="page-title">Pos Registration
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                  <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_name" CssClass="form-control"  runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="txt_name"
                                            SetFocusOnError="true" Display="Dynamic" ErrorMessage="Enter Customer Name" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                </div>
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Email Id<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_email" CssClass="form-control" runat="server"  Style="text-transform: uppercase"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_mobile"
                                            SetFocusOnError="true" ErrorMessage="Enter Mobile Number" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Aadhar Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_aadhar" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_aadhar"
                                            SetFocusOnError="true" ErrorMessage="Enter Aadhar Number" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Pan Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_pan" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_pan"
                                            SetFocusOnError="true" ErrorMessage="Enter Pan Number" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Aadhar Front<code>*</code></label>
                                    </div>
                                       <div class="col-lg-8">
                                    <asp:FileUpload ID="file_aadharfont" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="file_aadharfont" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Aadhar Back<code>*</code></label>
                                    </div>
                                      <div class="col-lg-8">
                                    <asp:FileUpload ID="file_adharback" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="file_adharback" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Pan Card<code>*</code></label>
                                    </div>
                                     <div class="col-lg-8">
                                    <asp:FileUpload ID="file_pan" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="file_pan" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                               <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">10 Mark Sheet<code>*</code></label>
                                    </div>
                                      <div class="col-lg-8">
                                    <asp:FileUpload ID="file_sheet" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="file_sheet" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Cancel chq / passbook /net banking statement latest<code>*</code></label>
                                    </div>
                                       <div class="col-lg-8">
                                    <asp:FileUpload ID="file_passbook" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="file_passbook" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">passport size Photo<code>*</code></label>
                                    </div>
                                   <div class="col-lg-8">
                                    <asp:FileUpload ID="file_photo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="file_photo" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">NOC if you already have POS Code from IRDA<code>*</code></label>
                                    </div>
                                   <div class="col-lg-8">
                                    <asp:FileUpload ID="file_noc" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="file_noc" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

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
             
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
        
    </div>

</asp:Content>

