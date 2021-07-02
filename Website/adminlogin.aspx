<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminlogin.aspx.cs" Inherits="adminlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Admin Login</title>
    <link rel="stylesheet" href="Design/vendors/iconfonts/font-awesome/css/all.min.css" />
    <link rel="stylesheet" href="Design/vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="Design/vendors/css/vendor.bundle.addons.css" />
    <link rel="stylesheet" href="Design/css/style.css" />
    <link rel="shortcut icon" href="Design/images/favicon.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
                    <div class="row flex-grow">
                        <div class="col-lg-6 d-flex align-items-center justify-content-center">
                            <div class="auth-form-transparent text-left p-3">
                                <div class="brand-logo">
                                    <asp:Image runat="server" id="imglogo"/>
                                </div>
                                <h4>Welcome back!</h4>
                                <h6 class="font-weight-light">Happy to see you again!</h6>
                                <div id="divlogin" runat="server">
                                <div class="form-group">
                                    <label for="exampleInputEmail">Username</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="fa fa-user text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txt_username" runat="server" MaxLength="180" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Username" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword">Password</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="fa fa-lock text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txt_userpassword" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" placeholder="Password" oncut="return false;" onpaste="return false;" oncopy="return false;" autocomplete="off" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="my-2 d-flex justify-content-between align-items-center">
                                    <div class="form-check">
                                        <label class="form-check-label text-muted">
                                        </label>
                                    </div>
                                   <%-- <a href="#" class="auth-link text-black">Forgot password?</a>--%>
                                </div>
                                <div class="my-3">
                                    <asp:Button ID="btn_login" runat="server" Text="LOGIN" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btn_login_Click" />
                                </div>
                                    </div>
                                <div id="divotp" runat="server" visible="false">
                                    <div class="form-group">
                                    <label for="exampleInputEmail">OTP</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="fa fa-user text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txt_otp" runat="server" MaxLength="6" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="OTP" oncut="return false;" onpaste="return false;" oncopy="return false;" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="my-3">
                                    <asp:Button ID="btn_LoginVerify" runat="server" Text="Verify" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btn_LoginVerify_Click"/>
                                </div>
                                </div>
                                   <div id="div_forgotpassword" runat="server" visible="false">
                                    <div class="form-group">
                                    <label for="exampleInputEmail">Email</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="fa fa-user text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txt_email" runat="server" MaxLength="6" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="my-3">
                                    <asp:Button ID="btn_forgot" runat="server" Text="Send" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btn_forgot_Click"/>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 login-half-bg d-flex flex-row">
                            <p class="text-white font-weight-medium text-center flex-grow align-self-end"><asp:Label ID="lblCopyright" runat="server"></asp:Label></p>
                        </div>
                    </div>
                </div>
                <!-- content-wrapper ends -->
            </div>
            <!-- page-body-wrapper ends -->
        </div>
        <!-- container-scroller -->
        <!-- plugins:js -->
        <script src="Design/vendors/js/vendor.bundle.base.js"></script>
        <script src="Design/vendors/js/vendor.bundle.addons.js"></script>
        <!-- endinject -->
        <!-- inject:js -->
        <script src="Design/js/off-canvas.js"></script>
        <script src="Design/js/hoverable-collapse.js"></script>
        <script src="Design/js/misc.js"></script>
        <script src="Design/js/settings.js"></script>
        <script src="Design/js/todolist.js"></script>
        <script src="Design/js/disablekey.min.js"></script>
        <script type="text/javascript">
            function blockSpace(evt) {
                if (evt.which === 32)
                    evt.preventDefault ? evt.preventDefault() : (evt.returnValue = false);
            }
            function removeallspaces(el) {
                // removes leading and trailing spaces  // replaces multiple spaces with one space  // Removes spaces after newlines
                el.value = el.value.replace(/(^\s*)|(\s*$)/gi, "").replace(/[ ]{2,}/gi, " ").replace(/\n +/, "\n");
                return;
            }
        </script>
       <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>

        <script type="text/javascript">
            $(function () {
                $('#btn_login').click(function () {
                    $(this).html('<img src="http://www.bba-reman.com/images/fbloader.gif" />');
                })
            })
        </script>
        <!-- endinject -->
    </form>
</body>
</html>
