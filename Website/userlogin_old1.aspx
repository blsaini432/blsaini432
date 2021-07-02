<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlogin_old1.aspx.cs" Inherits="userlogin" %>
<!DOCTYPE html>
<html lang="en">
<!-- Mirrored from www.urbanui.com/melody/template/pages/samples/lock-screen.html by HTTrack Website Copier/3.x [XR&CO'2014], Sat, 03 Aug 2019 12:21:43 GMT -->
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Melody Admin</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../../Design/vendors/iconfonts/font-awesome/css/all.min.css">
    <link rel="stylesheet" href="../../Design/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="../../Design/vendors/css/vendor.bundle.addons.css">
    <!-- endinject -->
    <!-- plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../../Design/css/style.css">1
    <!-- endinject -->
    <link rel="shortcut icon" href="../../Design/images/favicon.png" />

<body>
     <form id="form1" runat="server">
    <div class="container-scroller">
        <div class="container-fluid page-body-wrapper full-page-wrapper">
            <div class="content-wrapper d-flex align-items-center auth lock-full-bg">
                <div class="row w-100">
                    <div class="col-lg-4 mx-auto">
                        <div class="auth-form-transparent text-left p-5 text-center">
                               <asp:Image  runat="server" ID="imglogo" class="lock-profile-img"  />
                           <%-- <img src="../../Design/images/faces/face13.jpg" class="lock-profile-img" alt="img">--%>
                            <a class="pt-5">
                                <div id="divlogin" runat="server">
                                    <div class="form-group">
                                        
                                        <asp:TextBox ID="txt_username" runat="server" MaxLength="180" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Please Enter Username"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txt_userpassword" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" placeholder="Please Enter Password" autocomplete="off" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="mt-5">
                                         <asp:Button ID="btn_login" runat="server" Text="LOGIN"  CssClass="btn btn-block btn-success btn-lg font-weight-medium" OnClick="btn_login_Click"  />
                                       
                                    </div>
                                    <div class="mt-3 text-left">
                                        <asp:LinkButton ID="linkbtnforgot" runat="server" Text="Forgot Password ?" OnClick="linkbtnforgot_Click" CssClass="auth-link text-white"></asp:LinkButton>
                                        &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 
                                       
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Forgot Pin ?" OnClick="linkbtnpin_Click" CssClass="auth-link text-white"></asp:LinkButton>
                                       
                                    </div>
                                </div>
                                 <div id="divotp" runat="server" visible="false">
                                    <div class="form-group">
                                      <%--  <label for="exampleInputEmail">PIN</label>--%>
                                        <div class="input-group">
                                            
                                            <asp:TextBox ID="txt_otp" runat="server" MaxLength="6" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Transaction PIN" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_LoginVerify" runat="server" Text="Verify" CssClass="btn btn-block btn-success btn-lg font-weight-medium auth-form-btn" OnClick="btn_LoginVerify_Click" />
                                    </div>
                                </div>
                                 <div id="div_forgotpassword" runat="server" visible="false">
                                    <div class="form-group">
                                        <%--<label for="exampleInputEmail">Email</label>--%>
                                        <div class="input-group">
                                            
                                            <asp:TextBox ID="txt_email" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email / Mobile" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_forgot" runat="server" Text="Send" CssClass="btn btn-block btn-success btn-lg font-weight-medium auth-form-btn" OnClick="btn_forgot_Click"   />
                                    <br />
                                         <asp:LinkButton ID="LinkButton1" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="auth-link text-white"></asp:LinkButton>
                                    </div>
                                </div>
                                  <div id="div_forgotpin" runat="server" visible="false">
                                    <div class="form-group">
                                      <%--  <label for="exampleInputEmail">Email</label>--%>
                                        <div class="input-group">
                                            
                                            <asp:TextBox ID="txt_forgetpin" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email / Mobile" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_sendforgetpin" runat="server" Text="Send" CssClass="btn btn-block btn-success btn-lg font-weight-medium auth-form-btn" OnClick="btn_sendforgetpin_Click" />
                                      <br />
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="auth-link text-white"></asp:LinkButton>
                                    </div>

                                </div>
                            </a>
                             <p class="my-3" style="color:white"><asp:Label ID="lblCopyright" runat="server"></asp:Label></p>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- content-wrapper ends -->
        </div>
        <!-- page-body-wrapper ends -->
    
         </form>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="../../Design/vendors/js/vendor.bundle.base.js"></script>
    <script src="../../Design/vendors/js/vendor.bundle.addons.js"></script>
    <!-- endinject -->
    <!-- inject:js -->
    <script src="../../Design/js/off-canvas.js"></script>
    <script src="../../Design/js/hoverable-collapse.js"></script>
    <script src="../../Design/js/misc.js"></script>
    <script src="../../Design/js/settings.js"></script>
    <script src="../../Design/js/todolist.js"></script>
</head>
    <!-- endinject -->
</body>
<!-- Mirrored from www.urbanui.com/melody/template/pages/samples/lock-screen.html by HTTrack Website Copier/3.x [XR&CO'2014], Sat, 03 Aug 2019 12:21:43 GMT -->
</html>
