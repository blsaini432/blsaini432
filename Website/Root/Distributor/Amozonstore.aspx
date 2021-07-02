<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Amozonstore.aspx.cs" Inherits="Root_Admin_Amozonstore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700,800,900">
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Raleway:100,200,300,400,500,600,700,800,900">
    <link rel="stylesheet" href="../../Uploads/amazonstore/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../Uploads/amazonstore/css/animate.css">
    <link href="../../Uploads/amazonstore/css/main.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dashboard-wrapper-page">
        <div class="dashboard-panel">

            <div class="pane1-detail_1">
                <div class="row">
                    <asp:Repeater runat="server" ID="repeater1">
                        <ItemTemplate>
                            <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="pane1-detail_1">
                <div class="row">
                    <asp:Repeater runat="server" ID="repeater2">
                        <ItemTemplate>
                            <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="pane1-detail_3 mt-10">
                <div class="container">
                    <asp:Repeater runat="server" ID="repeater3">
                        <ItemTemplate>
                            <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="pane1-detail_2 mt-10">
                <div class="container">
                    <div class="row">
                        <asp:Repeater runat="server" ID="repeater4">
                            <ItemTemplate>
                                <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="pane1-detail_4 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-4 hover-effect d-flex align-items">
                            <div class="col-lg-5 col-md-6 col-sm-12">
                                <div class="compulsory-img">
                                    <img src="../../Uploads/amazonstore/img/hand-min.png" alt="">
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6 col-sm-12">
                                <div class="panel-detail-content">
                                    <div class="compulsory-title">
                                        <p>
                                            Plugin is compulsory for all stores(Old Stores also). Plugin is mandatory for tracking.<br>
                                            <br>
                                            Amazon Easy Chrome Plugin
                                        </p>
                                        <div class="submit-button"><a href="https://chrome.google.com/webstore/detail/associate-store/hienjaokaebggdlplipjdppmofmdgnjg" target="_blank" class="btn btn-primary">Install the Plugin</a> </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_5 mt-10">
                <div class="container hover-effect">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="section-title">
                                <h3>Benefits of Being A Store  Owner</h3>
                            </div>
                            <div class="advantage-title">
                                <h4>The Amazon Advantage</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="section-wrap-5 d-flex">
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <div class="img-blog">
                                    <div class="micon">
                                        <img src="../../Uploads/amazonstore/img/advantage_icon1.png" alt="">
                                    </div>
                                    <div class="advantage-text">Opportunity to partner with Amazon</div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <div class="img-blog">
                                    <div class="micon">
                                        <img src="../../Uploads/amazonstore/img/advantage_icon2.png" alt="">
                                    </div>
                                    <div class="advantage-text">Marketing support from Amazon*</div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <div class="img-blog">
                                    <div class="micon">
                                        <img src="../../Uploads/amazonstore/img/advantage_icon3.png" alt="">
                                    </div>
                                    <div class="advantage-text">Comprehensive sales and marketing training</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pane1-detail_2 mt-10">
                <div class="container">
                    <div class="row">
                        <asp:Repeater runat="server" ID="repeater5">
                            <ItemTemplate>
                                <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="pane1-detail_2 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-2 hover-effect d-flex">
                            <div class="col-md-4">
                                <div class="submit-button store-button"><a href="https://docs.google.com/forms/d/e/1FAIpQLSf4SmafXWw_VsXuyzNHLQFapqJda6yy772gCr1frhkPgwfdzw/viewform" target="_blank" class="btn btn-primary">Apply for MPOS Device</a> </div>
                            </div>
                            <div class="col-md-4">
                                <div class="submit-button store-button"><a href=" https://drive.google.com/file/d/1DyfuMRlja-vzt6YZ1yEZ9Tri4R6w28rm/view?usp=sharing" target="_blank" class="btn btn-primary">Download MPOS FAQ</a> </div>
                            </div>
                            <div class="col-md-4">
                                <div class="submit-button store-button"><a href="https://drive.google.com/file/d/1yoLF_BVzlcTjlQyCcN4YkOOtj-RhD9uL/view?usp=sharing" target="_blank" class="btn btn-primary">Download Terms & Conditions </a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_6 hover-effect mt-10">
                <div class="row">
                    <asp:Repeater runat="server" ID="repeater6">
                        <ItemTemplate>
                            <asp:Image class="" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="panel-detail_8 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-8 hover-effect d-flex align-items">
                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                <div class="commission-category scroll-category">
                                    <div class="category-headong"><span class="category-title">Category</span> <span class="commission-title">Commission</span> </div>
                                    <div class="category-wrapper">
                                        <ul>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Mobiles</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>1.5%</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Selected Mobiles</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>75 FLAT</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Wireless Accessories & Electronics</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>4.50%</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Large Appliances</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>5.625%</span></div>
                                            </li>


                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Apparels & Shoes</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>10.125%</span></div>
                                            </li>

                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>VIDEOS & SOFTWARE </span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>5.625%</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Gift Cards(Physical) & Consumables</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>2.25%</span></div>
                                            </li>

                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Jewellery Precious</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>0.225%</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Jewellery Non Precious</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>9.6%</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12"><span>Others(Kitchen, Pantry, Health and Personal Care, Home, beauty, Watches, Personal Care,Appliances, Luggage, Automotive, Home Improvement, Books, Toys, Baby, Furniture, Business Industrial Scientific Supplies, Lawn and Garden, Musical Instruments, Grocery, Office Products, Digital Products, Text, Pet Products, Video Games, Luxury Beauty)</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>7.5%</span></div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                <div class="commission-category">
                                    <div class="category-headong"><span class="category-title">Category</span> <span class="commission-title">New Customer Bonus</span> </div>
                                    <div class="category-wrapper">
                                        <ul>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 colxs-12"><span>1st Order greater than 600 rs.</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>95/-</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 colxs-12"><span>2nd Order greater than 600 rs.</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>35/-</span></div>
                                            </li>
                                            <li class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-8 colxs-12"><span>3rd Order greater than 600 rs.</span></div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><span>35/-</span></div>
                                            </li>
                                        </ul>
                                        <div class="bonus-title">First Order must be from Store Account. 2nd and 3rd Order from any account</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_9 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-9 hover-effect d-flex align-items">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="example-info">
                                    <h3>For Example, Store sold a Washing Machine</h3>
                                    <p>
                                        Washing Machine Commission i.e Large Appliance Commision is <span>5.625%</span>
                                        <br>
                                        <br>
                                        In this example,Cost of washing machine is Rs21,999.
                                        <br>
                                        <br>
                                        Store Commission will be 5.625% * 21,999 = <span>1237.44 Rs.</span>
                                        <br>
                                        <br>
                                        Amount will be credited to Store Owners Bank account after 60 days.
                                    </p>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="banner1-detail">
                                    <img src="../../Uploads/amazonstore/img/washing-machine-min.png" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_10 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-10 hover-effect d-flex align-items">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="example-info">
                                    <h3>For Example, Store sold a Peanut Butter</h3>
                                    <p>
                                        Peanut Butter Commission i.e Pantry Commision is <span>7.50%</span>
                                        <br>
                                        <br>
                                        In this example,cost of Peanut butter is Rs382.
                                        <br>
                                        <br>
                                        Store Commission will be 7.50% of 382 = <span>28.65Rs.</span>
                                        <br>
                                        <br>
                                        Amount will be credited to Store Owners Bank account after 60 days.
                                    </p>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="banner1-detail">
                                    <img src="../../Uploads/amazonstore/img/peanut-min.png" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_11 mt-10">
                <div class="container">
                    <div class="row section-wrap-11 hover-effect">
                        <div class="col-md-12">
                            <div class="open-store">
                                <div class="store-img">
                                    <img src="../../Uploads/amazonstore/img/interested-in-signup-min.png" alt="">
                                </div>
                                <div class="submit-button store-button"><a href="https://docs.google.com/forms/d/e/1FAIpQLSd8NO-wPnNiyrA2-ohiUZU0IX0hSJDkmcgYC1CuZg8BPky9kA/viewform" target="_blank" class="btn btn-primary">Sign up here</a> </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_12 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-12 hover-effect d-flex align-items">
                            <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-img">
                                    <img src="../../Uploads/amazonstore/img/amazon-store-training-min.png">
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-content">
                                    <h6>For training of Amazon Easy Store or for any issues:</h6>
                                    <p><span class="whatspp-img">
                                        <img src="../../Uploads/amazonstore/img/phone_icon_min.png"></span><span>+91-9044008326, +91-8896551445 , +91-9651067892 </span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_13 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-13 hover-effect d-flex align-items">
                            <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-img">
                                    <img src="../../Uploads/amazonstore/img/orderstracking-min.jpeg">
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-content">
                                    <h6>Order did not get tracked in Amazon Orders? Not to Worry!</h6>
                                    <p>Please enter the details here</p>
                                    <div class="submit-button"><a href="https://docs.google.com/forms/d/e/1FAIpQLSdTdYCn8hGeorN5Sg8RaUrp0PwHCNdLd3EdADbfUOwu3DmoxQ/viewform" target="_blank" class="btn btn-primary">Enter Tracking Issue</a> </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_14 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-14 hover-effect d-flex align-items">
                            <div class="col-lg-5 col-md-6 colsm-12 col-xs-12">
                                <div class="amazon-store-img">
                                    <img src="../../Uploads/amazonstore/img/amazon-app-min.png">
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6 colsm-12 col-xs-12">
                                <div class="amazon-store-content">
                                    <h6>It’s mandatory to install official amazon easy app to all store partners: </h6>
                                    <p>Download Amazon Easy App</p>
                                    <div class="submit-button"><a href="https://drive.google.com/drive/folders/1XdoU8m8_Muq8NkG5ul0iRcpMrQIA0DZH?usp=sharing" target="_blank" class="btn btn-primary">Amazon Easy App</a> </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_15 mt-10">
                <div class="container">
                    <div class="row">
                        <div class="section-wrap-15 hover-effect d-flex align-items">
                            <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-img">
                                    <img src="../../Uploads/amazonstore/img/amazon-easy-min.png">
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12">
                                <div class="amazon-store-content">
                                    <h6>You can download the Amazon Easy Boards from below link </h6>
                                    <div class="submit-button"><a href="https://drive.google.com/drive/folders/17lxlPIW0pSvtO2YBVxxg1YpfpYEDSiSi?usp=sharing" target="_blank" class="btn btn-primary">Amazon Easy Store Boards</a> </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pane1-detail_1">
                <div class="row">
                    <div class="section-wrap-1 hover-effect d-flex">
                        <div class="col-md-12">
                            <a href="https://drive.google.com/drive/folders/1-5Zn7HO8QPrBHIH5uVmhDWXfJtq_97Sg?usp=sharing" target="_blank"></a>
                                <div class="banner1-detail1">
                                    <img src="../../Uploads/amazonstore/img/Amazon dost.jpg" alt="">
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-detail_16 mt-10">
                <div class="container">
                    <div class="row section-wrap-16 hover-effect">
                        <div class="col-lg-6 col-md-12 col-sm-12">
                            <div class="open-store">
                                <div class="store-img">
                                    <img src="../../Uploads/amazonstore/img/amazon-earnings-min.png" alt="">
                                </div>
                                <div class="submit-button store-button"><a href="Earning_Report.aspx" class="btn btn-primary">Amazon Earnings</a> </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-12 col-sm-12">
                            <div class="open-store">
                                <div class="store-img">
                                    <img src="../../Uploads/amazonstore/img/amazon-orders-min.png" alt="">
                                </div>
                                <div class="submit-button store-button"><a href="order_report.aspx" class="btn btn-primary">Amazon Orders</a> </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <script src="../../Uploads/amazonstore/js/jquery.min.js"></script>
    <script src="../../Uploads/amazonstore/js/bootstrap.min.js"></script>
</asp:Content>



