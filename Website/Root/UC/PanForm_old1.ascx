<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PanForm_old1.ascx.cs" Inherits="Root_UC_PanForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="hdn_amount" runat="server" />


<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Pan Form
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
                                    <label class="col-form-label" style="color: green">Amount For Pan (In Rs):-</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Select Application Type<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddl_applicationtype" AutoPostBack="true" runat="server" CssClass="form-control chosenselect"
                                        Style="width: 100%" OnSelectedIndexChanged="ddl_applicationtype_SelectedIndexChanged">
                                        <asp:ListItem Value="New" Selected="True">Apply For New PAN</asp:ListItem>
                                        <asp:ListItem Value="update">Update Existing Application</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddl_applicationtype" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row" id="trTxnNo" runat="server" visible="false">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Category of Applicant<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtTxnNo" Visible="false" runat="server" MaxLength="20" CssClass="form-control"
                                        OnTextChanged="txtTxnNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTxnNo" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row" id="pan1" runat="server" visible="false">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Category of Applicant<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_panNo" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="panval1" runat="server" ErrorMessage="Please Enter PAN Card Number !"
                                        ControlToValidate="txt_panNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Category of Applicant<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddl_pancat" runat="server" CssClass="form-control chosenselect"
                                        Style="width: 100%" OnSelectedIndexChanged="ddl_pancat_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Individual" Selected="True">Individual</asp:ListItem>
                                        <asp:ListItem Value="Firm">Firm</asp:ListItem>
                                        <asp:ListItem Value="Body of Individuals">Body of Individuals</asp:ListItem>
                                        <asp:ListItem Value="Association of Persons">Association of Persons</asp:ListItem>
                                        <asp:ListItem Value="Local Authority">Local Authority</asp:ListItem>
                                        <asp:ListItem Value="Company">Company</asp:ListItem>
                                        <asp:ListItem Value="Trust">Trust</asp:ListItem>
                                        <asp:ListItem Value="Artificial Juridical Person">Artificial Juridical Person</asp:ListItem>
                                        <asp:ListItem Value="Goverment">Goverment</asp:ListItem>
                                        <asp:ListItem Value="Limited Liability Partnership">Limited Liability Partnership</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_pancat" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Date Of Application<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_Rdate" runat="server" Enabled="false" MaxLength="10" CssClass="form-control"></asp:TextBox>


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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rdoBtnLstGender" ErrorMessage="Please select Gender" ValidationGroup="reg"></asp:RequiredFieldValidator>

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
                                    <label class="col-form-label">Name Required On Pan Card<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_nameonpan" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_nameonpan"
                                        Display="Dynamic" ErrorMessage="Please Enter As Par Pan Card Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Father Name<code>*</code></label>
                                </div>
                                <div class="col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="txt_ffristname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fullname"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Middle Name</label>
                                    <asp:TextBox ID="txt_Fmiddlename" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_Middlename"
                                        Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txt_flastname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_flastname"
                                        Display="Dynamic" ErrorMessage="Please Enter Last Name !" SetFocusOnError="True"
                                        ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_dob" ErrorMessage="Please select  DOB " ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_dob"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Contact Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_telno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="txt_telno" ErrorMessage="Enter Valid Email" ID="RegularExpressionValidator2" ValidationExpression="^[0-9]{10,10}$" runat="server" ValidationGroup="reg"></asp:RegularExpressionValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txt_telno" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ControlToValidate="txtEmail" ValidationGroup="reg" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Aadhar Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_adhar" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAadhar" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                                        ControlToValidate="txt_adhar" Display="Dynamic" SetFocusOnError="True" ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Residence Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_resiadd" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Enter Residence Address !"
                                        ControlToValidate="txt_resiadd" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Pin Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_pincode" runat="server" MaxLength="50"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Office Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_cadd" runat="server" MaxLength="4000" TextMode="MultiLine" Rows="3"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Communication Address !"
                                        ControlToValidate="txt_cadd" Display="Dynamic" SetFocusOnError="True" ValidationGroup="reg"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Document PDF<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="FileUploadIdentityImage" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="FileUploadIdentityImage"
                                        Display="Dynamic" ErrorMessage="Please Select Identity Proof " ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="reg">*</asp:RequiredFieldValidator>

                                </div>
                            </div>

                           <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB Proof<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="FiledobressImage" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FiledobressImage" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                           <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload Form No. 49A(I)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fupForm49A" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fupForm49A"
                                        Display="Dynamic" ErrorMessage="Please Select Form " ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="reg">*</asp:RequiredFieldValidator>

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

