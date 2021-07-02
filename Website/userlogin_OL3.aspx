
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
                                    <div class="input login-filds">
                                     
                                          <asp:TextBox ID="txt_username" runat="server"  MaxLength="180" autocomplete="off" class="input_field" placeholder="Username"></asp:TextBox>

                                        <label class="input_label"><i class="fa fa-user-o" aria-hidden="true"></i><span>Enter Mobile Number</span></label>
                                        <div class="bar"></div>
                                    </div>

                                    <div class="input login-filds">
                                           <asp:TextBox ID="txt_userpassword" runat="server" MaxLength="50" class="input_field" autocomplete="off" placeholder="Password" TextMode="Password"></asp:TextBox>
                                        <label class="input_label">
                                            <i class="fa fa-lock" aria-hidden="true"></i><span>Password</span></label>
                                        <div class="bar"></div>
                                    </div>
                                    <div class="lonin-links-filds"><span class="forget-password"><a href="#">Forget Password</a> </span><span class="forget-pin"><a href="#">Forget Pin</a> </span></div>
                                  
                                     <asp:Button ID="btn_login" runat="server" Text="LOGIN" CssClass="card_button" OnClick="btn_login_Click" />
                                </form>
                                <div class="card_info">
                                    <p class="text-center">© Copyright 2021 Ezulix.com </p>
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
