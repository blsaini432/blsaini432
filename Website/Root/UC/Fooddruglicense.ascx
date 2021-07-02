<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fooddruglicense.ascx.cs" Inherits="Root_UC_Fooddruglicense" %>
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
        <h3 class="page-title">Food Registration  Form
        </h3>
    </div>
    <div class="col-12">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label" style="color: green">Amount For Food Licenses (In Rs):-</label>

                            </div>
                            <div class="col-lg-8">

                                <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>

                       <%-- <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Type<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                    <asp:ListItem>Select Type</asp:ListItem>
                                    <asp:ListItem>Food License 1 year</asp:ListItem>
                                    <asp:ListItem>Food License 3 year</asp:ListItem>
                                    <asp:ListItem>Food License 5 year</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_nameofcop" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name As Per Pan Card-<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_nameofcop" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nameofcop" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Gender<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:RadioButtonList ID="txt_nameofapp" runat="server">
                                    <asp:ListItem>MALE</asp:ListItem>
                                    <asp:ListItem>FEMALE</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_nameofapp" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mobile No<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_mob" runat="server" MaxLength="10" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_email" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Residence-Address<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_addofbus" runat="server" MaxLength="1000" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_addofbus" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Date of Birth<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_busstrdate" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_busstrdate" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                               
                                    <cc1:CalendarExtender runat="server" ID="txt_new" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_busstrdate" TargetControlID="txt_busstrdate">
                                    </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_busstrdate"
                                    Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="d"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan-Number<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_kindofbus" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_kindofbus" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar-Numbe<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_yearly" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress="return isAlphaNumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_yearly" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Shop-Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_tyofbus" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_tyofbus" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Shop-Address<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_corressadd" runat="server" MaxLength="50" CssClass="form-control"
                                    onkeypress=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_corressadd" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <label>Required Documents-</label>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Aadhar Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_aadcard" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="fup_aadcard" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Photo & Sign<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_photo" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="fup_photo" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Pan-Card<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_decform" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="fup_decform" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <%--<div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File1<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file" runat="server" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File2<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file2" runat="server" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File3<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file3" runat="server" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">File4<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file4" runat="server" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Download Declaration form<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <a href="../../Uploads/Declaration-Hindi.pdf">Download Form</a>
                               
                                 </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Upload Declaration Form<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fup_file5" runat="server" />
                            </div>
                        </div>--%>


                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="a" class="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="a" class="btn btn-primary" OnClick="btnReset_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

