<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="openstore.aspx.cs" Inherits="openstore" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="" runat="Server">
      <style type="text/css">
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:20%; text-align:left;}
</style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <!-- Start Banner -->
    <div class="store-page-section">
<div class="container">
<div class="col-md-6 store-bg wow slideInLeft animated" data-wow-duration="2.0s">
<h1><strong>Apexmart <span> Store</span></strong></h1>
<p>Be a part of Faster Growing network of apexmart Franchises</p>
</div>
</div>
</div>
 <!-- End Banner -->
 <!-- Started Steps Section -->
<div class="stape3-section white-light-bg">
<div class="container">
<div class="row">
<div class="col-md-12">
<div class="sec-title-two centered wow slideInRight animated animated" data-wow-duration="2.0s">
            <h2>Get Started in <span class="red-color">3 Simple Steps</span></h2>   
            <div class="title-text">Be a part of profitable franchise in 3 simple steps. Follow the instructions mentioned below:</div>
          </div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="step-colom wow slideInLeft animated" data-wow-duration="2.0s">
<img src="newdesign/images/icons/store_icon1.png" />
<h5>Open a Store</h5>
</div>
</div>
<div class="col-md-4">
<div class="step-colom wow slideInUp animated" data-wow-duration="2.5s">
<img src="newdesign/images/icons/store_icon2.png" />
<h5>Sell Anythings</h5>
</div>
</div>
<div class="col-md-4">
<div class="step-colom wow slideInRight animated" data-wow-duration="3.0s">
<img src="newdesign/images/icons/store_icon3.png" />
<h5>Earn Money</h5>
</div>
</div>
</div>
</div>
</div>
 <!-- End Started Steps Section -->
   <!-- Form Section -->
<div class="form-section">
<div class="container">
<div class="row">
<div class="col-md-12"
<div class="form-text form-title wow slideInUp animated" data-wow-duration="2.0s">
REALIZE YOUR ENTREPRENEURIAL POTENTIAL AND START YOUR JOURNEY WITH US
</div>
</div>
</div>
<div class="row wrap-form-contact form-detail wow slideInUp animated" data-wow-duration="2.0s">
    <h5 style ="color:red;position:inherit"><%= HttpContext.Current.Session["message5"]  %></h5>
<div class="col-md-6">
<div class="wrap-input3 validate-input" data-validate="Name is required">
<asp:TextBox ID="txt_name" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" required
   placeholder="Name"  autocomplete="off"></asp:TextBox>

</div>
</div>

<div class="col-md-6">
<div class="wrap-input3 validate-input" data-validate="Name is required">
<asp:TextBox ID="txt_mobile" runat="server" MaxLength="10" class="form-control form-control-lg border-left-0" required
   placeholder="Mobile"  autocomplete="off"></asp:TextBox>

</div>
</div>

<div class="col-md-6">
<div class="wrap-input3 validate-input" data-validate="Name is required">
<asp:TextBox ID="txt_address" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Address"  autocomplete="off"></asp:TextBox>

</div>
</div>

<div class="col-md-6">
<div class="wrap-input3 validate-input" data-validate="Message is required">
<asp:TextBox ID="txt_message" runat="server" MaxLength="50" class="form-control form-control-lg border-left-0" required
   placeholder="Message"  autocomplete="off"></asp:TextBox>
   

</div>
</div>

<div class="col-md-12">
<div class="form-submit-button ">
 <asp:Button ID="btn_login" runat="server" Text="Submit" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btnSave_Click" />
                                                   
</div>
</div>

</div>
</div>
</div>
   <!-- End Form Section -->
   
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
                <div class="form-group"> <span class="newletter_btn_bg"></span> <a href="Contactus.html" class="newletter_btn">Contact Us</a> </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
    <!-- End Newletter Section -->
  
</asp:Content>
