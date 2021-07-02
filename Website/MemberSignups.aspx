<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="MemberSignups.aspx.cs" Inherits="MemberSignups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .swMain
        {
            width: 99%;
            background-color: #ccffdd;
            padding: 10px;
            height: auto;
            margin-top: 10px;
            float: left;
        }
        .ManagePageArea
        {
            border-radius: 10px;
            padding: 10px;
        }
   
        td:first-child
        {
            min-width: 30% !important;
        }
        td:nth-child(2)
        {
            min-width: 2% !important;
        }
        .breadcrumb {
	position: relative;}
    </style>
    <script type="text/javascript">
        function myvldt(a) {
            document.getElementById("txtravi").value = "1";
        }

              
    </script>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtravi" runat="server" ClientIDMode="Static" display="none" Text="0"></asp:TextBox>
    
    <section class="container" style=" margin-top:3%; display:none">
                
                                 <ol class="breadcrumb">
                                    <li>
                                          <asp:RadioButton ID="rbtnUPS" runat="server" AutoPostBack="true" 
                                              ToolTip="UPS Registration" GroupName="Reg" ClientIDMode="Static" Checked="true" 
                                              Text=" UPS Registration" oncheckedchanged="rbtnUPS_CheckedChanged" />
                                    </li>
                                    <li>
                                        <asp:RadioButton ID="rbtnSeller" runat="server" ToolTip="Seller Registration" AutoPostBack="true"
                                            GroupName="Reg" ClientIDMode="Static" Text=" Seller Registration" 
                                            oncheckedchanged="rbtnSeller_CheckedChanged" />
                                    </li>
                                </ol>
                <h1 style="margin-top: 80px;font-size: 22px;">
                    <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
                    <%--<small>Admin Panel</small>--%>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Member</a></li>
                    <li class="active"><asp:Label ID="litsubtitlemenu" runat="server" Text="Utility Payment Station Registration"></asp:Label></li>
                </ol>
            </section>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="container">
                <table class="table table-bordered table-hover ">
                    <tr>
                        <td>
                         <!-- Smart Wizard -->
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Smart Wizard -->
                            <div ID="wizard" class="swMain">
                                <br />
                                <strong class="star">Note : Fields with <span class="red">*</span> are mandatory 
                                fields.</strong>
                                <br />
                                <asp:MultiView ID="mv" runat="server">
                                    <asp:View ID="mvv1" runat="server">
                                        <asp:Panel ID="pnlMV0" runat="server" DefaultButton="btnNext">
                                            <div>
                                                <h2 class="StepTitle">
                                                    Personal Information</h2>
                                                <p>
                                                    <table class="table table-bordered table-hover ">
                                                        <tr>
                                                            <td class="td1">
                                                                <span class="red">*</span>First Name
                                                            </td>
                                                            <td class="td2">
                                                                :
                                                            </td>
                                                            <td class="td3">
                                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" 
                                                                    MaxLength="50" placeholder="First Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvFirstNamesignup" runat="server" 
                                                                    ControlToValidate="txtFirstName" Display="Dynamic" 
                                                                    ErrorMessage="Please Enter FirstName !" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td1">
                                                                <span class="red">*</span>Last Name
                                                            </td>
                                                            <td class="td2">
                                                                :
                                                            </td>
                                                            <td class="td3">
                                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" 
                                                                    MaxLength="50" placeholder="Last Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvLastNamesignup" runat="server" 
                                                                    ControlToValidate="txtLastName" Display="Dynamic" 
                                                                    ErrorMessage="Please Enter LastName !" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </p>
                                            </div>
                                            <div>
                                                <h2 class="StepTitle">
                                                    Contact Information</h2>
                                                <p>
                                                    <table class="table table-bordered table-hover ">
                                                        <tr>
                                                            <td>
                                                                Address
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" 
                                                                    Height="50px" placeholder="Address" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr  id="trood" runat="server" visible=false>
                                                            <td>
                                                                Landmark
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLandmark" runat="server" CssClass="form-control" 
                                                                    MaxLength="50" placeholder="Landmark"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td1">
                                                                <span class="red">*</span>Country Name
                                                            </td>
                                                            <td class="td2">
                                                                :
                                                            </td>
                                                            <td class="td3">
                                                                <asp:DropDownList ID="ddlCountryName" runat="server" AutoPostBack="True" 
                                                                    CssClass="form-control"  Height="40px"
                                                                    OnSelectedIndexChanged="ddlCountryName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4signup" runat="server" 
                                                                    ControlToValidate="ddlCountryName" Display="Dynamic" 
                                                                    ErrorMessage="Please Select Country !" InitialValue="0" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="red">*</span>State Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlStateName" runat="server" AutoPostBack="True" 
                                                                    CssClass="form-control"  Height="40px"
                                                                    OnSelectedIndexChanged="ddlStateName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvStateNamesignup" runat="server" 
                                                                    ControlToValidate="ddlStateName" Display="Dynamic" 
                                                                    ErrorMessage="Please Select State !" InitialValue="0" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="red">*</span> City Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCityName" runat="server" CssClass="form-control"  Height="40px">
                                                                    <asp:ListItem Value="0">Select City</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCityNamesignup" runat="server" 
                                                                    ControlToValidate="ddlCityName" Display="Dynamic" 
                                                                    ErrorMessage="Please Select City !" InitialValue="0" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="red">*</span>ZIP
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZIP" runat="server" CssClass="form-control" MaxLength="6" 
                                                                    placeholder="Zip"></asp:TextBox>
                                                                <cc2:FilteredTextBoxExtender ID="txtZIP_FilteredTextBoxExtender" runat="server" 
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtZIP">
                                                    </cc2:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2signup" runat="server" 
                                                                    ControlToValidate="txtZIP" Display="Dynamic" 
                                                                    ErrorMessage="Please enter pin code !" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="red">*</span>Email
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                                                                    MaxLength="50" placeholder="Email"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revEmailsignup" runat="server" 
                                                                    ControlToValidate="txtEmail" Display="Dynamic" 
                                                                    ErrorMessage="Email is not valid !" SetFocusOnError="True" 
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                                    ValidationGroup="v0">*</asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="rfvEmailsignup" runat="server" 
                                                                    ControlToValidate="txtEmail" Display="Dynamic" 
                                                                    ErrorMessage="Please Enter Email !" SetFocusOnError="True" ValidationGroup="v0"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="red">*</span>Mobile
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" 
                                                                    MaxLength="10" placeholder="Mobile No."></asp:TextBox>
                                                                <cc2:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                                                    </cc2:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="rfvMobilesignup" runat="server" 
                                                                    ControlToValidate="txtMobile" Display="Dynamic" 
                                                                    ErrorMessage="Please Enter Mobile !" SetFocusOnError="True" 
                                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none;">
                                                            <td>
                                                                Date Of Birth
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMsignupdob" runat="server" class="form-control" 
                                                                    placeholder="Date Of Birth"></asp:TextBox>
                                                                <cc2:CalendarExtender ID="txtMsignupdobaa_c" runat="server" Animated="False" 
                                                                    Format="dd-MMM-yyyy" PopupButtonID="txtMsignupdob" PopupPosition="TopLeft" 
                                                                    TargetControlID="txtMsignupdob"></cc2:CalendarExtender>
                                                                <span style=" font-size:12px; color:#00BBF2; float:right;">&quot;akviraonline.in.in will 
                                                                celebrate your b&#39;day&quot;</span>
                                                            </td>
                                                        </tr>
                                                        <tr id="trot" runat="server" visible=false>
                                                            <td>
                                                                Profile Pic
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fupmppic" runat="server" CssClass="form-control" />
                                                            </td>
                                                        </tr>
                                                        <tr id="troo" runat="server" visible=false>
                                                            <td valign="top">
                                                                Landline No
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSTDCode" runat="server" CssClass="form-control" 
                                                                    MaxLength="10" placeholder="STD Code"></asp:TextBox>
                                                                &nbsp;
                                                                <cc2:FilteredTextBoxExtender ID="txtSTDCode_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtSTDCode">
                                                    </cc2:FilteredTextBoxExtender>
                                                                <asp:TextBox ID="txtLadline" runat="server" CssClass="form-control" 
                                                                    MaxLength="20" placeholder="Phone No."></asp:TextBox>
                                                                <cc2:FilteredTextBoxExtender ID="txtLadline_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtLadline">
                                                    </cc2:FilteredTextBoxExtender>
                                                                <br />
                                                                <font color="red">Note : Don&#39;t use &#39;0&#39; (Zero) in STD Code.</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" 
                                                                    ClientIDMode="Static" Enabled="false" ValidationGroup="v0" />
                                                                I agree the <a href="Policy" style="color: #F7A72A;" target="_blank" 
                                                                    title="Privacy Policy">Privacy Policy</a> &amp; <a href="TNC" 
                                                                    style="color: #F7A72A;" target="_blank" title="Terms &amp; Conditions">Terms 
                                                                &amp; Conditions</a> of akviraonline.in.in
                                                                <%--  <asp:CustomValidator ID="CustomValidator1signup" CssClass="rfv" runat="server" ErrorMessage="Please Check Terms & Condition !"
                                            ClientValidationFunction="ValidateCheckBox1" ValidationGroup="v0"><img src="images/warning.png" style=" width:16px;"/></asp:CustomValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <%--OnClientClick="return myvldt(this);"--%>
                                                                <asp:Button ID="btnNext" runat="server" class="btn btn-primary mybtnr" 
                                                                    Text="Next" ValidationGroup="v0" onclick="btnNext_Click1" />
                                                                <br />
                                                                <br />
                                                                <asp:ValidationSummary ID="ValidationSummary1signup" runat="server" 
                                                                    ClientIDMode="Static" ShowMessageBox="true" ShowSummary="false" 
                                                                    Style="display: none" ValidationGroup="v0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </p>
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View ID="View1" runat="server">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="s">
                                            <div>
                                                <h2 class="StepTitle">
                                                    Payment</h2>
                                                <p>
                                                    <table class="table table-bordered table-hover ">
                                                        <tr>
                                                            <td>
                                                                <table class="nav-justified">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                                                Font-Underline="True" ForeColor="Black" 
                                                                                Text="*You Will Pay for membership fee 0/-"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                                                Font-Underline="True" ForeColor="Black" 
                                                                                Text="to proceed  on given account details" Visible="false"></asp:Label>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                </table></td>
                                                           
                                                            <td>
                                                               
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="s" runat="server" class="btn btn-primary mybtnr" 
                                                                    Text="Submit" ValidationGroup="v1" OnClick="btnSubmit1_Click" />
                                                            </td>
                                                            <td>
                                                                <br />
                                                                <br />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                                    ClientIDMode="Static" Style="display: none" ValidationGroup="v1" />
                                                            </td>
                                                        </tr>


                                                    </table>
                                                </p>
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                            <!-- End SmartWizard Content -->
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Medium" 
                                ForeColor="Black" Text="Name"></asp:Label>
                        </td>
                    </tr>
                </table>
            </section>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>

           <%-- <asp:AsyncPostBackTrigger ControlID="rbtnUPS" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="rbtnSeller" EventName="CheckedChanged" />--%>

           <%-- <asp:PostBackTrigger ControlID="btnSubmit1" />--%>
            <asp:PostBackTrigger ControlID="btnNext" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
