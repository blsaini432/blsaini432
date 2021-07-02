<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlogin_ol2.aspx.cs" Inherits="userlogin" %>

<!doctype html>
<html class="no-js" lang="">
<!-- Mirrored from affixtheme.com/html/xmee/demo/login-28.html by HTTrack Website Copier/3.x [XR&CO'2014], Thu, 08 Apr 2021 19:37:01 GMT -->
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Xmee | Login and Register Form Html Templates</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="img/favicon.png">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="newlogin/css/bootstrap.min.css">
    <!-- Fontawesome CSS -->
    <link rel="stylesheet" href="newlogin/css/fontawesome-all.min.css">
    <!-- Flaticon CSS -->
    <link rel="stylesheet" href="newlogin/font/flaticon.css">
    <!-- Google Web Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&amp;display=swap" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="newlogin/style.css" rel="stylesheet" />

</head>

<body>
   
    <!--[if lt IE 8]>
        <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
    <![endif]-->
    <section class="fxt-template-animation fxt-template-layout28" data-bg-image="newlogin/img/figure/bg28-l.jpg">
          <form id="form1" >
        <div class="fxt-content">
            <div class="fxt-header">
                <div class="">
                 <asp:Image runat="server" ID="imglogo" Width="150px" Height="60px" />
                </div>
                <ul class="fxt-switcher-wrap">
                    <li><a href="login-28.html" class="switcher-text active">Login</a></li>
                    <li><a href="register-28.html" class="switcher-text inline-text">Register</a></li>
                    <li><a href="forgot-password-28.html" class="switcher-text">Forgot Password</a></li>
                </ul>
            </div>
            <div class="fxt-form">
                <div class="fxt-transformY-50 fxt-transition-delay-1">
                    <p>Login into your account</p>
                </div>
                        <div class="form-group">
                            <div class="fxt-transformY-50 fxt-transition-delay-2">
                                
                               <input type="email" id="emaolil" class="form-control" name="email" placeholder="Email" required="required">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="fxt-transformY-50 fxt-transition-delay-3">
                               <input type="email" id="email" class="form-control" name="email" placeholder="Email" required="required">
                                <i toggle="#password" class="fa fa-fw fa-eye toggle-password field-icon"></i>
                            </div>
                        </div>
                 
                    <div id="divotp" runat="server" visible="false">
                        <div class="form-group">
                            <div class="fxt-transformY-50 fxt-transition-delay-3">
                              <%--  <asp:TextBox ID="txt_otp" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" placeholder="Please Enter Password" autocomplete="off" TextMode="Password"></asp:TextBox>--%>
                                <i toggle="#password" class="fa fa-fw fa-eye toggle-password field-icon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="fxt-transformY-50 fxt-transition-delay-4">
                            <div class="fxt-checkbox-area">
                                <div class="checkbox">
                                    <input id="checkbox1" type="checkbox">
                                    <label for="checkbox1">Keep me logged in</label>
                                </div>

                               
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div class="fxt-style-line">
                <div class="fxt-transformY-50 fxt-transition-delay-5">
                    <h3>Or Login With</h3>
                </div>
            </div>
            <ul class="fxt-socials">
                <li class="fxt-facebook fxt-transformY-50 fxt-transition-delay-6">
                    <a href="#" title="Facebook"><i class="fab fa-facebook-f"></i></a>
                </li>
                <li class="fxt-twitter fxt-transformY-50 fxt-transition-delay-7">
                    <a href="#" title="twitter"><i class="fab fa-twitter"></i></a>
                </li>
                <li class="fxt-google fxt-transformY-50 fxt-transition-delay-8">
                    <a href="#" title="google"><i class="fab fa-google-plus-g"></i></a>
                </li>
                <li class="fxt-linkedin fxt-transformY-50 fxt-transition-delay-9">
                    <a href="#" title="linkedin"><i class="fab fa-linkedin-in"></i></a>
                </li>
                <li class="fxt-pinterest fxt-transformY-50 fxt-transition-delay-10">
                    <a href="#" title="pinterest"><i class="fab fa-pinterest-p"></i></a>
                </li>
            </ul>
        </div>
    </section>
    <!-- jquery-->
    <script src="newlogin/js/jquery-3.5.0.min.js"></script>
    <!-- Popper js -->
    <script src="newlogin/js/popper.min.js"></script>
    <!-- Bootstrap js -->
    <script src="newlogin/js/bootstrap.min.js"></script>
    <!-- Imagesloaded js -->
    <script src="newlogin/js/imagesloaded.pkgd.min.js"></script>
    <!-- Particles js -->
    <script src="newlogin/js/particles.js"></script>
    <script src="newlogin/js/particles-3.js"></script>
    <!-- Validator js -->
    <script src="newlogin/js/validator.min.js"></script>
    <!-- Custom Js -->
    <script src="newlogin/js/main.js"></script>
    <script src="newlogin/js/main.js"></script>
</body>


<!-- Mirrored from affixtheme.com/html/xmee/demo/login-28.html by HTTrack Website Copier/3.x [XR&CO'2014], Thu, 08 Apr 2021 19:37:08 GMT -->
</html>
