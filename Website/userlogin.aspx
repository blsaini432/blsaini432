<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlogin.aspx.cs" Inherits="userlogin" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login page</title>
    <link rel="stylesheet" type="text/css" href="Login/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="Login/css/style.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</head>
<body>
    <div class="login-section">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="login-form-area">
                        <div class="login-wrapper">
                            <div class="logo-area">
                                <img src="Login/img/logo.png" alt="">
                            </div>
                            <div class="card login-page">
                                <h1 class="login_title">Login to Continue...</h1>
                                <form class="card_form login-form-detail" runat="server">
                                    <div id="divlogin" runat="server">
                                        <div class="input login-filds">
                                            <asp:TextBox ID="txt_username" runat="server" MaxLength="180" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Username"></asp:TextBox>
                                            <%--<label class="input_label"><i class="fa fa-user-o" aria-hidden="true"></i><span></span></label>--%>
                                            <div class="bar"></div>
                                        </div>

                                        <div class="input login-filds">
                                            <asp:TextBox ID="txt_userpassword" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" autocomplete="off" placeholder="Password" TextMode="Password"></asp:TextBox>
                                            <%--<label class="input_label">
                                            <i class="fa fa-lock" aria-hidden="true"></i><span></span></label>--%>
                                            <div class="bar"></div>
                                        </div>
                                        <div class="lonin-links-filds">
                                            <span class="forget-password"><div class="lonin-links-filds"><span class="forget-password"><asp:LinkButton ID="linkbtnforgot" runat="server" Text="Forgot Password ?" OnClick="linkbtnforgot_Click" CssClass="auth-link text-black"></asp:LinkButton></span>                              
                                             <span class="forget-pin"><asp:LinkButton ID="LinkButton2" runat="server" Text="Forgot Pin ?" OnClick="linkbtnpin_Click" CssClass="auth-link text-black"></asp:LinkButton> </span></div></span>
                                     
                                        </div>
                                        <%--  <div class="lonin-links-filds"><span class="forget-password"><a href="#">Forget Password</a> </span><span class="forget-pin"><a href="#">Forget Pin</a> </span></div>--%>
                                        <asp:Button ID="btn_login" runat="server" Text="SIGN IN" CssClass="card_button" OnClick="btn_login_Click" />
                                    </div>
                                    <div id="divotp" runat="server" visible="false">
                                        <div class="input login-filds">
                                            <asp:TextBox ID="txt_otp" runat="server" MaxLength="6" onkeypress="blockSpace(event)" autocomplete="off" placeholder="Pin" class="form-control form-control-lg border-left-0" TextMode="Password"></asp:TextBox>

                                            <div class="bar"></div>
                                        </div>
                                        <asp:Button ID="btn_LoginVerify" runat="server" Text="Verify" CssClass="card_button" OnClick="btn_LoginVerify_Click" />

                                    </div>
                                     <div id="div_forgotpassword" runat="server" visible="false">
                                         <div class="input login-filds">
                                          <asp:TextBox ID="txt_email" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email / Mobile" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                      
                                            <div class="bar"></div>
                                              </div>
                                          <asp:Button ID="btn_forgot" runat="server" Text="Send" CssClass="card_button" OnClick="btn_forgot_Click" style="border-radius: 69px;"  />
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="card_button"></asp:LinkButton>
                                        </div>
                                     <div id="div_forgotpin" runat="server" visible="false">
                                         <div class="input login-filds">
                                          <asp:TextBox ID="txt_forgetpin" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" placeholder="Register Email / Mobile" class="form-control form-control-lg border-left-0" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                        
                                            <div class="bar"></div>
                                              </div>
                                          <asp:Button ID="btn_sendforgetpin" runat="server" Text="Send" CssClass="card_button" OnClick="btn_sendforgetpin_Click" style="border-radius: 69px;" />
                                        <asp:LinkButton 8ID="LinkButton3" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="card_button"></asp:LinkButton>
                                        </div>
                                </form>
                                <div class="card_info" style="font-weight: bold;">
                                   
                                      <p class="text-center"><asp:Label ID="lblCopyright" runat="server"></asp:Label></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <svg class="rabbit" version="1.2" viewBox="0 0 600 600">
        <defs>
            <filter id="goo">
                <feBlend in="SourceGraphic" in2="goo" />
            </filter>
        </defs>
    </svg>
    <script src="Login/js/jquery-3.2.1.min.js"></script>
    <script src="Login/js/bootstrap.min.js"></script>
</body>
</html>


