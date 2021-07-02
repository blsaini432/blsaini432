<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ForgotPassowrd.aspx.cs" Inherits="ForgotPassowrd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="css/sing.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="jsUnit mainCnt">

        <section id="formScreen">
            <div class="container">
                <div class="col-md-6">
                    <h1 class=""><b>Forgot Passowrd</b></h1>
                </div>
            </div>
        </section>

        <section id="contScreen">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 col-xs-12 fr">
                        <div class="formWrap" id="signup-form">
                            <div class="FPFormInner">
                                <h2>Forgot Passowrd<b></b></h2>                            

                                    <div class="raw clear" id="step11" runat="server" style="display: block">

                                    <div class="raw clear">
                                      
                                       <asp:TextBox ID="txtFGEmailForgot" runat="server" class="form-control" placeholder="Registered Email ID" ValidationGroup="FGForgot"  MaxLength="30"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="rfvFGEmailForgot" runat="server" CssClass="rfv" ControlToValidate="txtFGEmailForgot" ErrorMessage="Please Enter your registered Email ID !" ValidationGroup="FGForgot" SetFocusOnError="true"><img src="images/warning.png"/></asp:RequiredFieldValidator>
                                       <asp:RegularExpressionValidator ID="revFGEmailForgot" runat="server" CssClass="rfv" ControlToValidate="txtFGEmailForgot" ErrorMessage="Invalid Email !" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FGForgot"><img src="images/warning.png"/></asp:RegularExpressionValidator>
                                    </div>             
                                    <div class="clear">
                                        <asp:Button ID="btnFGSend" runat="server" Text="Send" class="btn btn-primary mybtn" OnClick="btnFGSend_Click" ValidationGroup="FGForgot" />
                                        <a href="Signup.aspx"><span style="color: #019934; float:right;">Sign Up !!!</span></a>
                                       <asp:ValidationSummary ID="vsFGForgot" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="FGForgot" />                                      
                                    </div>                               
                            </div>
                        </div>
                    </div>
                     </div>
                    <div class="col-md-12 col-xs-12">
                        <ul id="signFeature">
                            <li>
                                <span><i class="fa fa-certificate"></i></span>
                                <div>
                                    <h3>100% ASSURANCE</h3>
                                    <p>You have our 100% assurance for secure transaction every time you book with us. Book on PAYUJUNC from anywhere</p>
                                </div>
                            </li>
                            <li>
                                <span><i class="fa fa-delicious"></i></span>
                                <div>
                                    <h3>PAYUJUNC TRUST</h3>
                                    <p>We trust you and value your money, We issue refunds with a no questions asked guarantee, that's our promise!</p>
                                </div>
                            </li>
                            <li>
                                <span><i class="fa fa-money"></i></span>
                                <div>
                                    <h3>PAYMENT OPTIONS</h3>
                                    <p>Pay easily in few clicks. Choose from secure payment options such as Credit/Debit Card, Net Banking and Cash Card</p>
                                </div>
                            </li>
                        </ul>
                    </div>

               
                <!-- end of row -->
            </div>
        </section>

    </div>

</asp:Content>

