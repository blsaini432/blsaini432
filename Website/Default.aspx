<%@ Page Title="" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="banner-section">
            <div class="auto-container">
                <div class="row clearfix">

                    <!-- Content Column -->
                    <div class="content-column col-xl-6 col-lg-6 col-md-6 col-sm-12">
                        <div class="inner-column wow slideInUp animated" data-wow-duration="2.0s">
                            <h2> THE DIGITAL STORE OF RURAL INDIA </h2>
                            <div class="text">Apexmart is a community of budding entrepreneurs all over India who bring about change one franchised store at a time. Apexmart phygital stores acts as a hub of digital products & services that every consumer needs. We enable rural consumers to make digital transactions.</div>
                        </div>
                    </div>

                    <!-- Image Column -->
                    <div class="image-column col-xl-6 col-lg-6 col-md-6 col-sm-12">
                        <div class="inner-column parallax-scene-6">
                            <div class=" wow slideInUp animated" data-wow-duration="2.0s">
                                <div class="banner-form">
                                    <div class="form-colum">
                                        <div class="banner-form-content">
                                            <h3 class="form-top-title"> Start your Entrepreneurial Journey Today!</h3>
                                            <p class="form-text">Fill in the below details to receive a call back from us. </p>
                                            <div class="form-detail">
                                                <div class="contact-detail">
                                                    <div class="col-sm-6">
                                       <h5 style ="color:red;position:inherit"><%= HttpContext.Current.Session["message"]  %></h5>
                                        </div>
                                                    <div class="form-group text-input-button">
                                                         <asp:TextBox ID="txt_mobile" runat="server" MaxLength="10" class="form-control form-control-lg border-left-0" required
                                                         placeholder="Mobile Number"  autocomplete="off"></asp:TextBox>
                                                      
                                                        <samp class="bar"></samp>
                                                    </div>
                                                    <div class="form-group text-input-button">
                                                         <asp:TextBox ID="txt_pin" runat="server" MaxLength="10" class="form-control form-control-lg border-left-0" required
                                                         placeholder="Pin Code"  autocomplete="off"></asp:TextBox>
                                                        
                                                        <samp class="bar"></samp>
                                                    </div>
                                                </div>
                                                <div class="submit-box">
                                                     <asp:Button ID="btn_login" runat="server" Text="send" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" OnClick="btn_login_Click" />
                                                   
                                                </div>
                                                <div class="form-bottom-text">
                                                    <p class="banner-bottom-text"><!--Our sales associate would call you back shortly or--> You can reach us on : <span>+91 9044008326</span>, Whatsapp us @ <span>+91 9044008326</span></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    <!-- End Banner Carousel Section -->
    <!-- About Us Section -->
    <section class="aboutus-section">
            <div class="auto-container">
                <div class="row clearfix">

                    <!-- Title Column -->
                    <div class="title-column col-lg-12 col-md-12 col-sm-12">
                        <div class="inner-column">
                            <!-- Sec Title Two -->
                            <div class="about-title wow slideInLeft animated" data-wow-duration="2.0s">
                                <div class="title-text">Welcome To Apexmart</div>
                                <h2>TRANSFORMING THE WAY INDIA SHOPS <span>ONLINE</span></h2>
                            </div>
                            <div class="aboutus-text wow slideInRight animated" data-wow-duration="2.5s"> Apexmart is a movement born on account of the necessity for providing a level playing field by reaching the unreached with our superheroes (retailers) present all over India. We are a network of offline stores which are a 'one stop shop' for every product in the online market in the cheapest rates possible. </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="benefit-box text-center wow slideInUp animated" data-wow-duration="1.0s">
                            <div class="benefit-icon">
                                <figure class="m-0"><img class="img-fluid" src="newdesign/images/icons/empowering.png"></figure>
                            </div>
                            <div class="benefit-text">
                                <h5>Empowering Micro-Entrepreneurs</h5>
                                <p>Our franchise model with low investment empowers micro-entrepreneurs in rural India. We help market high-quality products and services at best margins by partnering with reputed brands like amazon.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="benefit-box text-center wow slideInUp animated" data-wow-duration="1.5s">
                            <div class="benefit-icon">
                                <figure class="m-0"><img class="img-fluid" src="newdesign/images/icons/assisted.png"></figure>
                            </div>
                            <div class="benefit-text">
                                <h5>Assisted &amp; Informed Buying</h5>
                                <p>Our trained marketing expert provides the best buying experience in rural India. Their assistance and guidance makes online shopping easy in rural India.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="benefit-box text-center wow slideInUp animated" data-wow-duration="2.0s">
                            <div class="benefit-icon">
                                <figure class="m-0"><img class="img-fluid" src="newdesign/images/icons/ruralmarket.png"></figure>
                            </div>
                            <div class="benefit-text">
                                <h5>Easy Accessibility to Rural Market</h5>
                                <p>Through setting up of successful franchises with the partnership and support of e-commerce giants like Amazon to provide additional development opportunities.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- Image Column -->
                    <div class="image-column col-lg-12 col-md-12 col-sm-12">
                        <div class="inner-column wow fadeInRight" data-wow-delay="0ms" data-wow-duration="1500ms">
                            <div class="image parallax-scene-2">
                                <div data-depth="0.50" class="image parallax-layer">
                                    <div class="aboutus-img wow slideInUp animated" data-wow-duration="2.0s"> <img src="newdesign/images/resource/aboutus_img.png" alt="" /> </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    <!-- End About Us Section -->

    <div class="content-wrap">
        <div class="content-wrap-bg">
            <div class="container clearfix">
                <div class="row topmargin-lg bottommargin-sm">
                    <div class="col-md-12">
                        <div class="sec-title-two centered wow slideInRight animated animated" data-wow-duration="1.0s" style="visibility: visible; animation-duration: 1s; animation-name: slideInRight;">
                            <h2>Why Apexmart <span class="red-light-color">Store</span></h2>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 left-blog">
                        <div class="feature-box topmargin wow slideInLeft animated" data-wow-duration="1.5s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon1.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Improved Infrastructure</h3>
                            <p>Increased access of internet in far-off rural areas</p>
                        </div>
                        <div class="feature-box topmargin wow slideInLeft animated" data-wow-duration="2.0s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon5.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Trained Store Expert</h3>
                            <p>The store experts are trained to help make the rural customers take decisions comfortably.</p>
                        </div>
                        <div class="feature-box topmargin wow slideInLeft animated" data-wow-duration="2.5s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon3.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Reputed &amp; Trusted Brands</h3>
                            <p>We partner with known and trusted brands to give your store an edge.</p>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 hidden-sm bottommargin center">
                        <div class=" wow slideInUp animated" data-wow-duration="2.5s">
                            <img src="newdesign/images/icons/why-stor.png" alt="iphone 2">
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 right-blog">
                        <div class="feature-box topmargin wow slideInRight animated" data-wow-duration="1.5s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon4.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Assisted Ecommerce</h3>
                            <p>Enabling rural audiences comfortably trust and carry out online sales and place orders.</p>
                        </div>
                        <div class="feature-box topmargin wow slideInRight animated" data-wow-duration="2.0s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon2.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Zero Inventory</h3>
                            <p>No need to store any product anywhere.</p>
                        </div>
                        <div class="feature-box topmargin wow slideInRight animated" data-wow-duration="2.5s">
                            <div class="fbox-icon bounceIn animated" data-animate="bounceIn" data-delay="600">
                                <a href="#">
                                    <img src="newdesign/images/icons/why_icon6.jpg" alt="Responsive Layout"></a>
                            </div>
                            <h3>Rural Entrepreneurship</h3>
                            <p>An opportunity to be a micro-entrepreneur in rural area with our guidance and help.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Our Services Section -->
    <div class="service-section">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="sec-title-two centered wow slideInRight animated" data-wow-duration="2.0s">
                        <h2>Turn your shop <span class="red-color">into supermarket</span></h2>
                        <div class="title-text">We Are Specialized in the Following Services</div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-6 col-sm-12">
                    <div class="Product-service service-column-first">
                        <ul class="data-timeline data-timeline-right list-inline">
                            <li class="data-event wow fadeInDown" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s; animation-name: fadeInDown;">
                                <h5>Mobile Recharge</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.3s" style="visibility: visible; animation-delay: 0.3s; animation-name: fadeInDown;">
                                <h5>Dth Recharge</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.4s" style="visibility: visible; animation-delay: 0.4s; animation-name: fadeInDown;">
                                <h5>Data Card</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>Electricity Bill</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>Landline Bill</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>Gas Bill</h5>
                                <p>&nbsp;</p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-12">
                    <div class="digital-service service-column-secound">
                        <ul class="data-timeline data-timeline-right list-inline">
                            <li class="data-event wow fadeInDown" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s; animation-name: fadeInDown;">
                                <h5>Money Transfer</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.3s" style="visibility: visible; animation-delay: 0.3s; animation-name: fadeInDown;">
                                <h5>Aeps</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.4s" style="visibility: visible; animation-delay: 0.4s; animation-name: fadeInDown;">
                                <h5>Uti Pan Card</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.6s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>mobile electronics</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>home appliances</h5>
                                <p>&nbsp;</p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-12">
                    <div class="Product-service service-column-three">
                        <ul class="data-timeline data-timeline-right list-inline">
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>fashion</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s; animation-name: fadeInDown;">
                                <h5>beauty & health</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.3s" style="visibility: visible; animation-delay: 0.3s; animation-name: fadeInDown;">
                                <h5>municipal tax</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>sports & fitness</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.4s" style="visibility: visible; animation-delay: 0.4s; animation-name: fadeInDown;">
                                <h5>booking & stationary</h5>
                                <p>&nbsp;</p>
                            </li>

                        </ul>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-12">
                    <div class="digital-service service-column-four">
                        <ul class="data-timeline data-timeline-right list-inline">
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>flight</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInDown;">
                                <h5>home & kitchen</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s; animation-name: fadeInDown;">
                                <h5>bus</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.4s" style="visibility: visible; animation-delay: 0.4s; animation-name: fadeInDown;">
                                <h5>gift card</h5>
                                <p>&nbsp;</p>
                            </li>
                            <li class="data-event wow fadeInDown" data-wow-delay="0.3s" style="visibility: visible; animation-delay: 0.3s; animation-name: fadeInDown;">
                                <h5>automobile accessories </h5>
                                <p>&nbsp;</p>
                            </li>


                        </ul>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class=" parallax-scene-4">
                        <div data-depth="0.50" class="image parallax-layer">
                            <div class="service_img_column wow slideInUp animated" data-wow-duration="2.0s">
                                <img alt="" class="img-fluid" src="newdesign/images/resource/aboutus_img.png">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Our Services Section -->
    <!-- platform Section -->
    <section class="platform-section">
            <div class="auto-container">
                <!-- Sec Title -->
                <div class="sec-title-two centered wow slideInRight animated" data-wow-duration="2.0s">
                    <h2> Benefits of <span class="benefits">Joining us as a retailer</span> </h2>
                </div>
                <div class="row clearfix">
                    <div class="col-md-6">
                        <div class="retailer-blog wow slideInLeft animated" data-wow-duration="1.0s">
                            <div class="img-blog"><img src="newdesign/images/icons/retailer1.png"></div>
                            <div class="joing-content-blog">
                                <h3>LOW INVESTMENT</h3>
                                <p>Our very low investment gives you an extra boost to kickstart your business Journey.</p>
                            </div>
                        </div>
                        <div class="retailer-blog wow slideInLeft animated" data-wow-duration="2.0s">
                            <div class="img-blog"> <img src="newdesign/images/icons/retailer2.png"> </div>
                            <div class="joing-content-blog">
                                <h3>TRAINING & SUPPORT</h3>
                                <p>Comprehensive training & prompt support</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="retailer-blog wow slideInRight animated" data-wow-duration="1.0s">
                            <div class="img-blog support_img"><img src="newdesign/images/icons/retailer3.png"></div>
                            <div class="joing-content-blog">
                                <h3>FULL BACK-END SUPPORT</h3>
                                <p>You focus on customers, rest is taken care of by us</p>
                            </div>
                        </div>
                        <div class="retailer-blog wow slideInRight animated" data-wow-duration="2.0s">
                            <div class="img-blog"><img src="newdesign/images/icons/retailer4.png"></div>
                            <div class="joing-content-blog">
                                <h3>CROSS-SELL OPPORTUNITY</h3>
                                <p>Multiply your revenue by cross-selling various services</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    <!-- End platform Section -->
    <!-- Counter Section -->
    <section class="counter-section">
            <!-- Icon One -->
            <div class="icon-one paroller" data-paroller-factor="-0.10" data-paroller-factor-lg="0.10" data-paroller-type="foreground" data-paroller-direction="horizontal" style="background-image:url(images/icons/icon-14.png)"></div>
            <!-- Pattern Layer -->
            <div class="auto-container">
                <!-- Sec Title -->
                <!-- Fact Counter -->
                <div class="fact-counter">
                    <div class="row clearfix">

                        <!--Column-->
                        <div class="column counter-column col-lg-3 col-md-6 col-sm-12">
                            <div class="inner wow zoomIn animated" data-wow-duration="1.0s">
                                <div class="content">
                                    <div class="count-outer count-box">
                                        <div class="icon-box">
                                            <span class="icon"><img src="newdesign/images/icons/fact1.png" alt=""></span>
                                            <div class="circles-box"> <span class="circle-one"></span> <span class="circle-two"></span> </div>
                                        </div>
                                        <span class="count-text" data-speed="2500" data-stop="1000">0</span>+
                                    </div>
                                    <h4 class="counter-title">Global Customer</h4>
                                </div>
                            </div>
                        </div>

                        <!--Column-->
                        <div class="column counter-column col-lg-3 col-md-6 col-sm-12">
                            <div class="inner wow zoomIn animated" data-wow-duration="1.5s">
                                <div class="content">
                                    <div class="count-outer count-box alternate">
                                        <div class="icon-box">
                                            <span class="icon"><img src="newdesign/images/icons/fact4.png" alt=""></span>
                                            <div class="circles-box"> <span class="circle-one"></span> <span class="circle-two"></span> </div>
                                        </div>
                                        <span class="count-text" data-speed="4000" data-stop="100">0</span>%
                                    </div>
                                    <h4 class="counter-title">Satisfied Clients </h4>
                                </div>
                            </div>
                        </div>

                        <!--Column-->
                        <div class="column counter-column col-lg-3 col-md-6 col-sm-12">
                            <div class="inner wow zoomIn animated" data-wow-duration="2.0s">
                                <div class="content">
                                    <div class="count-outer count-box">
                                        <div class="icon-box">
                                            <span class="icon"><img src="newdesign/images/icons/funfact_color1.png" alt=""></span>
                                            <div class="circles-box"> <span class="circle-one"></span> <span class="circle-two"></span> </div>
                                        </div>
                                        <span class="count-text" data-speed="3000" data-stop="460">0</span>+
                                    </div>
                                    <h4 class="counter-title">Distributors</h4>
                                </div>
                            </div>
                        </div>

                        <!--Column-->
                        <div class="column counter-column col-lg-3 col-md-6 col-sm-12">
                            <div class="inner wow zoomIn animated" data-wow-duration="2.5s">
                                <div class="content">
                                    <div class="count-outer count-box">
                                        <div class="icon-box">
                                            <span class="icon"><img src="newdesign/images/icons/fact2.png" alt=""></span>
                                            <div class="circles-box"> <span class="circle-one"></span> <span class="circle-two"></span> </div>
                                        </div>
                                        <span class="count-text" data-speed="3000" data-stop="750">0</span>+
                                    </div>
                                    <h4 class="counter-title">Retailers</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    <!-- End Counter Section -->
    <!-- Newletter Section -->
    <section class="newsletter-section margin-bottom wow slideInUp animated" data-wow-duration="2.0s">
            <div class="auto-container">
                <div class="inner-container">
                    <div class="row clearfix">

                        <!-- Title Column -->
                        <div class="title-column col-lg-10 col-md-9 col-sm-12">
                            <div class="inner-column">
                                <span class="icon flaticon-rocket-ship"></span>
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

