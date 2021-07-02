<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Dashboard_old.aspx.cs" Inherits="Root_Admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <script src="../Angularjsapp/distributorapp.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Dashboard
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <%--<div class="row grid-margin">
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
        </div>--%>
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
                    <div class="col-lg-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Latest News</h4>

                                <div class="mt-4">
                                    <div class="accordion accordion-multi-colored" id="accordion-6" role="tablist">
                                        <div class="card">
                                            <div class="new-area">
                                                <marquee id="test" behavior="scroll" direction="left" scrolldelay="200" scrollamount="10" onmouseover="document.all.test.stop()"
                                                    onmouseout="document.all.test.start()">
                                <div class="event-content-holder">
          <h4 class="new-tittle"> <asp:Repeater ID="rptnews" runat="server">
                                            <ItemTemplate>
                                             
                                                    <div class="card-block block-1">                                                     
                                                        <p class="card-text" style="color:red;font-size:30px">
                                                            <%# Eval("NewsDesc") %></p>
                                                    </div>
                                              
                                            </ItemTemplate>
                                        </asp:Repeater></h4>
        </div>
                                </marquee>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        
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

