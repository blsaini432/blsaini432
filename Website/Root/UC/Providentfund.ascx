<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Providentfund.ascx.cs" Inherits="Root_UC_Providentfund" %>
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


    //-->
</script>
<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title"> Provident Fund Form
        </h3>
    </div>
    <div class="col-12">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label" style="color: green">Amount For Provident Fund (In Rs):-</label>

                            </div>
                            <div class="col-lg-8">
                                <asp:Label ID="lblamt" Font-Bold="true" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Applicant's Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_name" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_name" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Gender<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
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
                                <label class="col-form-label">Name As Per PF Account<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_fem" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_fem" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">DOB As Per PF Account<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_date" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_date" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>

                                <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_date" TargetControlID="txt_date">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_date"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name As Per Pan<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_rename" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_rename" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_rel" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_rel" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">DOB As Per Pan<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_dpan" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_dpan" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>

                                <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_dpan" TargetControlID="txt_dpan">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_dpan"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name As Per Aadhar<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_par" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_par" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_sta" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_sta" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">DOB As Per Aadhar<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_dis" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dis" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>

                                <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_dis" TargetControlID="txt_dis">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_dis"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Residence Address<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_add" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_add" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">UAN Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_uann" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator155" runat="server" ControlToValidate="txt_uann" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">UAN Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_uanp" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_uanp" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <lable>	Required Documents-</lable>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_photo" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="fup_photo" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_age" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="fup_age" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Check Or Passbook Photo<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_passbook" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="fup_age" ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File1<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file" runat="server" AllowMultiple="true" />
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
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="c" class="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="c" class="btn btn-primary" OnClick="btnReset_Click" />

                    </div>
                    <?
                </div>
            </div>
        </div>
    </div>
</div>
