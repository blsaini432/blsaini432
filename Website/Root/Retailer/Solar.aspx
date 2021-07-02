<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Solar.aspx.cs" Inherits="Root_Retailer_Solar" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Solar Energy
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
                                    <label class="col-form-label" style="color: green">Amount For solar energy:--</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">FRANCHISEE CODE<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Acno" runat="server"
                                            autocomplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Acno" runat="server" ControlToValidate="txt_Acno"
                                            SetFocusOnError="true" ErrorMessage="Enter Account Number"
                                            ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                       
                                       

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">CUSTOMER NAME<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_name" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_name"
                                            SetFocusOnError="true" ErrorMessage="Enter Name " ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">FATHER NAME<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_fathername" runat="server" autocomplete="off"
                                            MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_fathername"
                                            ErrorMessage="Enter Father name" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                       

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">MOBILE NUMBER<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" runat="server"
                                            autocomplete="off" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_mobile"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile Number"
                                            ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                            TargetControlID="txt_Acno">
                                        </cc1:FilteredTextBoxExtender>
                                        

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">STATE<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_state" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_state"
                                            SetFocusOnError="true" ErrorMessage="Enter state " ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">CITY/TOWN<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_city" runat="server" autocomplete="off"
                                            MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_city"
                                            ErrorMessage="Enter city " ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                       

                                    </div>
                                </div>
                                <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">YOU ARE INTERSTED IN<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="RadioButtonList1" runat="server" AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem>Select Type</asp:ListItem>
                                        <asp:ListItem>WATER PUMPS</asp:ListItem>
                                        <asp:ListItem>ROOFTOP SYSTEMS</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">HP TYPES<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_hp" runat="server" autocomplete="off"
                                            MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_hp"
                                            ErrorMessage="Enter Valid Amount" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                       

                                    </div>
                                </div>

                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <label class="col-form-label">AADHAR<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:FileUpload ID="file_adhar" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="file_adhar" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <label class="col-form-label">PAN<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:FileUpload ID="file_pan" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="file_pan" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <label class="col-form-label">BANK PASSBOOK<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:FileUpload ID="file_bank" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="file_bank" ErrorMessage="*" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

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

