<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Emitra_form.aspx.cs" Inherits="Root_Retailer_Emitra_form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Emitra Registration
            </h3>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">

                                         <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Emitra Registration  Fee<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                  <asp:Label ID="lblamount" runat="server" style="color:red"/>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">SSO ID<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_ssoid" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="pan" ErrorMessage="Enter sso is "
                                                    ControlToValidate="txt_ssoid"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Mobile No.<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="pan" ErrorMessage="Enter Customer Mobile No."
                                                    ControlToValidate="txt_mobile"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_mobile"
                                                    ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Email ID<code></code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="pan" ErrorMessage="Enter email ID ."
                                                    ControlToValidate="txt_email"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Shop Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_shopname" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="pan" ErrorMessage="Enter shop Name"
                                                    ControlToValidate="txt_shopname"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Shop Address<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_address" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="pan" ErrorMessage="Enter address "
                                                    ControlToValidate="txt_address"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="file_aadhar" runat="server" />&nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="pan" ErrorMessage="aadhar upload "
                                                           ControlToValidate="file_aadhar"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">

                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload Photo<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="file_photo" runat="server" />&nbsp;   
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="pan" ErrorMessage=" upload photo "
                                                           ControlToValidate="file_photo"></asp:RequiredFieldValidator>                
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload Pan<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="file_pan" runat="server" />&nbsp;
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="pan" ErrorMessage="aadhar upload "
                                                         ControlToValidate="file_pan"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload 10th marksheet<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="file_marsheet" runat="server" />&nbsp;
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="pan" ErrorMessage=" upload 10th marksheet "
                                                     ControlToValidate="file_marsheet"></asp:RequiredFieldValidator>
                                            </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <label class="col-form-label">Police verification<code>*</code></label>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:FileUpload ID="file_Police" runat="server" />&nbsp;
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="pan" ErrorMessage="upload Police verification "
                                                           ControlToValidate="file_Police"></asp:RequiredFieldValidator>
                                                </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label class="col-form-label">Bank detail<code>*</code></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:FileUpload ID="file_bank" runat="server" />&nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="pan" ErrorMessage="upload bank details "
                                                        ControlToValidate="file_bank"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <div class="col-lg-3">
                                                        <label class="col-form-label">Jan adhaar card<code>*</code></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:FileUpload ID="file_jan" runat="server" />&nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="pan" ErrorMessage="upload jan aadhar card "
                                                        ControlToValidate="file_jan"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                           
                                        
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btn_Getbill" runat="server" Text="Submit" ValidationGroup="pan" 
                                                    UseSubmitBehavior="false" CssClass="btn btn-primary"
                                                    OnClick="btn_submit" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>

