<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master"
    AutoEventWireup="true" CodeFile="Aeps_reg.aspx.cs" Inherits="Root_Distributor_Aeps_reg"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">AEPS Registration Form            </h3>
        </div>

        <div class="row grid-margin">
            <div class="col-12">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="faq-section">
                                    <div class="container-fluid bg-success py-2">
                                        <p class="mb-0 text-white">Important Points</p>
                                    </div>
                                    <div id="accordion-1" class="accordion">
                                        <div class="card">
                                            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion-1" style="">
                                                <div class="card-body">
                                                    1. All Documents should be self attested and clearly Scanned . if aadharcard uploaded than front and back side should be in single image only<br />
                                                    2. If Name is different in Pan and Aadhar Card than upload self declaration form else not required.<br />
                                                    3. Document Upload Size should not be greater than 1 MB.<br />
                                                    4. All * Sign are mandatory fields.<br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">First Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Enter First Name"
                                            ValidationGroup="reg"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="Ftbe_Name" runat="server"
                                            TargetControlID="txtFirstName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Last Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                            ValidationGroup="reg" ErrorMessage="Enter Last Name"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtLastName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Shop Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Shopname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Shopname" runat="server" ErrorMessage="Enter Shop Name" ControlToValidate="txt_Shopname" ValidationGroup="reg"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            TargetControlID="txtLastName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>


                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">PAN Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Pan" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Enter PAN Number" runat="server" ControlToValidate="txt_Pan" ValidationGroup="reg"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ErrorMessage="Please check PAN Format" ControlToValidate="txt_Pan"
                                            ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="reg"></asp:RegularExpressionValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Mobile Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Mno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Mno" runat="server" ErrorMessage="Enter Mobile Number" ControlToValidate="txt_Mno" ValidationGroup="reg"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            TargetControlID="txt_Mno" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ControlToValidate="txt_Mno" ID="Rev_Name" ValidationExpression="^[0-9]{10,10}$" runat="server" ValidationGroup="reg"></asp:RegularExpressionValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">State<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddtPriState" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddtPriState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_PriState" ErrorMessage="Select State" runat="server" ControlToValidate="ddtPriState"
                                            ValidationGroup="reg" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">City<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlPriCity" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select City</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPriCity" ErrorMessage="Select City"
                                            ValidationGroup="reg" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Address<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_PriAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_PriAddress" runat="server" ErrorMessage="Select Address" ControlToValidate="txt_PriAddress" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">PIN Code<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_PriPin" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_PriPin" runat="server" ControlToValidate="txt_PriPin" ValidationGroup="reg" ErrorMessage="Select PIN Code"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            TargetControlID="txt_PriPin" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Select Address Proof<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_Address" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_Address"
                                            ValidationGroup="reg" InitialValue="0" ErrorMessage="Select Address Proof"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Address Proof Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_AddProofnumber" runat="server" placeholder="Enter Address Proof Numer" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_AddProofnumber" runat="server" ControlToValidate="txt_AddProofnumber" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    </div>
                                </div>


                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload Address Proof<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="fu_Address" runat="server" CssClass="form-control" />&nbsp;
                      
                        

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btn_Address" runat="server" Text="Upload Address" OnClick="btn_Address_Click" CssClass="btn btn-success btn-rounded btn-fw" />

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Self Declaration Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_SelftDeclaNumber" runat="server" placeholder="Self Declaration Numer" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload Self Declaration<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="fu_SelfDec" runat="server" CssClass="form-control" />


                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btn_SelDec" runat="server" Text="Upload Form"
                                            OnClick="btn_SelDec_Click" CssClass="btn btn-success btn-rounded btn-fw" />&nbsp;
                            <asp:Button ID="btn_Sefdownlod" runat="server" Text="Download Form" CssClass="btn btn-warning btn-fw"
                                OnClick="btn_Sefdownlod_Click" />
                                    </div>
                                </div>




                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" ValidationGroup="reg" Style="cursor: pointer" CssClass="btn btn-primary btn-fw" OnClick="Btn_Submit_Click" />
                                        <asp:Button ID="btn_Reset" runat="server" Text="Reset" Style="cursor: pointer" CssClass="btn btn-danger btn-fw"
                                            OnClick="btn_Reset_Click" />

                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
