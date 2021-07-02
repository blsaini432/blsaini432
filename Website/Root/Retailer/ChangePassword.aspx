<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Root_Member_ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
               Change Password
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
                                <label class="col-form-label">Current Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                   <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="20" TextMode="Password" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Current Password !"
                                ControlToValidate="txtCurrentPassword" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">New Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                               <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" TextMode="Password" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter New Password !"
                                ControlToValidate="txtNewPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator display="Dynamic" runat="server" id="RegularExpressionValidator2" errormessage="Password must be 8-10 characters long with at least one numeric,one alphabet and one special character." forecolor="Red" validationexpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<;])(?!.*\s).*$" controltovalidate="txtNewPassword"></asp:RegularExpressionValidator>


                            </div>
                        </div>

                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Confirm Password<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                 <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="20" TextMode="Password" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please Enter Confirm Password !"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Confirm Password does not match"
                                SetFocusOnError="True" ValidationGroup="v"></asp:CompareValidator>
                                <asp:RegularExpressionValidator display="Dynamic" runat="server" id="RegularExpressionValidator1" errormessage="Password must be 8-10 characters long with at least one numeric,one alphabet and one special character." forecolor="Red" validationexpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+}{:;'?/>.<;])(?!.*\s).*$" controltovalidate="txtConfirmPassword"></asp:RegularExpressionValidator>

                            </div>
                        </div>

                        
                      <div class="form-group row">
                            <div class="col-lg-3">
                                
                            </div>
                            <div class="col-lg-8">
                               <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
           
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger"  />
                   
                            </div>
                        </div>

                        </div></div></div></div></ContentTemplate>   <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
                </asp:UpdatePanel></div>
</asp:Content>
