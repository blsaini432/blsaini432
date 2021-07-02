<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="Contactus_old.aspx.cs" Inherits="Contactus" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style type="text/css">
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:20%; text-align:left;}
</style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <!-- Start Contact Banner -->
  <div class="contactus-page-section">
    <div class="conatiner"></div>
  </div>
  <!-- End Contact Banner --> 
  
  <!-- Start Contact Detail Section -->
  <section class="cotnact-info">
    <div class="container">
      <div class="row info-detail-contact d-flex align-items-center">
        <div class="col-md-6">
          <div class="contact-left wow slideInLeft animated animated" data-wow-duration="2.0s">
            <h5>You can visit our office :</h5>
            <div class="address-blog">
              <div class="address-img"><img src="newdesign/images/icons/address.png"> </div>
              <p class="address-text"> Apexmart Corporate Office Amritpali,<br />
                Ballia City Ballia -277001</p>
            </div>
          </div>
        </div>
        <div class="col-md-6 border-left">
          <div class="contact-right wow slideInRight animated animated" data-wow-duration="2.0s">
            <div class="mail-id">Email : <a href="#">support@apexmart.in</a></div>
            <div class="phone-detail">
              <div class="phone-info">Contact us at :</div>
              <p><i class="fa fa-phone"></i> : +91 9044008326</p>
              <p><i class="fa fa-phone"></i> : +91 9651067892</p>
            </div>
            <div class="working-detail">
              <p>(Working hours from 9 A.M. to 9 P.M. Monday to Saturday )</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- End Contact Detail Section --> 
  
  <!-- Start Form Section -->
     <h4 style ="color:red;position:inherit"><%= HttpContext.Current.Session["message1"]  %></h4>
  <div class="form-section white-light-bg">
    <div class="container">
      <div class="row wrap-form-contact form-detail wow slideInUp animated animated" data-wow-duration="2.0s">
        <div class="col-md-6">
          <div class="wrap-input3 validate-input" data-validate="Name is required">
           <asp:TextBox ID="txt_name" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Name"  autocomplete="off"></asp:TextBox>
            <span class="focus-input3"></span> </div>
        </div>
        <div class="col-md-6">
          <div class="wrap-input3 validate-input" data-validate="Valid email is required: ex@abc.xyz">
           <asp:TextBox ID="txt_email" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Email"  autocomplete="off"></asp:TextBox>
            <span class="focus-input3"></span> </div>
        </div>
        <div class="col-md-12">
          <div class="wrap-input3 validate-input" data-validate="Message is required">
              <asp:TextBox ID="txt_address" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Message"  autocomplete="off"></asp:TextBox>
            <span class="focus-input3"></span> </div>
        </div>
        <div class="col-md-12">
          <div class="form-submit-button">
            <asp:Button ID="btn_login" runat="server" Text="Submit" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btnSave_Click" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- End Form Section --> 
</asp:Content>
