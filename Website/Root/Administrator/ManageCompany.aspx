<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageCompany.aspx.cs" Inherits="Root_SuperAdmin_ManageCompany" ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Company Information
            </h3>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Company Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                    Display="Dynamic" ErrorMessage="Please Enter Company Name !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
               
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Company Owner</label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtCompanyOwner" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Phone</label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mobile<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile"
                                    Display="Dynamic" ErrorMessage="Please Enter Mobile !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server"
                                                    TargetControlID="txtMobile" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Email<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                    Display="Dynamic" ErrorMessage="Please Enter Email !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="v"></asp:RegularExpressionValidator>
                            </div>
                        </div>


                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Website<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtWebsite" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvWebsite" runat="server" ControlToValidate="txtWebsite"
                                    Display="Dynamic" ErrorMessage="Please Enter Website !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                                   <asp:RegularExpressionValidator ID="regUrl" runat="server"  ControlToValidate="txtWebsite" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$"  Text="Enter a valid URL" />  
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Fax</label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtFax" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                      
                      
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Copyright<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtCopyright" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCopyright" runat="server" ControlToValidate="txtCopyright"
                                    Display="Dynamic" ErrorMessage="Please Enter Copyright !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Address</label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">PinCode<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtPIN" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtPIN_FilteredTextBoxExtender" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txtPIN">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvPIN" runat="server" ControlToValidate="txtPIN"
                                    Display="Dynamic" ErrorMessage="Please Enter PIN !" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Country<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList ID="ddlCountryName" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCountryName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCountryName"
                                    Display="Dynamic" ErrorMessage="Please Select Country !" SetFocusOnError="True"
                                    ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">State<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList ID="ddlStateName" runat="server" AutoPostBack="True"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlStateName_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Select State"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ControlToValidate="ddlStateName"
                                    Display="Dynamic" ErrorMessage="Please Select State !" SetFocusOnError="True"
                                    ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">City<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList ID="ddlCityName" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="Select City"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="ddlCityName"
                                    Display="Dynamic" ErrorMessage="Please Select CityName !" SetFocusOnError="True"
                                    ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                                 <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Company Logo<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="FileUploadCompanyLogo" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvCompanyLogo" runat="server" ControlToValidate="FileUploadCompanyLogo"
                                    Display="Dynamic" ErrorMessage="Please Select Company Logo !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                       
                        <div class="form-group row">
                            <div class="col-lg-3">
                              
                            </div>
                            <div class="col-lg-8">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                ValidationGroup="v" class="btn btn-primary" OnClick="btnSubmit_Click"  />
                            <asp:HiddenField ID="hidCompanyLogo" runat="server" />
                            <asp:HiddenField ID="hidDiscountID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
            </ContentTemplate>
                <Triggers>
                      <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
</asp:Content>
