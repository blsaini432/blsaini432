<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Digitalsignature.ascx.cs" Inherits="Root_UC_Digitalsignature" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1 {
        height: 30px;
    }

    .style2 {
        height: 23px;
    }

    .auto-style1 {
        width: 100%;
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
<%--<table class="auto-style1">
    <h3>Digital Signature</h3>
    <tr>
        <td class="aleft">
            <h3>
                <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong></h3>
        </td>
    </tr>
    <tr>
        <td>
            <h3>
                Amount For Digitalsignature (In Rs):-
            </h3>
        </td>
        <td>
            <h3>
                <asp:Label ID="lblamt" Font-Bold="true" Font-Size="Medium" ForeColor="Green" runat="server"
                    Text=""></asp:Label>
            </h3>
        </td>
    </tr>
 
    <tr>
        <td><h5>
                        <strong>Name As Per Pan Card</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_name" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_name" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
    </tr>

     <tr>
        <td><h5>
                        <strong>Gender</strong>
                    </h5></td>
        <td>  
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="93px">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Fmale</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
     <tr>
        <td><h5>
                        <strong>Mobile&nbsp; No</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_mob" runat="server" MaxLength="10" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Phone number" ControlToValidate="txt_mob" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ></asp:RegularExpressionValidator> 

        </td>
    </tr>
    <tr>
        <td><h5>
                        <strong>Email id</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_email" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress=""></asp:TextBox>
            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
        </td>
    </tr>
     <tr>
        <td><h5>
                        <strong>Residence-Address</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_add" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_add" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
    </tr>
  
    <tr>
        <td><h5>
                        <strong>Pin Code</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_rel" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_rel" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td><h5>
                        <strong>Date of birth-</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_date" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_date" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
        <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                PopupButtonID="txt_date" TargetControlID="txt_date">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_date"
                Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
    </tr>
   
   
    <tr>
        <td><h5>
                        <strong>Pan Number</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_fem" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_fem" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td><h5>
                        <strong>Aadhar-Number</strong>
                    </h5></td>
        <td><asp:TextBox ID="txt_par" runat="server" MaxLength="50" CssClass="form-control"
                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_par" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
        </td>
    </tr>
   
    <tr>
        <td><h4>Required Documents-</h4></td>
        <td>&nbsp;</td>
    </tr>
       <tr  id="PhotoSign" runat="server">
         <td><h5>
                        <strong>Pan Card</strong>
                    </h5></td>
        <td>
            <asp:FileUpload ID="fup_photo" runat="server" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fup_photo"
                Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="b">*</asp:RequiredFieldValidator>
         </td>
    </tr>
     <tr  id="Tr1" runat="server">
         <td><h5>
                        <strong>Aadhar Card</strong>
                    </h5></td>
        <td>
            <asp:FileUpload ID="fup_age" runat="server" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_age"
                Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="b">*</asp:RequiredFieldValidator>
         </td>
    </tr>
    
    <tr>
         <td></td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="b" class="btn btn-primary" OnClick="btnSubmit_Click" />
             <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="b" class="btn btn-primary" OnClick="btnReset_Click" />
         </td>
    </tr>
    
</table>--%>



<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Digitalsignature Form
        </h3>
    </div>

    <div class="row grid-margin">
        <div class="col-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label" style="color: green">Amount For Digitalsignature (In Rs):-</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label"> DSC Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control " OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                           <asp:ListItem>Select DSC Type </asp:ListItem>
                                          <asp:ListItem>Class -2, 2years Individual </asp:ListItem>
                                           <asp:ListItem>Class-2, 2years with encryption (combo), </asp:ListItem>
                                           <asp:ListItem> Class-2, 2years with encryption (combo) govt </asp:ListItem>
                                           <asp:ListItem>Class-3, 2years with encryption (combo),  </asp:ListItem>
                                    </asp:DropDownList>
                             
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                        Display="Dynamic" ErrorMessage="Please select type  !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_name" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_name"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Gender<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="form-control"  RepeatDirection="Horizontal">
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Fmale</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="Please select Aadhar Number" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_email" runat="server" MaxLength="50" CssClass="form-control"
                                      ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email"  ErrorMessage="Invalid Email Format" ValidationGroup="reg"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_mob" runat="server" MaxLength="10" CssClass="form-control"
                                        ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ErrorMessage="Enter valid Phone number" ControlToValidate="txt_mob" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ValidationGroup="reg"></asp:RegularExpressionValidator>

                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txt_mob" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_add" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_add" ErrorMessage="Please Enter address " ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pin Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_rel" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_rel" ErrorMessage="Please Enter Pin Code" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_date" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_date" ErrorMessage="Please select  DOB " ValidationGroup="reg"></asp:RequiredFieldValidator>
                                    </td>
        <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
            PopupButtonID="txt_date" TargetControlID="txt_date">
        </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_date"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pan Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_fem" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_fem" ErrorMessage="Please Enter Pan Number" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Aadhar Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_par" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_par" ErrorMessage="Please Enter Aadhar Number" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <%--  <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_Identity_Click" />--%>
                                    <asp:FileUpload ID="fup_age" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="fup_age"
                                        Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="reg">*</asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload PAN<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_photo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_age" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                              <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">File1(pdf)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_file1" runat="server" CssClass="form-control" />
                                 

                                </div>
                            </div>
                              <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">File2(pdf)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_file2" runat="server" CssClass="form-control" />
                                   

                                </div>
                            </div>
                           
                            <div class="form-group row">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                        ValidationGroup="reg" />

                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
