<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="dmr_old.aspx.cs" Inherits="Root_Retailer_dmr" %>
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

    <script type="text/javascript">

        function PrintPanel() {
            var panel = document.getElementById("<%=dv_Print_Recipt.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Transaction Receipt</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        var app = angular.module("distributorApp", ['angularUtils.directives.dirPagination']);
        app.controller("distributorCntrl", function ($scope, $http, $timeout, $filter) {

            $scope.GetBeniRecord = function (id) {
                //alert("test");
                debugger;
                GetBeniDetail(id);
            }
            function GetBeniDetail(id) {
                $http({
                    url: '../Distributor/dmr.aspx/GetBeniDetail',
                    method: "POST",
                    data: { Id: id }
                }).success(function (response) {
                    $("#loader").hide();
                    $scope.getbenidetails = response.d;
                })
            };



            $scope.GetBeniDelete = function (id) {
                //alert("test");
                debugger;
                GetBeniDeleteDetail(id);
            }
            function GetBeniDeleteDetail(id) {
                $http({
                    url: '../Distributor/dmr.aspx/GetBeniDeleteDetail',
                    method: "POST",
                    data: { Id: id }
                }).success(function (response) {
                    alert(response.d);
                    if (response.d == "TXN") {
                        $("#openModaltransfer").dialog({
                            title: "jQuery Modal Dialog Popup",
                            buttons: {
                                Close: function () {
                                    $(this).dialog('close');
                                }
                            },
                            modal: true
                        });
                        return false;

                        //$("#loader").hide();
                        //  $("#openModaltransfer .modal-title").html(title);
                        // $("#openModaltransfer .modal-body").html(body);
                        //$("#openModaltransfer").modal("show");

                    }
                    else {
                        alert(response.d);
                    }

                })
            };
        });
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
        function ShowProgressBar(btn) {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
            document.getElementById(btn.id).disabled = true;
            document.getElementById(btn.id).value = 'Submitting...';
        }
        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
            updateProgress.style.display = "block";
        }

        function showbeniProgress() {
            var updateProgress = $get("<%= UpdateProgress2.ClientID %>");
            updateProgress.style.display = "block";
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
       <div ng-app="distributorApp"  ng-controller="distributorCntrl">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h3>
        </div>
           <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <asp:Repeater runat="server" ID="repeater1">
                            <ItemTemplate>

                                <li data-transition="slideright" data-slotamount="1" data-masterspeed="1000" data-delay="4000" data-saveperformance="off">
                                    <asp:Image ID="myCarousel" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                                </li>
                         </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
           <div class="col-md-12 grid-margin stretch-card d-none d-md-flex">
              <div class="card">
                <div class="card-body">
           <div class="row" id="divhome" runat="server">
                    <div class="col-4">
                      <ul class="nav nav-tabs nav-tabs-vertical" role="tablist">
                        <li class="nav-item">
                          <a class="nav-link" id="home-tab-vertical" data-toggle="tab" href="#home-2" role="tab" aria-controls="home-2" aria-selected="false">
                         New Remitter Register
                          <i class="fa fa-home text-info ml-2"></i>
                          </a>
                        </li>
                        <li class="nav-item">
                          <a class="nav-link active show" id="profile-tab-vertical" data-toggle="tab" href="#profile-2" role="tab" aria-controls="profile-2" aria-selected="true">
                          Remitter Login
                          <i class="fa fa-user text-danger ml-2"></i>
                          </a>
                        </li>
                      </ul>
                    </div>
                    <div class="col-8">
                      <div class="tab-content tab-content-vertical">
                        <div class="tab-pane fade" id="home-2" role="tabpanel" aria-labelledby="home-tab-vertical">
                  <div class="row grid-margin" id="divremitter" runat="server" visible="true">
            <div class="col-12">
                <asp:UpdatePanel ID="up2" runat="server">
                    <ContentTemplate>
                <div class="card">
                    <div class="card-body">
                                 <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name<code>*</code></label>
                            </div>
                            <div class="col">
                             <asp:TextBox ID="txt_Name" autocomplete="off" MaxLength="30" CssClass="form-control" placeholder="Enter Name"
                                                TabIndex="1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_name" runat="server" CssClass="rfv" ControlToValidate="txt_Name"
                                                ErrorMessage="Please Enter Name !" ValidationGroup="vg_reg" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regExp_Allowtext" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_Name" ErrorMessage="Only characters allowed !" ValidationGroup="vg_reg"
                                                SetFocusOnError="true" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>
                            </div>
                  </div>

                                      <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">SurName<code>*</code></label>
                            </div>
                            <div class="col">
                           <asp:TextBox ID="txt_SurName" autocomplete="off" MaxLength="30" CssClass="form-control"
                                                placeholder="Enter SurName" TabIndex="1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_SurName" ErrorMessage="Please Enter SurName !" ValidationGroup="vg_reg"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_SurName" ErrorMessage="Only characters allowed !" ValidationGroup="vg_reg"
                                                SetFocusOnError="true" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>
                            </div>
                  </div>

                                  <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mobile<code>*</code></label>
                            </div>
                            <div class="col">
                            <asp:TextBox ID="txt_Mobile" CssClass="form-control" MaxLength="10" autocomplete="off"
                                                placeholder="Enter Customer Mobile no" TabIndex="2" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_Mobile" runat="server" CssClass="rfv" ControlToValidate="txt_Mobile"
                                                ErrorMessage="Please Enter Mobile No. !" ValidationGroup="vg_reg" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_Mobile"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_Mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="vg_reg"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                            </div>
                  </div>

                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pin Code<code>*</code></label>
                            </div>
                            <div class="col">
                            <asp:TextBox ID="txt_Pin" autocomplete="off" MaxLength="6" CssClass="form-control" placeholder="Enter Pin Code"
                                                TabIndex="3" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_Pin" runat="server" CssClass="rfv" ControlToValidate="txt_Pin"
                                                ErrorMessage="Please Enter Pin Code !" ValidationGroup="vg_reg" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Pin" runat="server" TargetControlID="txt_Pin"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>
                                              <div class="form-group row" id="divregotp" runat="server" visible="false">
                                                     <div class="col-lg-3">
                                <label class="col-form-label">OTP<code>*</code></label>
                            </div>
                            <div class="col">

                                    <asp:TextBox ID="txt_remitterotp" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_remitterotp"
                            ErrorMessage="*" ValidationGroup="sas"></asp:RequiredFieldValidator>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txt_remitterotp">
                                                </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>


                              <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col">
                           <asp:Button ID="btn_Reg" runat="server" ValidationGroup="vg_reg"  Text="Register" UseSubmitBehavior="false"
                                                OnClick="btn_Reg_Click" OnClientClick="if (!Page_ClientValidate('vg_reg')){ return false; } this.disabled = true; this.value = 'Registering...Please Wait';" CssClass="btn btn-primary btn-lg btn-block" />
                          <asp:Button ID="btn_remiitervalidate" Visible="false" runat="server" Text="Submit" OnClick="btn_remiitervalidate_Click" CssClass="btn btn-primary btn-lg btn-block" ValidationGroup="sas" OnClientClick="if (!Page_ClientValidate('sas')){ return false; } this.disabled = true; this.value = 'Validating OTP...';"  UseSubmitBehavior="false"/>
                                           
                                        
                            </div>
                  </div>
                       </div></div>
                        </ContentTemplate>
                    <Triggers>
                          <asp:PostBackTrigger ControlID="btn_Reg" />
                    </Triggers>
                    </asp:UpdatePanel>
            </div>
            </div>
                        </div>
                        <div class="tab-pane fade active show" id="profile-2" role="tabpanel" aria-labelledby="profile-tab-vertical">
                     <div class="row grid-margin" id="divremitterreg" runat="server" visible="true">
                                       <div class="col-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                
                        <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col">
                                <asp:TextBox ID="txt_loginID" runat="server"  placeholder="Remitter Mobile Number" autocomplete="off" CssClass="form-control" MaxLength="10"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfv_loginID" runat="server" ControlToValidate="txt_loginID"
                                                    SetFocusOnError="True" Display="Dynamic" ErrorMessage="Invalid Remitter Mobile Number"
                                                    ValidationGroup="vglogin"></asp:RequiredFieldValidator>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txt_loginID">
                                                </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                
                            </div>
                            <div class="col-lg-8">
                                 <asp:HiddenField ID="hidStatus" runat="server" Value="0" />
                         <asp:Button ID="btn_login" ValidationGroup="vglogin" runat="server" Text="Continue" class="btn btn-primary btn-lg btn-block" OnClientClick="if (!Page_ClientValidate('vglogin')){ return false; } this.disabled = true; this.value = 'Submitting...Please Wait';" OnClick="btn_login_Click1" UseSubmitBehavior="false" /> 
                           
                           
                            </div>
                        </div>
                    </div>
                </div>
                    </ContentTemplate>   <Triggers>
                          <asp:PostBackTrigger ControlID="btn_login" />
                    </Triggers></asp:UpdatePanel></div>
                           </div>
                        </div>
                
                      </div>
                    </div>
                  </div>


                    </div></div></div>







        
   
    
             
           
           
           
           
           
           
           
             
        <div class="row" id="divbody" runat="server" visible="false">
              
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
                        <div id="loader"></div>
            <div> <strong>Please Wait.. While We are Verifying Your Details</strong></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
            <div class="col-md-6 grid-margin stretch-card">
                <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
              <div class="card">
                <div class="card-body">
                    
            <div class="row">
              <div class="col-lg-6 col-xs-6">
                <div class="row">
                  <div class="col-md-12" align="left">
                       <td><a href="#openModal" class="btn btn-danger btn-icon-text" title="click to view member detail">Change Remitter<i class="fas fa-pencil-alt btn-icon-append"></i></a></td>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12" align="left">
                    <strong>Hi, <span id="rm_name"> <asp:Label ID="lbl_Remname" runat="server" Text="" Style="text-transform: uppercase"></asp:Label></span></strong>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12" align="left">
                    <strong><span id="rem_mobile"><asp:Label ID="lbl_Remmno" runat="server" Text=""></asp:Label></span></strong>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12" align="left">
                    <span data-class="" id="rm_kyc"></span>
                 
                  </div>
                </div>
              </div>              
              <div class="col-lg-6 col-xs-6">
                <div class="pull-right">
                  <div class="row">
                    <strong>Total Limit</strong>
                  </div>
                    <div class="row">
                       <align="right"><span style="color:green"></span><span id="totallimit" class="count" style="color:green"><asp:Label ID="lbl_Consumed" runat="server" Text=""></asp:Label></span></align>
                    </div>
                </div>
                <div class="col-md-12">
                  <div class="row">
                   <strong>Remaining</strong>
                  </div>
                       <div class="row">
                          <align="right"> <span style="color:green"></span><span id="remaininglimit" class="count" style="color:green"><asp:Label ID="lbl_reming" runat="server" Text=""></asp:Label></span></align>
                           </div>
              
              </div>
            </div>
                
                      <div class="row">
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Action<code>*</code></label>
                            </div>
                            <div class="col-lg-9">
                              
                                <asp:DropDownList ID="ddl_action" runat="server" CssClass="btn btn-outline-dark  dropdown-toggle" style="width:100%;">
                                
                                      <asp:ListItem Text="Add Benificary" Value="1">
                                    </asp:ListItem>
                                 
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvnewsdesc" runat="server" ControlToValidate="ddl_action"
                                    Display="Dynamic" ErrorMessage="Please Select Action !" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                  
                            </div>
                        </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Account Number<code>*</code></label>
                            </div>
                            <div class="col-lg-9">
                                 <asp:TextBox ID="txt_Acno" CssClass="form-control" runat="server" placeholder="Enter Account No."
                                                    autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_Acno" runat="server" ControlToValidate="txt_Acno"
                                                    SetFocusOnError="true" ErrorMessage="Please Enter Account No Of Beneficiary"
                                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Acno" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txt_Acno">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_Acno" ID="regExp_Acno"
                                                    ValidationExpression="^[\s\S]{9,18}$" runat="server" ErrorMessage="Enter correct bank Account Number."
                                                    ValidationGroup="v"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                            <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Bank Name<code>*</code></label>
                            </div>
                            <div class="col-lg-6">

                                 <asp:DropDownList ID="ddl_choosebank" CssClass="btn btn-outline-dark  dropdown-toggle" runat="server"
                                                    OnSelectedIndexChanged="ddl_choosebankchanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_choosebank"
                                    Display="Dynamic" ErrorMessage="Please Select Bank !" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                          
                            <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">IFSC Code<code>*</code></label>
                            </div>
                            <div class="col-lg-4">
                               <asp:TextBox ID="txt_Ifsccode" CssClass="form-control" runat="server" Style="text-transform: uppercase"
                                                    Enabled="false" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_Ifsccode"
                                                    SetFocusOnError="true" ErrorMessage="Please Enter IFSC Code" ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                                <div class="col-lg-4">
                                      <asp:CheckBox ID="chkselectifsc" runat="server" Text="Don't Know IFSC Code" CssClass="form-control"
                                                    OnCheckedChanged="chkselectifscchanged" AutoPostBack="true" />
                                </div>
                        </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Benificiary Name<code>*</code></label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txt_Beniname" CssClass="form-control" runat="server" MaxLength="50" placeholder="Enter Beni Name"
                                                    autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_Beniname" runat="server" ControlToValidate="txt_Beniname"
                                                    SetFocusOnError="true" ErrorMessage="Please Enter Name Of Beneficiary" ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                           <div class="col-lg-3">
                                <asp:Button ID="Button2" runat="server" Text="Get Name" CssClass="btn btn-outline-success btn-fw" ValidationGroup="v"  OnClick="Button2_Click" />
                            </div>
                        </div>

                                                 <div class="form-group row" id="divtransferamount" runat="server" visible="false">
                            <div class="col-lg-3">
                                <label class="col-form-label">Amount</label>
                            </div>
                            <div class="col-lg-6">
                                     <asp:TextBox ID="txt_Amount" CssClass="form-control" runat="server" autocomplete="off"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amount"
                                                ErrorMessage="Please Enter Amount" ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amount"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>




                                         <div class="form-group row" id="diventerotp" runat="server" visible="false">
                          <div class="col-lg-3">
                              <label class="col-form-label">Enter OTP</label>
                              </div>
  <div class="col-lg-6">
      <asp:TextBox ID="txt_dmrotp" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_dmrotp"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_dmrotp" ErrorMessage="Enter OTP" ForeColor="Red" ValidationGroup="v"></asp:RequiredFieldValidator>
                    
  </div>
                                                </div>

                                <div class="form-group row">
                            <div class="col-lg-3">
                               
                            </div>
                            <div class="col-lg-9">
                                  <asp:Button ID="btn_Addbeni" ValidationGroup="v" runat="server" Text="Add Beneficiary" CssClass="btn btn-primary"
                                                    OnClick="btn_Addbeni_Click" />
                                    <asp:Button ID="btn_SendAmount" runat="server" ValidationGroup="v" Text="Transfer Amount" CssClass="btn btn-primary btn-lg btn-block"
                                                OnClick="btn_SendAmount_Click" Visible="false" UseSubmitBehavior="false" OnClientClick="if (!Page_ClientValidate('v')){ return false; } this.disabled = true; this.value = 'Sending OTP...';"/>
                                <asp:Button ID="btn_dmrotp" Visible="false" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="Proceed" UseSubmitBehavior="false" OnClick="btn_dmrotp_Click" OnClientClick="if (!Page_ClientValidate('v')){ return false; } this.disabled = true; this.value = 'Verifying OTP...';" ValidationGroup="v"/>
                            </div>
                        </div>

                       </div>  </div>
                </div>
              </div>
                     </ContentTemplate>
                </asp:UpdatePanel>
                        <%--Modal Popup Code--%>
                    <div id="openModal" class="modalDialog">
                                <div>
                                    <a href="#close" title="Close" class="close">X</a>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="info-box">
                                                    <h5 style="text-align: center;">Change Remitter</h5>
                                                    <hr>
                                              <div class="form-group row">
                            <div class="col">

                                <asp:TextBox ID="Text1" runat="server"  placeholder="Remitter Mobile Number" autocomplete="off" CssClass="form-control" MaxLength="10"></asp:TextBox>
                           
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Text1"
                                                    SetFocusOnError="True" Display="Dynamic" ErrorMessage="Invalid Remitter Mobile Number"
                                                    ValidationGroup="vglogin"></asp:RequiredFieldValidator>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789"
                                                    TargetControlID="Text1">
                                                </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>
                                                    <div class="form-group row">
                                                                                 <asp:Button ID="btn_loginnew" ValidationGroup="vglogin" runat="server" Text="Continue" class="btn btn-primary btn-lg btn-block" OnClientClick="if (!Page_ClientValidate('vglogin')){ return false; } this.disabled = true; this.value = 'Submitting...Please Wait';" OnClick="btn_loginnew_Click1" UseSubmitBehavior="false" /> 
                                                       
                                                    </div>
                         
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                     <%--Modal Popup Code end--%>
            </div>

             <div class="col-md-6 grid-margin stretch-card">
              <div class="card">
                <div class="card-body">
                       <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel_BeneficiaryDetails">
        <ProgressTemplate>
                        <div id="loader"></div>
            <div> <strong>Please Wait.. While We are Verifying Your Details</strong></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                                <div class="row">
                  <div class="col-md-12" align="left">
                      <asp:Button ID="btnaddbenecheck" runat="server" CssClass="btn btn-warning btn-icon-text" Text="Add New Benificiary" OnClick="btnaddbenecheck_Click" />
                     
                  </div>
                </div><br />
                  <h4 class="card-title">BENEFICIARY LIST</h4>   
                  
                <asp:UpdatePanel ID="UpdatePanel_BeneficiaryDetails" runat="server">
                        <ContentTemplate>
                    <asp:Repeater ID="rpt_benificiary" runat="server" OnItemCommand ="gv_Benificerydetails_RowCommand">
                        <ItemTemplate>
                <div class="form-group row">
                     <div class="col-lg-6 col-xs-6"><div><strong style="font-size:12px;"><%# Eval("name") %></strong></div><div style="color:#666;font-size:11px; line-height:1.5">A/c: <%# Eval("account") %></div><div style="color:#666;font-size:11px; line-height:1.5; margin-bottom:6px;">IFSC:&nbsp;<%# Eval("ifsc") %></div></div>
                       <div class="col-lg-6 col-xs-6" align="right"> 
                             <asp:Button ID="Button1" runat="server" Text="Transfer" CssClass="btn btn-danger" CommandName="imps" CommandArgument='<%#Eval("id")%>' ToolTip="Click to delete to Benificery" />   
                           <asp:Button ID="btn_Delete" runat="server" Text="Delete" CssClass="btn btn-danger btn-xs _remove" CommandName="del" CommandArgument='<%#Eval("id")%>' ToolTip="Click to delete to Benificery" />   
                     </div>
               </div>

                        </ItemTemplate>
                    </asp:Repeater> 
                           </ContentTemplate>
                                      
                                    </asp:UpdatePanel>
                     <%-----ModalPopupBeneOTP-----%>  
        <input type="button" value="OpenModalPopup" id="openButton" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btnClose" runat="server" style="display: none;" />
                 <cc1:ModalPopupExtender ID="mp_DelOtp" runat="server" PopupControlID="Panel_Del" TargetControlID="openButton"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
                 <asp:Panel ID="Panel_Del" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                       <div>
                                    <asp:Button ID="btnclosse" Text="X" runat="server" CssClass="close" OnClick="btnclose_Click" />
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="info-box">
                                                    <h5 style="text-align: center;">Enter OTP</h5>
                                                    <hr>
                                              <div class="form-group row">
                            <div class="col">

                                    <asp:TextBox ID="txt_DelOtp" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_DelOtp" runat="server" ControlToValidate="txt_DelOtp"
                            ErrorMessage="*" ValidationGroup="vgDelOtp"><img src="../../images/warning.png" /></asp:RequiredFieldValidator>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txt_DelOtp">
                                                </cc1:FilteredTextBoxExtender>
                            </div>
                  </div>
                                                    <div class="form-group row">
                         <asp:Button ID="btn_BeniDelOtpSub" runat="server" Text="Submit" OnClick="btn_BeniDelOtpSub_Click" CssClass="btn btn-primary btn-lg btn-block" ValidationGroup="vgDelOtp" OnClientClick="if (!Page_ClientValidate('vgDelOtp')){ return false; } this.disabled = true; this.value = 'Validating OTP...';"  UseSubmitBehavior="false"/>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
<%--
                      <div class="row">
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Enter OTP<code>*</code></label>
                            </div>
                            <div class="col-lg-9">
                            
                                </div></div>
                            <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-9">
                                
                                </div></div>
                      </div>--%>
        </asp:Panel> 
                    <%-----ModalPopupBeneOTP-----%>  


                        


        <input type="button" value="OpenModalPopup" id="btn_Recipt_Open" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btn_Recipt_Close" runat="server"
            style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_Recipt" runat="server" PopupControlID="pnl_Recipt"
            TargetControlID="btn_Recipt_Open" CancelControlID="btn_Recipt_Close" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnl_Recipt" runat="server" CssClass="modalPopupRecept" align="center" Style="display: none;
            width: 50%; height: auto;">
            <div id="dv_Print_Recipt" runat="server">
                <div style="padding: 5px 8px 4px 4px">
                    <div style="float: left">
                        <b>Transaction Receipt<br />
                            Thank you for transacting at &nbsp;<asp:Label ID="lbl_Company_Name" runat="server"></asp:Label></b>
                    </div>
                    <div style="float: right">
                        <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="~/Images/logo.png" Width="170px"
                            BorderStyle="None" /></div>
                    <div>
                        <table style="width: 100%; border: 1px solid black">
                            <tr style="border: 1px solid black">
                                <th style="border: 1px solid black">
                                    <b>REMITTER</b>
                                </th>
                                <th style="border: 1px solid black">
                                    <b>BENEFICIARY</b>
                                </th>
                            </tr>
                            <tr style="border: 1px solid black">
                                <td style="border: 1px solid black">
                                    <asp:Label ID="lbl_Remitter_Name" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbl_Remitter_Mobile" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black">
                                    <asp:Label ID="lbl_Beni_Name" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_Bani_ac_ifsc" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="grid_Recipt" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE" SortExpression="created">
                                    <ItemTemplate>
                                        <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("created")))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField ControlStyle-CssClass="margin-auto" DataField="EzulixTranid" HeaderText="ORDER ID"
                                    SortExpression="EzulixTranid"></asp:BoundField>
                                <asp:BoundField ControlStyle-CssClass="margin-auto" DataField="mode" HeaderText="MODE"
                                    SortExpression="mode"></asp:BoundField>
                                <asp:BoundField ControlStyle-CssClass="margin-auto" DataField="amount" HeaderText="VALUE (Rs)"
                                    SortExpression="amount"></asp:BoundField>
                                <asp:BoundField ControlStyle-CssClass="margin-auto" DataField="status_" HeaderText="Status"
                                    SortExpression="status_"></asp:BoundField>
                                <asp:BoundField ControlStyle-CssClass="margin-auto" DataField="ref_no" HeaderText="Bank Reference ID"
                                    SortExpression="ref_no"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Button ID="btn_Recipt_Print" runat="server" Text="Print" OnClientClick="return PrintPanel();" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_Recipt_PrintClose" runat="server" Text="Close" 
                onclick="btn_Recipt_PrintClose_Click"  />
        </asp:Panel>
                  
                </div>
              </div>
            </div>
     </div></div></div>
</asp:Content>

