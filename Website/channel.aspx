<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="channel.aspx.cs" Inherits="channel" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="" runat="Server">
      <style type="text/css">
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:20%; text-align:left;}
</style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="distributor-page-section">
    <div class="container">
      <div class="col-md-7 distributor-bg wow slideInLeft animated animated" data-wow-duration="2.0s">
        <h1><strong>Distributors & <span class="red-drak-color">Channel Partner</span></strong></h1>
        <p>If you have a network of a wide number of stores and agents in your mandal/district/state or any other province across India, you may partner with Apexmart and start a business venture by partnering with us in setting up multiple stores in your desired demography</p>
      </div>
    </div>
  </div>
  <div class="requirements-channel-section">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="chennel-partner-title text-center wow slideInUp animated animated" data-wow-duration="2.0s">
            <h4>Requirements of a channel Partner/Distributor</h4>
          </div>
        </div>
      </div>
      <div class="row benefit-data">
        <div class="col-md-4">
          <div class="requirements-block wow slideInUp animated animated" data-wow-duration="2.0s">
            <div class="chennel-icon"> <img src="newdesign/images/icons/channel_icon1.png" alt=""> </div>
            <div class="chennel-text">
              <h5>Should have knowledge about the demographics where you want to become a distributor/channel partner. </h5>
            </div>
          </div>
        </div>
        <div class="col-md-4">
          <div class="requirements-block wow slideInUp animated animated" data-wow-duration="2.5s">
            <div class="chennel-icon"> <img src="newdesign/images/icons/channel_icon2.png" alt=""> </div>
            <div class="chennel-text">
              <h5>Should have access to a wide network of stores/agents.</h5>
            </div>
          </div>
        </div>
        <div class="col-md-4">
          <div class="requirements-block wow slideInUp animated animated" data-wow-duration="3.0s">
            <div class="chennel-icon"> <img src="newdesign/images/icons/channel_icon3.png" alt=""> </div>
            <div class="chennel-text">
              <h5>Should have basic experience in sales or marketing.</h5>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="benefits-channel-section">
    <div class="container">
      <div class="row">
        <div class="col-md-12 wow fadeInRight" data-wow-delay="0.2s">
          <div class="chennel-partner-title text-center">
            <h4>Benefits of Channel Partner/Distributor</h4>
          </div>
        </div>
      </div>
      <div class="row benefit-data">
        <div class="col-md-6">
          <div class="requirements-block wow slideInUp animated animated" data-wow-duration="2.0s">
            <div class="chennel-icon"> <img src="newdesign/images/icons/channel_icon4.png" alt=""> </div>
            <div class="chennel-text">
              <h5>Get a fixed fee on every store you set up for Apexmart - ACTIVE Income.</h5>
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="requirements-block wow slideInUp animated animated" data-wow-duration="2.5s">
            <div class="chennel-icon"> <img src="newdesign/images/icons/channel_icon5.png" alt=""> </div>
            <div class="chennel-text">
              <h5>You get to create a network of your stores, and you make money on the sales driven by each of the stores you set-up - PASSIVE Income (ROYALTY).</h5>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="form-section">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="form-text channel-form-title wow slideInUp animated animated" data-wow-duration="2.0s"> Start your journey now </div>
        </div>
      </div>
         <h4 style ="color:red;position:inherit"><%= HttpContext.Current.Session["message3"]  %></h4>
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
        <div class="col-md-6">
          <div class="wrap-input3 validate-input" data-validate="Name is required">
            <asp:TextBox ID="txt_pin" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Pin Code"  autocomplete="off"></asp:TextBox>
            <span class="focus-input3"></span> </div>
        </div>
        <div class="col-md-6">
          <div class="wrap-input3 validate-input" data-validate="Message is required">
          <asp:TextBox ID="txt_message" runat="server" MaxLength="100" class="form-control form-control-lg border-left-0" required
   placeholder="Message"  autocomplete="off"></asp:TextBox>
            <span class="focus-input3"></span> </div>
        </div>
        <div class="col-md-12">
          <div class="form-submit-button ">
           <asp:Button ID="btn_login" runat="server" Text="Submit" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btnSave_Click" />
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
