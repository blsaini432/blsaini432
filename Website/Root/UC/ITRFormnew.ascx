<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ITRFormnew.ascx.cs" Inherits="Root_UC_ITRFormnew" %>
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
        <h3 class="page-title">ITR FORM
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
                                    <label class="col-form-label" style="color: green">Amount For ITR (In Rs):--</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                            <%--<div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">ITR Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="RadioButtonList1" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem>Select Type</asp:ListItem>
                                        <asp:ListItem>With Balane Sheet</asp:ListItem>
                                        <asp:ListItem>Without Balane Sheet</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>--%>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label" style="color: green">ITR form Instruction :--</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="Label21" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text="Individual ITR only"></asp:Label>
                                </div>
                            </div>
                            <%--<div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">ITR Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true"
                                        RepeatDirection="Horizontal" ValidationGroup="v">
                                        <asp:ListItem>Salaried</asp:ListItem>
                                        <asp:ListItem>Non-Salaried</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="Please select " ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>

                            </div>--%>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Income Tax Login Password<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:TextBox ID="TXT_PASS" runat="server" MaxLength="50" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TXT_PASS"
                                        Display="Dynamic" ErrorMessage="Please Enter Income Tax Login Password  !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>

                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_NamePan" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_NamePan"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Father Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_FatherName" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_FatherName"
                                        Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                                        CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dob"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_MobilePartner" runat="server" MaxLength="12" CssClass="form-control" onkeypress="return isNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_MobilePartner"
                                        Display="Dynamic" ErrorMessage="Please Enter Mobile !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="v"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Permanent Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_permanentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_presentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" id="aadharcard" runat="server" >
                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <%--  <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_Identity_Click" />--%>
                                    <asp:FileUpload ID="fup_Aadhar" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fup_Aadhar" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row" id="pancardds" runat="server">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload PAN<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_Photo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_Photo" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row" id="bankfinanicialst" runat="server" >
                                <div class="col-lg-3">
                                    <label class="col-form-label">Bank statement<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_bankstatementfinani" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fup_bankstatementfinani" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row" id="bankaccountstt" runat="server" visible="false">

                                <div class="col-lg-3">
                                    <label class="col-form-label">BankAccountDetails<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_bankaccountdetails" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fup_bankaccountdetails" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row" id="form16tr" runat="server">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Form16<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_form16" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="fup_form16" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row" id="File1" runat="server" visible="false">

                                <div class="col-lg-3">
                                    <label class="col-form-label">File1(pdf)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_File1" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="fup_form16" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row" id="File2" runat="server" visible="false">

                                <div class="col-lg-3">
                                    <label class="col-form-label">File2(pdf)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_File2" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="fup_form16" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="s" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                      <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="s" class="btn btn-primary" OnClick="btnReset_Click" />--%>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                        ValidationGroup="v" />

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
