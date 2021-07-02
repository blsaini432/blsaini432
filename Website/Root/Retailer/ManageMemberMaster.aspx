<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true"
    CodeFile="ManageMemberMaster.aspx.cs" Inherits="Root_Retailer_ManageMemberMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage Member Master 
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Basic Setting</h4>
                        <div class="form-group">
                            <label for="exampleInputName1">Member Type<code>*</code></label>
                            <asp:Label ID="lblmembertype" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="exampleInputEmail3">Package</label>
                            <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                Display="Dynamic" ErrorMessage="Please Select Package !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 grid-margin">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Member Information</h4>

                        <p class="card-description">
                            Personal info
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">First Name<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                            Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Last Name<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="form-control" autcomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                            Display="Dynamic" ErrorMessage="Please Enter LastName !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Aadhar</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_aadhar" autcomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txt_aadhar" ValidationExpression="^(\d{12}|\d{16})$" ErrorMessage="enter valid aadharnumber" ValidationGroup="v" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">PAN</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_PAN" autcomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txt_PAN" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ErrorMessage="enter valid pancardnumber" ValidationGroup="v" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <p class="card-description">
                            Business Details
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Shop Name</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_ShopName" autcomplete="off" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Shop Address</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_shopaddress" autcomplete="off" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">GST Number</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_gstnumber" runat="server" autcomplete="off" CssClass="form-control" MaxLength="15"></asp:TextBox>


                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Business PAN Number</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_businesspan" autcomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txt_businesspan" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ErrorMessage="enter valid business pancardnumber" ValidationGroup="v" />

                                    </div>
                                </div>
                            </div>
                        </div>
                        <p class="card-description">
                            Address
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Address<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtAddress" autcomplete="off" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress"
                                            Display="Dynamic" ErrorMessage="Please Enter Address !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Country<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlCountryName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryName_SelectedIndexChanged"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCountryName"
                                            Display="Dynamic" ErrorMessage="Please Select Country !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">State<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlStateName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStateName_SelectedIndexChanged"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ControlToValidate="ddlStateName"
                                            Display="Dynamic" ErrorMessage="Please Select State !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Postcode<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtZIP" autcomplete="off" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtZIP_FilteredTextBoxExtender" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txtZIP">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter ZipCode !"
                                            ControlToValidate="txtZIP" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">City<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlCityName" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select City</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="ddlCityName"
                                            Display="Dynamic" ErrorMessage="Please Select City !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <p class="card-description">
                            Contact
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Email<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtEmail" autcomplete="off" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                                            ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="v"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email !"
                                            ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Mobile<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Mobile !"
                                            ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mobile Number is not valid !"
                                            ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([0]|\+91)?[6789]\d{9}$"
                                            ValidationGroup="v"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">LandLine STDCode</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSTDCode" autcomplete="off" runat="server" MaxLength="10" placeholder="STD Code"
                                            CssClass="form-control"></asp:TextBox>&nbsp;
                                                    <cc1:FilteredTextBoxExtender ID="txtSTDCode_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtSTDCode">
                                                    </cc1:FilteredTextBoxExtender>


                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Landline Number</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtLadline" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtLadline_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtLadline">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--<p class="card-description">
                            Account Details
                        </p>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Account Number<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_accountnumber" autcomplete="off" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">IFSC CODE<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_ifsc" autcomplete="off" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Bank Name<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_bankname" autcomplete="off" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <p class="card-description">
                            Profile Pic
                        </p>
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Upload Pic (Only in Jpg/PNG)</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload ID="fupmppic" runat="server" CssClass="form-control" />
                                        <asp:Image ID="imgUser" runat="server" Height="150" Width="150" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <div class="col-sm-9">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />

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
