<%@ Control Language="C#" AutoEventWireup="true" CodeFile="udyogaadhar.ascx.cs" Inherits="Root_UC_udyogaadhar" %>
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

<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Udyog Aadhara Form
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
                                <label class="col-form-label" style="color: green">Amount For Udyog Aadhara (In Rs):-</label>

                            </div>
                            <div class="col-lg-8">

                                <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar No<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_aadhar" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_aadhar" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                            </div>
                          </div>
                <div class="form-group row">
                    <div class="col-lg-3">
                        <label class="col-form-label">Full Name<code>*</code></label>
                    </div>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txt_fullname" runat="server" MaxLength="50" CssClass="form-control"
                            onkeypress=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_fullname" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                    </div>
                </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">NSocial Categoryame<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:DropDownList ID="txt_soccat" runat="server" Width="124px" Height="35px" CssClass="form-control">
                                        <asp:ListItem>SC</asp:ListItem>
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>ST</asp:ListItem>
                                        <asp:ListItem>OBC</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_soccat" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Gender<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="83px">
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_mob" runat="server" MaxLength="10" CssClass="form-control"
                                        onkeypress=""></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Phone number" ControlToValidate="txt_mob" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email id<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_email" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress=""></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Physically Handicapped<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddl_phyhan" runat="server" Height="40px" Width="120px" CssClass="form-control">
                                        <asp:ListItem>yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_phyhan" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name of Enterprise<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_nameofent" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_nameofent" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Nature- Of -Business<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_tyoforg" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_tyoforg" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">PAN - Number<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_panno" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_panno" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_resadd" runat="server" MaxLength="1000"  CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_resadd" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                         <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Shop Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_shop" runat="server" MaxLength="1000"  CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_resadd" ErrorMessage="*" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Date of Commencemen<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_daofcomm" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_daofcomm" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>

                                    <cc1:CalendarExtender runat="server" ID="txt_new" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_daofcomm" TargetControlID="txt_daofcomm">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_daofcomm"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="d"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <label>Bank Details</label>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">
                                        Account No
                                    <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_accno" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_accno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">IFSC Code<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_ifsc" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_ifsc" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Persons of Employed<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_noemp" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_noemp" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Investment (In Lakhs)<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_invt" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_invt" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <label>Required Documents-</label>



                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Aadhar Card<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_Aadhar" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fup_Aadhar" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">PAN Card<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_PAN" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="fup_PAN" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Bank Passbook<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:FileUpload ID="fup_bkpass" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="fup_bkpass" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        <label>Not compulsory-</label>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Files1<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_file1" runat="server" AllowMultiple="true" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Files2<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_file2" runat="server" AllowMultiple="true" />
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Files3<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_file3" runat="server" AllowMultiple="true" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Files4<code>*</code></label>
                                </div>
                                <div class="col-lg-8">

                                    <asp:FileUpload ID="fup_file4" runat="server" AllowMultiple="true" />
                                </div>
                            </div>


                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="d" class="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="d" class="btn btn-primary" OnClick="btnReset_Click" />

</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
