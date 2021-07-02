<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="SMSApiMaster.aspx.cs" Inherits="Root_Admin_SMSApiMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

            <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                SMS API
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
                                <label class="col-form-label">API Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                  <asp:TextBox ID="txtAPIName" runat="server" MaxLength="50" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAPIName" runat="server" ControlToValidate="txtAPIName"
                                Display="Dynamic" ErrorMessage="Please Enter API Name !" SetFocusOnError="True"
                                ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                             <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Recharge URL<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                 <asp:TextBox ID="txtURL" runat="server" MaxLength="250" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvURL" runat="server" ErrorMessage="Please Enter URL !"
                                ControlToValidate="txtURL" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="regUrl" runat="server"  ControlToValidate="txtURL" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$"  Text="Enter a valid URL" />  
                            </div>
                        </div>
                                
                            <div class="form-group row">
                            <div class="col-lg-3">
                                <asp:TextBox ID="txtprm1" runat="server" MaxLength="50" ValidationGroup="v" Text="uname" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm1" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                ControlToValidate="txtprm1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator><code>*</code>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtprm1val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm1val" runat="server" ErrorMessage="Please Enter Parameter-1 Value !"
                                ControlToValidate="txtprm1val" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>  
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <asp:TextBox ID="txtprm2" runat="server" MaxLength="50" ValidationGroup="v" Text="pwd" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                ControlToValidate="txtprm2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator><code>*</code>
                            </div>
                            <div class="col-lg-8">
                             <asp:TextBox ID="txtprm2val" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                ControlToValidate="txtprm2val" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                           <div class="form-group row">
                            <div class="col-lg-3">
                              <asp:TextBox ID="txtprm3" runat="server" MaxLength="50" ValidationGroup="v" Text="senderid" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                ControlToValidate="txtprm3" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator><code>*</code>
                            </div>
                            <div class="col-lg-8">
                           <asp:TextBox ID="txtprm3val" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Parameter-1 Value !"
                                ControlToValidate="txtprm3val" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <asp:TextBox ID="txtprm4" runat="server" MaxLength="50" ValidationGroup="v" Text="other" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-lg-8">
                            <asp:TextBox ID="txtprm4val" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                 <asp:TextBox ID="txtprm5" runat="server" MaxLength="50" ValidationGroup="v" Text="Other" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-lg-8">
                            <asp:TextBox ID="txtprm5val" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                               <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Value for Mobile<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                               <asp:TextBox ID="Txtprm6" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" Text="to" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Value for Message<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                              <asp:TextBox ID="txtprm7" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" Text="msg" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                               <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Any Other Value<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                              <asp:TextBox ID="Txtprm8" runat="server" MaxLength="50" ValidationGroup="v"  CssClass="form-control" Text="route=T" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                          <div class="form-group row">
                            <div class="col-lg-3">
                                
                            </div>
                            <div class="col-lg-8">
                               <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v"  class="btn btn-primary"
                                onclick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset"  class="btn btn-danger"
                                onclick="btnReset_Click" />
                            </div>
                        </div>
                        </div>
                    </div></ContentTemplate>
                </asp:UpdatePanel></div>
                       

    </asp:Content>



