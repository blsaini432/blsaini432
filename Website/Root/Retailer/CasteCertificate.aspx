<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="CasteCertificate.aspx.cs" Inherits="Root_Distributor_CasteCertificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Caste Certificate
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
                                                <label class="col-form-label">Caste Certificate  Fee<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                  <asp:Label ID="lblamount" runat="server" style="color:red"/>
                                            </div>
                                        </div>
                                         <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Customer Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_name" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="pan" ErrorMessage="Enter Customer Name"
                                                    ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Father Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_father" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="pan" ErrorMessage="Enter Father Name"
                                                    ControlToValidate="txt_father"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Customer Mobile No.<code>*</code></label>
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
                                                <label class="col-form-label">Date Of Birth <code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_date" runat="server" MaxLength="50" onkeypress="return false;"
                                                    CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                                    PopupButtonID="txt_date" TargetControlID="txt_date">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_date"
                                                    Display="Dynamic" ErrorMessage="Please Enter  Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                                    ValidationGroup="getbill"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="fu_Identity" runat="server" />&nbsp;
                        <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_aadhar_Click" />


                                            </div>
                                        </div>

                                        <div class="form-group row">

                                            <div class="col-lg-3">
                                                <label class="col-form-label">Upload Photo<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:FileUpload ID="fu_Address" runat="server" />&nbsp;
                        <asp:Button ID="btn_Address" runat="server" Text="Upload PAN" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_photo_Click" Width="155px" />

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btn_Getbill" runat="server" Text="Submit" ValidationGroup="pan" OnClientClick="if (!Page_ClientValidate('getbill')){ return false; } this.disabled = true; this.value = 'Processing...';"
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

