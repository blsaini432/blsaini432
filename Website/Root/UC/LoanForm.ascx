<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoanForm.ascx.cs" Inherits="Root_UC_LoanForm" %>
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
        <h3 class="page-title">Loan Form
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
                                 <label class="col-form-label" style ="color:green">Amount For Loan (In Rs):-</label>
                             
                            </div>
                            <div class="col-lg-8">
                                
                                <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Loan Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="RadioButtonList1" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem>Select Loan Type</asp:ListItem>
                                        <asp:ListItem>Personal Loan</asp:ListItem>
                                        <asp:ListItem>Business Loan</asp:ListItem>
                                        <asp:ListItem>Vehicle Loan</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_fullname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fullname"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>

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
                                    <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <%--  <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_Identity_Click" />--%>
                                    <asp:FileUpload ID="fup_aadcard" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fup_aadcard" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload PAN<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_age" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_age" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Bank statement<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="txt_fathername" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_fathername" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">ITR<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="txt_mothername" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_mothername" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label id="gst" runat="server" class="col-form-label">GST<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="txt_name" runat="server" CssClass="form-control"/>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label id="TXT_Insurance" runat="server" class="col-form-label">Insurance<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="txt_address" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="s" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                      <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="s" class="btn btn-primary" OnClick="btnReset_Click" />--%>
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

