<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GstRegistration.ascx.cs" Inherits="Root_UC_GstRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        <h3 class="page-title">GST Registration Form
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
                                    <label class="col-form-label" style="color: green">Amount For GST  (In Rs):--</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">GST Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="txt_gsttype" runat="server" CssClass="form-control"  RepeatDirection="Horizontal">
                                        <asp:ListItem>Composition</asp:ListItem>
                                        <asp:ListItem>Non Composition</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_gsttype"
                                        Display="Dynamic" ErrorMessage="Please select Gst type  !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Entity Details<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                     <asp:RadioButtonList ID="txt_ent" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                        <asp:ListItem>Individual</asp:ListItem>
                                           <asp:ListItem>HUF</asp:ListItem>
                                           <asp:ListItem> Company</asp:ListItem>
                                           <asp:ListItem>Firm</asp:ListItem>
                                        <asp:ListItem>AOP</asp:ListItem>
                                          <asp:ListItem>Trust</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ent"
                                        Display="Dynamic" ErrorMessage="Please select Entity Details !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Nature Of Business <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="txt_nat" runat="server" CssClass="form-control"  RepeatDirection="Horizontal">
                                        <asp:ListItem>Trading</asp:ListItem>
                                        <asp:ListItem>Services</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nat"
                                        Display="Dynamic" ErrorMessage="Please select Nature Of Business!" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Firm/Company/Legal Name<code>*</code></label>
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
                                    <label class="col-form-label">Pan Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_pannum" runat="server" MaxLength="50" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_pannum"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                           <%-- <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">State <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_sta" runat="server" MaxLength="50" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_NamePan"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>--%>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">District <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dis" runat="server" MaxLength="50" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_NamePan"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Annual Turnover <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_turn" runat="server" MaxLength="50" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_NamePan"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Business Object <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_buss" runat="server" MaxLength="100" CssClass="form-control" onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_NamePan"
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
                                    <label class="col-form-label">Account Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                  <asp:RadioButtonList ID="txt_banktype" runat="server" CssClass="form-control"  RepeatDirection="Horizontal">
                                        <asp:ListItem>Saving</asp:ListItem>
                                        <asp:ListItem>Current</asp:ListItem>
                                    </asp:RadioButtonList>
                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please select Account type  !"
                                        ControlToValidate="txt_banktype" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Account Holder Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_accname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter Email !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Account Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_accnum" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Enter Email !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">IFSC Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_ifsc" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter Email !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Bank Name <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_bankname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Enter Email !"
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
                              <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Aadhar Card<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_aadhar" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="fup_aadhar" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                              <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pan Card<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_pan" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="fup_pan" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                              <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Photo<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_photos" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fup_photos" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Sale Purchase<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_sale" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fup_sale" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Copy Of Gst <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_gst" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_gst" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

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
