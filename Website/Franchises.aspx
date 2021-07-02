<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Franchises.aspx.cs" Inherits="Franchises" %>

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
    <asp:TextBox ID="txtravi" runat="server" ClientIDMode="Static" Text="0" Style="display: none;"></asp:TextBox>

    <section class="container" style="margin-top: 3%;">
                
                                 <ol class="breadcrumb" style="display:none;">
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
               
            </section>
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
                                <strong class="star"style="color:black">Note : Fields with <span class="red">*</span> are mandatory 
                                fields.</strong>
                                <br />
                                <asp:MultiView ID="mv" runat="server">
                                    <asp:View ID="mvv1" runat="server">
                                       <%-- <asp:Panel ID="pnlMV0" runat="server" DefaultButton="btnNext">--%>
                                            <div>
                                                <h2 class="StepTitle">
                                                   Franchise Apply</h2>
                                               
                                                    <table class="table table-bordered table-hover ">
                                                        <tr>
                                                            <td class="td1" style="color:black">
                                                                <span class="red">*</span> Name
                                                            </td>
                                                            <td class="td2">
                                                                :
                                                            </td>
                                                            <td class="td3">
                                                                <asp:TextBox ID="txt_cardname" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cardname"
                                            SetFocusOnError="true" ErrorMessage="Enter name" ValidationGroup="v"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td1"style="color:black">
                                                                <span class="red" >*</span>Mobile Number
                                                            </td>
                                                            <td class="td2">
                                                                :
                                                            </td>
                                                            <td class="td3">
                                                                <asp:TextBox ID="txt_mobiles" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="10" onkeypress="return isNumeric(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_mobiles"
                                            SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_Mobiles"
                                            ValidChars="0123456789">
                                        </cc2:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="rfv"
                                            ControlToValidate="txt_Mobiles" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                            SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                 
                                               
                                                  
                                                        <tr>
                                                            <td style="color:black">
                                                                email
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                              <asp:TextBox ID="txt_email" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Ifsccode" runat="server" ControlToValidate="txt_email"
                                            SetFocusOnError="true" ErrorMessage="Enter Email " ValidationGroup="v"></asp:RequiredFieldValidator>
                                                         
                                                            </td>
                                                        </tr>
                                                        <tr  id="trood" runat="server">
                                                            <td style="color:black">
                                                                Pin code
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <div class="col-lg-8">
                                        <asp:TextBox ID="txt_pin" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_pin"
                                            SetFocusOnError="true" ErrorMessage="Enter Pin Code " ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_pin"
                                            ValidChars="0123456789">
                                        </cc2:FilteredTextBoxExtender>
                                    </div>
                                                            </td>
                                                        </tr>
                                                         <tr  id="tr1" runat="server">
                                                            <td style="color:black">
                                                                Area
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txt_area" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_area"
                                            SetFocusOnError="true" ErrorMessage="Enter Area " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td1" style="color:black">
                                                                <span class="red" >*</span>Country Name
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
                                                            <td style="color:black">
                                                                <span class="red" >*</span>State Name
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
                                                            <td style="color:black">
                                                                <span class="red" >*</span> City Name
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
                                                            <td style="color:black">
                                                                <span class="red">*</span>Age
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txt_age" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_age"
                                            SetFocusOnError="true" ErrorMessage="Enter Age " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:black">
                                                                <span class="red">*</span>Educational
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                              <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Educational</asp:ListItem>
                                        <asp:ListItem>Below 12th Standard</asp:ListItem>
                                        <asp:ListItem>Passed 12th Standard</asp:ListItem>
                                        <asp:ListItem>Graduate Degree Holder</asp:ListItem>
                                        <asp:ListItem>Post Graduate Degree Holder</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:black">
                                                                <span class="red">*</span>Work Experienc
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_exp" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_exp"
                                            SetFocusOnError="true" ErrorMessage="Enter Work Experience " ValidationGroup="v"></asp:RequiredFieldValidator>
                                       <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_exp"
                                            ValidChars="0123456789">
                                        </cc2:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none;">
                                                            <td style="color:black">
                                                               How did you get to know about us?
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                               <asp:DropDownList ID="drop_abouts" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Abouts Us</asp:ListItem>
                                        <asp:ListItem>Newspapers Ad</asp:ListItem>
                                        <asp:ListItem>Interanet Ad</asp:ListItem>
                                        <asp:ListItem>Friend  Or Family</asp:ListItem>
                                        <asp:ListItem>Others</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                            </td>
                                                        </tr>
                                                        <tr id="trot" runat="server" >
                                                            <td style="color:black">
                                                                If you get an Amazon Easy store, who will run it?
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                               <asp:DropDownList ID="drop_amazon" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select Amazon Easy store </asp:ListItem>
                                        <asp:ListItem>MySelf</asp:ListItem>
                                        <asp:ListItem>Other Family Member</asp:ListItem>
                                        <asp:ListItem>Hired Staff</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                            </td>
                                                        </tr>
                                                        <tr id="troo" runat="server" >
                                                            <td valign="top" style="color:black">
                                                               Do you have other businesses
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                 <asp:DropDownList ID="drop_business" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" >
                                        <asp:ListItem>Select other businesse </asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>                                                                         
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                              
                                                           
                                                            </td>
                                                        </tr>
                                                          <tr id="tr2" runat="server" >
                                                            <td valign="top" style="color:black">
                                                              Communication Address (with nearest landmark)
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                  <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_address"
                                            SetFocusOnError="true" ErrorMessage="Enter address " ValidationGroup="v"></asp:RequiredFieldValidator>
                                     
                                                              
                                                           
                                                            </td>
                                                        </tr>
                                                          <tr id="tr3" runat="server" >
                                                            <td valign="top" style="color:black">
                                                              Message
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                  <asp:TextBox ID="txt_message" CssClass="form-control" runat="server"
                                            autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_message"
                                            SetFocusOnError="true" ErrorMessage="Enter message " ValidationGroup="v"></asp:RequiredFieldValidator>
                                     
                                                              
                                                           
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <%--OnClientClick="return myvldt(this);"--%>
                                                               <asp:button id="btnSubmit" runat="server" text="Submit "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                            validationgroup="v" onclick="btnSubmit_Click"></asp:button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                             
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                   
                                </asp:MultiView>
                            </div>
                            <!-- End SmartWizard Content -->
                          <%--  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
            <%--<asp:PostBackTrigger ControlID="btnNext" />--%>
        </triggers>
    </asp:UpdatePanel>
</asp:Content>
