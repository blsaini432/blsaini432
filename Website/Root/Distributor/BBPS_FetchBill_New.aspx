<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="BBPS_FetchBill_New.aspx.cs" Inherits="Root_Distributor_BBPS_FetchBill_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="../../Design/css/bbps_module.css">
   <script type="text/javascript">
     //        function pageLoad() {
     //            var manager = window.Sys.WebForms.PageRequestManager.getInstance();
     //            manager.add_endRequest(endRequest);
     //            manager.add_beginRequest(OnBeginRequest);
     //        }

     function OnBeginRequest() {
         window.$get('UpProgress').style.display = "block";
     }

     function endRequest() {
         window.$get('UpProgress').style.display = "none";
     }
    </script>
    <style type="text/css">
        .divWaiting
        {
            position: fixed;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper bg_gray">
        
                <!-- BBPS Logo section---->
<div class="bbps-logo-section">
  <div class="row">
    <div class="bbps-logo"><a href="#"><img src="../../Design/images/bbps_module/bbps_logo.png"></a></div>
  </div>
</div>
        
                 <asp:UpdatePanel ID="UpdatePanel_Home" runat="server">
                        <ContentTemplate>
          <!-- BBPS Module-1---->
<div class="bill-form-section">
    <div class="row">
      <div class="col-md-12">
        <div class="service-title">
          <h3>Home/Recharge & Bill/Electricity</h3>
        </div>
      </div>
      <div class="col-lg-12">
        <div class="bill-info-wrap bill_bg_white bill-found-module">
          <div class="col-md-8 col-sm-12 col-xs-12">
            <div class="bill-detail">
              <h3 class="biil-title"> Bill Found </h3>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Biller Name</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblbillername" runat="server" ></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Biller ID</span> <span class="bill-dynamic-deta">  <asp:Label ID="lblbillerid" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Customer Name</span> <span class="bill-dynamic-deta">  <asp:Label ID="lblcustomername" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Bill Date</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblbilldate" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Bill Period</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblbilperiod" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Due Date</span> <span class="bill-dynamic-deta"><asp:Label ID="lblduedate" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Bill Amount</span> <span class="bill-dynamic-deta"> Rs.<asp:Label ID="lblbilamount" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Customer Convenience Fee</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblcustomerfee" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Total Amount</span> <span class="bill-dynamic-deta"> Rs. <asp:Label ID="lbltotalamount" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Late Payment Fees</span> <span class="bill-dynamic-deta"> <asp:Label ID="lbllatefees" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Fixed Charges</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblfixedcharges" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Additional Charges</span> <span class="bill-dynamic-deta"> <asp:Label ID="lbladditionalcharges" runat="server"></asp:Label></span> </div>
            </div>
          </div>
          <div class="col-md-4 col-sm-12 col-xs-12">
            <div class="submit-button single-service-submit bill-found-button">
                <asp:Button ID="btnsubmit" runat="server" Text="Pay Now" CssClass="btn btn-default serch-button" OnClick="btnsubmit_Click" OnClientClick="this.disabled = true; this.value = 'Submitting...';"  UseSubmitBehavior="false" />
                                      
            </div>
          </div>
        </div>
      </div>
    </div>
</div>
                            </ContentTemplate>
                        <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />
                     </Triggers>
                 </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpProgress" runat="server" style="height: 100%; width: 100%;">
        <ProgressTemplate>
            <div class="divWaiting">
                <div id="progressBackgroundFilter">
                </div>
                <div id="processMessage">
                    <div class="loading_page" align="center" style="background-color: White; vertical-align: middle">
                        <img src="../../Design/images/pageloader.gif" alt="Loading..." style="float: none" />&#160;Please
                        Wait...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        </div>
</asp:Content>

