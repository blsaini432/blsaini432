<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Accountopen.ascx.cs" Inherits="Root_UC_Accountopen" %>
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
        <h3 class="page-title">Account Open 
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
                                    <label class="col-form-label" style="color: green">Amount For Account Open (In Rs):--</label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">BANK NAME<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:DropDownList ID="RadioButtonList1" runat="server" Height="35px" CssClass="form-control">
                                        <asp:ListItem>SELECT BANK LIST</asp:ListItem>
                                        <asp:ListItem>State Bank of India</asp:ListItem>
                                        <asp:ListItem>HDFC</asp:ListItem>
                                        <asp:ListItem>Axis Bank</asp:ListItem>
                                        <asp:ListItem>ICICI Bank</asp:ListItem>
                                        <asp:ListItem>Yes Bank</asp:ListItem>
                                        <asp:ListItem>Paytm payment Bank</asp:ListItem>
                                        <asp:ListItem>Airtel  payment Bank</asp:ListItem>
                                        <asp:ListItem>Fino Bank </asp:ListItem>
                                        <asp:ListItem>Indlusand Bank</asp:ListItem>
                                        <asp:ListItem>STUDENT ACCOUNT</asp:ListItem>
                                        <asp:ListItem>JANDHAN ACCOUNT</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">BRANCH NAME<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_branch" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_branch"
                                        Display="Dynamic" ErrorMessage="Please Enter BRANCH NAME !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <%-- <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DATE<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_dob" ErrorMessage="Please select  DOB " ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_dob"
                                        Display="Dynamic" ErrorMessage="Please Enter DATE (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>--%>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Customer Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="tadlist" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                        <asp:ListItem>CURRENT </asp:ListItem>
                                        <asp:ListItem>SAVING</asp:ListItem>
                                        <asp:ListItem>OTHERS</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tadlist" ErrorMessage="Please select type" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">WORK Status<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_work" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_work"
                                        Display="Dynamic" ErrorMessage="Please Enter work status !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Applicant Name<code>*</code></label>
                                </div>
                                <div class="col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="txt_fristname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fullname"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Middle Name</label>
                                    <asp:TextBox ID="txt_Middlename" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_Middlename"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txt_lastname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_lastname"
                                        Display="Dynamic" ErrorMessage="Please Enter Last Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name of Father/Husband<code>*</code></label>
                                </div>
                                <div class="col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="txt_fname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fullname"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Middle Name</label>
                                    <asp:TextBox ID="txt_fmiddle" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_Middlename"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txt_flast" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_flast"
                                        Display="Dynamic" ErrorMessage="Please Enter Last Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dateofb" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_dateofb" ErrorMessage="Please select  DOB " ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dateofb" TargetControlID="txt_dateofb">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_dateofb"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Gender<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="rdoBtnLstGender" runat="server" CssClass="form-control" Width="93px" RepeatDirection="Horizontal">
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Fmale</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rdoBtnLstGender" ErrorMessage="Please select Gender" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">MOTHER NAME<code>*</code></label>
                                </div>
                                <div class="col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="TXT_mname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-lg-3">
                                    <label>Middle Name</label>
                                    <asp:TextBox ID="txt_mmname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txt_lname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_lname"
                                        Display="Dynamic" ErrorMessage="Please Enter Last Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Marital status<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                        <asp:ListItem>Married</asp:ListItem>
                                        <asp:ListItem>UNMarried</asp:ListItem>
                                        <asp:ListItem>Others</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="rdoBtnLstGender" ErrorMessage="Please select Gender" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ControlToValidate="txt_email" ValidationGroup="reg" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Email" ControlToValidate="txt_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_mob" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="txt_mob" ID="Rev_Name" ValidationExpression="^[0-9]{10,10}$" runat="server" ValidationGroup="reg"></asp:RegularExpressionValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txt_mob" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">AADHAR NUMBER<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_aadhar" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_aadhar" ValidationGroup="reg" ErrorMessage="Please Enter Aadhar number"></asp:RequiredFieldValidator>

                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Valid Aadhar Number" ControlToValidate="txt_aadhar"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">PAN NUMBER<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_pan" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_pan" ValidationGroup="reg" ErrorMessage="Please Enter Pan Number"></asp:RequiredFieldValidator>

                                    <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter Valid pan" ControlToValidate="txt_pan"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <h4>Residential address</h4>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_add" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_add"
                                        Display="Dynamic" ErrorMessage="Please Enter Address !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pin Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_pin" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_pin"
                                        Display="Dynamic" ErrorMessage="Please Enter pin !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">State<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_state" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_state"
                                        Display="Dynamic" ErrorMessage="Please Enter state !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">City<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_city" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_city"
                                        Display="Dynamic" ErrorMessage="Please Enter city !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <h4>Parmanent address</h4>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_addes" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_addes"
                                        Display="Dynamic" ErrorMessage="Please Enter Address !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pin Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_pins" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_pins"
                                        Display="Dynamic" ErrorMessage="Please Enter pin !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">State<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_states" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_states"
                                        Display="Dynamic" ErrorMessage="Please Enter state !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">City<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_citys" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_citys"
                                        Display="Dynamic" ErrorMessage="Please Enter city !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <h4>NOMINEE details</h4>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">FULL NAME<code>*</code></label>
                                </div>
                                <div class="col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="txt_nname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fullname"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Middle Name</label>
                                    <asp:TextBox ID="txt_nmname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_Middlename"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txt_nlast" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_nlast"
                                        Display="Dynamic" ErrorMessage="Please Enter Last Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">AADHAR NUMBER<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_naadhar" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_naadhar" ValidationGroup="reg" ErrorMessage="Please Enter Aadhar number"></asp:RequiredFieldValidator>

                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter Valid Aadhar Number" ControlToValidate="txt_aadhar"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dname" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_dname" ErrorMessage="Please select  DOB " ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dname" TargetControlID="txt_dname">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txt_dname"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_naddress" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_naddress"
                                        Display="Dynamic" ErrorMessage="Please Enter address !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">State<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_nstate" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt_nstate"
                                        Display="Dynamic" ErrorMessage="Please Enter state !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">City<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_ncity" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_ncity"
                                        Display="Dynamic" ErrorMessage="Please Enter city !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_aadcard" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fup_aadcard" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload PAN<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_age" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_age" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">photo<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="txt_photo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_photo" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

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

