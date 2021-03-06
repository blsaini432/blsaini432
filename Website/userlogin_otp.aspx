<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlogin_otp.aspx.cs" Inherits="userlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Member Login</title>
    <link rel="stylesheet" href="Design/vendors/iconfonts/font-awesome/css/all.min.css" />
    <link rel="stylesheet" href="Design/vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="Design/vendors/css/vendor.bundle.addons.css" />
    <link rel="stylesheet" href="Design/css/style.css" />
    <link rel="shortcut icon" href="Design/images/favicon.png" />
</head>  
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid">
                <div class="row box-bd " style ="border: 2px solid #f2e2e2;padding: 14px;">
                    <div class="col-md-3">
                        <div class="brand-logo" >
                                        <asp:Image  runat="server" ID="imglogo" width="150px" height="60px"/>
                                    </div>
                    </div>
                    <div class="col-md-3">
                        <div class="contact-bar" style="color: blue">
                            
                                   <p style="font-weight:1000 !important" >Contact No:<%= Session["Mobile"].ToString() %></p>
                                
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mail-fild">
                            <ul class="socials-links"">                                                             
                                    <h6 style="color: #1c0af9;font-weight:1000 !important">Email ID:<%= Session["email"] %> </h6>                                                          
                                    <p style="font-weight:1000!important;color:blue">Follow Us :                             
                                    <a href="#"><img src="Uploads/User/Profile/face.png" width="23px" /></a>
                                  <a href="#"><img src="Uploads/User/Profile/insata.jpg" width="20px" /></a>
                                 <a href="#"><img src="Uploads/User/Profile/twiter.png" width="23px" /></a>
                                 <a href="#"><img src="Uploads/User/Profile/youtb.png" width="23px" /></a>
                                </p>
                               
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="join-btn" style="border: 1px solid aliceblue;background: aqua;padding: 8px;margin: 11px;width: 77px;border-radius: 19px;color: burlywood;">
                            <a style="color:black" href="/MemberSignup.aspx">JOIN US</a>
                             
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
                    <div class="row flex-grow">     
                        <div class="col-lg-6">
                            <asp:Repeater runat="server" ID="repeater1">
                                <ItemTemplate>
                                    <asp:Image class="mySlides" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" height="390px" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-lg-6 d-flex align-items-center justify-content-center">
                            <div class="auth-form-transparent text-left p-3">
                                <div class="brand-logo" style="color:#0b0be8"> Enter Your Credential’s to continue..</div>
                                <h6 class="font-weight-light"></h6>
                                <div id="divlogin" runat="server">
                                    <div class="form-group">
                                        <%-- <label for="exampleInputEmail">Username</label>--%>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="fa fa-user text-primary"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txt_username" runat="server" MaxLength="180" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Please Enter Username"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <%-- <label for="exampleInputPassword">Password</label>--%>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="fa fa-lock text-primary"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txt_userpassword" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" placeholder="Please Enter Password" autocomplete="off" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-2 d-flex justify-content-between align-items-center">
                                        <%--<div class="form-check">
                                            <label class="form-check-label text-muted">
                                            </label>
                                        </div>--%>
                                        <asp:LinkButton ID="linkbtnforgot" runat="server" Text="Forgot Password ?" OnClick="linkbtnforgot_Click" CssClass="auth-link text-black"></asp:LinkButton>
                                        <br />
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Forgot Pin ?" OnClick="linkbtnpin_Click" CssClass="auth-link text-black"></asp:LinkButton>

                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_login" runat="server" Text="LOGIN" CssClass="btn btn-block btn-info btn-lg font-weight-medium auth-form-btn" OnClick="btn_login_Click" />
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
                                            <asp:TextBox ID="txt_otp" runat="server" MaxLength="6" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Transaction OTP" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_LoginVerify" runat="server" Text="Verify" CssClass="btn btn-block btn-info btn-lg font-weight-medium auth-form-btn" OnClick="btn_LoginVerify_Click" />
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
                                            <asp:TextBox ID="txt_email" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email / Mobile" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_forgot" runat="server" Text="Send" CssClass="btn btn-block btn-info btn-lg font-weight-medium auth-form-btn" OnClick="btn_forgot_Click" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="auth-link text-black"></asp:LinkButton>
                                    </div>
                                </div>



                                <div id="div_forgotpin" runat="server" visible="false">
                                    <div class="form-group">
                                        <label for="exampleInputEmail">Email</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend bg-transparent">
                                                <span class="input-group-text bg-transparent border-right-0">
                                                    <i class="fa fa-user text-primary"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txt_forgetpin" runat="server" MaxLength="25" onkeypress="blockSpace(event)" autocomplete="off" class="form-control form-control-lg border-left-0" placeholder="Register Email / Mobile" oncut="return false;" onpaste="return false;" oncopy="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="my-3">
                                        <asp:Button ID="btn_sendforgetpin" runat="server" Text="Send" CssClass="btn btn-block btn-info btn-lg font-weight-medium auth-form-btn" OnClick="btn_sendforgetpin_Click" />
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="Back to Home" OnClick="LinkButton1_Click" CssClass="auth-link text-black"></asp:LinkButton>
                                    </div>

                                </div>
                              
                            </div>
                            
                        </div>
                          <p class="copyj" style="padding-left:44%;margin-top:-60px"><asp:Label ID="lblCopyright" runat="server"></asp:Label></p>
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

        <script>
            var slideIndex = 0;
            carousel();
            function carousel() {
                var i;
                var x = document.getElementsByClassName("mySlides");
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = "none";
                }
                slideIndex++;
                if (slideIndex > x.length) { slideIndex = 1 }
                x[slideIndex - 1].style.display = "block";
                setTimeout(carousel, 10000);
            }
     </script>
        <!-- endinject -->
    </form>
</body>
</html>
