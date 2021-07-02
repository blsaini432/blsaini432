<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MemberSignup_free.aspx.cs" Inherits="MemberSignup" %>

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
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
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
                                                                    CssClass="form-control"  Height="30px"
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
                                                                    CssClass="form-control"  Height="30px"
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
                                                                <asp:DropDownList ID="ddlCityName" runat="server" CssClass="form-control"  Height="30px">
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
                                                      
                                                        
                                                       
                                                       
                                                           
                                                        <tr>
                                                           
                                                            <td>
                                                               <%-- <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" 
                                                                    ClientIDMode="Static" Enabled="false" ValidationGroup="v0" />--%>
                                                                I agree the <a href="Policy" style="color: #F7A72A;" target="_blank" 
                                                                    title="Privacy Policy">Privacy Policy</a> &amp; <a href="TNC" 
                                                                    style="color: #F7A72A;" target="_blank" title="Terms &amp; Conditions">Terms 
                                                                &amp; Conditions</a> of Bharathpay.net
                                                              
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
                                                                    Text="Next" ValidationGroup="v0" onclick="btnSubmit1_Click" />
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
                                   
                                </asp:MultiView>
                            </div>
                            <!-- End SmartWizard Content -->
                            <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Medium" 
                                ForeColor="Black" Text="Name"></asp:Label>--%>
                        </td>
                    </tr>
                </table>
            </section>
        </contenttemplate>
        <triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>

           <%-- <asp:AsyncPostBackTrigger ControlID="rbtnUPS" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="rbtnSeller" EventName="CheckedChanged" />--%>

           <%-- <asp:PostBackTrigger ControlID="btnSubmit1" />--%>
            <asp:PostBackTrigger ControlID="btnNext" />
        </triggers>
    </asp:UpdatePanel>
</asp:Content>
