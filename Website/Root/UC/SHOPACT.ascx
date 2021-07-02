<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SHOPACT.ascx.cs" Inherits="Root_UC_SHOPACT" %>
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
<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">SHOP ACT
        </h3>
    </div>

    <div class="col-12">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label" style="color: green">Amount For Shop Act (In Rs):-</label>

                            </div>
                            <div class="col-lg-8">

                                <asp:Label ID="lblamt" Font-Bold="true" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name As per Pan Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_nameofest" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nameofest" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Gender<code>*</code></label>
                            </div>
                            <div class="col-lg-8">

                                <asp:RadioButtonList ID="txt_addofest" runat="server">
                                    <asp:ListItem>MALE</asp:ListItem>
                                    <asp:ListItem>FEMALE</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_addofest" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                          <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Office Rented Or own<code>*</code></label>
                            </div>
                            <div class="col-lg-8">

                                <asp:RadioButtonList ID="RadioButtonList3" runat="server">
                                    <asp:ListItem>YES</asp:ListItem>
                                    <asp:ListItem>NO</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_addofest" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pin Code<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_pincode" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_pincode" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mobile<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_mob" runat="server" MaxLength="10" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Phone number" ControlToValidate="txt_mob" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Email ID<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_email" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">DOB<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_daofcommbus" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_daofcommbus" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_daofcommbus" TargetControlID="txt_daofcommbus">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_daofcommbus"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>

                          <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Business Start Date<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="buss_date" runat="server" MaxLength="100" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="buss_date" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender runat="server" ID="txt_doasdsabs" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="buss_date" TargetControlID="buss_date">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="buss_date"
                                    Display="Dynamic" ErrorMessage="Please Enter date (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Shop Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_natofbus" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_natofbus" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Shop Address<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_noofemp" runat="server" MaxLength="1000" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_noofemp" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_nameofemp" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_nameofemp" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_aadhar" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_aadhar" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Residential Address-<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_resaddofemp" runat="server" MaxLength="1000" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_resaddofemp" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <label>Required Documents-</label>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Photo<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_photo" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="fup_photo" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Sign<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_sign" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_photo" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_aadhar" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="fup_aadhar" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_self" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="fup_self" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">shop Photo<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_oldshop" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fup_oldshop" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File1<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="Actualphoto" runat="server" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File2<code>*</code></label>
                            </div>
                            <div class="col-lg-8">

                                <asp:FileUpload ID="fup_file2" runat="server" AllowMultiple="true" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File3<code>*</code></label>
                            </div>
                            <div class="col-lg-8">

                                <asp:FileUpload ID="fup_file3" runat="server" AllowMultiple="true" />
                            </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File4<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file4" runat="server" AllowMultiple="true" />

                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Download SelfDeclaration_Shop Form<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <a href="../../Uploads/SelfDeclaration_Shop%20(1).pdf">Download Form</a>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Upload SelfDeclaration_Shop Form<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file5" runat="server" />
                            </div>
                        </div>


                       <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="a" class="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="a" class="btn btn-primary" OnClick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>