<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="AepsPayOut.aspx.cs" Inherits="Root_Retailer_AepsPayOut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">.
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
              <script src="../../Design/js/jquery.min.js" type="text/javascript"></script>

    <script>
        $(document).on('click', '._txnhistory', function ()
        {
            window.location = "AepsNewTranscation.aspx";
        });
        $(document).on('click', '._downloaddriver', function ()
        {
            var Uri = location.origin + "/EKYC_DRIVERS.zip";
            window.location = Uri;
        });
        $(document).on('click', '._payout', function ()
        {
            window.location = "aepspayout.aspx";
        });
        $(document).on('click', '._txnhistorys', function ()
        {
            window.location = "aepsdashboard.aspx";
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
               <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
              AEPS Panel
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="deshboard-form-section">
        <div class="row">
            <div class="col-3">
      </div>
            <div class="col-9">
                <img src="../../Uploads/Yeslogo.jpg" class="img-responsive" style="padding-left: 525px;" />
            </div>
        </div>
        <div class="deshboard-form-menu">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                                     <ul class="nav nav-pills nav-pills-success" id="pills-tab" role="tablist">
                    <li class="nav-item">
                      <a class="_txnhistorys nav-link" id="pills-home-tab" data-toggle="pill" href="#pills-home" role="tab" aria-controls="pills-home" aria-selected="true">Aeps Dashboard</a>
                    </li>
                    <li class="nav-item">
                      <a class="_downloaddriver nav-link" id="pills-profile-tab" data-toggle="tab" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="false">Biomatric Drivers</a>
                    </li>
                    <li class="nav-item">
                      <a class=" _payout nav-link" id="pills-contact-tab" data-toggle="pill" href="#pills-contact" role="tab" aria-controls="pills-contact" aria-selected="false">Aeps Pay Out</a>
                    </li>
                  </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-panel-section">
            <div class="container" style="margin-top: 50px; width: 100%">
                <div class="row">
                    <div class="col-12 grid-margin">
                        <div class="card">
                            <div class="card-body">
                                <p style="color:red">
                                    1. Payout Service will be work on  working days only
                           <br />
                                    2. Service will work between 11 am to 5 PM daily
                                </p>
                                <div class="tab-content tabcontent-border">
                                    <div class="tab-pane active" id="home" role="tabpanel" aria-expanded="true">
                                        <div class="pad-20">
                                                                                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Account Number:<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_Acno" runat="server"
                                                    autocomplete="off" CssClass="form-control" MaxLength="19" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_Acno" runat="server" ControlToValidate="txt_Acno"
                                                    SetFocusOnError="true" ErrorMessage="Enter Account Number"
                                                    ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Acno" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txt_Acno">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_Acno" ID="regExp_Acno"
                                                    ValidationExpression="^[\s\S]{9,18}$" runat="server" ErrorMessage="Enter Valid Account Number"
                                                    ValidationGroup="vgBeni"></asp:RegularExpressionValidator>

                            </div>
                        </div>
                                                                                                                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">IFSC :<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                 <asp:TextBox ID="txt_Ifsccode" CssClass="form-control" ReadOnly="true" runat="server" Style="text-transform: uppercase" 
                                                    autocomplete="off" MaxLength="11"></asp:TextBox>
                                                         <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_Ifsccode" ID="RegularExpressionValidator1"
                                                    ValidationExpression="^[A-Za-z]{4}[a-zA-Z0-9]{7}$" runat="server" ErrorMessage="Enter Valid IFSC Code"
                                                    ValidationGroup="vgBeni"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_Ifsccode"
                                                    SetFocusOnError="true" ErrorMessage="Enter IFSC Code" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                                                                                                  <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Amount :<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_Amount"  runat="server" autocomplete="off"
                                                MaxLength="6" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amount"
                                                ErrorMessage="Enter Amount" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amount"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>


                                                                                   <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-8">
                                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                ValidationGroup="vgBeni"  CssClass="btn btn-primary" onclick="btnSubmit_Click"  />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset"  
                                CssClass="btn btn-primary" onclick="btnReset_Click" />
                            </div>
                        </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      

            <input type="button" value="OpenModalPopup" id="btn_opendmr" runat="server" style="display: none;" />
    <input type="button" value="CloseModalPopup" id="btn_closedmrc" runat="server" style="display: none;" />
    <cc1:ModalPopupExtender ID="mpe_dmrotp" runat="server" PopupControlID="pnltransotp"
        TargetControlID="btn_opendmr" CancelControlID="btn_closedmrc" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnltransotp" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 50%;">
        <h3 align="center">OTP For Transaction</h3>
        Enter OTP:<asp:TextBox ID="txt_dmrotp" runat="server" Height="25px" Width="152px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dmrotp"
            ErrorMessage="*" ValidationGroup="aas"></asp:RequiredFieldValidator>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btn_dmrotp" runat="server" Text="Submit"
                        ValidationGroup="aas" Width="104px" OnClick="btn_dmrotp_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Wait...'" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_Closedmr" runat="server" ValidationGroup="daas"
                            Text="Close" OnClick="btn_Closedmr_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
        </div>
        </div>
                        </div></div></div></div></div>





    
</asp:Content>
