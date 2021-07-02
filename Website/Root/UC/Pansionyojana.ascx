<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pansionyojana.ascx.cs" Inherits="Root_UC_Pansionyojana" %>
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
        <h3 class="page-title">Pansion Yojana Form
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
                                 <label class="col-form-label" style ="color:green">Amount For Pansionyojna (In Rs):-</label>
                             
                            </div>
                            <div class="col-lg-8">
                                
                                <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_names" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_names"
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
                                <label class="col-form-label">Mobile Number <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_mob" runat="server" MaxLength="10"  CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ErrorMessage="Enter valid Phone number" ControlToValidate="txt_mob" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ValidationGroup="reg"></asp:RegularExpressionValidator>

                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txt_mob" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar Number <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_aadhar" runat="server" MaxLength="12" CssClass="form-control" onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_aadhar"
                                    Display="Dynamic" ErrorMessage="Please Enter Aadhar Number !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txt_mob" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Father <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_father" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_father"
                                    Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Bank Deatils<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_bank" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_bank"
                                    Display="Dynamic" ErrorMessage="Please Enter Bank Deatils !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Upload Aadhar Card<code>*</code></label>
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
                                <label class="col-form-label">Upload PAN Card<code>*</code></label>
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
