<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="partner_withus.aspx.cs" Inherits="partner_withus" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="" runat="Server">
      <style type="text/css">
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:20%; text-align:left;}
</style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
   <div class="form-section white-light-bg">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="form-text form-title wow slideInUp animated animated" data-wow-duration="2.0s"> PARTNER WITH US AND LET’S DIGITILIZE OUR NATION WITH PRESENCE OF RETAILERS ALL OVER INDIA. </div>
        </div>
      </div>
         <h5 style ="color:red;position:inherit"><%= HttpContext.Current.Session["message4"]  %></h5>
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
   placeholder="Address"  autocomplete="off"></asp:TextBox>
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
  <div class="partner-section">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="sec-title-two centered wow slideInRight animated animated animated" data-wow-duration="2.0s">
            <h2>We are proud to <span class="red-color"> partner with</span></h2>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-4">
          <div class="partner-colom wow slideInUp animated animated" data-wow-duration="2.0s">
            <div class="partner-img"> <img src="newdesign/images/resource/amezon.jpg"> </div>
            <h5>Major E-commerce Partner</h5>
          </div>
        </div>
        <div class="col-md-4">
          <div class="partner-colom wow slideInUp animated animated" data-wow-duration="2.5s">
            <div class="partner-img"> <img src="newdesign/images/resource/ZestMoney.jpg"> </div>
            <h5>Major Finance Partner</h5>
          </div>
        </div>
        <div class="col-md-4">
          <div class="partner-colom wow slideInUp animated animated" data-wow-duration="3.0s">
            <div class="partner-img"> <img src="newdesign/images/resource/1mg.jpg"> </div>
            <h5>Online Medicine Booking Partner</h5>
          </div>
        </div>
      </div>
    </div>
  </div>
  
  <!-- Newletter Section -->
  <section class="newsletter-section margin-bottom wow slideInUp animated" data-wow-duration="2.0s">
    <div class="auto-container">
      <div class="inner-container">
        <div class="row clearfix"> 
          
          <!-- Title Column -->
          <div class="title-column col-lg-10 col-md-9 col-sm-12">
            <div class="inner-column"> <span class="icon flaticon-rocket-ship"></span>
              <h4>Join our ever growing community Today !</h4>
              <div class="text">Simply fill the form below to get started and be a part of a revolution. Let's together change the way rural India shops online.</div>
            </div>
          </div>
          
          <!-- Form Column -->
          <div class="form-column col-lg-2 col-md-3 col-sm-12">
            <div class="inner-column"> 
              <!--Emailed Form-->
              <div class="emailed-form">
                <div class="form-group"> <span class="newletter_btn_bg"></span> <a href="Contactus.aspx" class="newletter_btn">Contact Us</a> </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- End Newletter Section -->
</asp:Content>
