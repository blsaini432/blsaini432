<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master"
    AutoEventWireup="true" CodeFile="Fast_TagActivation.aspx.cs" Inherits="Root_DistributorFast_TagActivation"
    MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
                <ol class="breadcrumb">
                   
                </ol>
            </section>
    <section>
             <table class="table table-bordered table-hover ">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Underline="True" 
                                Text="Fast Tag Activation"></asp:Label>
                                </td>
                                </tr>
                    <tr>
                        <td>
                            <div id="wizard" class="swMain">
                                <br />
                                <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>&nbsp;&nbsp;
                                <br />
                                  <div>
                                        <table class="table table-bordered table-hover ">
                                            <tr>
                                                <td style="text-align:center">
                                                    <div id="ddiv" runat="server" >
                                                        <table class="table">
                                                            <tr>
                                                                <td class="aleft" colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr id="pan2" runat="server">
                                                                <td>
                                                                    TID</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_tid" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_tid" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2"><strong>Personal Details</strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td>FirstName</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_firstname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_firstname" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>LastName</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_lastname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txt_lastname" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Mobile</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_mobile" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Email</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_email" ForeColor="Red"></asp:RequiredFieldValidator>

                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2"><strong>Vehicle Details</strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Vehicle Class</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_vehcileclass" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select Class</asp:ListItem>
                                                                        <asp:ListItem>Class-4</asp:ListItem>
                                                                        <asp:ListItem>Class-5</asp:ListItem>
                                                                        <asp:ListItem>Class-6</asp:ListItem>
                                                                        <asp:ListItem>Class-7</asp:ListItem>
                                                                        <asp:ListItem>Class-12</asp:ListItem>
                                                                        <asp:ListItem>Class-15</asp:ListItem>
                                                                        <asp:ListItem>Class-16</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="ddl_vehcileclass" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Vehicle Type</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_vehciletype" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select Type</asp:ListItem>
                                                                        <asp:ListItem>Commercial</asp:ListItem>
                                                                        <asp:ListItem>Non Comercial</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="ddl_vehciletype" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Vehicle Registration</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_vehiclereg" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txt_vehiclereg" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Chaiss Number</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_vehichaissnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txt_vehichaissnumber" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>RC Front&nbsp; Image</td>
                                                                <td>
                                                                    <asp:FileUpload ID="fup_frontimage" runat="server" CssClass="form-control"/>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="fup_frontimage" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>RC Back Image</td>
                                                                <td>
                                                                    <asp:FileUpload ID="fup_backimage" runat="server" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="fup_backimage" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                            </td>
                                                            </tr>
                                                      
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                        </div>
                                </td>
                                </tr>
                                </table>
            </section>
</asp:Content>--%>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Fast Tag Activation
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">TID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_tid" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter TID Number" ControlToValidate="txt_tid" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">FirstName<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_firstname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter First Name " ControlToValidate="txt_firstname" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Lastname<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_lastname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Last Name " ControlToValidate="txt_lastname" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Mobile<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Mobile Number" ControlToValidate="txt_mobile" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Email<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Email Number" ControlToValidate="txt_email" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vehicle Class<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_vehcileclass" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select Class</asp:ListItem>
                                            <asp:ListItem>Class-4</asp:ListItem>
                                            <asp:ListItem>Class-5</asp:ListItem>
                                            <asp:ListItem>Class-6</asp:ListItem>
                                            <asp:ListItem>Class-7</asp:ListItem>
                                            <asp:ListItem>Class-12</asp:ListItem>
                                            <asp:ListItem>Class-15</asp:ListItem>
                                            <asp:ListItem>Class-16</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Vehicle Class " ControlToValidate="ddl_vehcileclass" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vehicle Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_vehciletype" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select Type</asp:ListItem>
                                            <asp:ListItem>Commercial</asp:ListItem>
                                            <asp:ListItem>Non Comercial</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Vehicle Type" ControlToValidate="ddl_vehciletype" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vehicle Registration<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_vehiclereg" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Vehicle Registration" ControlToValidate="txt_vehiclereg" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">Chaiss Number<code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txt_vehichaissnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Chaiss Number" ControlToValidate="txt_vehichaissnumber" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">RC Front  Image<code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:FileUpload ID="fup_frontimage" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Upload RC Front " ControlToValidate="fup_frontimage" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">RC Back Image<code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:FileUpload ID="fup_backimage" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Upload RC Back" ControlToValidate="fup_backimage" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset"
                                            CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btn_Submit" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
