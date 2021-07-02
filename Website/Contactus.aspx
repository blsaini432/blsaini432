<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="Contactus.aspx.cs" Inherits="Contactus" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style type="text/css">
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:20%; text-align:left;}
</style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="about_us page-section" id="about"> 
    <div class="container">
        <h2>Contact Us</h2>
      <asp:Repeater ID="repPage" runat="server">
                    <ItemTemplate>
       
        <div class="row" style="margin-top:100px">
            <div class="col-md-6">
                        <p><asp:Literal ID="Literal1" runat="server" Text='<%#Eval("PageDesc")%>'></asp:Literal></p>
            </div>
             </ItemTemplate>
                </asp:Repeater>
            <div class="col-md-6">
                <h5>
                    Write us here</h5>
                <div class="mobile-recharge">
                    <p class="form-row"> <br />
                        <label>
                           Full Name
                        </label>
                       <br />
                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Enter First Name.."
                           ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                            CssClass="rfv" ErrorMessage="Please Enter First Name !" ValidationGroup="vgProfile"
                            SetFocusOnError="true" Display="None"><img src="images/warning.png" /></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Enter last Name.."
                            ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                            CssClass="rfv" ErrorMessage="Please Enter Last Name !" ValidationGroup="vgProfile"
                            SetFocusOnError="true" Display="None"><img src="images/warning.png" /></asp:RequiredFieldValidator><br />
                    </p>
                    <p class="form-row"> <br />
                        <label>
                            E-mail
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter email.."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            CssClass="rfv" ErrorMessage="Please Enter Email !" ValidationGroup="vgProfile"
                            SetFocusOnError="true" Display="None"><img src="images/warning.png" /></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                            ControlToValidate="txtEmail" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="vgProfile"><img src="images/warning.png"/></asp:RegularExpressionValidator>
                    </p>
                    <p class="form-row"> <br />
                        <lable>
                                            Mobile
                                        </lable>
                        <asp:TextBox ID="txtmobile" runat="server" class="form-control" MaxLength="10" placeholder="Enter mobile.."></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterType="Numbers" TargetControlID="txtmobile">
                        </cc1:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Please Enter Mobile !"
                            ControlToValidate="txtmobile" Display="None" SetFocusOnError="True" ValidationGroup="vgProfile"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                    </p>
                    <p class="form-row"> <br />
                        <label>
                            Query / Suggestions
                        </label>
                        <asp:TextBox ID="txtIssue" runat="server" class="form-control" TextMode="MultiLine"
                            placeholder="Enter your request information.." Height="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Query or Suggestions !"
                            ControlToValidate="txtIssue" Display="None" SetFocusOnError="True" ValidationGroup="vgProfile"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                    </p>
                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="vgProfile"
                        OnClick="btnSave_Click" />
                    <asp:ValidationSummary ID="vsProfile" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="vgProfile" />
                </div>
            </div>
        </div>
        </section>
</asp:Content>
