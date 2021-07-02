<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Root_Admin_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <script src="../Angularjsapp/distributorapp.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper dashboard-content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Dashboard </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
               <div class="row grid-margin">
                        <div class="col-12">
                            <div class="card card-statistics">
                                <div class="card-body">
                                    <div class="d-flex flex-column flex-md-row align-items-center justify-content-between">
                                        <div class="statistics-item">
                                            <p><i class="icon-sm fa fa-user mr-2"></i>New users </p>
                                            <h2>54000</h2>
                                            <label class="badge badge-outline-success badge-pill">2.7% increase</label>
                                        </div>
                                        <div class="statistics-item">
                                            <p><i class="icon-sm fas fa-hourglass-half mr-2"></i>Avg Time </p>
                                            <h2>123.50</h2>
                                            <label class="badge badge-outline-danger badge-pill">30% decrease</label>
                                        </div>
                                        <div class="statistics-item">
                                            <p><i class="icon-sm fas fa-cloud-download-alt mr-2"></i>Downloads </p>
                                            <h2>3500</h2>
                                            <label class="badge badge-outline-success badge-pill">12% increase</label>
                                        </div>
                                        <div class="statistics-item">
                                            <p><i class="icon-sm fas fa-check-circle mr-2"></i>Update </p>
                                            <h2>7500</h2>
                                            <label class="badge badge-outline-success badge-pill">57% increase</label>
                                        </div>
                                        <div class="statistics-item">
                                            <p><i class="icon-sm fas fa-chart-line mr-2"></i>Sales </p>
                                            <h2>9000</h2>
                                            <label class="badge badge-outline-success badge-pill">10% increase</label>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
         <%--       <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card card-statistics">
                            <div class="card-body" ng-app="distributorApp" ng-controller="distributorCntrl">
                                <div ng-repeat="x in rtdashboard" class="d-flex flex-column flex-md-row align-items-center justify-content-between">
                                    <div class="statistics-item">
                                        <p><i class="icon-sm fa fa-user mr-2"></i>E-Wallet Balance</p>
                                        <h2><a href="ListEWalletBalance.aspx" target="_blank" style="color: white;">{{x.EBalance}}</a></h2>
                                        </h2>
                                         
                                    </div>
                                    <div class="statistics-item">
                                        <p><i class="icon-sm fas fa-hourglass-half mr-2"></i>AEPS Balance </p>
                                        <h2><a href="AepsWallet.aspx" target="_blank" style="color: white;">{{x.RBalance}}</a></h2>
                                        </h2>
                                          
                                    </div>
                                    <div class="statistics-item">
                                        <p><i class="icon-sm fas fa-cloud-download-alt mr-2"></i>Today Recharge Amount </p>
                                        <h2>
                                            <a href="Recharge_ListHistory.aspx" target="_blank" style="color: white;">{{x.rechargeamount}}</a></h2>

                                    </div>
                                    <div class="statistics-item">
                                        <p><i class="icon-sm fas fa-check-circle mr-2"></i>Today DMR Amount </p>
                                        <h2>7500</h2>

                                    </div>
                                    <div class="statistics-item">
                                        <p><i class="icon-sm fas fa-chart-line mr-2"></i>Sales </p>
                                        <a href="DmrNewReport.aspx" target="_blank" style="color: white;">{{x.totaldmr}}</a></h2>
                                           
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="onoffswitch3">
                    <input type="checkbox" name="onoffswitch3" class="onoffswitch3-checkbox" id="myonoffswitch3" checked>
                    <label class="onoffswitch3-label" for="myonoffswitch3">
                        <span class="onoffswitch3-inner">
                            <span class="onoffswitch3-active">
                                <marquee class="scroll-text"> 
                            <asp:Repeater ID="rptnews" runat="server">
                                            <ItemTemplate>
                                             
                                                    <div class="">                                                     
                                                        <p class="" style="color:red">
                                                            <%# Eval("NewsDesc") %></p>
                                                    </div>
                                              
                                            </ItemTemplate>
                                        </asp:Repeater>
                        </marquee>
                                <span class="onoffswitch3-switch">Latest NEWS <i class="fas fa-times"></i></span>

                            </span>
                            <span class="onoffswitch3-inactive"><span class="onoffswitch3-switch">SHOW BREAKING NEWS</span></span>
                        </span>
                    </label>
                </div>
                <div class="service-section">
                    <div class="service-wrapper">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="service-heading banking-title">
                                    <h4 class="card-title">Banking/Financial Services</h4>
                                </div>
                            </div>
                        </div>
                        <div class="row panel-service-wrapper">
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/dmr.png" alt="">
                                        </div>

                                        <div class="service-title">
                                            <p>DMR</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.2s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/mpos.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>mPOS</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.4s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/payout.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Payout</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.6s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/xpress_dmr.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Xpress DMR</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.8s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/aeps.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>AEPS</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="2s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/banking_financial/gst.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>GST</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="service-wrapper">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="service-heading">
                                    <h4 class="card-title">Recharge & Bill Payment</h4>
                                </div>
                            </div>
                        </div>
                        <div class="row panel-service-wrapper">

                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/recharge_bill/recharge.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Recharge</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.3s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/other/pancard.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Pan Card</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.6s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/recharge_bill/insurance_premium.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Insurance Premium</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.9s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/recharge_bill/loan_repayment.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Loan-Repayment</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="2.2s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/recharge_bill/electricity.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Electricity</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="2.5s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/recharge_bill/lic_premium.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>LIC Premium</p>
                                        </div>
                                    </a>
                                </div>
                            </div>

                        </div>

                    </div>


                    <div class="service-wrapper">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="service-heading">
                                    <h4 class="card-title">Travel & Booking</h4>
                                </div>
                            </div>
                        </div>
                        <div class="row panel-service-wrapper">
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/travel_booking/bus.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Bus</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.4s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/travel_booking/flight.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Flight</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 col-xl-2">
                                <div class="service-blog-inner wow zoomIn animated" data-wow-duration="1s" data-wow-delay="1.8s">
                                    <a href="#">
                                        <div class="img-area">
                                            <img src="../../Design/newdesign/images/service_img/travel_booking/hotel.png" alt="">
                                        </div>
                                        <div class="service-title">
                                            <p>Hotel</p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


