<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Dashboard_lat.aspx.cs" Inherits="Root_Admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <script src="../Angularjsapp/distributorapp.js"></script>
  
<style>
.onoffswitch3
{
    position: relative; 
    -webkit-user-select:none; -moz-user-select:none; -ms-user-select: none;
}

.onoffswitch3-checkbox {
    display: none;
}

.onoffswitch3-label {
    display: block; overflow: hidden; cursor: pointer;
    border: 0px solid #999999; border-radius: 0px;
}

.onoffswitch3-inner {
    display: block; width: 200%; margin-left: -100%;
    -moz-transition: margin 0.3s ease-in 0s; -webkit-transition: margin 0.3s ease-in 0s;
    -o-transition: margin 0.3s ease-in 0s; transition: margin 0.3s ease-in 0s;
}

.onoffswitch3-inner > span {
    display: block; float: left; position: relative; width: 50%; height: 30px; padding: 0; line-height: 30px;
    font-size: 14px; color: white; font-family: 'Montserrat', sans-serif; font-weight: bold;
    -moz-box-sizing: border-box; -webkit-box-sizing: border-box; box-sizing: border-box;
}

.onoffswitch3-inner .onoffswitch3-active {
    padding-left: 10px;
    background-color: #EEEEEE; color: #FFFFFF;
}

.onoffswitch3-inner .onoffswitch3-inactive {
    width: 100px;
    padding-left: 16px;
    background-color: #EEEEEE; color: #FFFFFF;
    text-align: right;
}

.onoffswitch3-switch {
    display: block; width: 50%; margin: 0px; text-align: center; 
    border: 0px solid #999999;border-radius: 0px; 
    position: absolute; top: 0; bottom: 0;
}
.onoffswitch3-active .onoffswitch3-switch {
    background: #27A1CA; left: 0;
    width: 160px;
}
.onoffswitch3-inactive{
    background: #A1A1A1; right: 0;
    width: 20px;
}
.onoffswitch3-checkbox:checked + .onoffswitch3-label .onoffswitch3-inner {
    margin-left: 0;
}

.glyphicon-remove{
    padding: 3px 0px 0px 0px;
    color: #fff;
    background-color: #000;
    height: 25px;
    width: 25px;
    border-radius: 15px;
    border: 2px solid #fff;
}

.scroll-text{
    color: #000;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Dashboard
            </h3>
        </div>
        <div class="onoffswitch3">
            <input type="checkbox" name="onoffswitch3" class="onoffswitch3-checkbox" id="myonoffswitch3" checked>
            <label class="onoffswitch3-label" for="myonoffswitch3">
                <span class="onoffswitch3-inner">
                    <span class="onoffswitch3-active">                      
                       <marquee class="scroll-text"> 
                            <asp:Repeater ID="rptnews" runat="server">
                                            <ItemTemplate>
                                             
                                                    <div class="">                                                     
                                                        <p class="">
                                                            <%# Eval("NewsDesc") %></p>
                                                    </div>
                                              
                                            </ItemTemplate>
                                        </asp:Repeater>
                        </marquee>                             
                         <span class="onoffswitch3-switch">BREAKING NEWS <span class="glyphicon glyphicon-remove"></span></span>
                   
                             </span>
                    <span class="onoffswitch3-inactive"><span class="onoffswitch3-switch">SHOW BREAKING NEWS</span></span>
                </span>
            </label>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card card-statistics">
                    <div class="card-body" ng-app="distributorApp" ng-controller="distributorCntrl">
                        <div ng-repeat="x in rtdashboard" class="d-flex flex-column flex-md-row align-items-center justify-content-between">
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-hourglass-half mr-2"></i>
                                    E-Wallet Balance
                                </p>
                                <h2>
                                    <a href="ListEWalletBalance.aspx" target="_blank" style="color: white;">{{x.EBalance}}</a></h2>
                            </div>
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-cloud-download-alt mr-2"></i>
                                    AEPS Balance
                               
                                </p>
                                <h2>
                                    <a href="AepsWallet.aspx" target="_blank" style="color: white;">{{x.RBalance}}</a></h2>
                            </div>
                            <div class="statistics-item">
                                <p>
                                    <i class="icon-sm fas fa-check-circle mr-2"></i>
                                    Today Recharge Amount
                               
                                </p>
                                <h2>
                                    <a href="Recharge_ListHistory.aspx" target="_blank" style="color: white;">{{x.rechargeamount}}</a></h2>
                            </div>
                            <div class="statistics-item">
                                <p>
                                    Today DMR Amount
                               
                                </p>
                                <h2>
                                    <a href="DmrNewReport.aspx" target="_blank" style="color: white;">{{x.totaldmr}}</a></h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center" style="padding-left: 54px;">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="rechargepanel.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/aeps.png" alt="No Image Available" height="100" width="100" />
                                <label>AEPS</label>
                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="psa_reg.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/dmr.png" alt="No Image Available" height="100" width="100" />
                                 <label>AEPS</label>
                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="AepsDashboard.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/icici_aeps.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="bbpsnew.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/payout.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="IciciDashboard.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/xpress_dmr.png" alt="No Image Available" height="100" width="100" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    <%--   <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="rechargepanel.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/data card.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">

                                <a href="DMR.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/money transfer.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="dmrnew.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/xpress dmr.png" alt="No Image Available" height="100" width="100" />
                                <div class="ml-4">
                                    <a href="dmrnew.aspx" target="_blank">
                                        <h5 class="mb-0"style="color:#699d34">XPRESS DMR</h5>
                                    </a>
                                    <p class="mb-0"></p>
                                </div>
                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="AepsPayOut.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/payout.png" alt="No Image Available" height="100" width="100" />


                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="rechargepanel.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/dth.png" alt="No Image Available" height="100" width="100" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="busnew.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/bus.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">

                                <a href="FlightSearch.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/flight.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="rechargepanel.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/landline.png" alt="No Image Available" height="100" width="100" />
                                <%--<div class="ml-4">
                                    <a href="dmrnew.aspx" target="_blank">
                                        <h5 class="mb-0"style="color:#699d34">XPRESS DMR</h5>
                                    </a>
                                    <p class="mb-0"></p>
                                </div>
                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/mpos.png" alt="No Image Available" height="100" width="100" />


                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="HotelBooking.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/hotel.png" alt="No Image Available" height="100" width="100" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="bbpsnew.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/electricity.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">

                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/gas.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/pay premium.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/water.png" alt="No Image Available" height="100" width="100" />


                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/gst.png" alt="No Image Available" height="100" width="100" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/gereral insurance.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">

                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/train.png" alt="No Image Available" height="100" width="100" />

                            </div>

                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/e commerce.png" alt="No Image Available" height="100" width="100" />


                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/rbl bank.png" alt="No Image Available" height="100" width="100" />
                                <%--<div class="ml-4">
                                    <a href="dmrnew.aspx" target="_blank">
                                        <h5 class="mb-0"style="color:#699d34">XPRESS DMR</h5>
                                    </a>
                                    <p class="mb-0"></p>
                                </div>
                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="#.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/kotak.png" alt="No Image Available" height="100" width="100" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12">
                <div class="card" style="height: 150px">
                    <div class="card-body">
                        <div class="d-md-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="https://pnbnet.org.in/OOSA/" target="_blank" />
                                <img src="../../Design/ServicesImages/punjab bank.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">

                                <a href="https://www.rblbank.com/category/savings-accounts" target="_blank" />
                                <img src="../../Design/ServicesImages/rbl bank.png" alt="No Image Available" height="100" width="100" />

                            </div>

                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="https://www.sbiyono.sbi/wps/portal/accountopening/digital-account#!/customeraccountOpen" target="_blank" />
                                <img src="../../Design/ServicesImages/sbi bank.png" alt="No Image Available" height="100" width="100" />


                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="https://www.unionbankofindia.co.in/english/saving-account.aspx" target="_blank" />
                                <img src="../../Design/ServicesImages/union bank.png" alt="No Image Available" height="100" width="100" />

                            </div>
                            <div class="d-flex align-items-center mb-2 mb-md-0">
                                <a href="https://www.kotak.com/811-savingsaccount-ZeroBalanceAccount/811/ahome2.action" target="_blank" />
                                <img src="../../Design/ServicesImages/kotak.png" alt="No Image Available" height="100" width="100" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">--%>
            <%--<div class="col-md-8 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">
                            <i class="fas fa-table"></i>
                            Last Login IP:
                        </h4>
                        <div class="table-responsive">
                            <table class="table">

                                <thead>
                                    <tr>
                                        <th>Login IP
                                        </th>
                                        <th>Login Date/Time
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repEmployeeLoginDetail" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="font-weight-bold">
                                                    <%# Eval("LoginIP")%>
                                                </td>
                                                <td class="text-muted">
                                                    <%# String.Format("{0:dd MMM yy hh:mm tt}", Eval("LoginDate"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-md-8 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <asp:Repeater runat="server" ID="repeater1">
                            <ItemTemplate>
                                <asp:Image class="mySlides" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="400px" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="col-md-4 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">
                            <i class="fas fa-servicestack-alt"></i>
                            Manage Services
                        </h4>
                        <div class="table-responsive">
                            <asp:Repeater ID="rep" runat="server">
                                <ItemTemplate>
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Large" Text="Recharge"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("Recharge").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("Recharge")%>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Large" Text="UTI"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("UTI").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("UTI")%>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="AEPS"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblaeps" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("AEPS").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("AEPS")%>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="BBPS"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblbbps" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("BBPS") %>' ForeColor='<%# Eval("BBPS").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" Text="PrepaidCard"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblprepaidcard" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("prepaidcard") %>' ForeColor='<%# Eval("prepaidcard").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" Text="DMR"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldmr" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("DMR") %>' ForeColor='<%# Eval("DMR").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Large" Text="XPress DMR"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblxpressdmr" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("XpressDMR") %>' ForeColor='<%# Eval("XpressDMR").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Large" Text="PAYOUT"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblpayout" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("Payout") %>' ForeColor='<%# Eval("Payout").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                        </tr>

                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

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
</asp:Content>

