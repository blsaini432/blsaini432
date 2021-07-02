<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Refundapi.aspx.cs" Inherits="Refundapi" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>uti pan</title>
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
                               
                            </div>
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