<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pinrset.aspx.cs" Inherits="pinrset" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>reset password</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../../Design/vendors/iconfonts/font-awesome/css/all.min.css" />
    <link rel="stylesheet" href="../../Design/vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="../../Design/vendors/css/vendor.bundle.addons.css" />
    <link href="../../Design/Files/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../../Design/Files/responsive.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- endinject -->
    <!-- plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../../Design/css/style.css">
    <style>
        #loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../../Design/images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
 

  
    
  
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
    <style>
        .input {
            float: left;
            width: 87%;
        }
    </style>
</head>
<body>
   
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Pin<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                     <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtpassword"></asp:RequiredFieldValidator>
                                         <cc1:FilteredTextBoxExtender ID="txtTransactionPassword_FilteredTextBoxExtender"
                                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtpassword">
                                                    </cc1:FilteredTextBoxExtender>
                                     
                                    </div>
                                </div>
                                     <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Confirm Pin<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                     <asp:TextBox ID="txtconfirmpassword" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtconfirmpassword"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password or confirm password does not match" ForeColor="Red" ControlToValidate="txtconfirmpassword" ControlToCompare="txtpassword"></asp:CompareValidator>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtconfirmpassword">
                                                    </cc1:FilteredTextBoxExtender>
                                         

                                </div></div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                       
                                    </div>
                                    <div class="col-lg-8">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Reset password"  CssClass="btn btn-primary" OnClick="btnsubmit_Click"/>
                                    </div>
                                </div>
                                </div></div></div></div>
    </form>
</body>
</html>
