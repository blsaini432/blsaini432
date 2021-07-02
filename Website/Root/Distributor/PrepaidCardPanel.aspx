<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="PrepaidCardPanel.aspx.cs" Inherits="Root_Distributor_PrepaidCardPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <style type="text/css">
        
.modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 1px;
        border-style: solid;
        border-color: black;
        
        padding-left: 10px;
        width: 270px;
        height: 220px;
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                <div class="card-body">
                  <h4 class="card-title">PrepaidCard Panel</h4>
                  <div class="row">
                    <div class="col-4">
                      <ul class="nav nav-pills nav-pills-vertical nav-pills-info" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                        <li class="nav-item">
                          <a class="nav-link active show" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="false">
                            <i class="fa fa-home"></i>
                            Topup
                          </a>                          
                        </li>
                        <li class="nav-item">
                          <a class="nav-link" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">
                            <i class="fa fa-user"></i>
                            Check Balance
                          </a>                          
                        </li>
                        <li class="nav-item">
                          <a class="nav-link" id="v-pills-messages-tab" data-toggle="pill" href="#v-pills-messages" role="tab" aria-controls="v-pills-messages" aria-selected="true">
                            <i class="far fa-envelope-open"></i>
                            New Account
                          </a>                          
                        </li>
                           <li class="nav-item">
                          <a class="nav-link" id="v-pills-datacard-tab" data-toggle="pill" href="#v-pills-datacard" role="tab">
                            <i class="far fa-envelope-open"></i>
                            Upload KYC
                          </a>                          
                        </li>
                           <li class="nav-item">
                          <a class="nav-link" id="v-pills-landline-tab" data-toggle="pill" href="#v-pills-landline" role="tab" aria-controls="v-pills-landline" aria-selected="true">
                            <i class="far fa-envelope-open"></i>
                            Check Account Status
                          </a>                          
                        </li>
                      </ul>
                    </div>
                    <div class="col-8">
                      <div class="tab-content tab-content-vertical" id="v-pills-tabContent">
                        <div class="tab-pane fade active show" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                          <div class="media">
                            <div class="media-body">
                             <asp:UpdatePanel ID="UpdatePanel_Home" runat="server">
                            <ContentTemplate>
                                  <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput1">
                                               Last 4 Digits Of Card Number</label>
                                              <asp:TextBox ID="txt_lastdigitcard" CssClass="form-control" MaxLength="4" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfv" ControlToValidate="txt_lastdigitcard"
                                                    ErrorMessage=" Enter Last 4 Digits Of Card Number" ValidationGroup="vgVerMNos" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_lastdigitcard"
                                                    ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="label-control" for="projectinput6">
                                            Mobile No. Of Customer</label>
                                      <asp:TextBox ID="txt_Mobile" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfv" ControlToValidate="txt_Mobile"
                                                    ErrorMessage="Please Enter Mobile No. !" ValidationGroup="vgVerMNos" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_Mobile"
                                                    ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="rfv"
                                                    ControlToValidate="txt_Mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="vgVerMNos"
                                                    SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="label-control" for="projectinput7">
                                           PAN Numnber</label>
                                       <asp:TextBox ID="txt_customerpan" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="rfv" ControlToValidate="txt_customerpan"
                                                    ErrorMessage="Please Enter PAN number !" ValidationGroup="vgVerMNos" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput4">
                                                Refill Amount</label>
                                               <asp:TextBox ID="txt_tranamount" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfv" ControlToValidate="txt_tranamount"
                                                    ErrorMessage="Please Enter Transaction Amount !" ValidationGroup="vgVerMNos" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                 <div class="col-md-6">
                                        <div class="form-group">
                                             <asp:Button ID="btn_refill" runat="server" Text="Refill" CssClass="btn btn-primary"
                                                    ValidationGroup="vgVerMNos" OnClick="btn_refill_Click"   OnClientClick="if (!Page_ClientValidate('vgVerMNos')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                        UseSubmitBehavior="false"/>

                                 
                               </div>
                                             </div>

                                </div>
                            </ContentTemplate>
                                 <Triggers>
                                        <asp:PostBackTrigger ControlID="btn_refill" />
                                 </Triggers>
                        </asp:UpdatePanel>
                            </div>
                          </div>
                        </div>
                        <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                          <div class="media">
                            <div class="media-body">
                                   <asp:UpdatePanel ID="UpdatePanel_Registration" runat="server">
                            <ContentTemplate>
                                 <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput1">
                                                Mobile No</label>
                                            <asp:TextBox ID="txt_mobilechkbal" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                TabIndex="1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="rfv" ControlToValidate="txt_mobilechkbal"
                                                ErrorMessage="Please Enter Cardholder Mobile No. !" ValidationGroup="vgVerMNodd" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_mobilechkbal"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_mobilechkbal" ErrorMessage="Input correct mobile number !" ValidationGroup="vgVerMNodd"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput1">PAN Number</label>
                                             <asp:TextBox ID="txt_carddigitchkbal" CssClass="form-control" MaxLength="11" autocomplete="off"
                                                TabIndex="1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="rfv" ControlToValidate="txt_carddigitchkbal"
                                                ErrorMessage=" Enter Valid PAN Number" ValidationGroup="vgVerMNodd" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                        </div>  

                                    </div>
                                </div>
                                
                                <div class="form-actions">
                                      <asp:Button ID="btn_checkbalance" runat="server" Text="Check Balance" CssClass="btn btn-raised btn-raised btn-primary" OnClick="btn_checkbalance_Click"
                                                ValidationGroup="vgVerMNodd" OnClientClick="if (!Page_ClientValidate('vgVerMNodd')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                        UseSubmitBehavior="false" />

                                </div>
                                <br />
                                <div class="row">
                                <div id="showbalance" runat="server" visible="false" align="center" class="col-md-12">
                                              <div class="form-group">
                                <table class="table table-bordered">

                                        <tr>
                                            <td><strong>Mobile</strong></td>
                                            <td>
                                                <asp:Label ID="lblchk_cardmobile" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2"><strong>Remainig Balance</strong></td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblchk_cardbalance" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
</div>
                                </div>
                                    </div>
                                </ContentTemplate></asp:UpdatePanel>
                            </div>
                          </div>
                        </div>
                        <div class="tab-pane fade" id="v-pills-messages" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                          <div class="media">
                            <div class="media-body">
                       <asp:UpdatePanel ID="UpdatePanel_Profile" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="label-control" for="projectinput6">
                                                Mobile No.</label>
                                          <asp:TextBox ID="txt_uMobile" runat="server" autocomplete="off" CssClass="form-control" MaxLength="10" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_uMobile" CssClass="rfv" ErrorMessage="Please Enter Mobile No. !" SetFocusOnError="true" ValidationGroup="vgVerMNo"></asp:RequiredFieldValidator>
                                            <filteredtextboxextender id="FilteredTextBoxExtender4" runat="server" targetcontrolid="txt_uMobile" validchars="0123456789">
                                            </filteredtextboxextender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_uMobile" CssClass="rfv" ErrorMessage="Input correct mobile number !" SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$" ValidationGroup="vgVerMNo"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput2">
                                                Email</label>
                                            <asp:TextBox ID="txt_email" runat="server" autocomplete="off" CssClass="form-control" MaxLength="50" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_email" CssClass="rfv" ErrorMessage=" Enter Email" SetFocusOnError="true" ValidationGroup="vgVerMNo"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_email" ErrorMessage="Enter Valid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput3">
                                               PAN</label>
                                          <asp:TextBox ID="txt_PAN" runat="server" autocomplete="off" CssClass="form-control" MaxLength="50" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_PAN" CssClass="rfv" ErrorMessage=" Enter PAN Number" SetFocusOnError="true" ValidationGroup="vgVerMNo"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                             <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput3">
                                               Card Last 4 Digit</label>
                                          <asp:TextBox ID="txt_cardlastdigit" runat="server" autocomplete="off" CssClass="form-control" MaxLength="4" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_cardlastdigit" CssClass="rfv" ErrorMessage=" Enter Card Last 4 Digit" SetFocusOnError="true" ValidationGroup="vgVerMNo"></asp:RequiredFieldValidator>
                                            <filteredtextboxextender id="FilteredTextBoxExtender5" runat="server" targetcontrolid="txt_cardlastdigit" validchars="0123456789">
                                            </filteredtextboxextender>
                                        </div>
                                    </div>
                                </div>

                                             <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput3">
                                               Kit number</label>
                                                                                      <asp:TextBox ID="txt_cardkitnumber" runat="server" autocomplete="off" CssClass="form-control" MaxLength="12" TabIndex="1"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt_cardkitnumber"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button ID="btn_CustomerLogin" runat="server" CssClass="btn btn-primary" OnClick="btn_CustomerLogin_Click" TabIndex="2" Text="Genrate OTP" ValidationGroup="vgVerMNo" OnClientClick="if (!Page_ClientValidate('vgVerMNo')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                        UseSubmitBehavior="false"/>
                                </div> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                          </div>
                        </div>
                        <div class="tab-pane fade" id="v-pills-datacard" role="tabpanel">
                          <div class="media">
                            <div class="media-body">
                                <asp:UpdatePanel ID="UpdatePanel_Datacard" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput1">
                                               Mobile No</label>
                                            <asp:TextBox ID="txt_kycmobile" runat="server" autocomplete="off" CssClass="form-control" MaxLength="10" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_kycmobile" CssClass="rfv" ErrorMessage="Please Enter Mobile No. !" SetFocusOnError="true" ValidationGroup="vgVerMN0o"></asp:RequiredFieldValidator>
                                            <filteredtextboxextender id="FilteredTextBoxExtender7" runat="server" targetcontrolid="txt_kycmobile" validchars="0123456789">
                                            </filteredtextboxextender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_kycmobile" CssClass="rfv" ErrorMessage="Input correct mobile number !" SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$" ValidationGroup="vgVerMN0o"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="label-control" for="projectinput6">
                                            PAN Number</label>
                                       <asp:TextBox ID="txt_kycpannumber" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                TabIndex="1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="rfv" ControlToValidate="txt_kycpannumber"
                                                ErrorMessage="Please Enter PAN number !" ValidationGroup="vgVerMN0o" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="label-control" for="projectinput7">
                                                AadharCard</label>
                                          <asp:FileUpload ID="fup_aadhar" runat="server" CssClass="form-control" />
                                            <span>* Only in PDF (size max 2 MB)</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="rfv" ControlToValidate="fup_aadhar"
                                                ErrorMessage="Please upload self attested aadharcard" ValidationGroup="vgVerMN0o" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                 <div class="form-actions">
                         
<asp:Button ID="btn_uploadkyc" runat="server" Text="Upload KYC" class="btn btn-raised btn-raised btn-primary" OnClientClick="if (!Page_ClientValidate('vg3')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                ValidationGroup="vgVerMN0o" TabIndex="2" OnClick="btn_uploadkyc_Click"  UseSubmitBehavior="false" />
                                 
                                     </div>
                            </ContentTemplate>
                                     <Triggers>

                            <asp:PostBackTrigger ControlID="btn_uploadkyc" />

                        </Triggers>
                        </asp:UpdatePanel>
                                </div></div>
                        </div>

                        <div class="tab-pane fade" id="v-pills-landline" role="tabpanel">
                          <div class="media">
                            <div class="media-body">
                                 <asp:UpdatePanel ID="UpdatePanel_Landline" runat="server">
                            <ContentTemplate> 
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="projectinput2">
                                               Mobile Number
                                            </label><br/>
                                          <asp:TextBox ID="txt_acocuntmobile" CssClass="form-control" MaxLength="15" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="rfv" ControlToValidate="txt_acocuntmobile"
                                                    ErrorMessage=" Enter Acocunt Mobile" ValidationGroup="vgVerdMNo" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                          
                                         
                                        </div>
                                    </div>
                                      <div class="col-md-6">
                                        <div class="form-group">
                                              <label for="projectinput2">
                                               PAN Number
                                            </label><br/>
                                              <asp:TextBox ID="txt_accountpan" CssClass="form-control" MaxLength="11" autocomplete="off"
                                                    TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfv" ControlToValidate="txt_accountpan"
                                                    ErrorMessage="Enter PAN Number" ValidationGroup="vgVerdMNo" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                          </div>
                                    
                                </div>
       
                                <div class="form-actions">
                                    <asp:Button ID="btn_activatecard" runat="server" Text="Check Status" CssClass="btn btn-primary" OnClick="btn_activatecard_Click"
                                                    ValidationGroup="vgVerdMNo" OnClientClick="if (!Page_ClientValidate('vgVerdMNo')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                        UseSubmitBehavior="false"  />
                                  
                               
                                </div>
                                <br />
                                <div class="row">
                                <div class="col-md-12" id="card_status" runat="server" visible="false">
                                       <div class="form-group">
                                <table class="table table-bordered">
                                    <tr>
                                        <td colspan="2" style="background-color: brown; color: white;"><strong>Personal Information</strong></td>
                                    </tr>
                                    <tr>
                                        <td><strong>Card Number </strong></td>
                                        <td>
                                            <asp:Label ID="lbl_accountcardnumber" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Card Holder Name</strong></td>
                                        <td>
                                            <asp:Label ID="lblcardholder_name" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Mobile</strong></td>
                                        <td class="auto-style3">
                                            <asp:Label ID="lblchk_accountmobile" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2"><strong>Email</strong></td>
                                        <td class="auto-style2">
                                            <asp:Label ID="lblaccount_email" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2" colspan="2" style="background-color: brown; color: white;"><strong>Card Staus</strong></td>
                                    </tr>
                                    <tr>
                                        <td><strong>Card Type</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_cardtype" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Card Network</strong></td>
                                        <td class="auto-style3">
                                            <asp:Label ID="lbl_cardnetwork" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Card Business</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_cardbin" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Email</strong></td>
                                        <td class="auto-style3">
                                            <asp:Label ID="lbl_card_status" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>ATM Status</strong></td>
                                        <td class="auto-style3">
                                            <asp:Label ID="lbl_atm_status" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Kit Number</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_kitnumber" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Expiry Date</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_expiraydate" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>KYC Status</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_userkycmode" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Card Status</strong></td>
                                        <td>
                                            <asp:Label ID="lbl_cardstatus" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                           </div>
                            </div>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

        
        <input type="button" value="OpenModalPopup" id="btn_openremitterotp" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btn_closeotp" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="btn_openremitterotp"
            CancelControlID="btn_closeotp" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 40%;height: 250px;">
            <h4 align="center">OTP For Verification</h4>
            <table class="table table-bordered">
                <tr>
                    <td>Enter Email OTP:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_emailotp" runat="server" Height="25px"
                            Width="152px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                            ControlToValidate="txt_emailotp" ErrorMessage="*" ValidationGroup="sas"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Enter Mobile OTP:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_mobileotp" runat="server" Height="25px"
                            Width="152px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                            ControlToValidate="txt_mobileotp" ErrorMessage="*" ValidationGroup="sas"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btn_remiitervalidate" runat="server" Text="Submit" OnClick="btn_remiitervalidate_Click" CssClass="btn btn-primary"
                            ValidationGroup="sas" Width="104px" />
                        <asp:Button ID="btn_closermei" runat="server" OnClick="btn_closermei_Click" Text="Close" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
</asp:Content>

