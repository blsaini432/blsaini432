<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Creditcard.ascx.cs" Inherits="Root_UC_Creditcard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1 {
        height: 30px;
    }

    .style2 {
        height: 23px;
    }
</style>
<script type="text/javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function isAlphabets(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97))
            return true;
        else
            return false;
    }

    function isNumeric(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57)
            return true;
        else
            return false;
    }
    function isAlphaNumeric(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
            return true;
        else
            return false;
    }
    //-->
</script>
<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Credit Card Form
        </h3>
    </div>
    <%--<table class="table table-bordered table-hover ">
      <div class="card" style="height:72px;">
                    <div class="card-body">

    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Name<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_NamePan" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_NamePan"
                Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
              
        </td>
        </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Date of Birth<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                CssClass="form-control"></asp:TextBox>
            <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                PopupButtonID="txt_dob" TargetControlID="txt_dob">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dob"
                Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
        
    </tr>
  
   
    <tr>
       <td>
            <h5>
                <strong><span class="red"></span>Father's Name<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_FatherName" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_FatherName"
                Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>            
        </td>
        </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Mother Name<code>*</code></strong>
            </h5>
        </td>
        <td>
             <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Please Enter mother Name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr runat="server">
        <td class="style1">
            <h5>
                <strong><span class="red">Pancard<code>*</code></span>(Upto 100KB)</strong>
            </h5>
        </td>
        <td class="style1" id="panimagetd" runat="server">
            <asp:FileUpload ID="fup_Photo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="fup_Photo"
                Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>
        
    </tr>
   
    <tr>
        <td>
            <h5>
                <strong><span class="red">Aadharcard<code>*</code></span>(<span style="font-size:9.5pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;color:#222222;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">1.4MB TO 2 MB</span>)</strong>
            </h5>
        </td>
        <td class="style1">
            <asp:FileUpload ID="fup_Aadhar" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fup_Aadhar"
                Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>
       
    </tr>
    <tr>
        <td class="style2" id="bankfintd" runat="server">
            <h5>
                <strong><span class="red">ITR<code>*</code></span>(<span style="font-size:9.5pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;color:#222222;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">UP TO 2 MB</span>)</strong>
            </h5>
        </td>
        <td class="style2">
            <asp:FileUpload ID="fup_bankstatementfinani" runat="server" />
        </td>
         <asp:FileUpload ID="fup_bankstatementfinani" runat="server" />
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">BankAccountDetails<code>*</code></span>(<span style="font-size:9.5pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;color:#222222;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">UP TO 2 MB</span>)</strong></h5>
        </td>
        <td>
            <asp:FileUpload ID="fup_bankaccountdetails" runat="server" />
      
    </tr>
    <tr id="form16tr" runat="server" visible="false">
        <td  id="form16td" runat="server">
            <h5>
                <strong><span class="red">Form16<code>*</code></span>(Upto 100KB)</strong>
            </h5>
        </td>
        <td>
            <asp:FileUpload ID="fup_form16" runat="server" />
        </td>
        <td colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
       
        <td colspan="2">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" class="btn btn-primary"
                OnClick="btnSubmit_Click" />
       
            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="btnReset_Click" />
        </td>
    </tr>
  </div>
          </div>
</table>--%>


    <div class="col-12">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_name" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_name"
                                    Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Date Of Birth<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                                    CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dob"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Father/Husband<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_father" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_father"
                                    Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mother<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_mother" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_mother"
                                    Display="Dynamic" ErrorMessage="Please Enter mother Name !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Upload Aadhar<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                  <asp:FileUpload ID="fup_Aadhar" runat="server" CssClass="form-control" />&nbsp;
                              <%--  <asp:FileUpload ID="fup_Aadhar" runat="server" />--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fup_Aadhar"
                                    Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="v">*</asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="form-group row">

                            <div class="col-lg-3">
                                <label class="col-form-label">Upload PAN<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_Photo" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="fup_Photo"
                                    Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="v">*</asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">

                            <div class="col-lg-3">
                                <label class="col-form-label">Bank statement(image)<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_bankstatementfinani" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_bankstatementfinani"
                                    Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="v">*</asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">ITR<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="FUP_ITR" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FUP_ITR"
                                    Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="v">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-8">
                                <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="s" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                      <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="s" class="btn btn-primary" OnClick="btnReset_Click" />--%>
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                    ValidationGroup="v" />

                                <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
